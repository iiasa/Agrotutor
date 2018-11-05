namespace CimmytApp.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using CimmytApp.Core.Persistence;
    using DryIoc;
    using Microsoft.AppCenter;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;
    using Prism.DryIoc;
    using Prism.Ioc;

    public static class ContainerRegistryExtension
    {

        // Based on ASP.NET Core Localization https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization
        public static void RegisterLocalization(this IContainerRegistry containerRegistry, string resourcesPath = "Resources")
        {
            containerRegistry.GetContainer()
                .RegisterDelegate<IStringLocalizerFactory>(
                    x =>
                    {
                        return new ResourceManagerStringLocalizerFactory(
                            new OptionsManager<LocalizationOptions>(new OptionsFactory<LocalizationOptions>(
                                new IConfigureOptions<LocalizationOptions>[]
                                {
                                    new ConfigureOptions<LocalizationOptions>(options => options.ResourcesPath = resourcesPath),
                                },
                                new IPostConfigureOptions<LocalizationOptions>[] { })), new NullLoggerFactory());
                    }, new SingletonReuse());

            containerRegistry.GetContainer().Register(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        }

        public static void RegisterAppDataContext(this IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer()
                .RegisterInitializer<DbContextOptionsBuilder<AppDataContext>>((builder, resolver) =>
                {
                    builder.Configure();
                });

            containerRegistry.RegisterSingleton<IAppDataContext, AppDataContext>();
            containerRegistry.GetContainer()
                .RegisterInitializer<IAppDataContext>((context, resolver) =>
                {
                    // Start with a clean database
                    ////((AppDataContext)context).Database.EnsureDeleted();
                    try
                    {
                        ((AppDataContext)context).Database.EnsureCreated();
                    }
                    catch (Exception e)
                    {
                        AppCenterLog.Error("DbCreation","Error during DB creation", e);
                    }
                });
        }

        public static void RegisterSharedContextClasses(this IContainerRegistry containerRegistry) //TODO: rename!!!
        {
            containerRegistry.GetContainer()
                .Register(typeof(DbContextOptionsBuilder<>),
                    made: Made.Of(typeof(DbContextOptionsBuilder<>).GetConstructor(Type.EmptyTypes)));

            containerRegistry.GetContainer()
                .Register(typeof(DbContextOptions<>),
                    made: Made.Of(
                        r => typeof(DbContextOptionsBuilder<>).MakeGenericType(r.ServiceType.GenericTypeArguments.Single())
                            .GetProperty("Options", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly),
                        r => ServiceInfo.Of(
                            typeof(DbContextOptionsBuilder<>).MakeGenericType(r.ServiceType.GenericTypeArguments.Single()))));
        }
    }
}
