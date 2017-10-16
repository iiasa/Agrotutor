namespace CimmytApp.ViewModels
{
    using CimmytApp.BusinessContract;
    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Prism;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Navigation;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="ParcelMainPageViewModel" />
    /// </summary>
    public class ParcelMainPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
        /// <summary>
        /// Defines the _parcel
        /// </summary>
        private DTO.Parcel.Parcel _parcel;

        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Defines the _cimmytDbOperations
        /// </summary>
        private readonly ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        /// Gets or sets the Parcel
        /// </summary>
        public DTO.Parcel.Parcel Parcel
        {
            get => _parcel;
            set
            {
                SetProperty(ref _parcel, value);
                OnPropertyChanged("Parcel");
                if (value != null)
                    PublishDataset(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelMainPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations"/></param>
        /// <param name="eventAggregator">The <see cref="IEventAggregator"/></param>
        public ParcelMainPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _navigationService = navigationService;

            try
            {
                _cimmytDbOperations = cimmytDbOperations;
            }
            catch (Exception e)
            {
            }
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
        }

        /// <summary>
        /// Gets or sets the NavigateAsyncCommand
        /// </summary>
        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        /// <summary>
        /// The NavigateAsync
        /// </summary>
        /// <param name="page">The <see cref="string"/></param>
        private void NavigateAsync(string page)
        {
            _navigationService.NavigateAsync(page);
        }

        /// <summary>
        /// The GetDataset
        /// </summary>
        /// <returns>The <see cref="IDataset"/></returns>
        protected override IDataset GetDataset()
        {
            return Parcel;
        }

        /// <summary>
        /// The ReadDataset
        /// </summary>
        /// <param name="dataset">The <see cref="IDataset"/></param>
        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (DTO.Parcel.Parcel)dataset;
        }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                var id = (int)parameters["Id"];

                Parcel = _cimmytDbOperations.GetParcelById(id);
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Defines the IsActiveChanged
        /// </summary>
        public event EventHandler IsActiveChanged;
    }
}