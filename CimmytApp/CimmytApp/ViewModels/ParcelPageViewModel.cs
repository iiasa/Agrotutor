namespace CimmytApp.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using Prism.Navigation;
    using Prism;

    using DTO.Parcel;

    public class ParcelPageViewModel : BindableBase, INavigationAware, IActiveAware
    {
        private Parcel _parcel;

        public event EventHandler IsActiveChanged;

        public Parcel Parcel
        {
            get { return _parcel; }
            set { SetProperty(ref _parcel, value); }
        }

        public bool IsActive
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public ParcelPageViewModel()
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _parcel = (Parcel)parameters["parcel"];
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}