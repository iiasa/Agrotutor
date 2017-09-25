using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using CimmytApp.BusinessContract;
using Prism.Navigation;

namespace CimmytApp.Parcel.ViewModels
{
    public class DeleteParcelPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        private ICimmytDbOperations _dbOperations;
        private DTO.Parcel.Parcel _parcel;
        public DelegateCommand Delete { get; set; }
        public DelegateCommand GoBack { get; set; }

        public DeleteParcelPageViewModel(INavigationService navigationService, ICimmytDbOperations dbOperations)
        {
            _navigationService = navigationService;
            _dbOperations = dbOperations;
            Delete = new DelegateCommand(DeleteParcel);
            GoBack = new DelegateCommand(Back);
        }

        private void DeleteParcel()
        {
            _dbOperations.DeleteParcel(_parcel);
            _navigationService.NavigateAsync("MainPage");
        }

        private void Back()
        {
            _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                _parcel = (DTO.Parcel.Parcel)parameters["Parcel"];
            }
            else
            {
                Back();
            }
        }
    }
}