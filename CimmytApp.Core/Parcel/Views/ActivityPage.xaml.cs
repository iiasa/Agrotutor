namespace CimmytApp.Parcel.Views
{
    using CimmytApp.Parcel.ViewModels;

    public partial class ActivityPage
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