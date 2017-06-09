namespace CimmytApp.UWP
{
    using Prism.Unity;
    using Microsoft.Practices.Unity;

    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new CimmytApp.App(new UwpInitializer()));
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {
        }
    }
}