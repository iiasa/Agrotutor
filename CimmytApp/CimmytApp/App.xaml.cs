namespace CimmytApp
{
    using System.Reflection;
    using Prism.Unity;
    using Xamarin.Forms;

    using Localization;
    using Views;

    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            System.Diagnostics.Debug.WriteLine("====================================");

            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                Resx.AppResources.Culture = ci;
                DependencyService.Get<ILocalize>().SetLocale(ci);
            }
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
            /*
            if (Current.Properties.ContainsKey("not_first_launch"))
            {
                Current.Properties.Add("not_first_launch", true);
                NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
            }
            else
            {
                NavigationService.NavigateAsync("WelcomePage");
            }*/
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<WelcomePage>();
            Container.RegisterTypeForNavigation<ParcelsOverviewPage>();
        }
    }
}