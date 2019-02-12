using System.ComponentModel.DataAnnotations.Schema;

namespace Agrotutor.Core.Entities
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public  class Income
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [JsonProperty("id_tipo_bitacora", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseIncomeStringConverter))]
        public long? IdTipoBitacora { get; set; }

        [JsonProperty("id_siembra_cultivo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseIncomeStringConverter))]
        public long? IdSiembraCultivo { get; set; }

        [JsonProperty("cultivo_sembrado", NullValueHandling = NullValueHandling.Ignore)]
        public string CultivoSembrado { get; set; }

        [JsonProperty("id_labitacora", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseIncomeStringConverter))]
        public long? IdLabitacora { get; set; }

        [JsonProperty("anio", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseIncomeStringConverter))]
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

        [JsonProperty("nb_estatus_ciclo_agronomico", NullValueHandling = NullValueHandling.Ignore)]
        public string NbEstatusCicloAgronomico { get; set; }

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

        [JsonProperty("nb_gerente", NullValueHandling = NullValueHandling.Ignore)]
        public string NbGerente { get; set; }

        [JsonProperty("correo_electronico_gerente", NullValueHandling = NullValueHandling.Ignore)]
        public string CorreoElectronicoGerente { get; set; }

        [JsonProperty("id_hub", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseIncomeStringConverter))]
        public long? IdHub { get; set; }

        [JsonProperty("nb_hub", NullValueHandling = NullValueHandling.Ignore)]
        public string NbHub { get; set; }

        [JsonProperty("id_estado", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseIncomeStringConverter))]
        public long? IdEstado { get; set; }

        [JsonProperty("nb_estado", NullValueHandling = NullValueHandling.Ignore)]
        public string NbEstado { get; set; }

        [JsonProperty("id_municipio", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseIncomeStringConverter))]
        public long? IdMunicipio { get; set; }

        [JsonProperty("nb_municipio", NullValueHandling = NullValueHandling.Ignore)]
        public string NbMunicipio { get; set; }

        [JsonProperty("id_localidad", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseIncomeStringConverter))]
        public long? IdLocalidad { get; set; }

        [JsonProperty("nb_localidad", NullValueHandling = NullValueHandling.Ignore)]
        public string NbLocalidad { get; set; }

        [JsonProperty("ingreso_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string IncomePerHa { get; set; }

        [JsonProperty("tipo_produccion", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoProduccion { get; set; }
        public static List<Income> FromJson(string json) => JsonConvert.DeserializeObject<List<Income>>(json, Agrotutor.Core.Entities.IncomeConverter.Settings);
    }

    public static class SerializeIncome
    {
        public static string ToJson(this List<Income> self) => JsonConvert.SerializeObject(self, Agrotutor.Core.Entities.IncomeConverter.Settings);
    }

    internal static class IncomeConverter
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

    internal class ParseIncomeStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
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

        public static readonly ParseIncomeStringConverter Singleton = new ParseIncomeStringConverter();
    }
}
