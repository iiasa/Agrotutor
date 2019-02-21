using Agrotutor.Core;

namespace Agrotutor.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : CarouselPage
    {
        public WelcomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ItemsSource = ColorsDataModel.All;
        }
    }
}
