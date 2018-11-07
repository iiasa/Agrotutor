namespace CimmytApp.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.Core.Persistence;
    using Helper.Map.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAppDataService appDataService;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;

        private string title;

        public MainPageViewModel(IEventAggregator eventAggregator, INavigationService navigationService,
            IAppDataService appDataService, IStringLocalizer<MainPageViewModel> localizer) : base(localizer)
        {
            this.navigationService = navigationService;
            this.appDataService = appDataService;
            this.eventAggregator = eventAggregator;

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

        private async void NavigateToCalendar()
        {
            var parameters = new NavigationParameters();
            var plots = await this.appDataService.GetAllPlots();
            parameters.Add("Plots", plots);
            this.navigationService.NavigateAsync("NavigationPage/CalendarPage", parameters);
        }

        private async void NavigateToMap()
        {
            var polygons = new List<Polygon>();
            var parcelLocations = new List<Pin>();
            var plots = await this.appDataService.GetAllPlots();

            foreach (var item in plots)
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

                if (item.Position != null)
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
    }
}