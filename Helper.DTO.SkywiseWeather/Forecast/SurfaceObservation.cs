namespace Helper.DTO.SkywiseWeather.Forecast
{
    internal class SurfaceObservation
    {
        public string Station { get; set; }
        public double StationLatitude { get; set; }
        public double StationLongitude { get; set; }
        public string ObservationTime { get; set; }
        public string Weekday { get; set; }
        public int Temperature { get; set; }
        public int Dewpoint { get; set; }
        public int RelativeHumidity { get; set; }
        public int ApparentTemperature { get; set; }
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public int AtmosphericPressure { get; set; }
        public string WeatherType { get; set; }
        public string WeatherCode { get; set; }
        public string CloudCover { get; set; }
        public string IconUrl { get; set; }
        public string IconUrlPng { get; set; }
        public string DayOrNight { get; set; }
        public int Visibility { get; set; }
        public string MoonPhase { get; set; }
    }
}