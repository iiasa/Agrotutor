using System.Threading.Tasks;

namespace CimmytApp.Parcel.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    using DTO.Parcel;
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.BusinessContract;
    using Prism.Events;
    using CimmytApp.Parcel.Events;
    using System;
    using Prism.Commands;

    public class ParcelsOverviewPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private List<Parcel> _parcels;
        public DelegateCommand AddParcelCommand { get; set; }
        public DelegateCommand UploadCommand { get; set; }
        public DelegateCommand<object> ParcelDetailCommand { get; set; }

        public bool ShowUploadButton
        {
            get => _showUploadButton;
            set => SetProperty(ref _showUploadButton, value);
        }

        public List<Parcel> Parcels
        {
            get => _parcels;
            set => SetProperty(ref _parcels, value);
        }

        public bool IsParcelListEnabled
        {
            get => _isParcelListEnabled;
            set => SetProperty(ref _isParcelListEnabled, value);
        }

        private ICimmytDbOperations _cimmytDbOperations;
        private bool _isParcelListEnabled = true;
        private bool _showUploadButton;

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
            /*var uploadTask = new Task(UploadParcels);
            uploadTask.Start();*/ //TODO: find a proper place to trigger upload
        }

        private void UploadParcels()
        {
            ShowUploadButton = false;
            foreach (var parcel in Parcels)
            {
                parcel.Submit();
            }
            Parcels = Parcels; // Just for triggering setproperty
        }

        private void NavigateToParcelDetailPage(object id)
        {
            try
            {
                // IsParcelListEnabled = false;
                var navigationParameters = new NavigationParameters { { "Id", (int)id } };
                _navigationService.NavigateAsync("ParcelPage", navigationParameters);
            }
            catch (Exception e)
            {
            }
        }

        private void NavigateToAddParcelPage()
        {
            _navigationService.NavigateAsync("AddParcelPage");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}