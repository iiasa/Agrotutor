using System.Collections.Generic;
using CimmytApp.DTO.BEM;
using Helper.GeoWiki.API;

namespace CimmytApp.ViewModels
{
    using Prism.Modularity;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly IModuleManager _moduleManager;
        private object _legend;
        private INavigationService _navigationService;
        private string _title;

        public MainPageViewModel(IModuleManager moduleManager, INavigationService navigationService)
        {
            _moduleManager = moduleManager;
            _navigationService = navigationService;
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