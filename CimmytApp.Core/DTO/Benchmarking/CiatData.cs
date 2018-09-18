// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using CimmytApp.Core;
//
//    var ciatData = CiatData.FromJson(jsonString);
namespace CimmytApp.Core.DTO.Benchmarking
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CiatData
    {
        [JsonProperty("nombre_grupo")]
        public NombreGrupo NombreGrupo { get; set; }

        [JsonProperty("cultivo")]
        public Cultivo Cultivo { get; set; }

        [JsonProperty("producto")]
        public Producto Producto { get; set; }

        [JsonProperty("sistema_cultivo")]
        public SistemaCultivo SistemaCultivo { get; set; }

        [JsonProperty("variable")]
        public string Variable { get; set; }

        [JsonProperty("tipo")]
        public Tipo Tipo { get; set; }

        [JsonProperty("unidad")]
        public string Unidad { get; set; }

        [JsonProperty("rango_opt_min")]
        public string RangoOptMin { get; set; }

        [JsonProperty("rango_opt_max")]
        public string RangoOptMax { get; set; }

        [JsonProperty("valor_optimo")]
        public string ValorOptimo { get; set; }

        [JsonProperty("valor_suboptimo")]
        public string ValorSuboptimo { get; set; }
    }

    public enum Cultivo { Maiz };

    public enum NombreGrupo { Guanajuato };

    public enum Producto { Grano };

    public enum SistemaCultivo { Riego, Temporal };

    public enum Tipo { Categorica, Cuantitativa, TipoCuantitativa };

    public partial class CiatData
    {
        public static List<CiatData> FromJson(string json) => JsonConvert.DeserializeObject<List<CiatData>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<CiatData> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                CultivoConverter.Singleton,
                NombreGrupoConverter.Singleton,
                ProductoConverter.Singleton,
                SistemaCultivoConverter.Singleton,
                TipoConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CultivoConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Cultivo) || t == typeof(Cultivo?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Maiz")
            {
                return Cultivo.Maiz;
            }
            throw new Exception("Cannot unmarshal type Cultivo");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Cultivo)untypedValue;
            if (value == Cultivo.Maiz)
            {
                serializer.Serialize(writer, "Maiz");
                return;
            }
            throw new Exception("Cannot marshal type Cultivo");
        }

        public static readonly CultivoConverter Singleton = new CultivoConverter();
    }

    internal class NombreGrupoConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(NombreGrupo) || t == typeof(NombreGrupo?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Guanajuato")
            {
                return NombreGrupo.Guanajuato;
            }
            throw new Exception("Cannot unmarshal type NombreGrupo");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (NombreGrupo)untypedValue;
            if (value == NombreGrupo.Guanajuato)
            {
                serializer.Serialize(writer, "Guanajuato");
                return;
            }
            throw new Exception("Cannot marshal type NombreGrupo");
        }

        public static readonly NombreGrupoConverter Singleton = new NombreGrupoConverter();
    }

    internal class ProductoConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Producto) || t == typeof(Producto?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Grano")
            {
                return Producto.Grano;
            }
            throw new Exception("Cannot unmarshal type Producto");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Producto)untypedValue;
            if (value == Producto.Grano)
            {
                serializer.Serialize(writer, "Grano");
                return;
            }
            throw new Exception("Cannot marshal type Producto");
        }

        public static readonly ProductoConverter Singleton = new ProductoConverter();
    }

    internal class SistemaCultivoConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SistemaCultivo) || t == typeof(SistemaCultivo?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Riego":
                    return SistemaCultivo.Riego;
                case "Temporal":
                    return SistemaCultivo.Temporal;
            }
            throw new Exception("Cannot unmarshal type SistemaCultivo");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SistemaCultivo)untypedValue;
            switch (value)
            {
                case SistemaCultivo.Riego:
                    serializer.Serialize(writer, "Riego");
                    return;
                case SistemaCultivo.Temporal:
                    serializer.Serialize(writer, "Temporal");
                    return;
            }
            throw new Exception("Cannot marshal type SistemaCultivo");
        }

        public static readonly SistemaCultivoConverter Singleton = new SistemaCultivoConverter();
    }

    internal class TipoConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Tipo) || t == typeof(Tipo?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Categorica":
                    return Tipo.Categorica;
                case "Cuantitativa":
                    return Tipo.Cuantitativa;
                case "cuantitativa":
                    return Tipo.TipoCuantitativa;
            }
            throw new Exception("Cannot unmarshal type Tipo");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Tipo)untypedValue;
            switch (value)
            {
                case Tipo.Categorica:
                    serializer.Serialize(writer, "Categorica");
                    return;
                case Tipo.Cuantitativa:
                    serializer.Serialize(writer, "Cuantitativa");
                    return;
                case Tipo.TipoCuantitativa:
                    serializer.Serialize(writer, "cuantitativa");
                    return;
            }
            throw new Exception("Cannot marshal type Tipo");
        }

        public static readonly TipoConverter Singleton = new TipoConverter();
    }
}
