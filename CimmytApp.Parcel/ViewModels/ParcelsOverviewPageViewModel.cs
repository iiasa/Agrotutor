using System.Collections.ObjectModel;

namespace CimmytApp.Parcel.ViewModels
{
    using BusinessContract;
    using DTO.Parcel;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ParcelsOverviewPageViewModel" />
    /// </summary>
    public class ParcelsOverviewPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Defines the _eventAggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Defines the _parcels
        /// </summary>
        private List<Parcel> _parcels;


		public ObservableCollection<ParcelViewModel> ObservableParcel { get; set; }

		/// <summary>
		/// Gets or sets the AddParcelCommand
		/// </summary>
		public DelegateCommand AddParcelCommand { get; set; }

        /// <summary>
        /// Gets or sets the UploadCommand
        /// </summary>
        public DelegateCommand UploadCommand { get; set; }

        /// <summary>
        /// Gets or sets the ParcelDetailCommand
        /// </summary>
        public DelegateCommand<object> ParcelDetailCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowUploadButton
        /// </summary>
        public bool ShowUploadButton
        {
            get => _showUploadButton;
            set => SetProperty(ref _showUploadButton, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether ParcelsListIsVisible
        /// </summary>
        public bool ParcelsListIsVisible { get => _parcelsListIsVisible; set => SetProperty(ref _parcelsListIsVisible, value); }

        /// <summary>
        /// Gets or sets a value indicating whether AddParcelHintIsVisible
        /// </summary>
        public bool AddParcelHintIsVisible { get => _addParcelHintIsVisible; set => SetProperty(ref _addParcelHintIsVisible, value); }

        /// <summary>
        /// Gets or sets the Parcels
        /// </summary>
        public List<Parcel> Parcels
        {
            get => _parcels;
            set
            {
                SetProperty(ref _parcels, value);
                ParcelsListIsVisible = (value.Count > 0);
                AddParcelHintIsVisible = !ParcelsListIsVisible;
                ShowUploadButton = ParcelsListIsVisible;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsParcelListEnabled
        /// </summary>
        public bool IsParcelListEnabled { get => _isParcelListEnabled; set => SetProperty(ref _isParcelListEnabled, value); }

        /// <summary>
        /// Defines the _cimmytDbOperations
        /// </summary>
        private readonly ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        /// Defines the _isParcelListEnabled
        /// </summary>
        private bool _isParcelListEnabled = true;

        /// <summary>
        /// Defines the _showUploadButton
        /// </summary>
        private bool _showUploadButton;

        /// <summary>
        /// Defines the _parcelsListIsVisible
        /// </summary>
        private bool _parcelsListIsVisible = false;

        /// <summary>
        /// Defines the _addParcelHintIsVisible
        /// </summary>
        private bool _addParcelHintIsVisible = true;

	    private ParcelViewModel _oldParcel;

	    /// <summary>
        /// Initializes a new instance of the <see cref="ParcelsOverviewPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        /// <param name="eventAggregator">The <see cref="IEventAggregator"/></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations"/></param>
        public ParcelsOverviewPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _cimmytDbOperations = cimmytDbOperations;
            AddParcelCommand = new DelegateCommand(NavigateToAddParcelPage);
            UploadCommand = new DelegateCommand(UploadParcels);
            ParcelDetailCommand = new DelegateCommand<object>(NavigateToParcelDetailPage).ObservesCanExecute(o => IsParcelListEnabled);

            _parcels = new List<Parcel>();
            Parcels = cimmytDbOperations.GetAllParcels();
	        SetObservableParcel();
        }

	    private void SetObservableParcel()
	    {
		    ObservableParcel = new ObservableCollection<ParcelViewModel>();
		    foreach (var parcel in _parcels)
		    {
			    ObservableParcel.Add(new ParcelViewModel {Parcel = parcel, IsOptionsVisible = false});
		    }
	    }

	    /// <summary>
        /// The UploadParcels
        /// </summary>
        private void UploadParcels()
        {
            foreach (var parcel in Parcels)
            {
                parcel.Submit();
            }
            Parcels = Parcels; // Just for triggering setproperty
            ShowUploadButton = false;
        }

        /// <summary>
        /// The NavigateToParcelDetailPage
        /// </summary>
        /// <param name="id">The <see cref="object"/></param>
        private void NavigateToParcelDetailPage(object id)
        {
            try
            {
                // IsParcelListEnabled = false;
                var navigationParameters = new NavigationParameters { { "Id", (int)id } };
                _navigationService.NavigateAsync("ParcelMainPage", navigationParameters);
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// The NavigateToAddParcelPage
        /// </summary>
        private void NavigateToAddParcelPage()
        {
            _navigationService.NavigateAsync("AddParcelPage");
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
        }

        /// <summary>
        /// The OnNavigatingTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatingTo(NavigationParameters parameters)
        {
            _parcels = new List<Parcel>();
            Parcels = _cimmytDbOperations.GetAllParcels();
        }

	    public void HideOrShowParcel(ParcelViewModel parcel)
	    {
		    if (_oldParcel == parcel)
		    {
			    parcel.IsOptionsVisible = !parcel.IsOptionsVisible;
			    UpdateObservaleParcel(parcel);
			}
		    else
		    {
			    if (_oldParcel != null)
			    {
				    _oldParcel.IsOptionsVisible = false;
				    UpdateObservaleParcel(_oldParcel);
				}
			    parcel.IsOptionsVisible = true;
			    UpdateObservaleParcel(parcel);
			}
		    _oldParcel = parcel;
	    }

	    private void UpdateObservaleParcel(ParcelViewModel parcel)
	    {
		    var index = ObservableParcel.IndexOf(parcel);
		    ObservableParcel.Remove(parcel);
		    ObservableParcel.Insert(index,parcel);
	    }
    }
}