namespace CimmytApp.ViewModels
{
    using Acr.UserDialogs;

    using CimmytApp.Core.Benchmarking.ViewModels;
    using CimmytApp.Core.Benchmarking.Views;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;

    using Microsoft.Extensions.Localization;

    using Prism.Commands;
    using Prism.Navigation;

    public class ParcelMainPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;

        private Plot plot;

        public ParcelMainPageViewModel(
            INavigationService navigationService,
            IAppDataService appDataService,
            IStringLocalizer<ParcelMainPageViewModel> localizer)
            : base(localizer)
        {
            this._navigationService = navigationService;
            AppDataService = appDataService;
        }

        public DelegateCommand NavigateToCosts =>
            new DelegateCommand(
                async () =>
                {
                    if (Plot.BemData.Costo == null)
                    {
                        await UserDialogs.Instance.AlertAsync(
                            new AlertConfig
                            {
                                Title = "No data",
                                Message = "There isn't any cost data available at this location.",
                                OkText = "Ok"
                            });
                        return;
                    }

                    NavigationParameters param = new NavigationParameters
                                                 {
                                                     { "Cost", Plot.BemData.Costo }
                                                 };
                    this._navigationService.NavigateAsync("ViewCostoPage", param);
                });

        public DelegateCommand NavigateToIncome =>
            new DelegateCommand(
                async () =>
                {
                    if (Plot.BemData.Ingreso == null)
                    {
                        await UserDialogs.Instance.AlertAsync(
                            new AlertConfig
                            {
                                Title = "No data",
                                Message = "There isn't any income data available at this location.",
                                OkText = "Ok"
                            });
                        return;
                    }

                    NavigationParameters param = new NavigationParameters
                                                 {
                                                     { "Income", Plot.BemData.Ingreso }
                                                 };
                    this._navigationService.NavigateAsync("ViewIngresoPage", param);
                });

        public DelegateCommand NavigateToProfit =>
            new DelegateCommand(
                async () =>
                {
                    if (Plot.BemData.Utilidad == null)
                    {
                        await UserDialogs.Instance.AlertAsync(
                            new AlertConfig
                            {
                                Title = "No data",
                                Message = "There isn't any profit data available at this location.",
                                OkText = "Ok"
                            });
                        return;
                    }

                    NavigationParameters param = new NavigationParameters
                                                 {
                                                     { "Profit", Plot.BemData.Utilidad }
                                                 };
                    this._navigationService.NavigateAsync("ViewUtilidadPage", param);
                });

        public DelegateCommand NavigateToYield =>
            new DelegateCommand(
                async () =>
                {
                    if (Plot.BemData.Rendimiento == null)
                    {
                        await UserDialogs.Instance.AlertAsync(
                            new AlertConfig
                            {
                                Title = "No data",
                                Message = "There isn't any yield data available at this location.",
                                OkText = "Ok"
                            });
                        return;
                    }

                    NavigationParameters param = new NavigationParameters
                                                 {
                                                     { "Yield", Plot.BemData.Rendimiento }
                                                 };
                    this._navigationService.NavigateAsync("ViewRendimientoPage", param);
                });

        public DelegateCommand NavigateToWeather => new DelegateCommand(
            async () =>
            {
                var param = new NavigationParameters
                {
                    { "Location", Plot.Position }
                };
                this._navigationService.NavigateAsync("WeatherMainPage", param);
            });

        public DelegateCommand NavigateToPotentialYield => new DelegateCommand(
            async () =>
            {
                this._navigationService.NavigateAsync("BenchmarkingPage");
            });

        public DelegateCommand NavigateToPlanner => new DelegateCommand(
            async () =>
            {
                var param = new NavigationParameters
                            {
                                { CiatContentPageViewModel.PARAMETER_NAME_POSITION, Plot.Position },
                                { CiatContentPageViewModel.PARAMETER_NAME_CROP, "Maize" } //TODO use var
                            };
                this._navigationService.NavigateAsync("CiatContentPage", param);
            });

        public IAppDataService AppDataService { get; set; }

        public Plot Plot
        {
            get => this.plot;
            set => SetProperty(ref this.plot, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue("Plot", out Plot plot);
                if (plot != null)
                {
                    Plot = plot;
                }
                else
                {
                    this._navigationService.GoBackAsync();
                }
            }
        }
    }
}