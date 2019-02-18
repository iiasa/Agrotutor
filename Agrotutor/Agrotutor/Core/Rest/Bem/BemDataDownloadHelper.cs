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
        private static string CostParameter = "costo";
        private static string IncomeParameter = "ingreso";
        private static string YieldParameter = "rendimiento";
        private static string ProfitParameter = "utilidads";

        public static async Task<BemData> LoadBEMData(double? lat = null, double? lon = null, CropType? crop = null)
        {
            List<Cost> cost = await LoadCost(lat, lon, crop);
            List<Income> income = await LoadIncome(lat, lon, crop);
            List<Yield> yield = await LoadYield(lat, lon, crop);
            List<Profit> profit = await LoadProfit(lat, lon, crop);

            BemData bemData = new BemData
            {
                Cost = cost,
                Income = income,
                Yield = yield,
                Profit = profit,
                Date = DateTime.Now.ToShortDateString()
            };
            return bemData;
        }

        public static async Task<List<Cost>> LoadCost(double? lat = null, double? lon = null, CropType? crop = null)
        {
            return await Load<Cost>(CostParameter, lat, lon, crop);
        }

        public static async Task<List<Income>> LoadIncome(double? lat = null, double? lon = null, CropType? crop = null)
        {
            return await Load<Income>(IncomeParameter, lat, lon, crop);
        }

        public static async Task<List<Yield>> LoadYield(double? lat = null, double? lon = null, CropType? crop = null)
        {
            return await Load<Yield>(YieldParameter, lat, lon, crop);
        }

        public static async Task<List<Profit>> LoadProfit(double? lat = null, double? lon = null, CropType? crop = null)
        {
            return await Load<Profit>(ProfitParameter, lat, lon, crop);
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
