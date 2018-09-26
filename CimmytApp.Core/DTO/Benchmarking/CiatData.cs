namespace CimmytApp.Core.DTO.Benchmarking
{
    using System;
    using Microsoft.AppCenter;
    using System.Collections.Generic;

    public class CiatData
    {
        private const string Tag = "AGROTUTOR_CIAT_DATA";

        public CiatDataDetail CiatDataIrrigated { get; set; }

        public CiatDataDetail CiatDataNonIrrigated { get; set; }

        public static CiatData FromResponse(List<CiatResponseData> responseData, string requestUrl)
        {
            if (responseData == null) return null;

            CiatData data = new CiatData
            {
                CiatDataIrrigated = new CiatDataDetail{ DataType = CiatDataType.Irrigated },
                CiatDataNonIrrigated = new CiatDataDetail { DataType = CiatDataType.NonIrrigated }
            };

            data.CiatDataNonIrrigated.OptimalCultivars = new List<string>();
            data.CiatDataNonIrrigated.SuboptimalCultivars = new List<string>();
            data.CiatDataIrrigated.OptimalCultivars = new List<string>();
            data.CiatDataIrrigated.SuboptimalCultivars = new List<string>();

            foreach (CiatResponseData ciatResponseData in responseData)
            {
                CiatDataDetail detail = ciatResponseData.CultivationSystem == "Riego"
                    ? data.CiatDataIrrigated
                    : data.CiatDataNonIrrigated;

                switch (ciatResponseData.Variable)
                {
                    case "Densidad de siembra":
                        try
                        {
                            detail.SeedDensity = double.Parse(ciatResponseData.RangeOptMin);
                            detail.SeedDensityUnit = ciatResponseData.Unit;
                        }
                        catch (Exception e)
                        {
                            AppCenterLog.Error(CiatData.Tag,
                                $"Failed parsing seed density: {ciatResponseData.RangeOptMin} [{ciatResponseData.Unit}] ({requestUrl})",
                                e);
                        }

                        break;

                    case "Distancia entre plantas":
                        try
                        {
                            detail.PlantingDistanceMin = double.Parse(ciatResponseData.RangeOptMin);
                            detail.PlantingDistanceMax = double.Parse(ciatResponseData.RangeOptMax);
                            detail.PlantingDistanceUnit = ciatResponseData.Unit;
                        }
                        catch (Exception e)
                        {
                            AppCenterLog.Error(CiatData.Tag,
                                $"Failed parsing plant spacing: {ciatResponseData.RangeOptMin} - {ciatResponseData.RangeOptMax} [{ciatResponseData.Unit}] ({requestUrl})",
                                e);
                        }

                        break;

                    case "Nitrogeno total":
                        try
                        {
                            detail.TotalNitrogen = double.Parse(ciatResponseData.RangeOptMin);
                            detail.TotalNitrogenUnit = ciatResponseData.Unit;
                        }
                        catch (Exception e)
                        {
                            AppCenterLog.Error(CiatData.Tag,
                                $"Failed parsing total nitrogen: {ciatResponseData.RangeOptMin} [{ciatResponseData.Unit}] ({requestUrl})",
                                e);
                        }

                        break;
                    case "Numero de aplicaciones de insecticida":
                        try
                        {
                            detail.CountInsecticideApplication = double.Parse(ciatResponseData.RangeOptMin);
                        }
                        catch (Exception e)
                        {
                            AppCenterLog.Error(CiatData.Tag,
                                $"Failed parsing insecticide applications: {ciatResponseData.RangeOptMin} ({requestUrl})",
                                e);
                        }

                        break;
                    case "Radiacion solar acumulada":
                        try
                        {
                            detail.SolarRadiationMin = double.Parse(ciatResponseData.RangeOptMin);
                            detail.SolarRadiationMax = double.Parse(ciatResponseData.RangeOptMax);
                            detail.SolarRadiationUnit = ciatResponseData.Unit;
                        }
                        catch (Exception e)
                        {
                            AppCenterLog.Error(CiatData.Tag,
                                $"Failed parsing solar radiation: {ciatResponseData.RangeOptMin} - {ciatResponseData.RangeOptMax} [{ciatResponseData.Unit}] ({requestUrl})",
                                e);
                        }

                        break;
                    case "Rendimineto":
                        try
                        {
                            detail.YieldMin = double.Parse(ciatResponseData.RangeOptMin);
                            detail.YieldMax = double.Parse(ciatResponseData.RangeOptMax);
                            detail.YieldUnit = ciatResponseData.Unit;
                        }
                        catch (Exception e)
                        {
                            AppCenterLog.Error(CiatData.Tag,
                                $"Failed parsing yield" + $": {ciatResponseData.RangeOptMin} - {ciatResponseData.RangeOptMax} [{ciatResponseData.Unit}] ({requestUrl})",
                                e);
                        }

                        break;
                    case "Variedad":
                        if (ciatResponseData.ValueOptimal != null)
                        {
                            detail.OptimalCultivars.Add(ciatResponseData.ValueOptimal);
                        }
                        if (ciatResponseData.ValueSuboptimal!= null)
                        {
                            detail.SuboptimalCultivars.Add(ciatResponseData.ValueSuboptimal);
                        }

                        break;
                    default:
                        AppCenterLog.Warn(CiatData.Tag, $"Unsupported variable type received in response: {ciatResponseData.Variable} ({requestUrl})");
                        break;
                }
            }

            return data;
        }

        public enum CiatDataType
        {
            Irrigated,
            NonIrrigated
        }

        public class CiatDataDetail
        {
            public CiatDataType DataType { get; set; }

            public double SeedDensity { get; set; }
            public string SeedDensityUnit { get; set; }

            public double PlantingDistanceMin { get; set; }
            public double PlantingDistanceMax { get; set; }

            public string PlantingDistanceUnit { get; set; }

            public double? TotalNitrogen { get; set; }
            public string TotalNitrogenUnit { get; set; }

            public double CountInsecticideApplication { get; set; }

            public double SolarRadiationMin { get; set; }

            public double SolarRadiationMax { get; set; }

            public string SolarRadiationUnit { get; set; }

            public double YieldMin { get; set; }

            public double YieldMax { get; set; }

            public string YieldUnit { get; set; }

            public List<string> OptimalCultivars { get; set; }

            public List<string> SuboptimalCultivars { get; set; }
        }
    }
}
