using System.Collections.ObjectModel;
using Agrotutor.Core;
using Agrotutor.ViewModels;
using Microsoft.Extensions.Localization;

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
            
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is WelcomePageViewModel welcomeViewModel)
            {
                ItemsSource = new ObservableCollection<WelcomeModel>
                {
                    new WelcomeModel
                    {
                        Picture = "advice_intro.png",
                        Label = welcomeViewModel.StringLocalizer.GetString("welcome1")
                    },
                    new WelcomeModel
                    {
                        Picture = "Intro1_Agrotutor.png",
                        Label = welcomeViewModel.StringLocalizer.GetString("welcome2")

                    },
                    new WelcomeModel
                    {
                        Picture = "Intro2_Agrotutor.png",
                        Label = welcomeViewModel.StringLocalizer.GetString("welcome3")

                    },
                    new WelcomeModel
                    {
                        Picture = "Intro3_Agrotutor.png",
                        Label = welcomeViewModel.StringLocalizer.GetString("welcome4"),
                        ShowGetStarted = true
                    }
                };
            }
                
        }

    }
}
