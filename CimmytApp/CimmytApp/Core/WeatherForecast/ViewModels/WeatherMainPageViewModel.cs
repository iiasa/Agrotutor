﻿namespace CimmytApp.WeatherForecast.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Essentials;

    public class WeatherMainPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        ///     Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

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

        /// <summary>
        ///     Defines the _feltTemperature
        /// </summary>
        private string _feltTemperature;

        /// <summary>
        ///     Defines the _forecastDate
        /// </summary>
        private string _forecastDate;

        private string _growingDegreeDays;

        /// <summary>
        ///     Defines the _minMaxTemperature
        /// </summary>
        private string _minMaxTemperature;

        /// <summary>
        ///     Defines the _weatherData
        /// </summary>
        private WeatherData _weatherData;

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

        public WeatherMainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
                MinMaxTemperature = $"Mín: {value.MinTempC}°C - Máx: {value.MaxTempC}°C";
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
                ForecastDate = date.ToString("yyyy-MM-dd, HH:mm");
                FeltTemperature = $"sensación: {value.AppTempC}°C";
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

        /// <summary>
        ///     Gets or sets the FeltTemperature
        /// </summary>
        public string FeltTemperature
        {
            get => _feltTemperature;
            set => SetProperty(ref _feltTemperature, value);
        }

        /// <summary>
        ///     Gets or sets the ForecastDate
        /// </summary>
        public string ForecastDate
        {
            get => _forecastDate;
            set => SetProperty(ref _forecastDate, value);
        }

        public string GrowingDegreeDays
        {
            get => _growingDegreeDays;
            set => SetProperty(ref _growingDegreeDays, value);
        }

        /// <summary>
        ///     Gets or sets the MinMaxTemperature
        /// </summary>
        public string MinMaxTemperature
        {
            get => _minMaxTemperature;
            set => SetProperty(ref _minMaxTemperature, value);
        }

        public bool ShowForecastIsVisible { get => _showForecastIsVisible; set => SetProperty(ref _showForecastIsVisible, value); }
        public bool ShowHistoryIsVisible { get => _showHistoryIsVisible; set => SetProperty(ref _showHistoryIsVisible, value); }

        public DelegateCommand ShowForecast =>
            new DelegateCommand(() =>
            {
                var param = new NavigationParameters { { "Forecast", WeatherForecast } };
                _navigationService.NavigateAsync("DailyForecastPage", param);
            });

        public DelegateCommand ShowHistory =>
            new DelegateCommand(() =>
            {
                var param = new NavigationParameters { { "WeatherData", WeatherData } };
                _navigationService.NavigateAsync("WeatherDataSelection", param);
            });

        /// <summary>
        ///     Gets or sets the NavigateAsyncCommand
        /// </summary>
        public DelegateCommand<string> NavigateAsyncCommand
            => new DelegateCommand<string>((string page) =>
            {
                var parameters = new NavigationParameters
                {
                    //{ "DailyForecast", WeatherData.Location.DailySummaries.DailySummary }
                };

                if (page == "DailyForecastPage")
                {
                    if (WeatherData == null)
                    {
                        return;
                    }
                    _navigationService.NavigateAsync(page, parameters);
                }
                else
                {
                    _navigationService.NavigateAsync(page, parameters);
                }
            });

        public WeatherData WeatherData
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
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
        }

        public void OnNavigatingTo(NavigationParameters parameters) { }

        private async void LoadData()
        {
            if (Location != null)
            {
                WeatherData = await WeatherData.Download(Location.Latitude,
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