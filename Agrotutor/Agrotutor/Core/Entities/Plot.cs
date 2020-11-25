using System;
using System.Drawing;
using Agrotutor.Modules.Benchmarking;
using Agrotutor.Modules.Ciat.Types;

namespace Agrotutor.Core.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Agrotutor.Modules.Benchmarking.Types;
    using Agrotutor.Modules.PriceForecasting.Types;
    using Agrotutor.Modules.Weather;
    using Modules.Calendar.Types;
    using Modules.GeoWiki.GenericDatasetStorage;

    public class Plot
    {
        private Random randomColor;
        public Plot()
        {
            randomColor=new Random();
            Activities=new List<Activity>();
            Delineation=new List<DelineationPosition>();
            MediaItems=new List<MediaItem>();
            PriceForecast=new List<PriceForecast>();
            Irrigated = true;
        }
     
        public List<Activity> Activities { get; set; }

        public BemData BemData { get; set; }

        public ClimateType ClimateType { get; set; }

        public CropType CropType { get; set; }

        public List<DelineationPosition> Delineation { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public MaturityType MaturityType { get; set; }

        public string Name { get; set; }

        public bool Irrigated { get; set; }

        public int ArgbPlotColor { get; set; }

        public string DeviceId { get; set; }

        [NotMapped] 
        public List<WeatherForecast> CurrentWeather { get; set; }

        [NotMapped]
        public Color? PlotColor { get; set; }
        public Position Position { get; set; }

        public List<WeatherForecast> WeatherForecast { get; set; }

        public  List<WeatherHistory> WeatherHistory { get; set; }

        public  BenchmarkingInformation BenchmarkingInformation { get; set; }

        public  IEnumerable<PriceForecast> PriceForecast { get; set; }

        public  CiatData CiatData { get; set; }

        public  List<MediaItem> MediaItems { get; set; }

        [NotMapped]
        public bool IsTemporaryPlot { get; set; }

        public static IEnumerable<CalendarEvent> GetCalendarEvents(IEnumerable<Plot> plots)
        { 
            List<CalendarEvent> events = new List<CalendarEvent>();
            if (plots != null)
            {
                foreach (Plot plot in plots)
                {
                    if (plot == null) continue;
                    events.AddRange(plot.GetCalendarEvents());
                }
            }

            return events;
        }

        public IEnumerable<CalendarEvent> GetCalendarEvents()
        {
            List<CalendarEvent> events = new List<CalendarEvent>();

            if (Activities != null)
            {
                foreach (Activity activity in Activities)
                {
                    if (activity == null) continue;
                    events.Add(
                        new CalendarEvent
                        {
                            Data = activity,
                            AllDayEvent = true,
                            StartTime = activity.Date,
                            EndTime = activity.Date,
                            Title = activity.Name,
                            PlotColor = System.Drawing.Color.FromArgb(ArgbPlotColor),
                            Plot = this
                        });
                }
            }


            if (GetTargetCumulativeGdd() != null) {
                events.AddRange(GetWindowsOfOpportunity());
            }

            return events;
        }

        private IEnumerable<CalendarEvent> GetWindowsOfOpportunity()
        {
            var target = GetTargetCumulativeGdd();
            int fertilizationDistance = (int)Math.Round((decimal)target / 3);
            int[] thresholds = { 0, fertilizationDistance, fertilizationDistance * 2 };
            int i = 0;
            double sum = 0.0;
            var dates = new List<DateTime>();
            var events = new List<CalendarEvent>();

            if (WeatherHistory != null)
            {
                foreach (var historyItem in WeatherHistory)
                {
                    sum += historyItem.CalculateGdd(GetBaseTemperature());
                    if (sum > thresholds[i])
                    {
                        i++;
                        dates.Add(historyItem.Date);
                        if (i > 2) break;
                    }
                }
            }

            if (i <= 2 && WeatherForecast!=null)
            {

                foreach (var forecastItem in WeatherForecast)
                {
                    sum += forecastItem.CalculateGdd(GetBaseTemperature());
                    if (sum > thresholds[i])
                    {
                        i++;
                        dates.Add(forecastItem.DateTime);
                        if (i > 2) break;
                    }
                }
            }

            foreach (var date in dates)
            {
                events.Add(new CalendarEvent { StartTime = date });
                events.Add(new CalendarEvent { StartTime = date.AddDays(1) });
                events.Add(new CalendarEvent { StartTime = date.AddDays(2) });
            }

            return events;
        }

        public int? GetTargetCumulativeGdd()
        {
            int? targetGdd = null;
            if (CropType == CropType.Corn)
            {
                switch (MaturityType)
                {
                    case MaturityType.Early:
                        targetGdd = 1680;
                        break;
                    case MaturityType.SemiEarly:
                        targetGdd = 1890;
                        break;
                    case MaturityType.Intermediate:
                        targetGdd = 2100;
                        break;
                    case MaturityType.SemiLate:
                        targetGdd = 2310;
                        break;
                    case MaturityType.Late:
                        targetGdd = 2520;
                        break;
                }
            }
            return targetGdd;
        }

        public int? GetBaseTemperature()
        {
            int? baseTemperature = null;
            if (CropType == CropType.Corn)
            {
                switch (ClimateType)
                {
                    case ClimateType.Cold:
                        baseTemperature = 4;
                        break;
                    case ClimateType.TemperedSubtropical:
                        baseTemperature = 7;
                        break;
                    case ClimateType.Tropical:
                        baseTemperature = 9;
                        break;
                    case ClimateType.Hybrid:
                        baseTemperature = 10;
                        break;
                }
            }
            return baseTemperature;
        }

        public async void LoadBEMData()
        {
            BemData = await BemDataDownloadHelper.LoadBEMData(Position.Latitude, Position.Longitude);
        }

        // TODO: add pictures
        // Todo: add videos
        public async void Submit()
        {
            // TODO: remove this from here
            await GenericDatasetStorage.StoreDatasetAsync(this, -1, 16, 1, 1);
        }
    }
}
