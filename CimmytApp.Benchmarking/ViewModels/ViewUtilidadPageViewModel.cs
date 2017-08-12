namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.BEM;

    public class ViewUtilidadPageViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Utilidad> _datasets;

        public ObservableCollection<Utilidad> Datasets
        {
            get => _datasets;
            set => SetProperty(ref _datasets, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!parameters.ContainsKey("Datasets")) return;
            parameters.TryGetValue("Datasets", out object datasets);
            var ds = new ObservableCollection<Utilidad>();
            if (datasets == null) return;
            foreach (var dataset in (List<Utilidad>)datasets)
            {
                ds.Add(dataset);
            }
            Datasets = ds;
        }
    }
}