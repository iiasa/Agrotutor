namespace CimmytApp.Core.Geolocator
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CimmytApp.Core.Map;
    using Plugin.Geolocator;
    using Plugin.Geolocator.Abstractions;
    using Prism.Events;

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
            this._eventAggregator = eventAggregator;
            this.locator = CrossGeolocator.Current;
            this.locator.DesiredAccuracy = 50;

            this.locator.PositionChanged += Locator_PositionChanged;
            this.locator.StartListeningAsync(new TimeSpan(0, 0, 5), 1);
        }

        public bool IsBusy { get; set; }

        public string PositionLongitude { get; set; }

        public string PositionLatitude { get; set; }

        public string PositionStatus { get; set; }

        public bool CheckIfGPSIsEnabled()
        {
            if (this.locator != null)
            {
                var isGeolocationAvailable = this.locator.IsGeolocationAvailable;
                var isGeolocationEnabled = this.locator.IsGeolocationEnabled;
                if (isGeolocationAvailable && isGeolocationEnabled)
                    return true;
            }
            return false;
        }

        public async Task<GeoPosition> GetCurrentPosition()
        {
            GeoPosition posRes = null;
            if (CheckIfGPSIsEnabled())
            {
                this.cancelSource = new CancellationTokenSource();

                PositionStatus = string.Empty;
                PositionLatitude = string.Empty;
                PositionLongitude = string.Empty;
                IsBusy = true;

                await this.locator.GetPositionAsync(new TimeSpan(0, 0, 10), this.cancelSource.Token, true)
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

        public Task<bool> StopListening()
        {
            throw new NotImplementedException();
        }

        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            var position = e.Position;

            var pos = MapPosition(position);
            this._eventAggregator.GetEvent<LivePositionEvent>().Publish(pos);
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