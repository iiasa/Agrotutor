namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.BusinessContract;
    using CimmytApp.DTO.BEM;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class LocalBenchmarkingPageViewModel : BindableBase, INavigationAware
    {
        private readonly ICimmytDbOperations _cimmytDbOperations;
        private BemData _bemData;
        private object _cycle;
        private object _dataset;
        private List<BemDataset> _datasets;
        private object _year;

        public LocalBenchmarkingPageViewModel(ICimmytDbOperations cimmytDbOperations)
        {
            _cimmytDbOperations = cimmytDbOperations;
        }

        public List<BemDataset> Datasets
        {
            get => _datasets;
            set => SetProperty(ref _datasets, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            //GetData(parameters);
        }
    }
}