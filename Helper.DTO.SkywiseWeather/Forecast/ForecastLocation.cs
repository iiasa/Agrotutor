namespace Helper.DTO.SkywiseWeather.Forecast
{
    using System.Collections.Generic;

    public class ForecastLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Timezone { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string Offset { get; set; }
        public string LocalOffsetHours { get; set; }
        public SurfaceObservation CurrentWeatherObservation { get; set; }
        public List<DailySummary> DailySummaries { get; set; }
        public List<HourlySummary> HourlySummaries { get; set; }
    }
}