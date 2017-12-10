﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Helper.Map.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Helper.Map.ViewModels
{
    public class MapViewModel : BindableBase, INavigationAware
    {
        private Views.Map _view;
        private INavigationService _navigationService;
        private IEventAggregator _eventAggregator;
        private IPosition _geoLocator;
        public static string MapCenterParameterName = "MapCenter";
        public static string MapTaskParameterName = "MapTask";
        public static string PolygonsParameterName = "Polygons";
        public static string PointsParameterName = "Points";
        public static string ListenForUserLocationParameterName = "ListenForUserLocation";
        public static string FollowUserLocationParameterName = "FollowUserLocation";
        public static string MaximumLocationAccuracyParameterName = "MaximumLocationAccuracy";
        private bool _listenForUserLocation;
        private bool _followUserLocation;
        private int _maximumLocationAccuracy;
        private bool _showEnableLocationHint;
        private bool _buttonAcceptDelineationEnabled;
        private bool _buttonCancelDelineationEnabled;
        private bool _delineationButtonsVisible;
        private MapType _mapType;
        private bool _returnGeolocationButtonVisible;
        private GeoPosition _userLocation;
        private GeoPosition _currentGeoPosition;

        public DelegateCommand UseLocationCommand { get; private set; }
        public DelineationState CurrentDelineationState { get; private set; }
        public DelegateCommand<object> MapClickedCommand { get; private set; }
        public DelegateCommand AcceptDelineationCommand { get; private set; }
        public DelegateCommand CancelDelineationCommand { get; private set; }
        public bool ReturnGeolocationButtonEnabled { get; private set; }
        public bool ShowEnableLocationHint
        {
            get => _showEnableLocationHint;
            set => SetProperty(ref _showEnableLocationHint, value);
        }




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
        ///     Gets or sets the MapType
        /// </summary>
        public MapType MapType
        {
            get => _mapType;
            set => SetProperty(ref _mapType, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether ReturnGeolocationButtonVisible
        /// </summary>
        public bool ReturnGeolocationButtonVisible
        {
            get => _returnGeolocationButtonVisible;
            private set => SetProperty(ref _returnGeolocationButtonVisible, value);
        }

        public MapViewModel(IEventAggregator eventAggregator, IPosition geoLocator,
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

        private void CancelDelineation()
        {
            ButtonCancelDelineationEnabled = false;
            ButtonAcceptDelineationEnabled = false;
            _view.MapPolygons = new List<Polygon>();
            CurrentDelineationState = DelineationState.Inactive;
            _view.MapPins = new List<Pin>();
        }

        private void AcceptDelineation()
        {
            IList<Position> positions = _view.MapPolygons.ElementAt(0).Positions;
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

        private void UseLocation()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "GeoPosition", Point }
            };
            _navigationService.GoBackAsync(parameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            _eventAggregator.GetEvent<LivePositionEvent>().Unsubscribe(HandlePositionEvent);
        }
        private void InitializeSelectPolygon()
        {
            DelineationButtonsVisible = true;
        }
        private void InitializeSelectLocation()
        {
            ReturnGeolocationButtonVisible = true;
        }
        private void InitializeGetLocation()
        {
            ReturnGeolocationButtonVisible = true;
            GetPosition();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(MapViewModel.MapTaskParameterName))
            {
                parameters.TryGetValue(MapViewModel.MapTaskParameterName, out object mapTask);
                if (mapTask != null)
                {
                    SetMapTask((MapTask)mapTask);
                }
            }

            if (parameters.ContainsKey(MapViewModel.MapCenterParameterName))
            {
                parameters.TryGetValue(MapViewModel.MapCenterParameterName, out object mapCenter);
                if (mapCenter != null)
                {
                    _view.MoveCamera((CameraUpdate)mapCenter);
                }
            }
            else
            {
                _view.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position(-100,20), 15)));
            }

            if (parameters.ContainsKey(MapViewModel.ListenForUserLocationParameterName))
            {
                parameters.TryGetValue(MapViewModel.ListenForUserLocationParameterName,
                    out object listenForUserLocation);
                if (listenForUserLocation != null)
                {
                    _listenForUserLocation = (bool)listenForUserLocation;
                }
            }

            if (parameters.ContainsKey(MapViewModel.FollowUserLocationParameterName))
            {
                parameters.TryGetValue(MapViewModel.FollowUserLocationParameterName,
                    out object followUserLocation);
                if (followUserLocation != null)
                {
                    _followUserLocation = (bool)followUserLocation;
                }
            }

            if (parameters.ContainsKey(MapViewModel.MaximumLocationAccuracyParameterName))
            {
                parameters.TryGetValue(MapViewModel.MaximumLocationAccuracyParameterName,
                    out object maximumLocationAccuracy);
                if (maximumLocationAccuracy != null)
                {
                    _maximumLocationAccuracy = (int)maximumLocationAccuracy;
                }
            }
            if (parameters.ContainsKey(MapViewModel.PolygonsParameterName))
            {
                parameters.TryGetValue(MapViewModel.PolygonsParameterName, out object polygons);
                if (polygons != null)
                {
                    _view.MapPolygons = (List<Polygon>)polygons;
                }
            }
            if (parameters.ContainsKey(MapViewModel.PointsParameterName))
            {
                parameters.TryGetValue(MapViewModel.PointsParameterName, out object points);
                if (points != null)
                {
                    _view.MapPins = (List<Pin>)points;
                }
            }
            _view.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position(48,16),15)));
            ShowEnableLocationHint = true;
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

        public void SetViewReference(Views.Map map)
        {
            _view = map;

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
                        _view.MapPins.Clear();
                        Position position = new Position((double)value.Latitude, (double)value.Longitude);
                        _view.MapPins.Add(new Pin
                        {
                            Position = position
                        });
                        if (_followUserLocation)
                        {
                            _view.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(position, 15)));
                        }
                        ReturnGeolocationButtonEnabled = true;
                        break;

                    case MapTask.SelectLocation:
                    case MapTask.SelectPolygon:
                        break;
                }
            }
        }

        public MapTask _mapTask { get; private set; }
        public GeoPosition Point { get; private set; }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }


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

                    Position position = new Position((double)positionRes.Latitude, (double)positionRes.Longitude);
                    _view.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(position,15)));

                    _view.MapPins = new List<Pin>(new[]
                    {
                        new Pin
                        {
                            Position = position
                        }
                    });
                    Point = new GeoPosition { Latitude = position.Latitude, Longitude = position.Longitude };
                }

                _eventAggregator.GetEvent<LivePositionEvent>().Subscribe(HandlePositionEvent);
            }
        }

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
            if (_view.MapPins == null)
            {
                _view.MapPins = new List<Pin>();
            }
            _view.MapPins.Clear();
            Point = new GeoPosition
            {
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };
            _view.MapPins.Add(new Pin
            {
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

                    _view.MapPolygons = new List<Polygon>();
                    var polygon = new Polygon
                    {
                        StrokeColor = Color.Green,
                        StrokeWidth = 2f
                    };

                    polygon.Positions.Add(position);

                    _view.MapPolygons.Add(polygon);
                    if (_view.MapPins == null)
                    {
                        _view.MapPins = new List<Pin>();
                    }
                    _view.MapPins.Add(new Pin
                    {
                        Position = position
                    });
                    ButtonCancelDelineationEnabled = true;
                    CurrentDelineationState = DelineationState.ActiveNotEnoughPoints;
                    break;

                case DelineationState.ActiveNotEnoughPoints:
                case DelineationState.ActiveEnoughPoints:
                    int pointId = _view.MapPolygons[0].Positions.Count; //todo not use ViewPolygons[0]

                    _view.MapPolygons[0].Positions.Add(position);

                    if (_view.MapPolygons[0].Positions.Count > 2)
                    {
                        CurrentDelineationState = DelineationState.ActiveEnoughPoints;
                        ButtonAcceptDelineationEnabled = true;
                    }
                    else
                    {
                        CurrentDelineationState = DelineationState.ActiveNotEnoughPoints;
                    }

                    ButtonAcceptDelineationEnabled = CurrentDelineationState == DelineationState.ActiveEnoughPoints;

                    _view.MapPins.Add(new Pin
                    {
                        Position = position
                    });
                    break;
            }
        }
    }
}
