namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.BEM;

    public class ViewIngresoPageViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Ingreso> _datasets;

        public ObservableCollection<Ingreso> Datasets
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
            var ds = new ObservableCollection<Ingreso>();
            if (datasets == null) return;
            foreach (var dataset in (List<Ingreso>)datasets)
            {
                ds.Add(dataset);
            }
            Datasets = ds;
        }
    }
}