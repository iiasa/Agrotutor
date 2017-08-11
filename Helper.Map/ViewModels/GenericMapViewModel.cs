using Prism;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CimmytApp.DTO;
using Helper.Base.Contract;
using Xamarin.Forms.Maps;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using TK.CustomMap;
using Helper.Base.PublishSubscriberEvents;
using TK.CustomMap.Overlays;
using Xamarin.Forms;

namespace Helper.Map.ViewModels
{
    public class GenericMapViewModel : BindableBase, INavigationAware, IActiveAware
    {
        private readonly INavigationService _navigationService;

        private readonly IEventAggregator _eventAggregator;

        private bool _isGeolocationEnabled;
        private MapTask _mapTask = MapTask.DisplayGeometriesOnly;
        private DeliniationState _currentDeliniationState;

        public ICommand MapClickedCommand { get; set; }
        public ICommand MapLongPressCommand { get; set; }

        public DelegateCommand AcceptDeliniationCommand { get; set; }
        public DelegateCommand CancelDeliniationCommand { get; set; }

        private DeliniationState CurrentDeliniationState
        {
            get => _currentDeliniationState;
            set => SetProperty(ref _currentDeliniationState, value);
        }

        public bool ButtonAcceptDeliniationEnabled
        {
            get => _buttonAcceptDeliniationEnabled;
            set => SetProperty(ref _buttonAcceptDeliniationEnabled, value);
        }

        public bool ButtonCancelDeliniationEnabled
        {
            get => _buttonCancelDeliniationEnabled;
            set => SetProperty(ref _buttonCancelDeliniationEnabled, value);
        }

        public bool DeliniationButtonsVisible
        {
            get => _deliniationButtonsVisible;
            set => SetProperty(ref _deliniationButtonsVisible, value);
        }

        public bool ReturnGeolocationButtonVisible
        {
            get => _returnGeolocationButtonVisible;
            set => SetProperty(ref _returnGeolocationButtonVisible, value);
        }

        public bool ReturnGeolocationButtonEnabled
        {
            get => _returnGeolocationButtonEnabled;
            set => SetProperty(ref _returnGeolocationButtonEnabled, value);
        }

        private readonly IPosition _geoLocator;
        private Base.DTO.GeoPosition _currentGeoPosition;
        private Position _mapsPosition;
        private ObservableCollection<TKCustomMapPin> _customPinsList;
        private ObservableCollection<TKPolygon> _mapPolygonsList;
        private MapSpan _mapRegion;
        public DelegateCommand UseLocationCommand { get; set; }

        public ObservableCollection<TKCustomMapPin> CustomPinsList
        {
            get { return _customPinsList; }
            set
            {
                SetProperty(ref _customPinsList, value);
            }
        }

        public ObservableCollection<TKPolygon> MapPolygons
        {
            get => _mapPolygonsList;
            set => SetProperty(ref _mapPolygonsList, value);
        }

        public Position MapsPosition
        {
            get { return _mapsPosition; }
            set
            {
                SetProperty(ref _mapsPosition, value);
            }
        }

        public MapSpan MapRegion
        {
            get { return _mapRegion; }
            set
            {
                SetProperty(ref _mapRegion, value);
            }
        }

        public bool IsGeolocationEnabled
        {
            get { return _isGeolocationEnabled; }
            set
            {
                SetProperty(ref _isGeolocationEnabled, value);
            }
        }

        //IPosition geoLocator,
        public GenericMapViewModel(IEventAggregator eventAggregator, IPosition geoLocator, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _geoLocator = geoLocator;
            CurrentDeliniationState = DeliniationState.Inactive;
            MapClickedCommand = new Command(MapClicked);
            MapLongPressCommand = new Command(MapLongPress);
            AcceptDeliniationCommand = new DelegateCommand(AcceptDeliniation);
            CancelDeliniationCommand = new DelegateCommand(CancelDeliniation);
            ReturnGeolocationButtonEnabled = false;
            UseLocationCommand = new DelegateCommand(UseLocation);
            GetPosition();
        }

        private void CancelDeliniation()
        {
            ButtonCancelDeliniationEnabled = false;
            ButtonAcceptDeliniationEnabled = false;
            MapPolygons = null;
            CurrentDeliniationState = DeliniationState.Inactive;
            CustomPinsList.Clear();
        }

