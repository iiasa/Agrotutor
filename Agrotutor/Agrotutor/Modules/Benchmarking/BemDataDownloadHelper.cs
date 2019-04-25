using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agrotutor.Core.Entities;
using Flurl.Http;

namespace Agrotutor.Modules.Benchmarking
{
    public static class BemDataDownloadHelper
    {
        private static string CostParameter = "costo";
        private static string IncomeParameter = "ingreso";
        private static string YieldParameter = "rendimiento";
        private static string ProfitParameter = "utilidad";

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
            IFlurlRequest request = $"{Constants.BemBaseUrl}?type={parameter}&tkn={Constants.BemToken}"
                .WithBasicAuth(Constants.BemUsername, Constants.BemPassword);
            if (lat != null)
            {
                request.SetQueryParam("lat", lat.ToString().Replace(",","."));
            }

            if (lon != null)
            {
                request.SetQueryParam("lon", lon.ToString().Replace(",","."));
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
