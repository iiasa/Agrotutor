using System.ComponentModel.DataAnnotations.Schema;

namespace Agrotutor.Core.Entities
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Cost
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("id_tipo_bitacora", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseCostStringConverter))]
        public long? IdTipoBitacora { get; set; }

        [JsonProperty("id_siembra_cultivo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseCostStringConverter))]
        public long? IdSiembraCultivo { get; set; }

        [JsonProperty("cultivo_sembrado", NullValueHandling = NullValueHandling.Ignore)]
        public string CultivoSembrado { get; set; }

        [JsonProperty("id_bitacora", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseCostStringConverter))]
        public long? IdBitacora { get; set; }

        [JsonProperty("anio", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseCostStringConverter))]
        public long? Anio { get; set; }

        [JsonProperty("nb_ciclo", NullValueHandling = NullValueHandling.Ignore)]
        public string NbCiclo { get; set; }

        [JsonProperty("nb_terreno", NullValueHandling = NullValueHandling.Ignore)]
        public string NbTerreno { get; set; }

        [JsonProperty("nb_tipo_parcela", NullValueHandling = NullValueHandling.Ignore)]
        public string NbTipoParcela { get; set; }

        [JsonProperty("nb_productor", NullValueHandling = NullValueHandling.Ignore)]
        public string NbProductor { get; set; }

        [JsonProperty("tipo_permiso", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoPermiso { get; set; }

        [JsonProperty("nb_status_ciclo_agronomico", NullValueHandling = NullValueHandling.Ignore)]
        public string NbStatusCicloAgronomico { get; set; }

        [JsonProperty("nb_tipo_ciclo_agronomico", NullValueHandling = NullValueHandling.Ignore)]
        public string NbTipoCicloAgronomico { get; set; }

        [JsonProperty("nb_institucion", NullValueHandling = NullValueHandling.Ignore)]
        public string NbInstitucion { get; set; }

        [JsonProperty("nb_tecnico", NullValueHandling = NullValueHandling.Ignore)]
        public string NbTecnico { get; set; }

        [JsonProperty("correo_electronico_tecnico", NullValueHandling = NullValueHandling.Ignore)]
        public string CorreoElectronicoTecnico { get; set; }

        [JsonProperty("nb_formador", NullValueHandling = NullValueHandling.Ignore)]
        public string NbFormador { get; set; }

        [JsonProperty("correo_electronico_formador", NullValueHandling = NullValueHandling.Ignore)]
        public string CorreoElectronicoFormador { get; set; }

        [JsonProperty("nb_coordinador", NullValueHandling = NullValueHandling.Ignore)]
        public string NbCoordinador { get; set; }

        [JsonProperty("correo_electronico_coordinador", NullValueHandling = NullValueHandling.Ignore)]
        public string CorreoElectronicoCoordinador { get; set; }

        [JsonProperty("nbGerente", NullValueHandling = NullValueHandling.Ignore)]
        public string NbGerente { get; set; }

        [JsonProperty("correo_electronico_gerente", NullValueHandling = NullValueHandling.Ignore)]
        public string CorreoElectronicoGerente { get; set; }

        [JsonProperty("id_hub", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseCostStringConverter))]
        public long? IdHub { get; set; }

        [JsonProperty("nb_Hub", NullValueHandling = NullValueHandling.Ignore)]
        public string NbHub { get; set; }

        [JsonProperty("id_estado", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseCostStringConverter))]
        public long? IdEstado { get; set; }

        [JsonProperty("nb_estado", NullValueHandling = NullValueHandling.Ignore)]
        public string NbEstado { get; set; }

        [JsonProperty("id_municipio", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseCostStringConverter))]
        public long? IdMunicipio { get; set; }

        [JsonProperty("nb_municipio", NullValueHandling = NullValueHandling.Ignore)]
        public string NbMunicipio { get; set; }

        [JsonProperty("id_localidad", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseCostStringConverter))]
        public long? IdLocalidad { get; set; }

        [JsonProperty("nb_localidad", NullValueHandling = NullValueHandling.Ignore)]
        public string NbLocalidad { get; set; }

        [JsonProperty("bi_preparacion_suelo_herbicida_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string BiPreparacionSueloHerbicidaHa { get; set; }

        [JsonProperty("bii_preparacion_mecanica_suelo_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string BiiPreparacionMecanicaSueloHa { get; set; }

        [JsonProperty("c_siembra_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string CSiembraHa { get; set; }

        [JsonProperty("ci_resiembra_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string CiResiembraHa { get; set; }

        [JsonProperty("d_analisis_suelo_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string DAnalisisSueloHa { get; set; }

        [JsonProperty("fi_fertilizacion_quimica_suelo_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string FiFertilizacionQuimicaSueloHa { get; set; }

        [JsonProperty("fii_fertilizacion_quimica_foliar_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string FiiFertilizacionQuimicaFoliarHa { get; set; }

        [JsonProperty("fiii_fertilizacion_organica_abono_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string FiiiFertilizacionOrganicaAbonoHa { get; set; }

        [JsonProperty("g_uso_biofertilizante_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string GUsoBiofertilizanteHa { get; set; }

        [JsonProperty("h_uso_mejoradores_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string HUsoMejoradoresHa { get; set; }

        [JsonProperty("i_riego_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string IRiegoHa { get; set; }

        [JsonProperty("ji_control_quimico_maleza_siembra_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string JiControlQuimicoMalezaSiembraHa { get; set; }

        [JsonProperty("jii_labor_cultural_control_fisico_malezas_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string JiiLaborCulturalControlFisicoMalezasHa { get; set; }

        [JsonProperty("k_insecticida", NullValueHandling = NullValueHandling.Ignore)]
        public string KInsecticida { get; set; }

        [JsonProperty("l_fungicida_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string LFungicidaHa { get; set; }

        [JsonProperty("mi_cosecha_manual_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string MiCosechaManualHa { get; set; }

        [JsonProperty("mii_cosecha_mecanica_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string MiiCosechaMecanicaHa { get; set; }

        [JsonProperty("o_comercializacion_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string OComercializacionHa { get; set; }

        [JsonProperty("r_gastos_indirectos_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string RGastosIndirectosHa { get; set; }

        [JsonProperty("costo_produccion_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductionCost { get; set; }

        [JsonProperty("tipo_produccion", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoProduccion { get; set; }
        public static List<Cost> FromJson(string json) => JsonConvert.DeserializeObject<List<Cost>>(json, Agrotutor.Core.Entities.CostConverter.Settings);
    }

    public static class SerializeCost
    {
        public static string ToJson(this List<Cost> self) => JsonConvert.SerializeObject(self, Agrotutor.Core.Entities.CostConverter.Settings);
    }

    internal static class CostConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseCostStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (long.TryParse(value, out long l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseCostStringConverter Singleton = new ParseCostStringConverter();
    }
}