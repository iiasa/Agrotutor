namespace Agrotutor.Core.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Agrotutor.Modules.Benchmarking.Types;
    using Agrotutor.Modules.PriceForecasting.Types;
    using Agrotutor.Modules.Weather.Types;
    using Modules.Calendar.Types;
    using Modules.GeoWiki.GenericDatasetStorage;
    using Rest.Bem;

    public class Plot
    {
        public virtual List<Activity> Activities { get; set; }

        public virtual BemData BemData { get; set; }

        public ClimateType ClimateType { get; set; }

        public CropType CropType { get; set; }

        public virtual List<Position> Delineation { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public MaturityType MaturityType { get; set; }

        public string Name { get; set; }

        public virtual Position Position { get; set; }

        public virtual WeatherForecast WeatherForecast { get; set; }

        public virtual WeatherHistory WeatherHistory { get; set; }

        public virtual BenchmarkingInformation BenchmarkingInformation { get; set; }

        public virtual PriceForecast PriceForecast { get; set; }

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
            foreach (Activity activity in Activities)
            {
                events.Add(
                    new CalendarEvent
                    {
                        Data = activity,
                        AllDayEvent = true,
                        StartTime = activity.Date,
                        EndTime = activity.Date,
                        Title = activity.Name
                    });
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
    }
}
