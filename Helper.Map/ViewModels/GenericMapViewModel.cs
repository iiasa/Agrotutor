namespace Helper.Map.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using TK.CustomMap;
    using Xamarin.Forms.Maps;

    public class GenericMapViewModel : BindableBase, INavigationAware
    {
        public const string MapTaskParameterName = "MapTask";
        public const string MapSpanParameterName = "MapSpan";
        public const string ListenForUserLocationParameterName = "ListenForUserLocation";
        public const string FollowUserLocationParameterName = "FollowUserLocation";
        public const string MaximumLocationAccuracyParameterName = "MaximumLocationAccuracy";

        private static readonly MapSpan InitialMapSpan = MapSpan.FromCenterAndRadius(new Position(20, -100), new Distance(200000));

        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPosition _geoLocator;

        private MapTask _mapTask;
        private MapSpan _mapSpan;
        private bool _listenForUserLocation;
        private bool _followUserLocation;
        private int? _maximumLocationAccuracy;

        private GeoPosition _point;
        private List<GeoPosition> _polygon;

        private GeoPosition _userLocation;

        public GenericMapViewModel(IEventAggregator eventAggregator, IPosition geoLocator, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _geoLocator = geoLocator;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            _eventAggregator.GetEvent<LivePositionEvent>().Unsubscribe(HandlePositionEvent);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(MapTaskParameterName))
            {
                parameters.TryGetValue(MapTaskParameterName, out var mapTask);
                if (mapTask != null) SetMapTask((MapTask)mapTask);
            }

            if (parameters.ContainsKey(MapSpanParameterName))
            {
                parameters.TryGetValue(MapSpanParameterName, out var mapSpan);
                if (mapSpan != null) _mapSpan = (MapSpan)mapSpan;
            }

            if (parameters.ContainsKey(ListenForUserLocationParameterName))
            {
                parameters.TryGetValue(ListenForUserLocationParameterName, out var listenForUserLocation);
                if (listenForUserLocation != null) _listenForUserLocation = (bool)listenForUserLocation;
            }

            if (parameters.ContainsKey(FollowUserLocationParameterName))
            {
                parameters.TryGetValue(FollowUserLocationParameterName, out var followUserLocation);
                if (followUserLocation != null) _followUserLocation = (bool)followUserLocation;
            }

            if (parameters.ContainsKey(MaximumLocationAccuracyParameterName))
            {
                parameters.TryGetValue(MaximumLocationAccuracyParameterName, out var maximumLocationAccuracy);
                if (maximumLocationAccuracy != null) _maximumLocationAccuracy = (int)maximumLocationAccuracy;
            }
        }

        private void SetMapTask(MapTask mapTask)
        {
            _mapTask = mapTask;
            switch (mapTask)
            {
                case MapTask.GetLocation:
                    InitializeGetLocation();
                    break;

                case MapTask.SelectLocation:
                    InitializeSelectLocation();
                    break;

                case MapTask.SelectPolygon:
                    InitializeSelectPolygon();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mapTask), mapTask, null);
            }
        }

        private void HandlePositionEvent(GeoPosition position)
        {
            if (position == null) return;
            if ((_maximumLocationAccuracy != null) && (_maximumLocationAccuracy < position.Accuracy)) return;

            switch (_mapTask)
            {
                case MapTask.GetLocation:

                    break;

                case MapTask.SelectLocation:
                case MapTask.SelectPolygon:
                    break;
            }

            IsGeolocationEnabled = true;

            var tkCustomMapPin = CustomPinsList?.FirstOrDefault(x => x.ID == "userCurrLocation");
            if (tkCustomMapPin != null)
            {
                tkCustomMapPin.Position = new Position(position.Latitude, position.Longitude);
                if (_isGetLocationFeatureExist)
                {
                    MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromMeters(200));
                }
                //Enable use Location button in case the map is open from AddParcel Page and there is point already exist on the map
                ReturnGeolocationButtonEnabled = true;
            }
        }

        private void InitializeGetLocation()
        {
            throw new NotImplementedException();
        }

        private void InitializeSelectLocation()
        {
            throw new NotImplementedException();
        }

        private void InitializeSelectPolygon()
        {
            throw new NotImplementedException();
        }

        public void OnAppearing()
        {
            AdjustMapZoom();
            if (_mapTask == MapTask.GetLocation)
            {
                GetPosition();
            }
            MapType = MapType.Hybrid;
        }

        public void OnDisappearing()
        {
            _geoLocator.StopListening();
        }

        private async Task GetPosition()
        {
            if (_geoLocator != null)
            {
                var positionRes = await _geoLocator.GetCurrentPosition();

                _currentGeoPosition = positionRes;

                if (positionRes == null)
                {
                    IsGeolocationEnabled = false;
                }
                else
                {
                    IsGeolocationEnabled = true;

                    ReturnGeolocationButtonEnabled = true;

                    MapsPosition = new Position(positionRes.Latitude, positionRes.Longitude);
                    AdjustMapZoom();
                    //  MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromKilometers(50));

                    CustomPinsList = new ObservableCollection<TKCustomMapPin>(new[]
                    {
                        new TKCustomMapPin
                        {
                            ID = "userCurrLocation",
                            //Title = "Custom Callout Sample",
                            Position = MapsPosition,
                            // ShowCallout = true
                        }
                    });
                }

                _eventAggregator.GetEvent<LivePositionEvent>().Subscribe(HandlePositionEvent);
            }
        }
    }
}