using System.Linq;

namespace CimmytApp.Benchmarking.ViewModels
{
	using System.Collections.Generic;
    using Prism.Mvvm;

    using DTO.BEM;
    using Prism.Navigation;

    public class LocalBenchmarkingPageViewModel : BindableBase, INavigationAware
    {
        private readonly BemData _bemData;
        private object _cycle;
        private object _dataset;
        private List<BemDataset> _datasets;
        private object _year;

        public LocalBenchmarkingPageViewModel()
        {
            _bemData = new BemData();
        }

        public List<BemDataset> Datasets
        {
            get => _datasets;
            set => SetProperty(ref _datasets, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        private List<BemDataset> FilterDatasets<T>(List<BemDataset> bemDatasets) where T : BemDataset{
            
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

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            parameters.TryGetValue("dataset", out _dataset);
            parameters.TryGetValue("year", out _year);
            parameters.TryGetValue("cycle", out _cycle);

            List<BemDataset> datasets = new List<BemDataset>();
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