using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Helper.Map.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace Helper.Map.ViewModels
{
    public class MapViewModel : BindableBase, INavigationAware
    {
        private Views.Map _view;
        private INavigationService _navigationService;
        private IEventAggregator _eventAggregator;
        private IPosition _geoLocator;
        private static string MapCenterParameterName = "MapCenter";
        private bool _listenForUserLocation;
        private bool _followUserLocation;
        private int _maximumLocationAccuracy;

        public DelegateCommand UseLocationCommand { get; private set; }
        public DelineationState CurrentDelineationState { get; private set; }
        public DelegateCommand<object> MapClickedCommand { get; private set; }
        public DelegateCommand AcceptDelineationCommand { get; private set; }
        public DelegateCommand CancelDelineationCommand { get; private set; }
        public bool ReturnGeolocationButtonEnabled { get; private set; }

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
            throw new NotImplementedException();
        }

        private void AcceptDelineation()
        {
            throw new NotImplementedException();
        }

        private void MapClicked(object obj)
        {
            throw new NotImplementedException();
        }

        private void UseLocation()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

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
                    _view.MapPolygons = (List<Polygon>)polygons;
                }
            }
            if (parameters.ContainsKey(GenericMapViewModel.PointsParameterName))
            {
                parameters.TryGetValue(GenericMapViewModel.PointsParameterName, out object points);
                if (points != null)
                {
                    _view.MapPins = (List<Pin>)points;
                }
            }
            _view.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position(48,16),15)));
        }

        private void SetMapTask(MapTask mapTask)
        {
            throw new NotImplementedException();
        }

        public void SetViewReference(Views.Map map)
        {
            _view = map;

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
