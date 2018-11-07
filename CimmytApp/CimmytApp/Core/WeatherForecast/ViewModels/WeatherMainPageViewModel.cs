namespace CimmytApp.WeatherForecast.ViewModels
{
    using System;
    using CimmytApp.Core.Persistence.Entities;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    /// <summary>
    ///     Defines the <see cref="WeatherMainPageViewModel" />
    /// </summary>
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

        /// <summary>
        ///     Initializes a new instance of the <see cref="WeatherMainPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" /></param>
        public WeatherMainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
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
                MinMaxTemperature = $"Máx: {value.MaxTempC}°C - Mín: {value.MinTempC}°C";
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

        /// <summary>
        ///     Gets or sets the NavigateAsyncCommand
        /// </summary>
        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        /// <summary>
        ///     Gets or sets the Plot
        /// </summary>
        public Plot Plot { get; private set; }

        /// <summary>
        ///     Gets or sets the WeatherForecast
        /// </summary>
        public WeatherData WeatherData
        {
            get => _weatherData;
            set
            {
                SetProperty(ref _weatherData, value);
                if (value == null)
                {
                    return;
                }

                //CurrentHour = value..HourlySummaries.HourlySummary.ElementAt(0);
                //CurrentDay = value.Location.DailySummaries.DailySummary.ElementAt(0);
            }
        }

        /// <summary>
        ///     Gets or sets the WeatherIcon
        /// </summary>
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

        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue<Plot>("Plot", out var plot);
                if (plot != null)
                {
                    Plot = plot;
                    LoadData();
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The LoadData
        /// </summary>
        private async void LoadData()
        {
            if (Plot.Position != null)
            {
                WeatherData = await WeatherData.Download((double)Plot.Position.Latitude,
                    (double)Plot.Position.Longitude);
            }
        }

        /// <summary>
        ///     The NavigateAsync
        /// </summary>
        /// <param name="page">The <see cref="string" /></param>
        private void NavigateAsync(string page)
        {
            if (page == "DailyForecastPage")
            {
                if (WeatherData == null)
                {
                    return;
                }

                var parameters = new NavigationParameters
                {
                    //{ "DailyForecast", WeatherData.Location.DailySummaries.DailySummary }
                };
                _navigationService.NavigateAsync(page, parameters);
            }
            else
            {
                var parameters = new NavigationParameters
                {
                    { "Plot", Plot }
                };
                _navigationService.NavigateAsync(page, parameters);
            }
        }
    }
}