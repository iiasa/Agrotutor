using System;
using System.Threading;
using System.Threading.Tasks;
using Helper.Base.PublishSubscriberEvents;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Prism.Events;

namespace Helper.Map
{
    public class LocationBusiness : IPosition
    {
        private readonly IEventAggregator _eventAggregator;
        private CancellationTokenSource cancelSource;
        private readonly IGeolocator locator;

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

        public bool CheckIfGPSIsEnabled()
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

        public Task<bool> StopListening()
        {
            return locator?.StopListeningAsync();
        }

        public async Task<GeoPosition> GetCurrentPosition()
        {
            GeoPosition posRes = null;
            PositionStatus = string.Empty;
            PositionLatitude = string.Empty;
            PositionLongitude = string.Empty;
            IsBusy = true;
            if (CheckIfGPSIsEnabled())
            {
                if (!locator.IsListening)
                {
                    await locator.StartListeningAsync(5, 1);
                }
                cancelSource = new CancellationTokenSource();

                await locator.GetPositionAsync(1000, cancelSource.Token, false)
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

        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            var position = e.Position;

            var pos = MapPosition(position);
            _eventAggregator.GetEvent<LivePositionEvent>().Publish(pos);
        }

        private GeoPosition MapPosition(Position position)
        {
            if (position == null)
                throw new ArgumentNullException();

            var pos = new GeoPosition
            {
                Accuracy = position.Accuracy,
                Latitude = position.Latitude,
                Longitude = position.Longitude,
            };
            return pos;
        }
    }
}