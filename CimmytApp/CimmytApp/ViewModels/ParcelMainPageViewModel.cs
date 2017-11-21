namespace CimmytApp.ViewModels
{
    using System;
    using CimmytApp.BusinessContract;
    using CimmytApp.DTO.Parcel;
    using Prism;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    /// <summary>
    ///     Defines the <see cref="ParcelMainPageViewModel" />
    /// </summary>
    public class ParcelMainPageViewModel : BindableBase, INavigationAware, IActiveAware
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
        ///     Initializes a new instance of the <see cref="ParcelMainPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" /></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations" /></param>
        public ParcelMainPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            this._navigationService = navigationService;
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
            NavigateToMapCommand = new DelegateCommand(NavigateToMap);

            try
            {
                this._cimmytDbOperations = cimmytDbOperations;
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        ///     Defines the IsActiveChanged
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Gets or sets the NavigateAsyncCommand
        /// </summary>
        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        /// <summary>
        ///     Gets or sets the NavigateToMapCommand
        /// </summary>
        public DelegateCommand NavigateToMapCommand { get; set; }

        /// <summary>
        ///     Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel
        {
            get => this._parcel;
            set => SetProperty(ref this._parcel, value);
        }

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
            try
            {
                int id = (int)parameters["Id"];

                Parcel = this._cimmytDbOperations.GetParcelById(id);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     The NavigateAsync
        /// </summary>
        /// <param name="page">The <see cref="string" /></param>
        private void NavigateAsync(string page)
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "Parcel", Parcel }
            };
            if (page == "ParcelPage")
            {
                parameters.Add("EditEnabled", false);
            }
            this._navigationService.NavigateAsync(page, parameters);
        }

        /// <summary>
        ///     The NavigateToMap
        /// </summary>
        private void NavigateToMap()
        {
            //TODO parameters!!
            _navigationService.NavigateAsync("GenericMap");
        }
    }
}