using Prism;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Helper.Base.Contract;
using Helper.Base.DTO;
using Xamarin.Forms.Maps;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using TK.CustomMap;
using Xamarin.Forms;
using Helper.Base.PublishSubscriberEvents;

namespace Helper.Map.ViewModels
{


    public class GenericMapViewModel : BindableBase, INavigationAware, IActiveAware
    {
     
            private readonly INavigationService _navigationService;

            private readonly IEventAggregator _eventAggregator;

            private bool _isGeolocationEnabled;

            private readonly IPosition _geoLocator;
            private Position _mapsPosition;
            private ObservableCollection<TKCustomMapPin> _customPinsList;
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

            public GenericMapViewModel(IEventAggregator eventAggregator, INavigationService navigationService)
            {
                _navigationService = navigationService;
                _eventAggregator = eventAggregator;
              //  _geoLocator = geoLocator;
                UseLocationCommand=new DelegateCommand(UseLocation);
            }

        private void UseLocation()
        {
            
        }

        private void HandlePositionEvent(GeoPosition position)
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
            if (_geoLocator != null)
            {
                var positionRes = await _geoLocator.GetCurrentPosition();

                if (positionRes == null)
                {
                    IsGeolocationEnabled = false;
                }
                else
                {
                    IsGeolocationEnabled = true;

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

        public bool IsActive
        {
            get { return _isActive; }
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