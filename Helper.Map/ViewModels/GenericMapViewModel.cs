namespace Helper.Map.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using TK.CustomMap;
    using TK.CustomMap.Overlays;
    using Xamarin.Forms;

    /// <summary>
    ///     Defines the <see cref="GenericMapViewModel" />
    /// </summary>
    public class GenericMapViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        ///     Defines the FollowUserLocationParameterName
        /// </summary>
        public const string FollowUserLocationParameterName = "FollowUserLocation";

        /// <summary>
        ///     Defines the ListenForUserLocationParameterName
        /// </summary>
        public const string ListenForUserLocationParameterName = "ListenForUserLocation";

        /// <summary>
        ///     Defines the MapRegionParameterName
        /// </summary>
        public const string MapRegionParameterName = "MapRegion";

        /// <summary>
        ///     Defines the MapTaskParameterName
        /// </summary>
        public const string MapTaskParameterName = "MapTask";

        /// <summary>
        ///     Defines the MaximumLocationAccuracyParameterName
        /// </summary>
        public const string MaximumLocationAccuracyParameterName = "MaximumLocationAccuracy";

        public const string PointsParameterName = "Points";

        /// <summary>
        ///     Defines the PolygonsParameterName
        /// </summary>
        public const string PolygonsParameterName = "Polygons";

        /// <summary>
        ///     Defines the InitialMapRegion
        /// </summary>
        public static readonly MapSpan InitialMapRegion =
            MapSpan.FromCenterAndRadius(new Position(20, -100), new Distance(200000));

        /// <summary>
        ///     Defines the _eventAggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        ///     Defines the _geoLocator
        /// </summary>
        private readonly IPosition _geoLocator;

        /// <summary>
        ///     Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        private bool _buttonAcceptDelineationEnabled;

        private bool _buttonCancelDelineationEnabled;

        /// <summary>
        ///     Defines the _currentGeoPosition
        /// </summary>
        private GeoPosition _currentGeoPosition;

        /// <summary>
        ///     Defines the _delineationButtonsVisible
        /// </summary>
        private bool _delineationButtonsVisible;

        /// <summary>
        ///     Defines the _followUserLocation
        /// </summary>
        private bool _followUserLocation;

        /// <summary>
        ///     Defines the _listenForUserLocation
        /// </summary>
        private bool _listenForUserLocation;

        /// <summary>
        ///     Defines the _mapRegion
        /// </summary>
        private MapSpan _mapRegion;

        /// <summary>
        ///     Defines the _mapTask
        /// </summary>
        private MapTask _mapTask;

        /// <summary>
        ///     Defines the _mapType
        /// </summary>
        private MapType _mapType;

        /// <summary>
        ///     Defines the _maximumLocationAccuracy
        /// </summary>
        private int? _maximumLocationAccuracy;

        /// <summary>
        ///     Defines the _point
        /// </summary>
        private GeoPosition _point;

        /// <summary>
        ///     Defines the _polygon
        /// </summary>
        private ObservableCollection<GeoPosition> _polygon;

        /// <summary>
        ///     Defines the _returnGeolocationButtonEnabled
        /// </summary>
        private bool _returnGeolocationButtonEnabled;

        /// <summary>
        ///     Defines the _returnGeolocationButtonVisible
        /// </summary>
        private bool _returnGeolocationButtonVisible;

        private bool _showEnableLocationHint;

        /// <summary>
        ///     Defines the _userLocation
        /// </summary>
        private GeoPosition _userLocation;

        /// <summary>
        ///     Defines the _viewPins
        /// </summary>
        private ObservableCollection<TKCustomMapPin> _viewPins;

        /// <summary>
        ///     Defines the _viewPolygons
        /// </summary>
        private ObservableCollection<TKPolygon> _viewPolygons;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericMapViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The <see cref="IEventAggregator" /></param>
        /// <param name="geoLocator">The <see cref="IPosition" /></param>
        /// <param name="navigationService">The <see cref="INavigationService" /></param>
        public GenericMapViewModel(IEventAggregator eventAggregator, IPosition geoLocator,
            INavigationService navigationService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _geoLocator = geoLocator;

            UseLocationCommand =
                new DelegateCommand(UseLocation);//.ObservesCanExecute(o => ReturnGeolocationButtonEnabled);
            CurrentDelineationState = DelineationState.Inactive;
            MapClickedCommand = new DelegateCommand<object>(MapClicked);
            AcceptDelineationCommand = new DelegateCommand(AcceptDelineation);
            CancelDelineationCommand = new DelegateCommand(CancelDelineation);
            ReturnGeolocationButtonEnabled = false;
        }

        /// <summary>
        ///     Gets or sets the UseLocationCommand
        /// </summary>
        public DelegateCommand UseLocationCommand { get; }

        /// <summary>
        ///     Gets or sets the AcceptDelineationCommand
        /// </summary>
        public DelegateCommand AcceptDelineationCommand { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether ButtonAcceptDelineationEnabled
        /// </summary>
        public bool ButtonAcceptDelineationEnabled
        {
            get => _buttonAcceptDelineationEnabled;
            private set => SetProperty(ref _buttonAcceptDelineationEnabled, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether ButtonCancelDelineationEnabled
        /// </summary>
        public bool ButtonCancelDelineationEnabled
        {
            get => _buttonCancelDelineationEnabled;
            private set => SetProperty(ref _buttonCancelDelineationEnabled, value);
        }

        /// <summary>
        ///     Gets or sets the CancelDelineationCommand
        /// </summary>
        public DelegateCommand CancelDelineationCommand { get; set; }

        /// <summary>
        ///     Gets or sets the CurrentDelineationState
        /// </summary>
        public DelineationState CurrentDelineationState { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether DelineationButtonsVisible
        /// </summary>
        public bool DelineationButtonsVisible
        {
            get => _delineationButtonsVisible;
            private set => SetProperty(ref _delineationButtonsVisible, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether IsGeolocationEnabled
        /// </summary>
        public bool IsGeolocationEnabled { get; set; }

        /// <summary>
        ///     Gets or sets the MapClickedCommand
        /// </summary>
        public DelegateCommand<object> MapClickedCommand { get; set; }

        /// <summary>
        ///     Gets or sets the MapRegion
        /// </summary>
        public MapSpan MapRegion
        {
            get => _mapRegion;
            set => SetProperty(ref _mapRegion, value);
        }

        /// <summary>
        ///     Gets or sets the MapType
        /// </summary>
        public MapType MapType
        {
            get => _mapType;
            set => SetProperty(ref _mapType, value);
        }

        /// <summary>
        ///     Gets or sets the Point
        /// </summary>
        public GeoPosition Point
        {
            get => _point;
            set => SetProperty(ref _point, value);
        }

        /// <summary>
        ///     Gets or sets the Polygon
        /// </summary>
        public ObservableCollection<GeoPosition> Polygon
        {
            get => _polygon;
            set => SetProperty(ref _polygon, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether ReturnGeolocationButtonEnabled
        /// </summary>
        public bool ReturnGeolocationButtonEnabled
        {
            get => _returnGeolocationButtonEnabled;
            private set => SetProperty(ref _returnGeolocationButtonEnabled, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether ReturnGeolocationButtonVisible
        /// </summary>
        public bool ReturnGeolocationButtonVisible
        {
            get => _returnGeolocationButtonVisible;
            private set => SetProperty(ref _returnGeolocationButtonVisible, value);
        }

        public bool ShowEnableLocationHint
        {
            get => _showEnableLocationHint;
            set => SetProperty(ref _showEnableLocationHint, value);
        }

        /// <summary>
        ///     Gets or sets the UserLocation
        /// </summary>
        public GeoPosition UserLocation
        {
            get => _userLocation;
            set
            {
                SetProperty(ref _userLocation, value);

                switch (_mapTask)
                {
                    case MapTask.GetLocation:
                        IsGeolocationEnabled = true;
                        ViewPins.Clear();
                        Position position = new Position(value.Latitude, value.Longitude);
                        ViewPins.Add(new TKCustomMapPin()
                        {
                            Position = position
                        });
                        if (_followUserLocation)
                        {
                            MapRegion = MapSpan.FromCenterAndRadius(position, Distance.FromMeters(200));
                        }
                        Point = value;
                        ReturnGeolocationButtonEnabled = true;
                        break;

                    case MapTask.SelectLocation:
                    case MapTask.SelectPolygon:
                        break;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the ViewPins
        /// </summary>
        public ObservableCollection<TKCustomMapPin> ViewPins
        {
            get => _viewPins;
            set => SetProperty(ref _viewPins, value);
        }

        /// <summary>
        ///     Gets or sets the ViewPolygons
        /// </summary>
        public ObservableCollection<TKPolygon> ViewPolygons
        {
            get => _viewPolygons;
            set => SetProperty(ref _viewPolygons, value);
        }

        /// <summary>
        ///     The OnAppearing
        /// </summary>
        public void OnAppearing()
        {
            //AdjustMapZoom();
            if (_mapTask == MapTask.GetLocation)
            {
                //GetPosition();
            }
            MapType = MapType.Hybrid;
        }

        /// <summary>
        ///     The OnDisappearing
        /// </summary>
        public void OnDisappearing()
        {
            _geoLocator.StopListening();
        }

        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            _eventAggregator.GetEvent<LivePositionEvent>().Unsubscribe(HandlePositionEvent);
        }

        /// <summary>
        ///     The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(GenericMapViewModel.MapTaskParameterName))
            {
                parameters.TryGetValue(GenericMapViewModel.MapTaskParameterName, out object mapTask);
                if (mapTask != null)
                {
                    SetMapTask((MapTask)mapTask);
                }
            }

            if (parameters.ContainsKey(GenericMapViewModel.MapRegionParameterName))
            {
                parameters.TryGetValue(GenericMapViewModel.MapRegionParameterName, out object mapRegion);
                if (mapRegion != null)
                {
                    MapRegion = (MapSpan)mapRegion;
                }
            }
            else
            {
                MapRegion = GenericMapViewModel.InitialMapRegion;
            }

            if (parameters.ContainsKey(GenericMapViewModel.ListenForUserLocationParameterName))
            {
                parameters.TryGetValue(GenericMapViewModel.ListenForUserLocationParameterName,
                    out object listenForUserLocation);
                if (listenForUserLocation != null)
                {
                    _listenForUserLocation = (bool)listenForUserLocation;
                }
            }

            if (parameters.ContainsKey(GenericMapViewModel.FollowUserLocationParameterName))
            {
                parameters.TryGetValue(GenericMapViewModel.FollowUserLocationParameterName,
                    out object followUserLocation);
                if (followUserLocation != null)
                {
                    _followUserLocation = (bool)followUserLocation;
                }
            }

            if (parameters.ContainsKey(GenericMapViewModel.MaximumLocationAccuracyParameterName))
            {
                parameters.TryGetValue(GenericMapViewModel.MaximumLocationAccuracyParameterName,
                    out object maximumLocationAccuracy);
                if (maximumLocationAccuracy != null)
                {
                    _maximumLocationAccuracy = (int)maximumLocationAccuracy;
                }
            }
            if (parameters.ContainsKey(GenericMapViewModel.PolygonsParameterName))
            {
                parameters.TryGetValue(GenericMapViewModel.PolygonsParameterName, out object polygons);
                if (polygons != null)
                {
                    ViewPolygons = (ObservableCollection<TKPolygon>)polygons;
                }
            }
            if (parameters.ContainsKey(GenericMapViewModel.PointsParameterName))
            {
                parameters.TryGetValue(GenericMapViewModel.PointsParameterName, out object points);
                if (points != null)
                {
                    ViewPins = (ObservableCollection<TKCustomMapPin>)points;
                }
            }
        }

        /// <summary>
        ///     The AcceptDelineation
        /// </summary>
        private void AcceptDelineation()
        {
            List<Position> positions = ViewPolygons[0].Coordinates;
            if (positions.Count == 0)
            {
                _navigationService.GoBackAsync();
            }
            else
            {
                List<GeoPosition> geoPositions = positions.Select(position => new GeoPosition
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude

                    //    AcquiredThrough = TypeOfAcquisition.SelectedOnMap
                })
                    .ToList();

                NavigationParameters parameters = new NavigationParameters
                {
                    { "Delineation", geoPositions }
                };
                _navigationService.GoBackAsync(parameters);
            }
        }

        /// <summary>
        ///     The CancelDelineation
        /// </summary>
        private void CancelDelineation()
        {
            ButtonCancelDelineationEnabled = false;
            ButtonAcceptDelineationEnabled = false;
            ViewPolygons = null;
            CurrentDelineationState = DelineationState.Inactive;
            ViewPins.Clear();
        }

        /// <summary>
        ///     The GetPosition
        /// </summary>
        /// <returns>The <see cref="Task" /></returns>
        private async Task GetPosition()
        {
            ShowEnableLocationHint = !_geoLocator.CheckIfGPSIsEnabled();
            if (_geoLocator != null)
            {
                GeoPosition positionRes = await _geoLocator.GetCurrentPosition();

                _currentGeoPosition = positionRes;

                if (positionRes == null)
                {
                    IsGeolocationEnabled = false;
                }
                else
                {
                    IsGeolocationEnabled = true;

                    ReturnGeolocationButtonEnabled = true;

                    Position position = new Position(positionRes.Latitude, positionRes.Longitude);
                    MapRegion = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(50));

                    ViewPins = new ObservableCollection<TKCustomMapPin>(new[]
                    {
                        new TKCustomMapPin
                        {
                            ID = "userCurrLocation",
                            Position = position
                        }
                    });
                    Point = new GeoPosition(position.Latitude, position.Longitude);
                }

                _eventAggregator.GetEvent<LivePositionEvent>().Subscribe(HandlePositionEvent);
            }
        }

        /// <summary>
        ///     The HandlePositionEvent
        /// </summary>
        /// <param name="position">The <see cref="GeoPosition" /></param>
        private void HandlePositionEvent(GeoPosition position)
        {
            if (position == null)
            {
                return;
            }
            if (_maximumLocationAccuracy != null && _maximumLocationAccuracy < position.Accuracy)
            {
                return;
            }

            UserLocation = position;
        }

        /// <summary>
        ///     The InitializeGetLocation
        /// </summary>
        private void InitializeGetLocation()
        {
            ReturnGeolocationButtonVisible = true;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            GetPosition();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
        }

        /// <summary>
        ///     The InitializeSelectLocation
        /// </summary>
        private void InitializeSelectLocation()
        {
            ReturnGeolocationButtonVisible = true;
        }

        /// <summary>
        ///     The InitializeSelectPolygon
        /// </summary>
        private void InitializeSelectPolygon()
        {
            DelineationButtonsVisible = true;
        }

        /// <summary>
        ///     The MapClicked
        /// </summary>
        /// <param name="obj">The <see cref="object" /></param>
        private void MapClicked(object obj)
        {
            Position position = (Position)obj;

            switch (_mapTask)
            {
                case MapTask.SelectLocation:
                    OnMapClickedSelectLocation(position);
                    break;

                case MapTask.SelectPolygon:
                    OnMapClickedSelectPolygon(position);
                    break;

                default:
                    return;
            }
        }

        private void OnMapClickedSelectLocation(Position position)
        {
            if (ViewPins == null)
            {
                ViewPins = new ObservableCollection<TKCustomMapPin>();
            }
            ViewPins.Clear();
            Point = new GeoPosition
            {
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };
            ViewPins.Add(new TKCustomMapPin
            {
                ID = "polygon_marker",
                Position = position
            });
            ReturnGeolocationButtonEnabled = true;
        }

        private void OnMapClickedSelectPolygon(Position position)
        {
            switch (CurrentDelineationState)
            {
                case DelineationState.Inactive:

                    if (_mapTask != MapTask.SelectPolygon)
                    {
                        return;
                    }

                    ViewPolygons = new ObservableCollection<TKPolygon>();
                    TKPolygon polygon = new TKPolygon
                    {
                        StrokeColor = Color.Green,
                        StrokeWidth = 2f,
                        Color = Color.Red
                    };

                    polygon.Coordinates.Add(position);

                    ViewPolygons.Add(polygon);
                    if (ViewPins == null)
                    {
                        ViewPins = new ObservableCollection<TKCustomMapPin>();
                    }
                    ViewPins.Add(new TKCustomMapPin
                    {
                        ID = "polygon_marker_0",
                        Position = position
                    });
                    ButtonCancelDelineationEnabled = true;
                    CurrentDelineationState = DelineationState.ActiveNotEnoughPoints;
                    break;

                case DelineationState.ActiveNotEnoughPoints:
                case DelineationState.ActiveEnoughPoints:
                    int pointId = ViewPolygons[0].Coordinates.Count; //todo not use ViewPolygons[0]

                    ViewPolygons[0].Coordinates.Add(position);

                    if (ViewPolygons[0].Coordinates.Count > 2)
                    {
                        CurrentDelineationState = DelineationState.ActiveEnoughPoints;
                        List<Position> listCoordinate = ViewPolygons[0].Coordinates;
                        listCoordinate.Add(position);
                        ViewPolygons[0].Coordinates = new List<Position>(listCoordinate);
                        ButtonAcceptDelineationEnabled = true;
                    }
                    else
                    {
                        CurrentDelineationState = DelineationState.ActiveNotEnoughPoints;
                    }

                    ButtonAcceptDelineationEnabled = CurrentDelineationState == DelineationState.ActiveEnoughPoints;

                    ViewPins.Add(new TKCustomMapPin
                    {
                        ID = "polygon_marker_" + pointId,
                        Position = position
                    });
                    break;
            }
        }

        /// <summary>
        ///     The SetMapTask
        /// </summary>
        /// <param name="mapTask">The <see cref="MapTask" /></param>
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

        /// <summary>
        ///     The UseLocation
        /// </summary>
        private void UseLocation()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "GeoPosition", Point }
            };
            _navigationService.GoBackAsync(parameters);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}