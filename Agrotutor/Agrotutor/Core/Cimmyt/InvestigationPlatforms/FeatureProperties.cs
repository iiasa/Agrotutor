namespace Agrotutor.Core.Cimmyt.InvestigationPlatforms
{
    using Newtonsoft.Json;

    public class FeatureProperties
    {
        [JsonProperty("ABRVIACION")]
        public string Abrviacion { get; set; }

        [JsonProperty("ANIO_INST")]
        public long AnioInst { get; set; }

        [JsonProperty("ASNM")]
        public double Asnm { get; set; }

        [JsonProperty("CAMPUS")]
        public string Campus { get; set; }

        [JsonProperty("CICLO_AGRI")]
        public CicloAgri CicloAgri { get; set; }

        [JsonProperty("CUL_PRINC")]
        public string CulPrinc { get; set; }

        [JsonProperty("DIRECCION")]
        public string Direccion { get; set; }

        [JsonProperty("EMAIL_IR")]
        public string EmailIr { get; set; }

        [JsonProperty("EMAIL_TR")]
        public string EmailTr { get; set; }

        [JsonProperty("ESTADO")]
        public string Estado { get; set; }

        [JsonProperty("HUB")]
        public string Hub { get; set; }

        [JsonProperty("INST_COLAB")]
        public string InstColab { get; set; }

        [JsonProperty("INV_RESP")]
        public string InvResp { get; set; }

        [JsonProperty("LATITUD")]
        public double Latitud { get; set; }

        [JsonProperty("LOCALIDAD")]
        public string Localidad { get; set; }

        [JsonProperty("LONGITUD")]
        public double Longitud { get; set; }

        [JsonProperty("MUNICIPIO")]
        public string Municipio { get; set; }

        [JsonProperty("NOM_PLAT")]
        public string NomPlat { get; set; }

        [JsonProperty("NUM_ID")]
        public long NumId { get; set; }

        [JsonProperty("REG_HUM_OI")]
        public RegHum RegHumOi { get; set; }

        [JsonProperty("REG_HUM_PV")]
        public RegHum RegHumPv { get; set; }

        [JsonProperty("TEC_RESP")]
        public string TecResp { get; set; }

        [JsonProperty("TEL_IR")]
        public string TelIr { get; set; }

        [JsonProperty("TEL_TR")]
        public string TelTr { get; set; }
    }
}