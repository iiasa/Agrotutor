using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agrotutor.Core;
using Agrotutor.Modules.Charts.Types;
using Microcharts;
using Microsoft.Extensions.Localization;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace Agrotutor.Modules.Weather.ViewModels
{
    public class WeatherHistoryPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string WeatherHistoryParameterName = "WEATHER_HISTORY_PARAMETER";

        private List<string> _datasetNames;
        private int _graphDays;

        private int _selectedDataset;

        private List<WeatherHistory> _weatherData;
        private List<EntryWithTime> selectedValEntries;
        private PlotModel _chartModel;

        public WeatherHistoryPageViewModel(INavigationService navigationService,
            IStringLocalizer<WeatherHistoryPageViewModel> stringLocalizer)
            : base(navigationService, stringLocalizer)
        {
            Title = "WeatherHistoryPage";

            DatasetNames = new List<string>
            {
                StringLocalizer.GetString("precip"),
                StringLocalizer.GetString("rhh"),
                StringLocalizer.GetString("rhl"),
                StringLocalizer.GetString("sr"),
                StringLocalizer.GetString("tl"),
                StringLocalizer.GetString("th"),
                StringLocalizer.GetString("wa"),
                StringLocalizer.GetString("wdh"),
                StringLocalizer.GetString("wmh")
            };
            SelectedDataset = -1;
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
            }
        }

        public int SelectedDataset
        {
            get => _selectedDataset;
            set
            {
                _selectedDataset = value;
                if (MyWeatherData == null) return;
                List<double> items = null;
                List<DateTime> dates = null;
                var unit = "";
                switch (value)
                {
                    case 0:
                        items = MyWeatherData.Select(x => x.PrecipitationAmount).ToList();
                        unit = MyWeatherData.ElementAt(0).PrecipitationUnits;
                        break;
                    case 1:
                        items = MyWeatherData.Select(x => x.RelativeHumidityMax).ToList();
                        unit = "%"; 
                        break;
                    case 2:
                        items = MyWeatherData.Select(x => x.RelativeHumidityMin).ToList();
                        unit = "%";
                        break;
                    case 3:
                        items = MyWeatherData.Select(x => x.SolarRadiationAmount).ToList();
                        unit = MyWeatherData.ElementAt(0).SolarRadiationUnits;
                        break;
                    case 4:
                        items = MyWeatherData.Select(x => x.TemperatureMin).ToList();
                        unit = MyWeatherData.ElementAt(0).TemperatureUnits;
                        break;
                    case 5:
                        items = MyWeatherData.Select(x => x.TemperatureMax).ToList();
                        unit = MyWeatherData.ElementAt(0).TemperatureUnits;
                        break;
                    case 6:
                        items = MyWeatherData.Select(x => x.WindAverage).ToList();
                        unit = MyWeatherData.ElementAt(0).WindUnits;
                        break;
                    case 7:
                        items = MyWeatherData.Select(x => x.WindDayMax).ToList();
                        unit = MyWeatherData.ElementAt(0).WindUnits;
                        break;
                    case 8:
                        items = MyWeatherData.Select(x => x.WindMorningMax).ToList();
                        unit = MyWeatherData.ElementAt(0).WindUnits;
                        break;
                }
                dates = MyWeatherData.Select(x => x.Date).ToList();

                SelectedValEntries = EntryWithTime.From(items, dates);

                RenderChart();
            }
        }

        private void RenderChart()
        {
            ChartModel?.PlotView?.InvalidatePlot();
            ChartModel?.Axes.Clear();
            ChartModel?.Series.Clear();
            ChartModel = new PlotModel {Title = "", PlotType = PlotType.XY };

            var points = new List<DataPoint>();

            if (SelectedValEntries != null)
                foreach (var selectedValEntry in SelectedValEntries)
                {
                    var point = DateTimeAxis.CreateDataPoint(selectedValEntry.Time, selectedValEntry.Value);
                    points.Add(point);
                }

            var s = new LineSeries {ItemsSource = points};
            // s.YAxis.Unit = "$";
            ChartModel.Axes.Add(new DateTimeAxis {Position = AxisPosition.Bottom, StringFormat = "M/d/yy"});
            ChartModel.Series.Add(s);
        }

        public DelegateCommand<string> SetGraphDays =>
            new DelegateCommand<string>(val => { GraphDays = int.Parse(val); });


        public List<string> DatasetNames
        {
            get => _datasetNames;
            set => SetProperty(ref _datasetNames, value);
        }

        public List<WeatherHistory> MyWeatherData
        {
            get => _weatherData;
            set => SetProperty(ref _weatherData, value);
        }


        public PlotModel ChartModel
        {
            get => _chartModel;
            set => SetProperty(ref _chartModel, value);
        }

        public DelegateCommand PageAppearingCommand =>
            new DelegateCommand(async () => await PageAppearing());

        private Task PageAppearing()
        {
            SelectedDataset = 0;
            RenderChart();
            return Task.CompletedTask;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(WeatherHistoryParameterName))
            {
                parameters.TryGetValue<List<WeatherHistory>>(WeatherHistoryParameterName, out var weatherData);
                if (weatherData != null)
                {
                    MyWeatherData = weatherData;
                }
            }

            base.OnNavigatedTo(parameters);
        }
    }
}