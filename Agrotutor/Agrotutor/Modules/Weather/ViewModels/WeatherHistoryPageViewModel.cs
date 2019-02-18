namespace Agrotutor.Modules.Weather.ViewModels
{
    using System.Collections.Generic;
    using Microcharts;
    using Prism.Commands;
    using Prism.Navigation;
    using Microsoft.Extensions.Localization;

    using Core;
    using Types;
    using Charts.Types;

    public class WeatherHistoryPageViewModel : ViewModelBase, INavigatedAware
    {

        private List<string> _datasetNames;

        private int _selectedDataset;

        private WeatherHistory _weatherData;
        private Chart _currentChart;
        private List<EntryWithTime> selectedValEntries;
        private int _graphDays;

        public WeatherHistoryPageViewModel(INavigationService navigationService, IStringLocalizer<WeatherHistoryPageViewModel> stringLocalizer)
            :base(navigationService,stringLocalizer)
        {
            DatasetNames = new List<string>
            {
                "Días de grado creciente",
                "Días de grado de enfriamiento",
                "Días de grado de calefacción",
                "Precipitación diaria",
                "Radiación solar diaria",
                "Temperatura alta diaria",
                "Temperatura baja diaria",
                "Evapotranspiración diaria de cultivos cortos",
                "Evapotranspiración diaria de cultivos altos",
            };
            DatasetNames = new List<string>
            {
                "Growing degree days",
                "Cooling degree days",
                "Heating degree days",
                "Daily precipitation",
                "Daily solar radiation",
                "Daily high temperature",
                "Daily low temperature",
                "Daily evapotranspiration short crop",
                "Daily evapotranspiration tall crop",
            };
        }

        public int GraphDays
        {
            get => _graphDays;
            set => _graphDays = (value == 0) ? 365 : value;
        }

        public List<EntryWithTime> SelectedValEntries
        {
            get => selectedValEntries;
            set
            {
                selectedValEntries = value;
                CurrentChart = new LineChart { Entries = value };
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
                        SelectedValEntries = MyWeatherData.Dsr.GetChartEntries();
                        break;
                    case 5:
                        SelectedValEntries = MyWeatherData.Dht.GetChartEntries();
                        break;
                    case 6:
                        SelectedValEntries = MyWeatherData.Dlt.GetChartEntries();
                        break;
                    case 7:
                        SelectedValEntries = MyWeatherData.Desc.GetChartEntries();
                        break;
                    case 8:
                        SelectedValEntries = MyWeatherData.Detc.GetChartEntries();
                        break;
                }
            }
        }

        public DelegateCommand<string> SetGraphDays =>
            new DelegateCommand<string>((string val) =>
            {
                GraphDays = int.Parse(val);
            });


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

        public Chart CurrentChart { get => _currentChart; set => SetProperty(ref _currentChart, value); }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("WeatherHistory"))
            {
                parameters.TryGetValue<WeatherHistory>("WeatherHistory", out var weatherData);
                if (weatherData != null)
                {
                    MyWeatherData = weatherData;
                }
            }

            base.OnNavigatedTo(parameters);
        }
    }
}
