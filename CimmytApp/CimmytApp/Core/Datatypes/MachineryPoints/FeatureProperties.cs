namespace CimmytApp.Core.Datatypes.MachineryPoints
{
    using Newtonsoft.Json;

    public class FeatureProperties
    {
        [JsonProperty("ANIO")]
        public long Anio { get; set; }

        [JsonProperty("HUB")]
        public string Hub { get; set; }

        [JsonProperty("LOCALIDAD")]
        public string Localidad { get; set; }

        [JsonProperty("RESPONSABL")]
        public string Responsabl { get; set; }

        [JsonProperty("ENTIDAD")]
        public string Entidad { get; set; }

        [JsonProperty("GEOPOSICIO")]
        public string Geoposicio { get; set; }

        [JsonProperty("LATITUD")]
        public double Latitud { get; set; }

        [JsonProperty("LONGITUD")]
        public double Longitud { get; set; }

        [JsonProperty("DIRECCION")]
        public string Direccion { get; set; }

        [JsonProperty("PRECOMODAT")]
        public Precomodat? Precomodat { get; set; }

        [JsonProperty("REGISTRO")]
        public Precomodat? Registro { get; set; }
    }
}