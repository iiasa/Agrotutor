using Newtonsoft.Json;

namespace Helper.DTO.SkywiseWeather.Forecast
{
    using System.Collections.Generic;

    public class ForecastData
    {
        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        public List<ForecastLocation> ForecastLocation { get; set; }
    }
}