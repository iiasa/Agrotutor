namespace CimmytApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.Parcel.Events;
    using Helper.Map.ViewModels;
    using Helper.Realm.BusinessContract;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public class MainPageViewModel : BindableBase
    {
        private readonly ICimmytDbOperations _cimmytDbOperations;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;

        private string _title;

        public MainPageViewModel(IEventAggregator eventAggregator, INavigationService navigationService,
            ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DbConnectionRequestEvent>().Subscribe(OnDbConnectionRequest);
            _eventAggregator.GetEvent<DbConnectionAvailableEvent>().Publish();

            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
            NavigateToMapCommand = new DelegateCommand(NavigateToMap);
            NavigateToCalendarCommand = new DelegateCommand(NavigateToCalendar);
        }

        public MainPageViewModel()
        {
        }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        public DelegateCommand NavigateToCalendarCommand { get; set; }

        public DelegateCommand NavigateToMapCommand { get; set; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private void NavigateAsync(string page)
        {
            _navigationService.NavigateAsync(page);//, useModalNavigation: false);
        }

        private void NavigateToCalendar()
        {
            var parameters = new NavigationParameters();
            var parcelDTO = _cimmytDbOperations.GetAllParcels();
            var parcels = parcelDTO.Select(Parcel.FromDTO).ToList();
            parameters.Add("Parcels", parcels);
            _navigationService.NavigateAsync("TelerikCalendarPage", parameters);
        }

        private void NavigateToMap()
        {
            var polygons = new ObservableCollection<Polygon>();
            var parcelLocations = new ObservableCollection<Pin>();
            var parcelDTO = _cimmytDbOperations.GetAllParcels();
            var parcels = parcelDTO.Select(Parcel.FromDTO).ToList();

            foreach (var item in parcels)
            {
                var delineation = item.Delineation;

                if (delineation != null)
                {
                    var polygon = new Polygon
                    {
                        StrokeColor = Color.Green,
                        StrokeWidth = 2f
                    };
                    var listPosition = delineation.Select(positionitem =>
                            new Position((double)positionitem.Latitude, (double)positionitem.Longitude))
                        .ToList();
                    if (listPosition.Count <= 2)
                    {
                        continue;
                    }

                    foreach (var position in listPosition)
                    {
                        polygon.Positions.Add(position);
                    }

                    polygons.Add(polygon);
                }

                if (item.Position != null && item.Position.IsSet())
                {
                    parcelLocations.Add(new Pin
                    {
                        Position = new Position((double)item.Position.Latitude, (double)item.Position.Longitude)
                    });
                }
            }

            var parameters = new NavigationParameters
            {
                { MapViewModel.PolygonsParameterName, polygons },
                { MapViewModel.PointsParameterName, parcelLocations }
            };

            _navigationService.NavigateAsync("Map", parameters);
        }

        private void OnDbConnectionRequest()
        {
            _eventAggregator.GetEvent<DbConnectionEvent>().Publish(_cimmytDbOperations);
        }
    }
}