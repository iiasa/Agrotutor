using System;
using System.Threading;
using System.Threading.Tasks;
using CimmytApp.DTO;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Prism.Events;

namespace Helper.Geolocator
{
    public class LocationBusiness : IPosition
    {
        private readonly IEventAggregator _eventAggregator;
        private CancellationTokenSource cancelSource;
        private readonly IGeolocator locator;

        public LocationBusiness()
        {
        }

        public LocationBusiness(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            locator.PositionChanged += Locator_PositionChanged;
            locator.StartListeningAsync(5, 1);
        }

        public bool IsBusy { get; set; }

        public string PositionLongitude { get; set; }

        public string PositionLatitude { get; set; }

        public string PositionStatus { get; set; }

        public bool CheckIfGpsIsEnabled()
        {
            if (locator != null)
            {
                var isGeolocationAvailable = locator.IsGeolocationAvailable;
                var isGeolocationEnabled = locator.IsGeolocationEnabled;
                if (isGeolocationAvailable && isGeolocationEnabled)
                    return true;
            }
            return false;
        }

        public async Task<GeoPosition> GetCurrentPosition()
        {
            GeoPosition posRes = null;
            if (CheckIfGpsIsEnabled())
            {
                cancelSource = new CancellationTokenSource();

                PositionStatus = string.Empty;
                PositionLatitude = string.Empty;
                PositionLongitude = string.Empty;
                IsBusy = true;

                await locator.GetPositionAsync(10000, cancelSource.Token, true)
                    .ContinueWith(t =>
                    {
                        IsBusy = false;
                        if (t.IsFaulted)
                            PositionStatus = ((GeolocationException)t.Exception.InnerException).Error.ToString();
                        else if (t.IsCanceled)
                            PositionStatus = "Canceled";
                        else
                            return posRes = MapPosition(t.Result);

                        return posRes;
                    });
            }
            return posRes;
        }

        Task<GeoPosition> IPosition.GetCurrentPosition()
        {
            throw new NotImplementedException();
        }

        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            var position = e.Position;

            var pos = MapPosition(position);
            //_eventAggregator.GetEvent<LivePositionEvent>().Publish(pos);
        }

        private GeoPosition MapPosition(Position position)
        {
            if (position == null)
                throw new ArgumentNullException();

            var pos = new GeoPosition
            {
                Accuracy = position.Accuracy,
                Altitude = position.Altitude,
                AltitudeAccuracy = position.AltitudeAccuracy,
                Heading = position.Heading,
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                Speed = position.Speed,
                Timestamp = position.Timestamp
            };
            return pos;
        }
    }
}