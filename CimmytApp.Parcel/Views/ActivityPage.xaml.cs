namespace CimmytApp.Parcel.Views
{
    using CimmytApp.Parcel.ViewModels;
    using Xamarin.Forms;

    public partial class ActivityPage : ContentPage
    {
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