namespace Agrotutor.Modules.Weather.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Prism.Commands;
    using Prism.Navigation;
    using Location = Xamarin.Essentials.Location;

    using Core;
    using Types;
    using Microsoft.Extensions.Localization;

    public class WeatherPageViewModel : ViewModelBase, INavigatedAware
    {

        private string _cloudCover;

        /// <summary>
        ///     Defines the _currentDay
        /// </summary>
        private DailySummary _currentDay;

        /// <summary>
        ///     Defines the _currentHour
        /// </summary>
        private HourlySummary _currentHour;

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
        private WeatherHistory _weatherData;

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
        private WeatherForecast _weatherForecast;
        private bool _showForecastIsVisible;
        private bool _showHistoryIsVisible;

        public WeatherPageViewModel(INavigationService navigationService, IStringLocalizer<WeatherPageViewModel> stringLocalizer)
            : base(navigationService, stringLocalizer)
        {
            ShowHistoryIsVisible = false;
            ShowForecastIsVisible = false;
        }

        public string CloudCover
        {
            get => _cloudCover;
            set => SetProperty(ref _cloudCover, value);
        }

        /// <summary>
        ///     Gets or sets the CurrentDay
        /// </summary>
        public DailySummary CurrentDay
        {
            get => _currentDay;
            set
            {
                MinTemperature = $"{value.MinTempC}°C";
                MaxTemperature = $"{value.MaxTempC}°C";
                GrowingDegreeDays = value.Gdd;
                WeatherIcon = value.WxIcon;
                WeatherText = value.WxText;
                SetProperty(ref _currentDay, value);
            }
        }

        /// <summary>
        ///     Gets or sets the CurrentHour
        /// </summary>
        public HourlySummary CurrentHour
        {
            get => _currentHour;
            set
            {
                CurrentTemperature = $"{value.TempC}°C";
                var date = DateTime.Parse(value.TimeUtc);
                WindSpeed = value.WndSpdKph + " kph";
                WindDirection = value.WndDir;
                CloudCover = value.SkyCovPct + " %";
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
                var param = new NavigationParameters { { "WeatherHistory", WeatherHistory } };
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

        public WeatherHistory WeatherHistory
        {
            get => _weatherData;
            set
            {
                SetProperty(ref _weatherData, value);
                ShowHistoryIsVisible = value != null;
            }
        }

        public WeatherForecast WeatherForecast
        {
            get => _weatherForecast;
            set
            {
                SetProperty(ref _weatherForecast, value);
                ShowForecastIsVisible = value != null;

                if (value != null)
                {
                    CurrentHour = value.Location.HourlySummaries.HourlySummary.ElementAt(0);
                    CurrentDay = value.Location.DailySummaries.DailySummary.ElementAt(0);
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

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Location"))
            {
                parameters.TryGetValue<Location>("Location", out var location);
                if (location != null)
                {
                    Location = location;
                    Task.Run(() => LoadData());
                }
            }


            if (parameters.ContainsKey("Forecast"))
            {
                parameters.TryGetValue<WeatherForecast>("Forecast", out var forecast);
                if (forecast != null)
                {
                    WeatherForecast = forecast;
                }
                else Task.Run(() => LoadForecast());
            }
            else Task.Run(() => LoadForecast());
            base.OnNavigatedTo(parameters);
        }

        private async void LoadData()
        {
            if (Location != null)
            {
                WeatherHistory = await WeatherHistory.Download(Location.Latitude,
                    Location.Longitude);
            }
        }

        private async void LoadForecast()
        {
            if (Location != null)
            {
                WeatherForecast = await WeatherForecast.Download(Location.Latitude,
                    Location.Longitude);
            }
        }
    }
}
