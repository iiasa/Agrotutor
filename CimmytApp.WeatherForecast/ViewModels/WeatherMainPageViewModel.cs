using System;

namespace CimmytApp.WeatherForecast.ViewModels
{
    using System.Linq;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.Parcel;

    /// <summary>
    /// Defines the <see cref="WeatherMainPageViewModel" />
    /// </summary>
    public class WeatherMainPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private INavigationService _navigationService;

        /// <summary>
        /// Defines the _weatherForecast
        /// </summary>
        private WeatherForecast _weatherForecast;

        /// <summary>
        /// Defines the _currentTemperature
        /// </summary>
        private string _currentTemperature;

        /// <summary>
        /// Defines the _minMaxTemperature
        /// </summary>
        private string _minMaxTemperature;

        /// <summary>
        /// Defines the _forecastDate
        /// </summary>
        private string _forecastDate;

        /// <summary>
        /// Defines the _forecastLocation
        /// </summary>
        private string _forecastLocation;

        /// <summary>
        /// Defines the _feltTemperature
        /// </summary>
        private string _feltTemperature;

        private HourlySummary _currentHour;
        private DailySummary _currentDay;

        /// <summary>
        /// Gets or sets the WeatherForecast
        /// </summary>
        public WeatherForecast WeatherForecast
        {
            get => _weatherForecast;
            set
            {
                SetProperty(ref _weatherForecast, value);
                if (value == null) return;
                CurrentHour = value.Location.HourlySummaries.HourlySummary.ElementAt(0);
                CurrentDay = value.Location.DailySummaries.DailySummary.ElementAt(0);
                ForecastLocation = $"Pronóstico del tiempo para {value.Location.Attributes.City} ({value.Location.Attributes.Country})";
            }
        }

        public HourlySummary CurrentHour
        {
            get => _currentHour;
            set
            {
                CurrentTemperature = $"{value.TempC}°C";
                var date = DateTime.Parse(value.TimeUtc);
                ForecastDate = date.ToString("yyyy-MM-dd, HH:mm");
                SetProperty(ref _currentHour, value);
            }
        }

        public DailySummary CurrentDay
        {
            get => _currentDay;
            set
            {
                MinMaxTemperature = $"High: {value.MaxTempC}°C - Low: {value.MinTempC}°C";
                SetProperty(ref _currentDay, value);
            }
        }

        /// <summary>
        /// Gets or sets the FeltTemperature
        /// </summary>
        public string FeltTemperature { get => _feltTemperature; set => SetProperty(ref _feltTemperature, value); }

        /// <summary>
        /// Gets or sets the ForecastLocation
        /// </summary>
        public string ForecastLocation { get => _forecastLocation; set => SetProperty(ref _forecastLocation, value); }

        /// <summary>
        /// Gets or sets the ForecastDate
        /// </summary>
        public string ForecastDate { get => _forecastDate; set => SetProperty(ref _forecastDate, value); }

        /// <summary>
        /// Gets or sets the CurrentTemperature
        /// </summary>
        public string CurrentTemperature { get => _currentTemperature; set => SetProperty(ref _currentTemperature, value); }

        /// <summary>
        /// Gets or sets the MinMaxTemperature
        /// </summary>
        public string MinMaxTemperature { get => _minMaxTemperature; set => SetProperty(ref _minMaxTemperature, value); }

        /// <summary>
        /// Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherMainPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        public WeatherMainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcel = (Parcel)parcel;
                    LoadData();
                }
            }

            LoadData();
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        private async void LoadData()
        {
            WeatherForecast = await WeatherForecast.Download(47.800239, 16.292656);
        }
    }
}