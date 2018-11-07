namespace CimmytApp.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using Helper.Map.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public class ParcelMainPageViewModel : ViewModelBase, INavigatedAware
    {
        public IAppDataService AppDataService { get; set; }

        private readonly INavigationService _navigationService;
        private Plot plot;

        public ParcelMainPageViewModel(INavigationService navigationService, IAppDataService appDataService,
            IStringLocalizer<ParcelMainPageViewModel> localizer) : base(localizer)
        {
            _navigationService = navigationService;
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
            NavigateToMapCommand = new DelegateCommand(NavigateToMap);
            GoBackCommand = new DelegateCommand(GoBack);
            AppDataService = appDataService;
        }

        public DelegateCommand GoBackCommand { get; set; }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        public DelegateCommand NavigateToMapCommand { get; set; }

        public Plot Plot
        {
            get => this.plot;
            set => SetProperty(ref this.plot, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void LoadParcel(int id)
        {
            Plot = await AppDataService.GetPlot(id);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                var id = (int)parameters["Id"]; // todo TryGet!
                LoadParcel(id);
            }
            catch
            {
                // ignored
            }
        }

        private void GoBack()
        {
            _navigationService.NavigateAsync("app:///ParcelsOverviewPage");
        }

        private void NavigateAsync(string page)
        {
            var parameters = new NavigationParameters
            {
                { "Plot", Plot }
            };
            if (page == "ParcelPage")
            {
                parameters.Add("EditEnabled", false);
                parameters.Add("Caller", "ParcelMainPage");
            }
            _navigationService.NavigateAsync(page, parameters);
        }

        private void NavigateToMap()
        {
            var parameters = new NavigationParameters();
            var delineation = Plot.Delineation;
            if (delineation != null && delineation.Count > 2)
            {
                var polygon = new Polygon
                {
                    StrokeColor = Color.Green,
                    StrokeWidth = 2f
                };
                foreach (var geoPosition in delineation)
                {
                    polygon.Positions.Add(new Xamarin.Forms.GoogleMaps.Position((double)geoPosition.Latitude, (double)geoPosition.Longitude));
                }

                var viewPolygons = new List<Polygon>
                {
                    polygon
                };
                parameters.Add(MapViewModel.PolygonsParameterName, viewPolygons);
            }

            if (Plot.Position != null)
            {
                parameters.Add(MapViewModel.PointsParameterName, new List<Pin>
                {
                    new Pin
                    {
                        Position = new Xamarin.Forms.GoogleMaps.Position((double)Plot.Position.Latitude, (double)Plot.Position.Longitude)
                    }
                });
                parameters.Add(MapViewModel.MapCenterParameterName, CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                    new Xamarin.Forms.GoogleMaps.Position((double)Plot.Position.Latitude, (double)Plot.Position.Longitude), 15)));
            }

            _navigationService.NavigateAsync("Map", parameters);
        }
    }
}