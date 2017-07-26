namespace CimmytApp.Parcel.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    using DTO.Parcel;
    using CimmytApp.MockData;
    using System.Collections.Generic;
    using System.Linq;

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

        public ParcelsOverviewPageViewModel()
        {
            AddParcelCommand = new Command(NavigateToAddParcelPage);
            ParcelDetailCommand = new Command(NavigateToParcelDetailPage);
            // Todo: get parcels from sqlite

            List<Parcel> parcels = new TestParcels();

            //testcode:
            Parcels = new ObservableCollection<Parcel>
            {
                parcels.ElementAt(0),
                parcels.ElementAt(1)
            };
        }

        public ParcelsOverviewPageViewModel(INavigationService navigationService) : this()
        {
            _navigationService = navigationService;
        }

        private void NavigateToParcelDetailPage(object id)
        {
            var navigationParameters = new NavigationParameters { { "id", (int)id } };
            _navigationService.NavigateAsync("ParcelPage", navigationParameters);
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