namespace CimmytApp.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Acr.UserDialogs;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.Parcel.Events;
    using Helper.Map.ViewModels;
    using Helper.Realm.BusinessContract;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public class MainPageViewModel : ViewModelBase
    {
        private readonly ICimmytDbOperations cimmytDbOperations;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;

        private string title;

        public MainPageViewModel(IEventAggregator eventAggregator, INavigationService navigationService,
            ICimmytDbOperations cimmytDbOperations, IStringLocalizer<MainPageViewModel> localizer) : base(localizer)
        {
            this.navigationService = navigationService;
            this.cimmytDbOperations = cimmytDbOperations;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<DbConnectionRequestEvent>().Subscribe(OnDbConnectionRequest);
            this.eventAggregator.GetEvent<DbConnectionAvailableEvent>().Publish();

            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
            NavigateToMapCommand = new DelegateCommand(NavigateToMap);
            NavigateToCalendarCommand = new DelegateCommand(NavigateToCalendar);
        }

        public MainPageViewModel() : base(null)
        {
        }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        public DelegateCommand NavigateToCalendarCommand { get; set; }

        public DelegateCommand NavigateToMapCommand { get; set; }

        public string Title
        {
            get => this.title;
            set => SetProperty(ref this.title, value);
        }

        private void NavigateAsync(string page)
        {
            this.navigationService.NavigateAsync("NavigationPage/" + page);
        }

        private void NavigateToCalendar()
        {
            var parameters = new NavigationParameters();
            var parcelDTO = this.cimmytDbOperations.GetAllParcels();
            parameters.Add("Parcels", parcelDTO.Select(Parcel.FromDTO).ToList());
            this.navigationService.NavigateAsync("NavigationPage/CalendarPage", parameters);
        }

        private void NavigateToMap()
        {
            var polygons = new List<Polygon>();
            var parcelLocations = new List<Pin>();
            var parcelDTO = this.cimmytDbOperations.GetAllParcels();
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
                    var listPosition = delineation.Select(positionItem =>
                            new Position((double)positionItem.Latitude, (double)positionItem.Longitude))
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

            this.navigationService.NavigateAsync("NavigationPage/Map", parameters);
        }

        private void OnDbConnectionRequest()
        {
            this.eventAggregator.GetEvent<DbConnectionEvent>().Publish(this.cimmytDbOperations);
        }
    }
}