using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using CimmytApp.DTO;
using Prism.Navigation;

namespace CimmytApp.ViewModels
{
    public class ParcelPageViewModel : BindableBase
    {
        private Parcel _parcel;

        public Parcel Parcel
        {
            get { return _parcel; }
            set { SetProperty(ref _parcel, value); }
        }

        public ParcelPageViewModel()
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _parcel = (Parcel)parameters["parcel"];
        }
    }
}