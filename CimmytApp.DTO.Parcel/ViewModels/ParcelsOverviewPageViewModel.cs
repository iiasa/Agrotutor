using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CimmytApp.BusinessContract;
using CimmytApp.DTO;
using Prism.Navigation;
using Xamarin.Forms;

namespace CimmytApp.DTO.Parcel.ViewModels
{
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
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}