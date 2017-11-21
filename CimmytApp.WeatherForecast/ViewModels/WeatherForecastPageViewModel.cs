﻿namespace CimmytApp.WeatherForecast.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using CimmytApp.DTO.Parcel;
    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Helper.DTO.SkywiseWeather.Historical;
    using Helper.DTO.SkywiseWeather.Historical.Temperature;
    using Helper.Map;
    using Prism;
    using Prism.Events;
    using Prism.Navigation;
    using Xamarin.Forms;

    public class WeatherForecastPageViewModel : DatasetReceiverBindableBase, INavigationAware, IActiveAware,
        INotifyPropertyChanged
    {
        private DailyHighTemperature _dailyHighTemperature;
        private Parcel _parcel;
        private WeatherData _weatherData;
        private GeoPosition position;

        public WeatherForecastPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            TestCommand = new Command(Test);
        }

        public event EventHandler IsActiveChanged;

        public DailyHighTemperature DailyHighTemperature
        {
            get => _dailyHighTemperature;
            private set => SetProperty(ref _dailyHighTemperature, value);
        }

        public bool IsActive { get; set; }

        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                SetProperty(ref _parcel, value);
                if (_parcel.Latitude != null && _parcel.Longitude != null) // TODO - check if undefined - ==0.0?
                {
                    if (_parcel.Latitude == 0 && _parcel.Longitude == 0)
                    {
                        return;
                    }

                    position = new GeoPosition
                    {
                        Latitude = Parcel.Latitude,
                        Longitude = Parcel.Longitude
                    };
                    LoadWeatherDataAsync();
                }
            }
        }

        public ICommand TestCommand { get; set; }

        public WeatherData WeatherData
        {
            get => _weatherData;

            set => SetProperty(ref _weatherData, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public void Test()
        {
            int i = 0;
            i++;
        }

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
        }

        private async void LoadWeatherDataAsync()
        {
            WeatherData = await WeatherService.GetWeatherData(position);
            if (WeatherData != null)
            {
                DailyHighTemperature = WeatherData.DailyHighTemperature;
            }
        }
    }
}