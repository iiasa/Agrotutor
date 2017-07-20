namespace Helper.Map.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms.Maps;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using TK.CustomMap;

    using Base.Contract;
    using Base.DTO;

    public class GenericMapViewModel : BindableBase
    {
        public class LocationPageViewModel : BindableBase, INavigationAware
        {
            private readonly INavigationService _navigationService;

            private readonly IEventAggregator _eventAggregator;

            private bool _isGeolocationEnabled;

            private readonly IPosition _geoLocator;
            private Position _mapsPosition;
            private ObservableCollection<TKCustomMapPin> _customPinsList;
            private MapSpan _mapRegion;

            public LocationPageViewModel()
            {
            }

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

            public LocationPageViewModel(IEventAggregator eventAggregator, IPosition geoLocator, INavigationService navigationService)
            {
                _navigationService = navigationService;

                _eventAggregator = eventAggregator;

                _geoLocator = geoLocator;
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
                //  _eventAggregator.GetEvent<LivePositionEvent>().Unsubscribe(HandlePositionEvent);
            }

            public async void OnNavigatedTo(NavigationParameters parameters)
            {
                var positionRes = await _geoLocator.GetCurrentPosition();

                if (positionRes == null)
                {
                    IsGeolocationEnabled = false;
                }
                else
                {
                    IsGeolocationEnabled = true;
                    //  GrowerData = MapGrowPositionData(positionRes);

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

                // _eventAggregator.GetEvent<LivePositionEvent>().Subscribe(HandlePositionEvent);
            }

            public void OnNavigatingTo(NavigationParameters parameters)
            {
                
            }
        }
    }
}