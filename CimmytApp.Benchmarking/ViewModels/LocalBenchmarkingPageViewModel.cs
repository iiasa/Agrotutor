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
#pragma warning disable CS0169 // The field 'LocalBenchmarkingPageViewModel._bemData' is never used
        private BemData _bemData;
#pragma warning restore CS0169 // The field 'LocalBenchmarkingPageViewModel._bemData' is never used
#pragma warning disable CS0169 // The field 'LocalBenchmarkingPageViewModel._cycle' is never used
        private object _cycle;
#pragma warning restore CS0169 // The field 'LocalBenchmarkingPageViewModel._cycle' is never used
#pragma warning disable CS0169 // The field 'LocalBenchmarkingPageViewModel._dataset' is never used
        private object _dataset;
#pragma warning restore CS0169 // The field 'LocalBenchmarkingPageViewModel._dataset' is never used
        private List<BemDataset> _datasets;
#pragma warning disable CS0169 // The field 'LocalBenchmarkingPageViewModel._year' is never used
        private object _year;
#pragma warning restore CS0169 // The field 'LocalBenchmarkingPageViewModel._year' is never used

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

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}