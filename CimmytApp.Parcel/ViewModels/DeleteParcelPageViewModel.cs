namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using CimmytApp.DTO.Parcel;
    using Helper.Realm;
    using Helper.Realm.BusinessContract;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    /// <summary>
    ///     Defines the <see cref="DeleteParcelPageViewModel" />
    /// </summary>
    public class DeleteParcelPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        ///     Defines the _cimmytDbOperations
        /// </summary>
        private readonly ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        ///     Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        ///     Defines the _parcel
        /// </summary>
        private Parcel _parcel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeleteParcelPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" /></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations" /></param>
        public DeleteParcelPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            Delete = new DelegateCommand(DeleteParcel);
            GoBack = new DelegateCommand(Back);
        }

        /// <summary>
        ///     Gets or sets the Delete
        /// </summary>
        public DelegateCommand Delete { get; set; }

        /// <summary>
        ///     Gets or sets the GoBack
        /// </summary>
        public DelegateCommand GoBack { get; set; }

        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
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

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The Back
        /// </summary>
        private void Back()
        {
            _navigationService.GoBackAsync();
        }

        /// <summary>
        ///     The DeleteParcel
        /// </summary>
        private void DeleteParcel()
        {
            _cimmytDbOperations.DeleteParcel(_parcel.ParcelId);
            _navigationService.NavigateAsync("app:///MainPage");
        }
    }
}