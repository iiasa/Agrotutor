namespace CimmytApp.Core.Parcel.Views
{
    using CimmytApp.Core.Parcel.ViewModels;

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