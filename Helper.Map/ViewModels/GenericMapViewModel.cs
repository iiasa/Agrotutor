using Prism;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CimmytApp.BusinessContract;
using CimmytApp.DTO;
using CimmytApp.DTO.Parcel;
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

        public IEventAggregator _eventAggregator;
        private bool _isGetLocationFeatureExist;
        private bool _isGeolocationEnabled;
        private MapTask _mapTask = MapTask.DisplayGeometriesOnly;
        private int _parcelId;
        private DeliniationState _currentDeliniationState;
        private bool _finishDeliniationDrawing;
        public DelegateCommand<object> MapClickedCommand { get; set; }
        public DelegateCommand<object> MapLongPressCommand { get; set; }

        public DelegateCommand AcceptDeliniationCommand { get; set; }
        public DelegateCommand CancelDeliniationCommand { get; set; }

        public DelegateCommand OverrideDeliniationCommand { get; set; }
        private DeliniationState CurrentDeliniationState
        {
            get => _currentDeliniationState;
            set => SetProperty(ref _currentDeliniationState, value);
        }

        public bool ShowOverrideButton
        {
            get => _showOverrideButton; 
            set => SetProperty(ref _showOverrideButton, value);
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
        private readonly ICimmytDbOperations _cimmytDbOperations;
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
        public GenericMapViewModel(IEventAggregator eventAggregator, IPosition geoLocator, INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _geoLocator = geoLocator;
            _cimmytDbOperations = cimmytDbOperations;
            // _mapPolygonsList=new ObservableCollection<TKPolygon>();
            CurrentDeliniationState = DeliniationState.Inactive;
            MapClickedCommand = new DelegateCommand<object>(MapClicked);
            MapLongPressCommand = new DelegateCommand<object>(MapLongPress);
            AcceptDeliniationCommand = new DelegateCommand(AcceptDeliniation);
            CancelDeliniationCommand = new DelegateCommand(CancelDeliniation);
            OverrideDeliniationCommand=new DelegateCommand(OverrideDelinationMethod);
            ReturnGeolocationButtonEnabled = false;
            UseLocationCommand = new DelegateCommand(UseLocation).ObservesCanExecute(o => ReturnGeolocationButtonEnabled);
            GetPosition();
           var polyRes= GetAllPolygons();
            DrawPolygonsOnMap(polyRes);
        }

        private void OverrideDelinationMethod()
        {
            
        }


        private List<Parcel> GetAllPolygons()
        {
          return _cimmytDbOperations.GetAllParcels();
        
        }

        private void DrawPolygonsOnMap(List<Parcel>listParcels )
        {
            if (listParcels==null)
            return;
            MapPolygons = new ObservableCollection<TKPolygon>();
         

            foreach (var item in listParcels)
            {
                if (item.Polygon != null)
                {
                    var polygon = new TKPolygon
                    {
                        StrokeColor = Color.Green,
                        StrokeWidth = 2f,
                        Color = Color.Red,
                       
                    };
                    List<Position> listPosition = new List<Position>();
                    foreach (var positionitem in item.Polygon.ListPoints)
                    {
                        listPosition.Add(new Position(positionitem.Latitude, positionitem.Longitude));
                    }
                    if (listPosition.Count > 2)
                    {
                        polygon.Coordinates = listPosition;
                        MapPolygons.Add( polygon);
           
                    }

                }
            }
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
                    //    AcquiredThrough = TypeOfAcquisition.SelectedOnMap
                    });
                }
        
                var parameters = new NavigationParameters { { "Deliniation", geoPositions } };
                _navigationService.GoBackAsync(parameters);
            }
        }

        private void MapClicked(object obj)
        {
            if (CurrentDeliniationState == DeliniationState.Inactive) return;
            var position = (Position)obj;
            // var polygonsList = MapPolygons;
     
             var pointId = MapPolygons.ElementAt(0).Coordinates.Count;
         
           MapPolygons.ElementAt(0).Coordinates.Add(position);
   
            if (MapPolygons.ElementAt(0).Coordinates.Count > 2)
            {
                CurrentDeliniationState = DeliniationState.ActiveEnoughPoints;
                var listCoordinate = MapPolygons[0].Coordinates;
                listCoordinate.Add(position);
                MapPolygons[0].Coordinates = new List<Position>(listCoordinate);
            }
            else
            {
                CurrentDeliniationState = DeliniationState.ActiveNotEnoughPoints;
            }

            ButtonAcceptDeliniationEnabled = CurrentDeliniationState == DeliniationState.ActiveEnoughPoints;
 
            CustomPinsList.Add(new TKCustomMapPin
            {
                ID = "polygon_marker_" + pointId,
                Position = position,
            });

        }

        private async void MapLongPress(object obj)
        {




            if (_mapTask != MapTask.GetPolygon) return;
            var position = (Position)obj;
            if (CurrentDeliniationState != DeliniationState.Inactive)
            {
                MapClicked(obj);
                return;
            }

            MapPolygons = new ObservableCollection<TKPolygon>();
            var polygon = new TKPolygon // TODO: following settings don't affect stroke in polygon object
            {
                StrokeColor = Color.Green,
                StrokeWidth = 2f,
                Color = Color.Red,

            };

            polygon.Coordinates.Add(position);

            MapPolygons.Add(polygon);
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
                    if (_isGetLocationFeatureExist)
                    {
                        MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromMeters(200));
                    }
                    //Enable use Location button in case the map is open from AddParcel Page and there is point already exist on the map
                    ReturnGeolocationButtonEnabled = true;
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            _eventAggregator.GetEvent<LivePositionEvent>().Unsubscribe(HandlePositionEvent);
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            //  await GetPosition();
            if (parameters.ContainsKey("GetLocation"))
            {
                parameters.TryGetValue("GetLocation", out object getLocation);
                if (getLocation != null)
                {
                    _isGetLocationFeatureExist = (bool)getLocation;
                    ReturnGeolocationButtonVisible = _isGetLocationFeatureExist;

                    MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromMeters(200));
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
                    MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromMeters(200));
                    _mapTask = MapTask.GetPolygon;
                    _parcelId = (int) parameters["parcelId"];
                 
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

                    MapRegion = MapSpan.FromCenterAndRadius(MapsPosition, Distance.FromKilometers(50));

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
        private bool _showOverrideButton;


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