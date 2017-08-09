namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Prism.Mvvm;
    using Prism.Navigation;

    using BusinessContract;
    using DTO.BEM;

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
            GetData(parameters);
        }

        private List<BemDataset> FilterDatasets<T>(List<BemDataset> bemDatasets) where T : BemDataset
        {
            var datasets = new List<BemDataset>();
            datasets.AddRange(bemDatasets);
            List<T> selection;
            if (_year != null)
            {
                selection = new List<T>();
                selection.AddRange(datasets.Cast<T>().Where(dataset => dataset.OutYear.Equals((string)_year)));
                datasets.Clear();
                datasets.AddRange(selection);
            }
            if (_cycle != null)
            {
                selection = new List<T>();
                selection.AddRange(datasets.Cast<T>().Where(dataset => dataset.OutCycle.Equals((string)_cycle)));
                datasets.Clear();
                datasets.AddRange(selection);
            }
            return datasets;
        }

        private async Task GetData(NavigationParameters parameters)
        {
            _bemData = _cimmytDbOperations.GetBemData();
            if (_bemData.IsEmpty)
            {
                _bemData = await BemData.LoadBEMData();
                _cimmytDbOperations.SaveCostos(_bemData.Costo);
                _cimmytDbOperations.SaveIngresos(_bemData.Ingreso);
                _cimmytDbOperations.SaveRendimientos(_bemData.Rendimiento);
                _cimmytDbOperations.SaveUtilidades(_bemData.Utilidad);
            }

            parameters.TryGetValue("dataset", out _dataset);
            parameters.TryGetValue("year", out _year);
            parameters.TryGetValue("cycle", out _cycle);

            var datasets = new List<BemDataset>();
            switch (_dataset)
            {
                case "Costo":
                    datasets.AddRange(_bemData.Costo);
                    Datasets = FilterDatasets<Costo>(datasets);
                    break;

                case "Ingreso":
                    datasets.AddRange(_bemData.Ingreso);
                    Datasets = FilterDatasets<Ingreso>(datasets);
                    break;

                case "Rendimiento":
                    datasets.AddRange(_bemData.Rendimiento);
                    Datasets = FilterDatasets<Rendimiento>(datasets);
                    break;

                case "Utilidad":
                    datasets.AddRange(_bemData.Utilidad);
                    Datasets = FilterDatasets<Utilidad>(datasets);
                    break;
            }
        }
    }
}