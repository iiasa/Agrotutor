namespace Helper.DTO.SkywiseWeather.Historical
{
    using System;
    using SQLite.Net.Attributes;

    public class Value
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string validDate { get; set; }
        public DateTime validTime { get; set; }
        public float value { get; set; }
    }
}