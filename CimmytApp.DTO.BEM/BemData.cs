using System;

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
            //JsonConvert.DeserializeObject<List<Costo>>()
            int i = 0;
            i++;
        }
    }
}