namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.BEM;

    public class ViewRendimientoPageViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Rendimiento> _datasets;

        public ObservableCollection<Rendimiento> Datasets
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
            var ds = new ObservableCollection<Rendimiento>();
            if (datasets == null) return;
            foreach (var dataset in (List<Rendimiento>)datasets)
            {
                ds.Add(dataset);
            }
            Datasets = ds;
        }
    }
}