using CimmytApp.Parcel.ViewModels;
using Xamarin.Forms;

namespace CimmytApp.Parcel.Views
{
    public partial class ActivityPage : ContentPage
    {
        /// <summary>
        /// Defines the contextObj
        /// </summary>
        private readonly ActivityPageViewModel _contextObj;

        public ActivityPage()
        {
            InitializeComponent();
            _contextObj = (ActivityPageViewModel)BindingContext;
        }

        protected override bool OnBackButtonPressed()
        {
            _contextObj.SaveCommand.Execute();
            return true;
        }
    }
}