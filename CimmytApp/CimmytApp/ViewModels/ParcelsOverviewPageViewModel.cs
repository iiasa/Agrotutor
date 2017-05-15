using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CimmytApp.BusinessContract;
using Prism.Navigation;

namespace CimmytApp.ViewModels
{
    public class ParcelsOverviewPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        public ICommand AddParcelCommand { get; set; }
        private ObservableCollection<IDataset> Parcels;

        public ParcelsOverviewPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddParcelCommand = new DelegateCommand(NavigateToAddParcelPage);
            // Todo: get parcels from sqlite
        }

        private void NavigateToAddParcelPage()
        {
            _navigationService.NavigateAsync("AddParcelPage");
        }
    }
}