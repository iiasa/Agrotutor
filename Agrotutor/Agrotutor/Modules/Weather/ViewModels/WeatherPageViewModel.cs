namespace Agrotutor.Modules.Weather.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Prism.Commands;
    using Prism.Navigation;
    using Location = Xamarin.Essentials.Location;

    using Core;
    using Microsoft.Extensions.Localization;
    using System.Collections.Generic;
    using Agrotutor.Modules.Weather.Awhere.API;
    using Agrotutor.Core.Entities;

    public class WeatherPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string LocationParameterName = "WEATHER_PAGE_LOCATION";
        public static string ForecastParameterName = "WEATHER_PAGE_FORECAST";
        public static string CurrentParameterName = "WEATHER_PAGE_CURRENT";
        public static string HistoryParameterName = "WEATHER_PAGE_HISTORY";
        public static string PlotParameterName = "WEATHER_PAGE_PLOT";

        private string _cloudCover;

        /// <summary>
        ///     Defines the _currentDay
        /// </summary>
        private WeatherForecast _currentDay;

        /// <summary>
        ///     Defines the _currentHour
        /// </summary>
        private WeatherForecast _currentHour;

        /// <summary>
        ///     Defines the _currentTemperature
        /// </summary>
        private string _currentTemperature;

        private string _growingDegreeDays;

        private string _minTemperature;
        private string _maxTemperature;

        /// <summary>
        ///     Defines the _weatherData
        /// </summary>
        private List<WeatherHistory> _weatherData;

        /// <summary>
        ///     Defines the _weatherIcon
        /// </summary>
        private string _weatherIcon;

        /// <summary>
        ///     Defines the _weatherText
        /// </summary>
        private string _weatherText;

        private string _windDirection;
        private string _windSpeed;
        private List<WeatherForecast> _weatherForecast;
        private bool _showForecastIsVisible;
        private bool _showHistoryIsVisible;
        private bool _historyIsLoading;

        public WeatherPageViewModel(INavigationService navigationService, IStringLocalizer<WeatherPageViewModel> stringLocalizer)
            : base(navigationService, stringLocalizer)
        {
            Title = "WeatherPage";

            ShowHistoryIsVisible = false;
            ShowForecastIsVisible = false;
            HistoryIsLoading = true;
        }

        public bool HistoryIsLoading
        {
            get => _historyIsLoading;
            set => SetProperty(ref _historyIsLoading, value);
        }

        public string CloudCover
        {
            get => _cloudCover;
            set => SetProperty(ref _cloudCover, value);
        }

        /// <summary>
        ///     Gets or sets the CurrentDay
        /// </summary>
        public WeatherForecast CurrentDay
        {
            get => _currentDay;
            set
            {
                MinTemperature = $"{value.MinTemperature}°C";
                MaxTemperature = $"{value.MaxTemperature}°C";
                GrowingDegreeDays = value.CalculateGdd(BaseTemperature).ToString();
                WeatherText = value.GetWeatherText();
                SetProperty(ref _currentDay, value);
            }
        }

        /// <summary>
        ///     Gets or sets the CurrentHour
        /// </summary>
        public WeatherForecast CurrentHour
        {
            get => _currentHour;
            set
            {
                CurrentTemperature = $"{value.Temperature}°C";
                var date = value.DateTime;
                WeatherIcon = value.GetWeatherIcon();
                WindSpeed = value.GetWindText();
                CloudCover = value.CloudCoverPercent + " %";
                SetProperty(ref _currentHour, value);
            }
        }

        /// <summary>
        ///     Gets or sets the CurrentTemperature
        /// </summary>
        public string CurrentTemperature
        {
            get => _currentTemperature;
            set => SetProperty(ref _currentTemperature, value);
        }

        public string GrowingDegreeDays
        {
            get => _growingDegreeDays;
            set => SetProperty(ref _growingDegreeDays, value);
        }

        public string MaxTemperature
        {
            get => _maxTemperature;
            set => SetProperty(ref _maxTemperature, value);
        }

        public string MinTemperature
        {
            get => _minTemperature;
            set => SetProperty(ref _minTemperature, value);
        }

        public bool ShowForecastIsVisible { get => _showForecastIsVisible; set => SetProperty(ref _showForecastIsVisible, value); }
        public bool ShowHistoryIsVisible { get => _showHistoryIsVisible; set => SetProperty(ref _showHistoryIsVisible, value); }

        public DelegateCommand ShowForecast =>
            new DelegateCommand(() =>
            {
                var param = new NavigationParameters { { "Forecast", WeatherForecast } };
                NavigationService.NavigateAsync("WeatherForecastPage", param);
            });

        public DelegateCommand ShowHistory =>
            new DelegateCommand(() =>
            {
                var param = new NavigationParameters { { WeatherHistoryPageViewModel.WeatherHistoryParameterName, WeatherHistory } };
                NavigationService.NavigateAsync("WeatherHistoryPage", param);
            });

        /// <summary>
        ///     Gets or sets the NavigateAsyncCommand
        /// </summary>
        public DelegateCommand<string> NavigateAsyncCommand
            => new DelegateCommand<string>((string page) =>
            {
                var parameters = new NavigationParameters
                {
                    //{ "DailyForecast", WeatherHistory.Location.DailySummaries.DailySummary }
                };

                if (page == "WeatherForecastPage")
                {
                    if (WeatherHistory == null)
                    {
                        return;
                    }
                    NavigationService.NavigateAsync(page, parameters);
                }
                else
                {
                    NavigationService.NavigateAsync(page, parameters);
                }
            });

        public Core.Entities.Plot Plot { get; set; }

        public List<WeatherHistory> WeatherHistory
        {
            get => _weatherData;
            set
            {
                SetProperty(ref _weatherData, value);
                ShowHistoryIsVisible = value != null;
                HistoryIsLoading = value == null;
            }
        }

        public List<WeatherForecast> WeatherForecast
        {
            get => _weatherForecast;
            set
            {
                SetProperty(ref _weatherForecast, value);
                ShowForecastIsVisible = value != null;

                if (value != null && value.Any())
                {
                    CurrentDay = value.ElementAt(0);
                }
            }
        }

        public string WeatherIcon
        {
            get => _weatherIcon;
            set => SetProperty(ref _weatherIcon, value);
        }

        /// <summary>
        ///     Gets or sets the WeatherText
        /// </summary>
        public string WeatherText
        {
            get => _weatherText;
            set => SetProperty(ref _weatherText, value);
        }

        public string WindDirection
        {
            get => _windDirection;
            set => SetProperty(ref _windDirection, value);
        }

        public string WindSpeed
        {
            get => _windSpeed;
            set => SetProperty(ref _windSpeed, value);
        }

        public Location Location { get; set; }
        public int? BaseTemperature { get; private set; }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            if (parameters.ContainsKey(LocationParameterName))
            {
                parameters.TryGetValue<Location>(LocationParameterName, out var location);
                Location = location;
            }

            if (parameters.ContainsKey(PlotParameterName))
            {
                parameters.TryGetValue<Core.Entities.Plot>(PlotParameterName, out var plot);
                if (plot != null)
                {
                    Plot = plot;
                    Location = plot.Position;
                }
            }

            if (parameters.ContainsKey(HistoryParameterName))
            {
                parameters.TryGetValue<List<WeatherHistory>>(HistoryParameterName, out var history);
                if (history != null) WeatherHistory = history;
                else Task.Run(() => LoadData());
            }
            else Task.Run(() => LoadData());

            if (parameters.ContainsKey(CurrentParameterName))
            {
                parameters.TryGetValue<WeatherForecast>(CurrentParameterName, out var current);
                if (current != null) CurrentHour = current;
                else Task.Run(() => LoadCurrent());
            }
            else Task.Run(() => LoadCurrent());

            if (parameters.ContainsKey(ForecastParameterName))
            {
                parameters.TryGetValue<List<WeatherForecast>>(ForecastParameterName, out var forecast);
                if (forecast != null) WeatherForecast = forecast;
                else Task.Run(() => LoadForecast());
            }
            else Task.Run(() => LoadForecast());

            base.OnNavigatedTo(parameters);
        }

        private async void LoadData()
        {
            var creds = new UserCredentials
            {
                Username = Constants.AWhereWeatherAPIUsername,
                Password = Constants.AWhereWeatherAPIPassword
            };

            DateTime start = DateTime.Now.AddMonths(-3); 
            List<Activity> activities = Plot?.Activities ?? new List<Activity>();
            Activity startActivity = activities.Where(x => (x.ActivityType == ActivityType.Initialization))?.SingleOrDefault();
            if (startActivity != null) start = startActivity.Date;
            var end = DateTime.Now.AddYears(((DateTime)start).Year - DateTime.Now.Year).AddDays(-1);
            try
            {
                var history =
                    await WeatherAPI.GetObservationsAsync(Location.Latitude, Location.Longitude, creds, start, end);
                var apiResponse = Converter.GetHistoryFromApiResponse(history);
                if (apiResponse != null)
                {
                    WeatherHistory = apiResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void LoadForecast()
        {
            if (Location != null)
            {
                var creds = new UserCredentials
                {
                    Username = Constants.AWhereWeatherAPIUsername,
                    Password = Constants.AWhereWeatherAPIPassword
                };

                var forecast = await WeatherAPI.GetForecastAsync(Location.Latitude, Location.Longitude, creds);
                var forecasts = Converter.GetForecastsFromApiResponse(forecast);
                if (forecasts != null)
                {
                    WeatherForecast = forecasts;
                }
            }
        }

        private async void LoadCurrent()
        {
            if (Location != null)
            {
                var creds = new UserCredentials
                {
                    Username = Constants.AWhereWeatherAPIUsername,
                    Password = Constants.AWhereWeatherAPIPassword
                };

                var current = await WeatherAPI.GetCurrentAsync(Location.Latitude, Location.Longitude, creds);
                var forecast = Converter.GetForecastsFromApiResponse(current);
                if (forecast != null)
                {
                    CurrentHour = forecast.ElementAt(DateTime.Now.Hour);
                }
            }
        }
    }
}
