namespace Helper.DTO.SkywiseWeather.Historical
{
    using SQLite.Net.Attributes;

    public class Unit
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Description { get; set; }
        public string Label { get; set; }
    }
}