namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.BEM;

    public class ViewCostoPageViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Costo> _datasets;

        public ObservableCollection<Costo> Datasets
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
            var ds = new ObservableCollection<Costo>();
            if (datasets == null) return;
            foreach (var dataset in (List<Costo>)datasets)
            {
                ds.Add(dataset);
            }
            Datasets = ds;
        }
    }
}