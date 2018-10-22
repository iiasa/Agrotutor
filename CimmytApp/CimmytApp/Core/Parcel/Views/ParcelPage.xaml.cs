namespace CimmytApp.Parcel.Views
{
    using CimmytApp.Parcel.ViewModels;

    public partial class ParcelPage
    {
        private readonly ParcelPageViewModel _bindingContext;

        public ParcelPage()
        {
            InitializeComponent();
            _bindingContext = (ParcelPageViewModel)BindingContext;
        }

        protected override bool OnBackButtonPressed()
        {
            _bindingContext.GoBackCommand.Execute();
            return true;
        }
    }
}