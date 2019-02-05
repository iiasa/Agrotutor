﻿namespace Agrotutor.Core.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public class Yield
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [JsonProperty("ciclo_agronomico")]
        public string AgriculturalCycle { get; set; }

        [JsonProperty("id_bitacora")]
        public string BitacoraId { get; set; }

        [JsonProperty("id_de_tipo_de_bitacora_clave_foranea")]
        public string BitacoraTypeForeignKeyId { get; set; }

        [JsonProperty("nombre_cultivo_cosechado")]
        public string CropName { get; set; }

        [JsonProperty("hub")]
        public string Hub { get; set; }

        [JsonProperty("latitud")]
        public string Latitude { get; set; }

        [JsonProperty("longitud")]
        public string Longitude { get; set; }

        [JsonProperty("municipio")]
        public string Municipality { get; set; }

        [JsonProperty("rendimiento_ha")]
        public string Performance { get; set; }

        [JsonProperty("id_parcela")]
        public string PlotId { get; set; }

        [JsonProperty("tipo_de_parcela_testigo_o_innovacion")]
        public string PlotOrInnovationType { get; set; }

        [JsonProperty("tipo_produccion")]
        public string ProductionType { get; set; }

        [JsonProperty("nombre_producto_de_interes_economico_obtenido")]
        public string ProductOfEconomicInterestName { get; set; }

        [JsonProperty("estado")]
        public string State { get; set; }

        [JsonProperty("unidad_medida_rendimiento_producto_interes_economico_obtenido")]
        public string UnitOfPerformance { get; set; }

        [JsonProperty("anio")]
        public string Year { get; set; }
    }
}