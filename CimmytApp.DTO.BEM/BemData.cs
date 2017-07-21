using System;
using System.IO;

namespace CimmytApp.DTO.BEM
{
    using System.Collections.Generic;
    using System.Reflection;
    using Newtonsoft.Json;

    public class BemData
    {
        public List<Costo> Costo { get; set; }
        public List<Ingreso> Ingreso { get; set; }
        public List<Rendimiento> Rendimiento { get; set; }
        public List<Utilidad> Utilidad { get; set; }

        public BemData()
        {
            var assembly = typeof(BemData).GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();

            var serializer = new JsonSerializer();
            foreach (var resource in resources)
            {
                Stream stream;
                StreamReader streamReader;
                if (resource.EndsWith("costo.json"))
                {
                    stream = assembly.GetManifestResourceStream(resource);
                    streamReader = new StreamReader(stream);
                    Costo = (List<Costo>)serializer.Deserialize(streamReader, typeof(List<Costo>));
                }
                else if (resource.EndsWith("ingreso.json"))
                {
                    stream = assembly.GetManifestResourceStream(resource);
                    streamReader = new StreamReader(stream);
                    Ingreso = (List<Ingreso>)serializer.Deserialize(streamReader, typeof(List<Ingreso>));
                }
                else if (resource.EndsWith("rendimiento.json"))
                {
                    stream = assembly.GetManifestResourceStream(resource);
                    streamReader = new StreamReader(stream);
                    Rendimiento = (List<Rendimiento>)serializer.Deserialize(streamReader, typeof(List<Rendimiento>));
                }
                else if (resource.EndsWith("utilidad.json"))
                {
                    stream = assembly.GetManifestResourceStream(resource);
                    streamReader = new StreamReader(stream);
                    Utilidad = (List<Utilidad>)serializer.Deserialize(streamReader, typeof(List<Utilidad>));
                }
            }
            //JsonConvert.DeserializeObject<List<Costo>>()
            int i = 0;
            i++;
        }
    }
}