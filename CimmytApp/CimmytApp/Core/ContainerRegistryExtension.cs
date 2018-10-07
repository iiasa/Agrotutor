namespace CimmytApp.Core
{
    using DryIoc;
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
    }
}
