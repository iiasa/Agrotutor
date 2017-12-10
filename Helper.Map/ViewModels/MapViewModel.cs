using System;
using Helper.Map.Views;
using Prism.Mvvm;
using Prism.Navigation;

namespace Helper.Map.ViewModels
{
    public class MapViewModel : BindableBase, INavigationAware
    {
        private Views.Map _view;

        public MapViewModel()
        {
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void SetViewReference(Views.Map map)
        {
            _view = map;

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
