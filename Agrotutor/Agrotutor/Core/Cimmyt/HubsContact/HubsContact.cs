﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Agrotutor.Core.Cimmyt.HubsContact;
//
//    var hubsContact = HubsContact.FromJson(jsonString);

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Prism;
using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.Core.Cimmyt.HubsContact
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class HubsContact
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("crs", NullValueHandling = NullValueHandling.Ignore)]
        public Crs Crs { get; set; }

        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public List<Feature> Features { get; set; }
        
        public static async Task<HubsContact> FromEmbeddedResource()
        {
            var stringLocalizer = (IStringLocalizer<HubsContact>)PrismApplicationBase.Current.Container.Resolve(typeof(IStringLocalizer<HubsContact>));
            using (await MaterialDialog.Instance.LoadingSnackbarAsync(message: stringLocalizer.GetString("loading")))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = Constants.HubsContactFile;

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

    public partial class Crs
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public CrsProperties Properties { get; set; }
    }

    public partial class CrsProperties
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public partial class Feature
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public FeatureProperties Properties { get; set; }

        [JsonProperty("geometry", NullValueHandling = NullValueHandling.Ignore)]
        public Geometry Geometry { get; set; }
    }

    public partial class Geometry
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

    public partial class FeatureProperties
    {
        [JsonProperty("HUB", NullValueHandling = NullValueHandling.Ignore)]
        public string Hub { get; set; }

        [JsonProperty("Gerente", NullValueHandling = NullValueHandling.Ignore)]
        public string Gerente { get; set; }

        [JsonProperty("Email_Gte")]
        public string EmailGte { get; set; }

        [JsonProperty("Asistente", NullValueHandling = NullValueHandling.Ignore)]
        public string Asistente { get; set; }

        [JsonProperty("Email_Asis")]
        public string EmailAsis { get; set; }

        [JsonProperty("Telefono", NullValueHandling = NullValueHandling.Ignore)]
        public string Telefono { get; set; }

        [JsonProperty("Latitud", NullValueHandling = NullValueHandling.Ignore)]
        public double? Latitud { get; set; }

        [JsonProperty("Longitud", NullValueHandling = NullValueHandling.Ignore)]
        public double? Longitud { get; set; }
    }

    public partial class HubsContact
    {
        public static HubsContact FromJson(string json) => JsonConvert.DeserializeObject<HubsContact>(json, Agrotutor.Core.Cimmyt.HubsContact.HubsContactConverter.Settings);
    }

    public static class SerializeHubsContact
    {
        public static string ToJson(this HubsContact self) => JsonConvert.SerializeObject(self, Agrotutor.Core.Cimmyt.HubsContact.HubsContactConverter.Settings);
    }

    internal static class HubsContactConverter
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