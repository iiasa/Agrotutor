﻿



// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Agrotutor.Core.Cimmyt.MachineryPoints;
//
//    var machineryPoints = MachineryPoints.FromJson(jsonString);

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prism;
using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.Core.Cimmyt.MachineryPoints
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class MachineryPoints
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("crs", NullValueHandling = NullValueHandling.Ignore)]
        public Crs Crs { get; set; }

        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public List<Feature> Features { get; set; }
        
        public static async Task<MachineryPoints> FromEmbeddedResource()
        {
            var stringLocalizer = (IStringLocalizer<MachineryPoints>)PrismApplicationBase.Current.Container.Resolve(typeof(IStringLocalizer<MachineryPoints>));
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(message: stringLocalizer.GetString("loading")))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = Constants.MachineryPointsFile;

                string result;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = await reader.ReadToEndAsync();
                }

                return FromJson(result);
            }
        }
    }

    public class Crs
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public CrsProperties Properties { get; set; }
    }

    public class CrsProperties
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public class Feature
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public FeatureProperties Properties { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("coordinates", NullValueHandling = NullValueHandling.Ignore)]
        public List<double> Coordinates { get; set; }
        
        public Location ToLocation()
        {
            return new Location(Coordinates[1], Coordinates[0]);
        }
    }

    public class FeatureProperties
    {
        [JsonProperty("AŅO", NullValueHandling = NullValueHandling.Ignore)]
        public long? Aņo { get; set; }

        [JsonProperty("HUB", NullValueHandling = NullValueHandling.Ignore)]
        public string Hub { get; set; }

        [JsonProperty("LOCALIDAD", NullValueHandling = NullValueHandling.Ignore)]
        public string Localidad { get; set; }

        [JsonProperty("RESPONSABLE", NullValueHandling = NullValueHandling.Ignore)]
        public string Responsable { get; set; }

        [JsonProperty("ENTIDAD", NullValueHandling = NullValueHandling.Ignore)]
        public string Entidad { get; set; }

        [JsonProperty("GEOPOSICION")]
        public string Geoposicion { get; set; }

        [JsonProperty("LATITUD")]
        public double? Latitud { get; set; }

        [JsonProperty("LONGITUD")]
        public double? Longitud { get; set; }

        [JsonProperty("DIRECCION", NullValueHandling = NullValueHandling.Ignore)]
        public string Direccion { get; set; }

        [JsonProperty("PRECOMODAT", NullValueHandling = NullValueHandling.Ignore)]
        public string Precomodat { get; set; }

        [JsonProperty("REGISTRO", NullValueHandling = NullValueHandling.Ignore)]
        public string Registro { get; set; }
    }

    public partial class MachineryPoints
    {
        public static MachineryPoints FromJson(string json) => JsonConvert.DeserializeObject<MachineryPoints>(json, Agrotutor.Core.Cimmyt.MachineryPoints.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MachineryPoints self) => JsonConvert.SerializeObject(self, Agrotutor.Core.Cimmyt.MachineryPoints.Converter.Settings);
    }

    internal static class Converter
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
}
