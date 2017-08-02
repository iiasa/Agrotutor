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
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleManager _moduleManager;
        private ICimmytDbOperations _cimmytDbOperations;
        private object _legend;
        private INavigationService _navigationService;
        private string _title;

        public MainPageViewModel(IModuleManager moduleManager, IEventAggregator eventAggregator, INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _moduleManager = moduleManager;
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DbConnectionRequestEvent>().Subscribe(OnDbConnectionRequest);
            _eventAggregator.GetEvent<DbConnectionAvailableEvent>().Publish();
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
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

        private void OnDbConnectionRequest()
        {
            _eventAggregator.GetEvent<DbConnectionEvent>().Publish(_cimmytDbOperations);
        }
    }
}