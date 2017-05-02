using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;

namespace CimmytApp.ViewModels
{
    public class ParcelsOverviewPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        public ICommand AddParcelCommand { get; set; }

        public ParcelsOverviewPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddParcelCommand = new DelegateCommand(AddParcelClick);
        }

        private void AddParcelClick()
        {
            _navigationService.NavigateAsync("AddParcelPage");
        }
    }
}