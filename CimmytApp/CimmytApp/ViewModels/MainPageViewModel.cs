using System.Collections.Generic;
using CimmytApp.DTO.BEM;
using Helper.GeoWiki.API;

namespace CimmytApp.ViewModels
{
    using System;
    using CimmytApp.BusinessContract;
    using CimmytApp.Parcel.Events;
    using Prism.Events;
    using Prism.Modularity;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly IModuleManager _moduleManager;
        private object _legend;
        private INavigationService _navigationService;
		private ICimmytDbOperations _cimmytDbOperations;
		private readonly IEventAggregator _eventAggregator;
        private string _title;

        public MainPageViewModel(IModuleManager moduleManager, IEventAggregator eventAggregator, INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _moduleManager = moduleManager;
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DbConnectionRequestEvent>().Subscribe(OnDbRequest);
        }

        private void OnDbRequest()
        {
            _eventAggregator.GetEvent<DbConnectionEvent>().Publish(_cimmytDbOperations);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private object legend
        {
            get { return _legend; }
            set { _legend = value; }
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