namespace CimmytApp.Views
{
    using CimmytApp.ViewModels;

    public partial class ParcelMainPage
    {
        private readonly ParcelMainPageViewModel _bindingContext;

        public ParcelMainPage()
        {
            InitializeComponent();
            _bindingContext = (ParcelMainPageViewModel)BindingContext;
        }

        protected override bool OnBackButtonPressed()
        {
            _bindingContext.GoBackCommand.Execute();
            return true;
        }
    }
}