namespace CimmytApp.Core.Persistence.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Threading.Tasks;

    using CimmytApp.DTO.BEM;

    using Helper.HTTP;

    public class BemData
    {
        public virtual List<Cost> Costo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public virtual List<Income> Ingreso { get; set; }

        public virtual List<Yield> Rendimiento { get; set; }

        public virtual List<Profit> Utilidad { get; set; }

        public static async Task<BemData> LoadBEMData(double? lat = null, double? lon = null)
        {
            List<Cost> costo = await Load<Cost>("costo", lat, lon);
            List<Income> ingreso = await Load<Income>("ingreso", lat, lon);
            List<Yield> rendimiento = await Load<Yield>("rendimiento", lat, lon);
            List<Profit> utilidads = await Load<Profit>("utilidads", lat, lon);

            BemData bemData = new BemData
                              {
                                  Costo = costo,
                                  Ingreso = ingreso,
                                  Rendimiento = rendimiento,
                                  Utilidad = utilidads
                              };
            return bemData;
        }

        private static async Task<List<T>> Load<T>(string parameter, double? lat = null, double? lon = null)
        {
            string url = $"http://104.239.158.49/api.php?type={parameter}&tkn=E31C5F8478566357BA6875B32DC59";
            if (lat != null)
            {
                url += $"&lat={lat}";
            }

            if (lon != null)
            {
                url += $"&lon={lon}";
            }

            List<T> data = null;
            try
            {
                data = await RequestJson.Get<List<T>>(url);
            }
            catch (Exception)
            {
            }

            return data;
        }
    }
}