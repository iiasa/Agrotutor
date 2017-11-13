namespace Helper.Map.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Prism;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using TK.CustomMap;
    using TK.CustomMap.Overlays;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    using Base.Contract;
    using Base.PublishSubscriberEvents;

    using CimmytApp.BusinessContract;
    using CimmytApp.DTO;
    using CimmytApp.DTO.Parcel;

    /// <summary>
    /// Defines the <see cref="GenericMapViewModel" />
    /// </summary>
    public class GenericMapViewModel : BindableBase, INavigationAware, IActiveAware
    {
        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Defines the _eventAggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Defines the _isGetLocationFeatureExist
        /// </summary>
        private bool _isGetLocationFeatureExist;

        /// <summary>
        /// Defines the _isGeolocationEnabled
        /// </summary>
        private bool _isGeolocationEnabled;

        /// <summary>
        /// Defines the _mapTask
        /// </summary>
        private MapTask _mapTask = MapTask.DisplayGeometriesOnly;

        /// <summary>
        /// Defines the _parcelId
        /// </summary>
        private int _parcelId;

        /// <summary>
        /// Defines the _currentDelineationState
        /// </summary>
        private DelineationState _currentDelineationState;

        /// <summary>
        /// Defines the _finishDelineationDrawing
        /// </summary>
        private bool _finishDelineationDrawing;

        /// <summary>
        /// Defines the _isActive
        /// </summary>
        private bool _isActive;

        /// <summary>
        /// Defines the _returnGeolocationButtonVisible
        /// </summary>
        private bool _returnGeolocationButtonVisible;

        /// <summary>
        /// Defines the _returnGeolocationButtonEnabled
        /// </summary>
        private bool _returnGeolocationButtonEnabled;

        /// <summary>
        /// Defines the _delineationButtonsVisible
        /// </summary>
        private bool _delineationButtonsVisible;

        /// <summary>
        /// Defines the _buttonAcceptDelineationEnabled
        /// </summary>
        private bool _buttonAcceptDelineationEnabled = false;

        /// <summary>
        /// Defines the _buttonCancelDelineationEnabled
        /// </summary>
        private bool _buttonCancelDelineationEnabled = false;

        /// <summary>
        /// Defines the _showOverrideButton
        /// </summary>
        private bool _showOverrideButton;

        /// <summary>
        /// Defines the _mapType
        /// </summary>
        private MapType _mapType;

        /// <summary>
        /// Gets or sets the MapClickedCommand
        /// </summary>
        public DelegateCommand<object> MapClickedCommand { get; set; }

        /// <summary>
        /// Gets or sets the MapLongPressCommand
        /// </summary>
        public DelegateCommand<object> MapLongPressCommand { get; set; }

        /// <summary>
        /// Gets or sets the AcceptDelineationCommand
        /// </summary>
        public DelegateCommand AcceptDelineationCommand { get; set; }

        /// <summary>
        /// Gets or sets the CancelDelineationCommand
        /// </summary>
        public DelegateCommand CancelDelineationCommand { get; set; }

        /// <summary>
        /// Gets or sets the OverrideDelineationCommand
        /// </summary>
        public DelegateCommand OverrideDelineationCommand { get; set; }

        /// <summary>
        /// Gets or sets the CurrentDelineationState
        /// </summary>
        private DelineationState CurrentDelineationState { get => _currentDelineationState; set => SetProperty(ref _currentDelineationState, value); }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOverrideButton
        /// </summary>
        public bool ShowOverrideButton { get => _showOverrideButton; set => SetProperty(ref _showOverrideButton, value); }

        /// <summary>
        /// Gets or sets a value indicating whether ButtonAcceptDelineationEnabled
        /// </summary>
        public bool ButtonAcceptDelineationEnabled { get => _buttonAcceptDelineationEnabled; set => SetProperty(ref _buttonAcceptDelineationEnabled, value); }

        /// <summary>
        /// Gets or sets a value indicating whether ButtonCancelDelineationEnabled
        /// </summary>
        public bool ButtonCancelDelineationEnabled { get => _buttonCancelDelineationEnabled; set => SetProperty(ref _buttonCancelDelineationEnabled, value); }

        /// <summary>
        /// Gets or sets a value indicating whether DelineationButtonsVisible
        /// </summary>
        public bool DelineationButtonsVisible { get => _delineationButtonsVisible; set => SetProperty(ref _delineationButtonsVisible, value); }

        /// <summary>
        /// Gets or sets a value indicating whether ReturnGeolocationButtonVisible
        /// </summary>
        public bool ReturnGeolocationButtonVisible { get => _returnGeolocationButtonVisible; set => SetProperty(ref _returnGeolocationButtonVisible, value); }

        /// <summary>
        /// Gets or sets a value indicating whether ReturnGeolocationButtonEnabled
        /// </summary>
        public bool ReturnGeolocationButtonEnabled { get => _returnGeolocationButtonEnabled; set => SetProperty(ref _returnGeolocationButtonEnabled, value); }

        /// <summary>
        /// Gets or sets the MapType
        /// </summary>
        public MapType MapType { get => _mapType; set => SetProperty(ref _mapType, value); }

        /// <summary>
        /// Defines the _geoLocator
        /// </summary>
        private readonly IPosition _geoLocator;

        /// <summary>
        /// Defines the _cimmytDbOperations
        /// </summary>
        private readonly ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        /// Defines the _currentGeoPosition
        /// </summary>
        private Base.DTO.GeoPosition _currentGeoPosition;

        /// <summary>
        /// Defines the _mapsPosition
        /// </summary>
        private Position _mapsPosition;

        /// <summary>
        /// Defines the _customPinsList
        /// </summary>
        private ObservableCollection<TKCustomMapPin> _customPinsList;

        /// <summary>
        /// Defines the _mapPolygonsList
        /// </summary>
        private ObservableCollection<TKPolygon> _mapPolygonsList;

        /// <summary>
        /// Defines the _mapRegion
        /// </summary>
        private MapSpan _mapRegion;

        /// <summary>
        /// Gets or sets the UseLocationCommand
        /// </summary>
        public DelegateCommand UseLocationCommand { get; set; }

        /// <summary>
        /// Gets or sets the CustomPinsList
        /// </summary>
        public ObservableCollection<TKCustomMapPin> CustomPinsList
        {
            get => _customPinsList;
            set => SetProperty(ref _customPinsList, value);
        }

        /// <summary>
        /// Gets or sets the MapPolygons
        /// </summary>
        public ObservableCollection<TKPolygon> MapPolygons { get => _mapPolygonsList; set => SetProperty(ref _mapPolygonsList, value); }

        /// <summary>
        /// Gets or sets the MapsPosition
        /// </summary>
        public Position MapsPosition
        {
            get => _mapsPosition;
            set => SetProperty(ref _mapsPosition, value);
        }

        /// <summary>
        /// Gets or sets the MapRegion
        /// </summary>
        public MapSpan MapRegion
        {
            get => _mapRegion;
            set => SetProperty(ref _mapRegion, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsGeolocationEnabled
        /// </summary>
        public bool IsGeolocationEnabled
        {
            get => _isGeolocationEnabled;
            set => SetProperty(ref _isGeolocationEnabled, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMapViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The <see cref="IEventAggregator"/></param>
        /// <param name="geoLocator">The <see cref="IPosition"/></param>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations"/></param>
        public GenericMapViewModel(IEventAggregator eventAggregator, IPosition geoLocator, INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _geoLocator = geoLocator;
            _cimmytDbOperations = cimmytDbOperations;
            CurrentDelineationState = DelineationState.Inactive;
            MapClickedCommand = new DelegateCommand<object>(MapClicked);
            MapLongPressCommand = new DelegateCommand<object>(MapLongPress);
            AcceptDelineationCommand = new DelegateCommand(AcceptDelineation);
            CancelDelineationCommand = new DelegateCommand(CancelDelineation);
            OverrideDelineationCommand = new DelegateCommand(OverrideDelinationMethod);
            ReturnGeolocationButtonEnabled = false;
            UseLocationCommand = new DelegateCommand(UseLocation).ObservesCanExecute(o => ReturnGeolocationButtonEnabled);
        }

        /// <summary>
        /// The OverrideDelinationMethod
        /// </summary>
        private void OverrideDelinationMethod()
        {
        }

        /// <summary>
        /// The GetAllPolygons
        /// </summary>
        /// <returns>The <see cref="List{Parcel}"/></returns>
        public List<Parcel> GetAllPolygons()
        {
            return _cimmytDbOperations.GetAllParcels();
        }

        /// <summary>
        /// The DrawPolygonsOnMap
        /// </summary>
        public void DrawPolygonsOnMap()
        {
            var polyRes = GetAllPolygons();
            if (polyRes == null)
                return;
            MapPolygons = new ObservableCollection<TKPolygon>();

            foreach (var item in polyRes)
            {
                if (item.Polygon != null)
                {
                    var polygon = new TKPolygon
                    {
                        StrokeColor = Color.Green,
                        StrokeWidth = 2f,
                        Color = Color.Red,
                    };
                    var listPosition = item.Polygon.ListPoints.Select(positionitem => new Position(positionitem.Latitude, positionitem.Longitude)).ToList();
                    if (listPosition.Count > 2)
                    {
                        polygon.Coordinates = listPosition;
                        MapPolygons.Add(polygon);
                    }
                }
            }
        }

        /// <summary>
        /// The CancelDelineation
        /// </summary>
        private void CancelDelineation()
        {
            ButtonCancelDelineationEnabled = false;
            ButtonAcceptDelineationEnabled = false;
            MapPolygons = null;
            CurrentDelineationState = DelineationState.Inactive;
            CustomPinsList.Clear();
        }

        /// <summary>
        /// The AcceptDelineation
        /// </summary>
        private void AcceptDelineation()
        {
            var positions = MapPolygons.ElementAt(0).Coordinates;
            if (positions.Count == 0)
            {
                _navigationService.GoBackAsync();
            }
            else
            {
                var geoPositions = positions.Select(position => new GeoPosition
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude,
                    //    AcquiredThrough = TypeOfAcquisition.SelectedOnMap
                })
                    .ToList();

                var parameters = new NavigationParameters { { "Delineation", geoPositions } };
                _navigationService.GoBackAsync(parameters);
            }
        }

        /// <summary>
        /// The MapClicked
        /// </summary>
        /// <param name="obj">The <see cref="object"/></param>
        private void MapClicked(object obj)
        {
            if ((CurrentDelineationState == DelineationState.Inactive) && (!ChooseLocation)) return;
            var position = (Position)obj;
            // var polygonsList = MapPolygons;

            if (CurrentDelineationState != DelineationState.Inactive)
            {
                var pointId = MapPolygons.ElementAt(0).Coordinates.Count;

                MapPolygons.ElementAt(0).Coordinates.Add(position);

                if (MapPolygons.ElementAt(0).Coordinates.Count > 2)
                {
                    CurrentDelineationState = DelineationState.ActiveEnoughPoints;
                    var listCoordinate = MapPolygons[0].Coordinates;
                    listCoordinate.Add(position);
                    MapPolygons[0].Coordinates = new List<Position>(listCoordinate);
                }
                else
                {
                    CurrentDelineationState = DelineationState.ActiveNotEnoughPoints;
                }

                ButtonAcceptDelineationEnabled = CurrentDelineationState == DelineationState.ActiveEnoughPoints;

                CustomPinsList.Add(new TKCustomMapPin
                {
                    ID = "polygon_marker_" + pointId,
                    Position = position,
                });
            }
            else
            {
                if (CustomPinsList == null) CustomPinsList = new ObservableCollection<TKCustomMapPin>();
                CustomPinsList.Clear();
                _currentGeoPosition = new Base.DTO.GeoPosition
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude
                };
                CustomPinsList.Add(new TKCustomMapPin
                {
                    ID = "polygon_marker",
                    Position = position,
                });
            }
        }

        /// <summary>
        /// The MapLongPress
        /// </summary>
        /// <param name="obj">The <see cref="object"/></param>
        private async void MapLongPress(object obj)
        {
            if (_mapTask != MapTask.GetPolygon) return;
            var position = (Position)obj;
            if (CurrentDelineationState != DelineationState.Inactive)
            {
                MapClicked(obj);
                return;
            }

            MapPolygons = new ObservableCollection<TKPolygon>();
            var polygon = new TKPolygon
            {
                StrokeColor = Color.Green,
                StrokeWidth = 2f,
                Color = Color.Red,
            };

            polygon.Coordinates.Add(position);

            MapPolygons.Add(polygon);
            if (CustomPinsList == null) CustomPinsList = new ObservableCollection<TKCustomMapPin>();
            CustomPinsList.Add(new TKCustomMapPin
            {
                ID = "polygon_marker_0",
                Position = position,
            });
            ButtonCancelDelineationEnabled = true;
            CurrentDelineationState = DelineationState.ActiveNotEnoughPoints;
        }

        /// <summary>
        /// The UseLocation
        /// </summary>
        private void UseLocation()
        {
            var parameters = new NavigationParameters { { "GeoPosition", _currentGeoPosition } };
            _navigationService.GoBackAsync(parameters);
        }

        /// <summary>
        /// The HandlePositionEvent
        /// </summary>
        /// <param name="position">The <see cref="Base.DTO.GeoPosition"/></param>
        private void HandlePositionEvent(Base.DTO.GeoPosition position)
        {
            if (position == null)
            {
                IsGeolocationEnabled = false;
            }
            else
            {
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
        }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            _eventAggregator.GetEvent<LivePositionEvent>().Unsubscribe(HandlePositionEvent);
        }

        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("GetLocation"))
            {
                parameters.TryGetValue("GetLocation", out object getLocation);
                if (getLocation != null)
                {
                    _isGetLocationFeatureExist = (bool)getLocation;
                    ReturnGeolocationButtonVisible = _isGetLocationFeatureExist;

                    //   MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromMeters(200));
                    _mapTask = MapTask.GetLocation;
                    MapPolygons.Clear();
                }
            }

            if (parameters.ContainsKey("ChooseLocation"))
            {
                ReturnGeolocationButtonVisible = true;
                _mapTask = MapTask.ChooseLocation;
                ChooseLocation = true;
            }

            if (parameters.ContainsKey("GetPolygon"))
            {
                object getLocation;
                parameters.TryGetValue("GetPolygon", out getLocation);
                if (getLocation != null)
                {
                    DelineationButtonsVisible = (bool)getLocation;
                    //   MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromMeters(200));
                    _mapTask = MapTask.GetPolygon;
                    _parcelId = (int)parameters["parcelId"];
                    MapPolygons.Clear();
                }
            }

            if (parameters.ContainsKey("Center"))
            {
                var center = (GeoPosition)parameters["Center"];
                AdjustMapZoom(center);
            }
            else
            {
                AdjustMapZoom();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ChooseLocation
        /// </summary>
        public bool ChooseLocation { get; set; }

        /// <summary>
        /// The AdjustMapZoom
        /// </summary>
        /// <param name="center">The <see cref="GeoPosition"/></param>
        private void AdjustMapZoom(GeoPosition center)
        {
            var position = new Position(center.Latitude, center.Longitude);
            MapRegion = MapSpan.FromCenterAndRadius(position, Distance.FromMeters(200));
            MapsPosition = position;
        }

        /// <summary>
        /// The AdjustMapZoom
        /// </summary>
        public void AdjustMapZoom()
        {
            if (MapsPosition.Latitude != 0 && MapsPosition.Longitude != 0)
            {
                if (_mapTask == MapTask.GetPolygon || _mapTask == MapTask.GetLocation)
                {
                    MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromMeters(200));
                }
                else
                {
                    MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromMeters(200));
                }
            }
            else
            {
                MapRegion = MapSpan.FromCenterAndRadius(new Position(21.4474647791868, -100.371987260878), Distance.FromKilometers(900));
            }
        }

        /// <summary>
        /// The GetPosition
        /// </summary>
        /// <returns>The <see cref="System.Threading.Tasks.Task"/></returns>
        public async System.Threading.Tasks.Task GetPosition()
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

        /// <summary>
        /// The OnNavigatingTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value == IsActive) return;
                _isActive = value;
            }
        }

        /// <summary>
        /// Defines the IsActiveChanged
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary>
        /// The OnDisappearing
        /// </summary>
        public void OnDisappearing()
        {
            _geoLocator.StopListening();
        }

        /// <summary>
        /// The OnAppearing
        /// </summary>
        public void OnAppearing()
        {
            AdjustMapZoom();
            if (_mapTask == MapTask.GetLocation)
            {
                GetPosition();
            }
            MapType = MapType.Hybrid;
        }
    }
}