using System;
using Xamarin.Forms.Xaml;

namespace CimmytApp.Views
{
    using Xamarin.Forms;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
            }
        }
    }
}