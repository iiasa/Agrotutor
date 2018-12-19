namespace CimmytApp.Core.Persistence.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using CimmytApp.Core.Calendar.Types;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.DTO.Parcel;
    using Helper.GeoWiki.API.GenericDatasetStorage;

    public class Plot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public CropType CropType { get; set; }

        public ClimateType ClimateType { get; set; }

        public MaturityType MaturityType { get; set; }

        public virtual List<Activity> Activities { get; set; }

        public virtual Position Position { get; set; }

        public virtual List<Position> Delineation { get; set; }

        public virtual BemData BemData { get; set; }

        //TODO: add pictures
        //Todo: add videos
        public void Submit() // TODO: remove this from here
        {
            Storage.StoreDatasetAsync(this, -1, 16, 1, 1);
        }

        public IEnumerable<CalendarEvent> GetCalendarEvents()
        {
            var events = new List<CalendarEvent>();
            foreach (Activity activity in Activities)
            {
                events.Add(new CalendarEvent
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

        public static IEnumerable<CalendarEvent> GetCalendarEvents(IEnumerable<Plot> plots)
        {
            var events = new List<CalendarEvent>();
            foreach (Plot plot in plots)
            {
                events.AddRange(plot.GetCalendarEvents());
            }

            return events;
        }
    }
}
