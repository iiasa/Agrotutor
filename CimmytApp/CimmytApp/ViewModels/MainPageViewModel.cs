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
        private string _title;
        private readonly IModuleManager _moduleManager;
        private INavigationService _navigationService;

        private object _legend;

        private object legend
        {
            get { return _legend; }
            set { _legend = value; }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainPageViewModel(IModuleManager moduleManager, INavigationService navigationService)
        {
            _moduleManager = moduleManager;
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}