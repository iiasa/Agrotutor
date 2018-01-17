namespace CimmytApp.DTO.Benchmarking
{
    using Helper.Datatypes;

    public enum WdtIncDataset
    {
        [StringValue("cooling-degree-days")]
        CoolingDegreeDays,

        [StringValue("daily-evapotranspiration-short-crop")]
        DailyEvapotranspirationShortCrop,

        [StringValue("daily-evapotranspiration-tall-crop")]
        DailyEvapotranspirationTallCrop,

        [StringValue("daily-high-temperature")]
        DailyHighTemperature,

        [StringValue("daily-low-temperature")]
        DailyLowTemperature,

        [StringValue("daily-precipitation")]
        DailyPrecipitation,

        [StringValue("daily-solar-radiation")]
        DailySolarRadiation,

        [StringValue("growing-degree-days")]
        GrowingDegreeDays,

        [StringValue("heating-degree-days")]
        HeatingDegreeDays,

        [StringValue("hourly-dewpoint")]
        HourlyDewpoint,

        [StringValue("hourly-evapotranspiration-short-crop")]
        HourlyEvapotranspirationShortCrop,

        [StringValue("hourly-evapotranspiration-tall-crop")]
        HourlyEvapotranspirationTallCrop,

        [StringValue("hourly-precipitation")]
        HourlyPrecipitation,

        [StringValue("hourly-relative-humidity")]
        HourlyRelativeHumidity,

        [StringValue("hourly-solar-radiation")]
        HourlySolarRadiation,

        [StringValue("hourly-temperature")]
        HourlyTemperature,

        [StringValue("hourly-wind-direction")]
        HourlyWindDirection,

        [StringValue("hourly-wind-speed")]
        HourlyWindSpeed
    }
}