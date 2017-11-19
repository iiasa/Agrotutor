namespace Helper.Map.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using TK.CustomMap;
    using Xamarin.Forms.Maps;

    public class GenericMapViewModel : BindableBase, INavigationAware
    {
        public const string MapTaskParameterName = "MapTask";
        public const string MapRegionParameterName = "MapRegion";
        public const string ListenForUserLocationParameterName = "ListenForUserLocation";
        public const string FollowUserLocationParameterName = "FollowUserLocation";
        public const string MaximumLocationAccuracyParameterName = "MaximumLocationAccuracy";

        public static readonly MapSpan InitialMapRegion = MapSpan.FromCenterAndRadius(new Position(20, -100), new Distance(200000));

        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPosition _geoLocator;

        private MapTask _mapTask;
        private MapSpan _mapRegion;
        private bool _listenForUserLocation;
        private bool _followUserLocation;
        private int? _maximumLocationAccuracy;
        private bool _lockMapRegion = true;

        private GeoPosition _point;
        private List<GeoPosition> _polygon;

        private GeoPosition _userLocation;
        private MapType _mapType;

        public MapType MapType { get => _mapType; set => SetProperty(ref _mapType, value); }


        public MapSpan MapRegion
        {
            get
            {
                return _mapRegion;
            }

            set
            {
                SetProperty(ref _mapRegion, value);
            }
        }

        public GeoPosition UserLocation
        {
            get { return _userLocation; }
            set
            {

                SetProperty(ref _userLocation, value);



                switch (_mapTask)
                {
                    case MapTask.GetLocation:

                        break;

                    case MapTask.SelectLocation:
                    case MapTask.SelectPolygon:
                        break;
                }
                /*

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
                }*/
            }
        }

        public DelegateCommand UseLocationCommand { get; private set; }
        public bool ReturnGeolocationButtonEnabled { get; private set; } = true; //todo remove true

        public GenericMapViewModel(IEventAggregator eventAggregator, IPosition geoLocator, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _geoLocator = geoLocator;

            UseLocationCommand = new DelegateCommand(UseLocation).ObservesCanExecute(o => ReturnGeolocationButtonEnabled);
        }

        private void UseLocation()
        {
            MapRegion = (InitialMapRegion);
            /*var parameters = new NavigationParameters { { "GeoPosition", _currentGeoPosition } };
            _navigationService.GoBackAsync(parameters);*/
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

            if (parameters.ContainsKey(MapRegionParameterName))
            {
                parameters.TryGetValue(MapRegionParameterName, out var mapRegion);
                if (mapRegion != null) MapRegion = (MapSpan) mapRegion;
            } else{

                MapRegion = InitialMapRegion;
                
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


            UserLocation = position;
        }

        private void InitializeGetLocation()
        {
            //TODO
        }

        private void InitializeSelectLocation()
        {
            //Todo
        }

        private void InitializeSelectPolygon()
        {
            //TODo
        }

        public void OnAppearing()
        {
            //AdjustMapZoom();
            if (_mapTask == MapTask.GetLocation)
            {
                //GetPosition();
            }
            MapType = MapType.Hybrid;
        }

        public void OnDisappearing()
        {
            _geoLocator.StopListening();
        }

        /*
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
        }*/
    }
}