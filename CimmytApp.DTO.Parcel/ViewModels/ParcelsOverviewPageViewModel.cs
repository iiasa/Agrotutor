namespace CimmytApp.DTO.Parcel.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    public class ParcelsOverviewPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private ObservableCollection<Parcel> _parcels;
        public ICommand AddParcelCommand { get; set; }
        public ICommand ParcelDetailCommand { get; set; }

        public ObservableCollection<Parcel> Parcels
        {
            get { return _parcels; }
            set { SetProperty(ref _parcels, value); }
        }

        public ParcelsOverviewPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddParcelCommand = new DelegateCommand(NavigateToAddParcelPage);
            ParcelDetailCommand = new Command(NavigateToParcelDetailPage);
            // Todo: get parcels from sqlite

            //testcode:
            Parcels = new ObservableCollection<Parcel>
            {
                new Parcel
                {
                    ID = 1,
                    Crop = "Maize"
                },
                new Parcel
                {
                    ID = 2,
                    Crop = "Wheat"
                },
                new Parcel
                {
                    ID = 3,
                    Crop = "Triticale"
                },
                new Parcel
                {
                    ID = 4,
                    Crop = "This thing is long as heeeeelll..."
                }
            };
        }

        private void NavigateToParcelDetailPage(object id)
        {
            //App.CurrentParcel =
            var navigationParameters = new NavigationParameters { { "id", (int)id } };
            _navigationService.NavigateAsync("ParcelDetailPage", navigationParameters);
        }

        private void NavigateToAddParcelPage()
        {
            _navigationService.NavigateAsync("AddParcelPage");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}