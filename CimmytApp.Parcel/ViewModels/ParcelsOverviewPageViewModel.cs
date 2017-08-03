﻿namespace CimmytApp.Parcel.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    using DTO.Parcel;
    using CimmytApp.MockData;
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.BusinessContract;
    using Prism.Events;
    using CimmytApp.Parcel.Events;
    using System;

    public class ParcelsOverviewPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private List<Parcel> _parcels;
        public ICommand AddParcelCommand { get; set; }
        public ICommand ParcelDetailCommand { get; set; }

        public List<Parcel> Parcels
        {
            get { return _parcels; }
            set { SetProperty(ref _parcels, value); }
        }

        private ICimmytDbOperations _cimmytDbOperations;

        public ParcelsOverviewPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICimmytDbOperations cimmytDbOperations)
		{
			_navigationService = navigationService;
			_eventAggregator = eventAggregator;
			_cimmytDbOperations = cimmytDbOperations;
            AddParcelCommand = new Command(NavigateToAddParcelPage);
            ParcelDetailCommand = new Command(NavigateToParcelDetailPage);

            List<Parcel> parcels = new TestParcels();

            //testcode:
            Parcels = new List<Parcel>
            {
                parcels.ElementAt(0),
                parcels.ElementAt(1)
			};

            Parcels.AddRange(cimmytDbOperations.GetAllParcels());
            foreach (var parcel in Parcels) parcel.Submit();
        }

        private void NavigateToParcelDetailPage(object id)
        {
            var navigationParameters = new NavigationParameters { { "id", (int)id } };
            _navigationService.NavigateAsync("ParcelPage", navigationParameters);
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