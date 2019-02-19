using System.Collections.Generic;
using Agrotutor.Core;
using Agrotutor.Modules.Charts.Types;
using Agrotutor.Modules.Weather.Types;
using Microcharts;
using Microsoft.Extensions.Localization;
using Prism.Commands;
using Prism.Navigation;

namespace Agrotutor.Modules.Weather.ViewModels
{
    public class WeatherHistoryPageViewModel : ViewModelBase, INavigatedAware
    {
        private Chart _currentChart;

        private List<string> _datasetNames;
        private int _graphDays;

        private int _selectedDataset;

        private WeatherHistory _weatherData;
        private List<EntryWithTime> selectedValEntries;

        public WeatherHistoryPageViewModel(INavigationService navigationService,
            IStringLocalizer<WeatherHistoryPageViewModel> stringLocalizer)
            : base(navigationService, stringLocalizer)
        {
            DatasetNames = new List<string>
            {
                "Días de grado creciente",
                "Días de grado de enfriamiento",
                "Días de grado de calefacción",
                "Precipitación diaria",
                "Precipitaciones por hora",
                "Humedad relativa por hora",
                "Radiación solar diaria",
                "Radiación solar por hora",
                "Temperatura por hora",
                "Temperatura alta diaria",
                "Temperatura baja diaria",
                "Punto de rocío por hora",
                "Velocidad del viento por hora",
                "Dirección del viento por hora",
                "Evapotranspiración diaria de cultivos cortos",
                "Evapotranspiración diaria de cultivos altos",
                "Evapotranspiración horaria de cultivos cortos",
                "Evapotranspiración horaria de cultivos altos"
            };
            DatasetNames = new List<string>
            {
                "Growing degree days",
                "Cooling degree days",
                "Heating degree days",
                "Daily precipitation",
                "Hourly precipitation",
                "Hourly relative humidity",
                "Daily solar radiation",
                "Hourly solar radiation",
                "Hourly temperature",
                "Daily high temperature",
                "Daily low temperature",
                "Hourly dewpoint",
                "Hourly wind speed",
                "Hourly wind direction",
                "Daily evapotranspiration short crop",
                "Daily evapotranspiration tall crop",
                "Hourly evapotranspiration short crop",
                "Hourly evapotranspiration tall crop"
            };
        }

        public int GraphDays
        {
            get => _graphDays;
            set => _graphDays = value == 0 ? 365 : value;
        }

        public List<EntryWithTime> SelectedValEntries
        {
            get => selectedValEntries;
            set
            {
                selectedValEntries = value;
                CurrentChart = new LineChart {Entries = value};
            }
        }

        public int SelectedDataset
        {
            get => _selectedDataset;
            set
            {
                _selectedDataset = value;
                if (MyWeatherData == null) return;
                switch (value)
                {
                    case 0:
                        SelectedValEntries = MyWeatherData.Gdd.GetChartEntries();
                        break;
                    case 1:
                        SelectedValEntries = MyWeatherData.Cdd.GetChartEntries();
                        break;
                    case 2:
                        SelectedValEntries = MyWeatherData.Hdd.GetChartEntries();
                        break;
                    case 3:
                        SelectedValEntries = MyWeatherData.Dp.GetChartEntries();
                        break;
                    case 4:
                        SelectedValEntries = MyWeatherData.Hp.GetChartEntries();
                        break;
                    case 5:
                        SelectedValEntries = MyWeatherData.Hrh.GetChartEntries();
                        break;
                    case 6:
                        SelectedValEntries = MyWeatherData.Dsr.GetChartEntries();
                        break;
                    case 7:
                        SelectedValEntries = MyWeatherData.Hsr.GetChartEntries();
                        break;
                    case 8:
                        SelectedValEntries = MyWeatherData.Ht.GetChartEntries();
                        break;
                    case 9:
                        SelectedValEntries = MyWeatherData.Dht.GetChartEntries();
                        break;
                    case 10:
                        SelectedValEntries = MyWeatherData.Dlt.GetChartEntries();
                        break;
                    case 11:
                        SelectedValEntries = MyWeatherData.Hd.GetChartEntries();
                        break;
                    case 12:
                        SelectedValEntries = MyWeatherData.Hws.GetChartEntries();
                        break;
                    case 13:
                        SelectedValEntries = MyWeatherData.Hwd.GetChartEntries();
                        break;
                    case 14:
                        SelectedValEntries = MyWeatherData.Desc.GetChartEntries();
                        break;
                    case 15:
                        SelectedValEntries = MyWeatherData.Detc.GetChartEntries();
                        break;
                    case 16:
                        SelectedValEntries = MyWeatherData.Hesc.GetChartEntries();
                        break;
                    case 17:
                        SelectedValEntries = MyWeatherData.Hetc.GetChartEntries();
                        break;
                }
            }
        }

        public DelegateCommand<string> SetGraphDays =>
            new DelegateCommand<string>(val => { GraphDays = int.Parse(val); });


        public List<string> DatasetNames
        {
            get => _datasetNames;
            set => SetProperty(ref _datasetNames, value);
        }

        public WeatherHistory MyWeatherData
        {
            get => _weatherData;
            set => SetProperty(ref _weatherData, value);
        }

        public Chart CurrentChart
        {
            get => _currentChart;
            set => SetProperty(ref _currentChart, value);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("WeatherHistory"))
            {
                parameters.TryGetValue<WeatherHistory>("WeatherHistory", out var weatherData);
                if (weatherData != null) MyWeatherData = weatherData;
            }

            base.OnNavigatedTo(parameters);
        }
    }
}