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
                    events.AddRange(plot.GetCalendarEvents());
                }
            }

            return events;
        }

        public IEnumerable<CalendarEvent> GetCalendarEvents()
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            Color colorPerPlot = Color.FromArgb(randomColor.Next(256), randomColor.Next(256), randomColor.Next(256));
            if (Activities != null)
            {
                foreach (Activity activity in Activities)
                {
                    events.Add(
                        new CalendarEvent
                        {
                            Data = activity,
                            AllDayEvent = true,
                            StartTime = activity.Date,
                            EndTime = activity.Date,
                            Title = activity.Name,
                            Color = colorPerPlot,
                            Plot = this
                        });
                }
            }

            return events;
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

        internal object GetBaseTemperature()
        {
            throw new NotImplementedException();
        }
    }
}
