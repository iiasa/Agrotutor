namespace Agrotutor.Core
{
    using System;
    using Microsoft.AppCenter.Crashes;
    using Prism.Modularity;

    using Modules.Benchmarking;
    using Modules.Calendar;
    using Modules.Ciat;
    using Modules.Map;
    using Modules.Plot;
    using Modules.Weather;

    public static class ModuleCatalogExtension
    {
        public static void RegisterModules(this IModuleCatalog moduleCatalog)
        {
            try
            {

                Type mapModule = typeof(MapModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(mapModule)
                    {
                        ModuleName = mapModule.Name,
                        ModuleType = mapModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type plotModule = typeof(PlotModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(plotModule)
                    {
                        ModuleName = plotModule.Name,
                        ModuleType = plotModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type calendarModule = typeof(CalendarModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(calendarModule)
                    {
                        ModuleName = calendarModule.Name,
                        ModuleType = calendarModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type benchmarkingModule = typeof(BenchmarkingModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(benchmarkingModule)
                    {
                        ModuleName = benchmarkingModule.Name,
                        ModuleType = benchmarkingModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type weatherModule = typeof(WeatherModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(weatherModule)
                    {
                        ModuleName = weatherModule.Name,
                        ModuleType = weatherModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });

                Type ciatModule = typeof(CiatModule);
                moduleCatalog.AddModule(
                    new ModuleInfo(ciatModule)
                    {
                        ModuleName = ciatModule.Name,
                        ModuleType = ciatModule,
                        InitializationMode = InitializationMode.WhenAvailable
                    });
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }
    }
}
