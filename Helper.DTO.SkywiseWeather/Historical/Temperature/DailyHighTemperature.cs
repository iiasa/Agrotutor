namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    using System.Collections.Generic;
    using System.Linq;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("DailyHighTemperature")]
    public class DailyHighTemperature : HistoricalSeries
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }
        public Value[] series { get; set; }

        public List<Value> Series => series.ToList();

        [ForeignKey(typeof(WeatherData))]
        public int WeatherDataID { get; set; }
    }
}