using System;
using System.Linq;
using System.Reflection;
using Agrotutor.Core.Camera;
using Agrotutor.Core.Persistence;
using Agrotutor.Core.Tile;
using Agrotutor.Core.Tile.Resources;
using Agrotutor.Modules.Calendar.Components.Views;
using Agrotutor.Modules.Calendar.ViewModels;
using Agrotutor.ViewModels;
using Agrotutor.Views;
using DryIoc;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;

namespace Agrotutor.Core
{
    public static class ContainerRegistryExtension
    {
        // Based on ASP.NET Core Localization https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization
        public static void RegisterLocalization(this IContainerRegistry containerRegistry,
            string resourcesPath = "Resources")
        {
            containerRegistry.GetContainer()
                .RegisterDelegate<IStringLocalizerFactory>(
                    x =>
                    {
                        return new ResourceManagerStringLocalizerFactory(
                            new OptionsManager<LocalizationOptions>(new OptionsFactory<LocalizationOptions>(
                                new IConfigureOptions<LocalizationOptions>[]
                                {
                                    new ConfigureOptions<LocalizationOptions>(options =>
                                        options.ResourcesPath = resourcesPath)
                                },
                                new IPostConfigureOptions<LocalizationOptions>[] { })), new NullLoggerFactory());
                    }, new SingletonReuse());

            containerRegistry.GetContainer().Register(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        }

        public static void RegisterCameraService(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ICameraService, CameraService>();
        }

        public static void RegisterPages(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CitationPage, CitationPageViewModel>();
            containerRegistry.RegisterForNavigation<LinksPage, LinksPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<WebContentPage, WebContentPageViewModel>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
        }

        public static void RegisterPersistence(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSharedContextClasses();
            containerRegistry.RegisterAppDataContext();
            containerRegistry.Register<IAppDataService, AppDataService>();
        }

        public static void RegisterAppDataContext(this IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer()
                .RegisterInitializer<DbContextOptionsBuilder<AppDataContext>>((builder, resolver) =>
                {
                    Persistence.DbContextOptionsBuilderExtension.Configure(builder);
                });

            containerRegistry.RegisterSingleton<IAppDataContext, AppDataContext>();
            containerRegistry.GetContainer()
                .RegisterInitializer<IAppDataContext>((context, resolver) =>
                {
                    // Start with a clean database
                    ////((AppDataContext)context).Database.EnsureDeleted();
                    try
                    {
                        ((AppDataContext) context).Database.EnsureCreated();
                        
                    }
                    catch (Exception e)
                    {
                        AppCenterLog.Error("DbCreation", "Error during DB creation", e);
                        Crashes.TrackError(e);
                    }

                    try
                    {
                        ((AppDataContext)context).Database.Migrate();
                    }
                    catch (Exception e)
                    {
                        AppCenterLog.Error("DbMigration", "Error during DB Migration", e);
                        try
                        {
                            ((AppDataContext)context).Database.EnsureDeleted();
                            ((AppDataContext)context).Database.EnsureCreated();
                        }
                        catch (Exception exception)
                        {
                            AppCenterLog.Error("DbMigration", "Error during DB Migration fallback", e);
                            Crashes.TrackError(exception);
                        }
                    }
                });
        }

        public static void RegisterSharedContextClasses(this IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer()
                .Register(typeof(DbContextOptionsBuilder<>),
                    made: Made.Of(typeof(DbContextOptionsBuilder<>).GetConstructor(Type.EmptyTypes)));

            containerRegistry.GetContainer()
                .Register(typeof(DbContextOptions<>),
                    made: Made.Of(
                        r => typeof(DbContextOptionsBuilder<>)
                            .MakeGenericType(r.ServiceType.GenericTypeArguments.Single())
                            .GetProperty("Options",
                                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly),
                        r => ServiceInfo.Of(
                            typeof(DbContextOptionsBuilder<>).MakeGenericType(
                                r.ServiceType.GenericTypeArguments.Single()))));
        }

        public static void RegisterTileService(this IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer()
                .RegisterInitializer<DbContextOptionsBuilder<TileContext>>((builder, resolver) =>
                {
                    if (!Persistence.DbContextOptionsBuilderExtension.FileExists(builder, Constants.OfflineBasemapFilename))
                        Persistence.DbContextOptionsBuilderExtension.CopyFromStream(builder, Resources.GetIIASATiles(),
                            Constants.OfflineBasemapFilename);

                    Persistence.DbContextOptionsBuilderExtension.Configure(builder, Constants.OfflineBasemapFilename);
                });

            containerRegistry.Register<IReadOnlyTileService, ReadOnlyTileService>();
        }
    }
}