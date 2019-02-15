using Flurl;
using Flurl.Http;

namespace Agrotutor.Core.Rest.Bem
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Entities;

    public static class BemDataDownloadHelper
    {

        public static async Task<BemData> LoadBEMData(double? lat = null, double? lon = null, CropType? crop = null)
        {
            List<Cost> cost = await Load<Cost>("costo", lat, lon, crop);
            List<Income> income = await Load<Income>("ingreso", lat, lon, crop);
            List<Yield> yield = await Load<Yield>("rendimiento", lat, lon, crop);
            List<Profit> profit = await Load<Profit>("utilidads", lat, lon, crop);

            BemData bemData = new BemData
            {
                Cost = cost,
                Income = income,
                Yield = yield,
                Profit = profit
            };
            return bemData;
        }

        private static async Task<List<T>> Load<T>(string parameter, double? lat = null, double? lon = null, CropType? crop = null)
        {
            IFlurlRequest request = $"http://104.239.158.49/cimmytapiv2.php?type={parameter}&tkn=E31C5F8478566357BA6875B32DC59"
                .WithBasicAuth("cimmy2018", "tBTAibgFtHxaNE8ld7hpKKsx3n1ORIO");
            if (lat != null)
            {
                request.SetQueryParam("lat", lat);
            }

            if (lon != null)
            {
                request.SetQueryParam("lon", lon);
            }

            if (crop != null && crop==CropType.Corn) //TODO: use crop types
            {
                request.SetQueryParam("cultivo", "Maiz");
            }

            List<T> data = null;
            try
            {
                data = await request.GetJsonAsync<List<T>>();
            }
            catch (Exception)
            {
            }

            return data;
        }
    }
}
