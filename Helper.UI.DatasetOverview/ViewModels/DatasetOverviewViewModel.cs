using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CimmytApp.BusinessContract;
using Prism.Navigation;
using Xamarin.Forms;

namespace Helper.UI.DatasetOverview.ViewModels
{
    public class DatasetOverviewViewModel : BindableBase, INavigationAware
    {
        public ObservableCollection<IDataset> Datasets { get; private set; }

        // TODO check if can be IEnumerable (->collection doesn't change usually)
        public DataTemplate OverviewElementDataTemplate { get; private set; }

        public DatasetOverviewViewModel()
        {
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("datasets"))
            {
                Datasets = new ObservableCollection<IDataset>(parameters.GetValue<IEnumerable<IDataset>>("datasets"));
                if (Datasets.Count > 0)
                {
                    OverviewElementDataTemplate = Datasets.ElementAt(0).GetOverviewDataTemplate();
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}