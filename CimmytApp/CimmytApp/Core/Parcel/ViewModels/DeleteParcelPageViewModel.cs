namespace CimmytApp.Core.Parcel.ViewModels
{
    using System;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.ViewModels;
    using Helper.Realm.BusinessContract;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    /// <summary>
    ///     Defines the <see cref="DeleteParcelPageViewModel" />
    /// </summary>
    public class DeleteParcelPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly ICimmytDbOperations _cimmytDbOperations;

        private readonly INavigationService _navigationService;

        private Parcel _parcel;

        public DeleteParcelPageViewModel(INavigationService navigationService, 
            ICimmytDbOperations cimmytDbOperations, IStringLocalizer<DeleteParcelPageViewModel> localizer)
        : base(localizer)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            Delete = new DelegateCommand(DeleteParcel);
            GoBack = new DelegateCommand(Back);
        }

        public DelegateCommand Delete { get; set; }

        public DelegateCommand GoBack { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                _parcel = (Parcel)parameters["Parcel"];
            }
            else
            {
                try
                {
                    var id = (string)parameters["Id"];

                    _parcel = Parcel.FromDTO(_cimmytDbOperations.GetParcelById(id));
                }
                catch (Exception)
                {
                    Back();
                }
            }
        }

        private void Back()
        {
            _navigationService.GoBackAsync();
        }

        private void DeleteParcel()
        {
            _cimmytDbOperations.DeleteParcel(_parcel.ParcelId);
            _navigationService.NavigateAsync("app:///MainPage");
        }
    }
}