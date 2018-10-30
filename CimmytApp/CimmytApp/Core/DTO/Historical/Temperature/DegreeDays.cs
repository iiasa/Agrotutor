namespace Helper.DTO.SkywiseWeather.Historical.Temperature
{
    public abstract class DegreeDays : DailySeries
    {
        public float baseTemperature { get; set; }

        public float degreeDays { get; set; }
    }
}