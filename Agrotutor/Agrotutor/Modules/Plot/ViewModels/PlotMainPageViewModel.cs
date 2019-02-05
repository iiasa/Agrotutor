using Agrotutor.Modules.Ciat.ViewModels;

namespace Agrotutor.Modules.Plot.ViewModels
{
    using Acr.UserDialogs;
    using Microsoft.Extensions.Localization;

    using Prism.Commands;
    using Prism.Navigation;

    using Core;
    using Core.Entities;
    using Core.Persistence;

    public class PlotMainPageViewModel : ViewModelBase, INavigatedAware
    {
        private Plot plot;

        public PlotMainPageViewModel(
            INavigationService navigationService,
            IAppDataService appDataService,
            IStringLocalizer<PlotMainPageViewModel> localizer)
            : base(navigationService, localizer)
        {
            AppDataService = appDataService;
        }

        public DelegateCommand NavigateToCosts =>
            new DelegateCommand(
                async () =>
                {
                    if (Plot.BemData.Cost == null)
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
                                                     { "Cost", Plot.BemData.Cost }
                                                 };
                    await this.NavigationService.NavigateAsync("ViewCostPage", param);
                });

        public DelegateCommand NavigateToIncome =>
            new DelegateCommand(
                async () =>
                {
                    if (Plot.BemData.Income == null)
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
                                                     { "Income", Plot.BemData.Income }
                                                 };
                    await this.NavigationService.NavigateAsync("ViewIncomePage", param);
                });

        public DelegateCommand NavigateToProfit =>
            new DelegateCommand(
                async () =>
                {
                    if (Plot.BemData.Profit == null)
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
                                                     { "Profit", Plot.BemData.Profit }
                                                 };
                    await this.NavigationService.NavigateAsync("ViewUtilidadPage", param);
                });

        public DelegateCommand NavigateToYield =>
            new DelegateCommand(
                async () =>
                {
                    if (Plot.BemData.Yield == null)
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
                                                     { "Yield", Plot.BemData.Yield }
                                                 };
                    await this.NavigationService.NavigateAsync("ViewYieldPage", param);
                });

        public DelegateCommand NavigateToWeather => new DelegateCommand(
            async () =>
            {
                var param = new NavigationParameters
                {
                    { "Location", Plot.Position }
                };
                await this.NavigationService.NavigateAsync("WeatherPage", param);
            });

        public DelegateCommand NavigateToPotentialYield => new DelegateCommand(
            async () =>
            {
                await this.NavigationService.NavigateAsync("BenchmarkingPage");
            });

        public DelegateCommand NavigateToPlanner => new DelegateCommand(
            async () =>
            {
                var param = new NavigationParameters
                            {
                                { CiatPageViewModel.PARAMETER_NAME_POSITION, Plot.Position },
                                { CiatPageViewModel.PARAMETER_NAME_CROP, "Maize" } //TODO use var
                            };
                await this.NavigationService.NavigateAsync("CiatContentPage", param);
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
                    this.NavigationService.GoBackAsync();
                }
            }
        }
    }
}
