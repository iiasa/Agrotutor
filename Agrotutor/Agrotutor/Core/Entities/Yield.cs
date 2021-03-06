﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Agrotutor.Core.Entities;
//
//    var Yield = Yield.FromJson(jsonString);

using System.ComponentModel.DataAnnotations.Schema;

namespace Agrotutor.Core.Entities
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Yield
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [JsonProperty("id_bitacora", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseYieldStringConverter))]
        public long? IdBitacora { get; set; }

        [JsonProperty("id_de_tipo_de_bitacora_clave_foranea", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseYieldStringConverter))]
        public long? IdDeTipoDeBitacoraClaveForanea { get; set; }

        [JsonProperty("tipo_de_parcela_testigo_o_innovacion", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoDeParcelaTestigoOInnovacion { get; set; }

        [JsonProperty("nombre_cultivo_cosechado", NullValueHandling = NullValueHandling.Ignore)]
        public string NombreCultivoCosechado { get; set; }

        [JsonProperty("nombre_producto_de_interes_economico_obtenido", NullValueHandling = NullValueHandling.Ignore)]
        public string NombreProductoDeInteresEconomicoObtenido { get; set; }

        [JsonProperty("unidad_medida_rendimiento_producto_interes_economico_obtenido", NullValueHandling = NullValueHandling.Ignore)]
        public string UnidadMedidaRendimientoProductoInteresEconomicoObtenido { get; set; }

        [JsonProperty("rendimiento_ha", NullValueHandling = NullValueHandling.Ignore)]
        public string Performance { get; set; }

        [JsonProperty("tipo_produccion", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoProduccion { get; set; }

        [JsonProperty("id_parcela", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseYieldStringConverter))]
        public long? IdParcela { get; set; }

        [JsonProperty("anio", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseYieldStringConverter))]
        public long? Anio { get; set; }

        [JsonProperty("ciclo_agronomico", NullValueHandling = NullValueHandling.Ignore)]
        public string CicloAgronomico { get; set; }

        [JsonProperty("estado", NullValueHandling = NullValueHandling.Ignore)]
        public string Estado { get; set; }

        [JsonProperty("municipio", NullValueHandling = NullValueHandling.Ignore)]
        public string Municipio { get; set; }

        [JsonProperty("hub", NullValueHandling = NullValueHandling.Ignore)]
        public string Hub { get; set; }

        [JsonProperty("latitud", NullValueHandling = NullValueHandling.Ignore)]
        public string Latitud { get; set; }

        [JsonProperty("longitud", NullValueHandling = NullValueHandling.Ignore)]
        public string Longitud { get; set; }

        public static List<Yield> FromJson(string json) => JsonConvert.DeserializeObject<List<Yield>>(json, Agrotutor.Core.Entities.YieldConverter.Settings);
    }

    public static class SerializeYield
    {
        public static string ToJson(this List<Yield> self) => JsonConvert.SerializeObject(self, Agrotutor.Core.Entities.YieldConverter.Settings);
    }

    internal static class YieldConverter
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

    internal class ParseYieldStringConverter : JsonConverter
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

        public static readonly ParseYieldStringConverter Singleton = new ParseYieldStringConverter();
    }
}
