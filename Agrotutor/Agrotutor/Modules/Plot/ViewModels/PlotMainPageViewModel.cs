using System.Collections.Generic;
using Acr.UserDialogs;
using Agrotutor.Core;
using Agrotutor.Core.Entities;
using Agrotutor.Core.Persistence;
using Agrotutor.Modules.Benchmarking;
using Agrotutor.Modules.Benchmarking.ViewModels;
using Agrotutor.Modules.Ciat;
using Agrotutor.Modules.Ciat.Types;
using Agrotutor.Modules.Ciat.ViewModels;
using Agrotutor.Modules.PriceForecasting.Types;
using Agrotutor.Modules.PriceForecasting.ViewModels;
using Agrotutor.Modules.Weather.Types;
using Agrotutor.Modules.Weather.ViewModels;
using Microsoft.Extensions.Localization;
using Prism.Commands;
using Prism.Navigation;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.Modules.Plot.ViewModels
{
    public class PlotMainPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string PositionParameterName = "PLOT_MAIN_PAGE_POSITION";
        public static string CropTypeParameterName = "PLOT_MAIN_PAGE_CROP_TYPE";
        private readonly IDbService<Core.Entities.Plot> _plotDbService;
        private CropType cropType;

        private Position position;

        public PlotMainPageViewModel(
            INavigationService navigationService,
            IDbService<Core.Entities.Plot> plotDbService,
            IStringLocalizer<PlotMainPageViewModel> localizer)
            : base(navigationService, localizer)
        {
            _plotDbService = plotDbService;
        }

        public DelegateCommand NavigateToCosts =>
            new DelegateCommand(
                async () =>
                {
                    List<Cost> costs = null;
                    using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                    {
                        if (Position != null)
                            costs = await BemDataDownloadHelper.LoadCost(Position.Latitude, Position.Longitude,
                                CropType);
                    }

                    if (costs == null)
                    {
                        await UserDialogs.Instance.AlertAsync(
                            new AlertConfig
                            {
                                Title = StringLocalizer.GetString("no_data_title"),
                                Message = StringLocalizer.GetString("no_data_cost_message"),
                                OkText = StringLocalizer.GetString("no_data_ok")
                            });
                        return;
                    }

                    var param = new NavigationParameters
                    {
                        {ViewCostPageViewModel.CostsParameterName, costs}
                    };

                    await NavigationService.NavigateAsync("ViewCostPage", param);
                });

        public DelegateCommand NavigateToIncome =>
            new DelegateCommand(
                async () =>
                {
                    List<Income> incomes = null;
                    using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                    {
                        if (Position != null)
                            incomes = await BemDataDownloadHelper.LoadIncome(Position.Latitude, Position.Longitude,
                                CropType);
                    }

                    if (incomes == null)
                    {
                        await UserDialogs.Instance.AlertAsync(
                            new AlertConfig
                            {
                                Title = StringLocalizer.GetString("no_data_title"),
                                Message = StringLocalizer.GetString("no_data_income_message"),
                                OkText = StringLocalizer.GetString("no_data_ok")
                            });
                        return;
                    }

                    var param = new NavigationParameters
                    {
                        {ViewIncomePageViewModel.IncomesParameterName, incomes}
                    };

                    await NavigationService.NavigateAsync("ViewIncomePage", param);
                });

        public DelegateCommand NavigateToProfit =>
            new DelegateCommand(
                async () =>
                {
                    List<Profit> profits = null;
                    using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                    {
                        if (Position != null)
                            profits = await BemDataDownloadHelper.LoadProfit(Position.Latitude, Position.Longitude,
                                CropType);
                    }

                    if (profits == null)
                    {
                        await UserDialogs.Instance.AlertAsync(
                            new AlertConfig
                            {
                                Title = StringLocalizer.GetString("no_data_title"),
                                Message = StringLocalizer.GetString("no_data_profit_message"),
                                OkText = StringLocalizer.GetString("no_data_ok")
                            });
                        return;
                    }

                    var param = new NavigationParameters
                    {
                        {ViewProfitPageViewModel.ProfitsParameterName, profits}
                    };

                    await NavigationService.NavigateAsync("ViewProfitPage", param);
                });

        public DelegateCommand NavigateToYield =>
            new DelegateCommand(
                async () =>
                {
                    List<Yield> yields = null;
                    using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                    {
                        if (Position != null)
                            yields = await BemDataDownloadHelper.LoadYield(Position.Latitude, Position.Longitude,
                                CropType);
                    }

                    if (yields == null)
                    {
                        await UserDialogs.Instance.AlertAsync(
                            new AlertConfig
                            {
                                Title = StringLocalizer.GetString("no_data_title"),
                                Message = StringLocalizer.GetString("no_data_yield_message"),
                                OkText = StringLocalizer.GetString("no_data_ok")
                            });
                        return;
                    }

                    var param = new NavigationParameters
                    {
                        {ViewYieldPageViewModel.YieldsParameterName, yields}
                    };

                    await NavigationService.NavigateAsync("ViewYieldPage", param);
                });

        public DelegateCommand NavigateToWeather => new DelegateCommand(
            async () =>
            {
                WeatherForecast weatherForecast = null;
                using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                {
                    weatherForecast = await WeatherForecast.Download(Position.Latitude, Position.Longitude);
                }

                if (weatherForecast == null)
                {
                    await UserDialogs.Instance.AlertAsync(
                        new AlertConfig
                        {
                            Title = StringLocalizer.GetString("no_data_title"),
                            Message = StringLocalizer.GetString("no_data_weather_message"),
                            OkText = StringLocalizer.GetString("no_data_ok")
                        });
                    return;
                }

                var param = new NavigationParameters
                {
                    {"Location", Position},
                    {WeatherPageViewModel.ForecastParameterName, weatherForecast}
                };
                await NavigationService.NavigateAsync("WeatherPage", param);
            });

        public DelegateCommand NavigateToPotentialYield => new DelegateCommand(
            async () =>
            {
                CiatData ciatData = null;
                using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("loading")))
                {
                    ciatData = await CiatDownloadHelper.LoadData(Position, "Maiz");
                }

                if (ciatData == null)
                {
                    await UserDialogs.Instance.AlertAsync(
                        new AlertConfig
                        {
                            Title = StringLocalizer.GetString("no_data_title"),
                            Message = StringLocalizer.GetString("no_data_potential_yield_message"),
                            OkText = StringLocalizer.GetString("no_data_ok")
                        });
                    return;
                }

                var param = new NavigationParameters
                {
                    {PotentialYieldPageViewModel.DataParameterName, ciatData}
                };

                await NavigationService.NavigateAsync("PotentialYieldPage", param);
            });

        public DelegateCommand NavigateToPlanner => new DelegateCommand(
            async () =>
            {
                var param = new NavigationParameters
                {
                    {CiatPageViewModel.PARAMETER_NAME_POSITION, Position},
                    {CiatPageViewModel.PARAMETER_NAME_CROP, "Maize"}
                };
                await NavigationService.NavigateAsync("CiatPage", param);
            });

        public DelegateCommand NavigateToPriceForecast => new DelegateCommand(async () =>
        {
            var forecast = await PriceForecast.FromEmbeddedResource();
            var param = new NavigationParameters
            {
                {PriceForecastPageViewModel.PriceForecastParameterName, forecast}
            };
            await NavigationService.NavigateAsync("PriceForecastPage", param);
        });


        public Position Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public CropType CropType
        {
            get => cropType;
            set => SetProperty(ref cropType, value);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(PositionParameterName))
            {
                parameters.TryGetValue(PositionParameterName, out Position position);
                if (position != null)
                    Position = position;
                else
                    NavigationService.GoBackAsync();
            }

            if (parameters.ContainsKey(CropTypeParameterName))
            {
                parameters.TryGetValue(CropTypeParameterName, out CropType cropType);
                CropType = cropType;
            }

            base.OnNavigatedTo(parameters);
        }
    }
}