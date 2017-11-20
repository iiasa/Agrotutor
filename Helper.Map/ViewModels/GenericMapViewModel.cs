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
    using TK.CustomMap.Overlays;
    using Xamarin.Forms;
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
        private DelineationState _delineationState;

        private GeoPosition _point;
        private ObservableCollection<GeoPosition> _polygon; 
        private ObservableCollection<TKPolygon> _viewPolygons;
        private ObservableCollection<TKCustomMapPin> _viewPins;

        private GeoPosition _userLocation;
        private MapType _mapType;

        public GeoPosition Point { get { return _point; } set { SetProperty(ref _point, value); }}
        public ObservableCollection<GeoPosition> Polygon { get { return _polygon; } set { SetProperty(ref _polygon, value); } }
        public ObservableCollection<TKPolygon> ViewPolygons { get { return _viewPolygons; } set { SetProperty(ref _viewPolygons, value); } }
        public ObservableCollection<TKCustomMapPin> ViewPins { get { return _viewPins; } set { SetProperty(ref _viewPins, value); } }
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
        public bool ReturnGeolocationButtonEnabled { get; private set; }
        public bool ButtonCancelDelineationEnabled { get; private set; }
        public DelineationState CurrentDelineationState { get; private set; }
        public bool ButtonAcceptDelineationEnabled { get; private set; }

        public GenericMapViewModel(IEventAggregator eventAggregator, IPosition geoLocator, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _geoLocator = geoLocator;

            UseLocationCommand = new DelegateCommand(UseLocation).ObservesCanExecute(o => ReturnGeolocationButtonEnabled);
        }

        private void MapClicked(object obj)
        {
            if ((CurrentDelineationState == DelineationState.Inactive) && (_mapTask != MapTask.SelectLocation)) return;
            var position = (Position)obj;
            // var polygonsList = MapPolygons;

            if (CurrentDelineationState != DelineationState.Inactive)
            {
                var pointId = ViewPolygons[0].Coordinates.Count; //todo not use ViewPolygons[0]

                ViewPolygons[0].Coordinates.Add(position);

                if (ViewPolygons[0].Coordinates.Count > 2)
                {
                    CurrentDelineationState = DelineationState.ActiveEnoughPoints;
                    var listCoordinate = ViewPolygons[0].Coordinates;
                    listCoordinate.Add(position);
                    ViewPolygons[0].Coordinates = new List<Position>(listCoordinate);
                }
                else
                {
                    CurrentDelineationState = DelineationState.ActiveNotEnoughPoints;
                }

                ButtonAcceptDelineationEnabled = CurrentDelineationState == DelineationState.ActiveEnoughPoints;

                ViewPins.Add(new TKCustomMapPin
                {
                    ID = "polygon_marker_" + pointId,
                    Position = position,
                });
            }
            else
            {
                if (ViewPins == null) ViewPins = new ObservableCollection<TKCustomMapPin>();
                ViewPins.Clear();
                Point = new GeoPosition
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude
                };
                ViewPins.Add(new TKCustomMapPin
                {
                    ID = "polygon_marker",
                    Position = position,
                });
            }
        }

        private async void MapLongPress(object obj)
        {
            if (_mapTask != MapTask.SelectPolygon) return;
            var position = (Position)obj;
            if (_delineationState != DelineationState.Inactive)
            {
                MapClicked(obj);
                return;
            }

            ViewPolygons = new ObservableCollection<TKPolygon>();
            var polygon = new TKPolygon
            {
                StrokeColor = Color.Green,
                StrokeWidth = 2f,
                Color = Color.Red,
            };

            polygon.Coordinates.Add(position);

            ViewPolygons.Add(polygon);
            if (ViewPins == null) ViewPins = new ObservableCollection<TKCustomMapPin>();
            ViewPins.Add(new TKCustomMapPin
            {
                ID = "polygon_marker_0",
                Position = position,
            });
            ButtonCancelDelineationEnabled = true;
            CurrentDelineationState = DelineationState.ActiveNotEnoughPoints;
        }

        private void UseLocation()
        {
            var parameters = new NavigationParameters { { "GeoPosition", Point } };
            _navigationService.GoBackAsync(parameters);
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