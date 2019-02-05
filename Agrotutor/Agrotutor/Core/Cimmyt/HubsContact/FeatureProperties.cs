namespace Agrotutor.Core.Cimmyt.HubsContact
{
    using Newtonsoft.Json;

    public class FeatureProperties
    {
        [JsonProperty("HUB")]
        public string Hub { get; set; }

        [JsonProperty("Gerente")]
        public string Gerente { get; set; }

        [JsonProperty("Email_Gte")]
        public string EmailGte { get; set; }

        [JsonProperty("Asistente")]
        public string Asistente { get; set; }

        [JsonProperty("Email_Asis")]
        public string EmailAsis { get; set; }

        [JsonProperty("Telefono")]
        public string Telefono { get; set; }

        [JsonProperty("Latitud")]
        public double Latitud { get; set; }

        [JsonProperty("Longitud")]
        public double Longitud { get; set; }
    }
}