namespace Agrotutor.Core.Rest.Bem
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Entities;

    public static class BemDataDownloadHelper
    {

        public static async Task<BemData> LoadBEMData(double? lat = null, double? lon = null)
        {
            List<Cost> cost = await Load<Cost>("costo", lat, lon);
            List<Income> income = await Load<Income>("ingreso", lat, lon);
            List<Yield> yield = await Load<Yield>("rendimiento", lat, lon);
            List<Profit> profit = await Load<Profit>("utilidads", lat, lon);

            BemData bemData = new BemData
            {
                Cost = cost,
                Income = income,
                Yield = yield,
                Profit = profit
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
