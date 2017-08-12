namespace CimmytApp.DTO.BEM
{
    using Newtonsoft.Json;
    using SQLite.Net.Attributes;

    [Table("BEM-Ingreso")]
    public class Ingreso : BemDataset
    {
        [JsonProperty("id_bitacora")]
        public string BitacoraId { get; set; }

        [JsonProperty("id_tipo_bitacora")]
        public string BitacoraTypeId { get; set; }

        [JsonProperty("anio")]
        public string Year { get; set; }

        [JsonProperty("ciclo")]
        public string AgriculturalCycle { get; set; }

        [JsonProperty("tipo_produccion")]
        public string ProductionType { get; set; }

        [JsonProperty("tipo_parcela")]
        public string PlotType { get; set; }

        [JsonProperty("id_hub")]
        public string HubId { get; set; }

        [JsonProperty("hub")]
        public string Hub { get; set; }

        [JsonProperty("id_estado")]
        public string StateId { get; set; }

        [JsonProperty("estado")]
        public string State { get; set; }

        [JsonProperty("id_municipio")]
        public string MunicipalityId { get; set; }

        [JsonProperty("municipio")]
        public string Municipality { get; set; }

        [JsonProperty("id_localidad")]
        public string LocationId { get; set; }

        [JsonProperty("localidad")]
        public string Location { get; set; }

        [JsonProperty("ingresos_ha")]
        public string Income { get; set; }

        public override string GetCycle()
        {
            return AgriculturalCycle;
        }

        public override string GetDataType()
        {
            return "Ingreso";
        }

        public override string GetValue()
        {
            return Income;
        }

        public override string GetYear()
        {
            return Year;
        }
    }
}