        private void AcceptDeliniation()
        {
            var positions = MapPolygons.ElementAt(0).Coordinates;
            if (positions.Count == 0)
            {
                _navigationService.GoBackAsync();
            }
            else
            {
                var geoPositions = new List<CimmytApp.DTO.GeoPosition>();
                foreach (var position in positions)
                {
                    geoPositions.Add(new CimmytApp.DTO.GeoPosition
                    {
                        Latitude = position.Latitude,
                        Longitude = position.Longitude,
                        AcquiredThrough = TypeOfAcquisition.SelectedOnMap
                    });
                }
                geoPositions.Add(geoPositions.ElementAt(0));
                var parameters = new NavigationParameters { { "Deliniation", geoPositions } };
                _navigationService.GoBackAsync(parameters);
            }
        }

        private void MapClicked(object obj)
        {
            if (CurrentDeliniationState == DeliniationState.Inactive) return;
            var position = (Position)obj;
            var polygonsList = MapPolygons;
            var pointId = polygonsList.ElementAt(0).Coordinates.Count;
            polygonsList.ElementAt(0).Coordinates.Add(position);
            CurrentDeliniationState = (polygonsList.ElementAt(0).Coordinates.Count > 2)
                ? DeliniationState.ActiveEnoughPoints
                : DeliniationState.ActiveNotEnoughPoints;
            ButtonAcceptDeliniationEnabled = CurrentDeliniationState == DeliniationState.ActiveEnoughPoints;
            MapPolygons = polygonsList;
            CustomPinsList.Add(new TKCustomMapPin
            {
                ID = "polygon_marker_" + pointId,
                Position = position,
            });
        }

        private void MapLongPress(object obj)
        {
            if (_mapTask != MapTask.GetPolygon) return;
            var position = (Position)obj;
            if (CurrentDeliniationState != DeliniationState.Inactive)
            {
                MapClicked(obj);
                return;
            }

            _mapPolygonsList = new ObservableCollection<TKPolygon>();
            var polygon = new TKPolygon // TODO: following settings don't affect stroke in polygon object
            {
                StrokeColor = Color.Black,
                StrokeWidth = 2,
                Color = Color.FromHex("#885F9EA0")
            };
            polygon.Coordinates.Add(position);
            _mapPolygonsList.Add(polygon);
            MapPolygons = _mapPolygonsList;
            CustomPinsList.Add(new TKCustomMapPin
            {
                ID = "polygon_marker_0",
                Position = position,
            });
            ButtonCancelDeliniationEnabled = true;
            CurrentDeliniationState = DeliniationState.ActiveNotEnoughPoints;
        }

        private void UseLocation()
        {
            var parameters = new NavigationParameters { { "GeoPosition", _currentGeoPosition } };
            _navigationService.GoBackAsync(parameters);
        }

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
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            _eventAggregator.GetEvent<LivePositionEvent>().Unsubscribe(HandlePositionEvent);
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await GetPosition();
            if (parameters.ContainsKey("GetLocation"))
            {
                parameters.TryGetValue("GetLocation", out object getLocation);
                if (getLocation != null)
                {
                    ReturnGeolocationButtonVisible = (bool)getLocation;
                    _mapTask = MapTask.GetLocation;
                }
            }

            if (parameters.ContainsKey("GetPolygon"))
            {
                object getLocation;
                parameters.TryGetValue("GetPolygon", out getLocation);
                if (getLocation != null)
                {
                    DeliniationButtonsVisible = (bool)getLocation;
                    _mapTask = MapTask.GetPolygon;
                }
            }
        }

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

                    MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromMiles(.07));

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

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private bool _isActive;
        private bool _returnGeolocationButtonVisible;
        private bool _returnGeolocationButtonEnabled;
        private bool _deliniationButtonsVisible;
        private bool _buttonAcceptDeliniationEnabled = false;
        private bool _buttonCancelDeliniationEnabled = false;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value == IsActive) return;
                _isActive = value;
                if (_isActive)
                {
                    // Well, it seems we don't have to put anything here - it works now and I won't touch this
                    // The error was that ImageView can't be cast to ViewGroup when using the map in a tab and exiting the TabbedPage
                    // I assumed the map component didn't realize it is no more and still tries to draw.
                    // Back to .net greatness: no actual change leading to fixing a problem which shouldn't exist.
                    // Hope these comments don't break anything...
                }
                else
                {
                }
            }
        }

        public event EventHandler IsActiveChanged;
    }
}