using System.Linq;

namespace CimmytApp.Benchmarking.ViewModels
{
    using Prism.Mvvm;

    using DTO.BEM;
    using Prism.Navigation;
    using System.Collections.Generic;

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
                    var selection = new List<Costo>();
                    if (_year != null)
                    {
                        selection.AddRange(datasets.Cast<Costo>().Where(costo => costo.Year.Equals((string)_year)));
                    }
                    datasets.Clear();
                    datasets.AddRange(selection);
                    selection = new List<Costo>();
                    if (_cycle != null)
                    {
                        selection.AddRange(datasets.Cast<Costo>().Where(costo => costo.AgriculturalCycle.Equals((string)_cycle)));
                    }
                    datasets.Clear();
                    datasets.AddRange(selection);
                    break;

                case "Ingreso":
                    datasets.AddRange(_bemData.Ingreso);
                    var selection1 = new List<Ingreso>();
                    if (_year != null)
                    {
                        selection1.AddRange(datasets.Cast<Ingreso>().Where(ingreso => ingreso.Year.Equals((string)_year)));
                    }
                    datasets.Clear();
                    datasets.AddRange(selection1);
                    selection1 = new List<Ingreso>();
                    if (_cycle != null)
                    {
                        selection1.AddRange(datasets.Cast<Ingreso>().Where(ingreso => ingreso.AgriculturalCycle.Equals((string)_cycle)));
                    }
                    datasets.Clear();
                    datasets.AddRange(selection1);
                    break;

                case "Rendimiento":
                    datasets.AddRange(_bemData.Rendimiento);
                    var selection2 = new List<Rendimiento>();
                    if (_year != null)
                    {
                        selection2.AddRange(datasets.Cast<Rendimiento>().Where(rendimiento => rendimiento.Year.Equals((string)_year)));
                    }
                    datasets.Clear();
                    datasets.AddRange(selection2);
                    selection2 = new List<Rendimiento>();
                    if (_cycle != null)
                    {
                        selection2.AddRange(datasets.Cast<Rendimiento>().Where(rendimiento => rendimiento.AgriculturalCycle.Equals((string)_cycle)));
                    }
                    datasets.Clear();
                    datasets.AddRange(selection2);
                    break;

                case "Utilidad":
                    datasets.AddRange(_bemData.Utilidad);
                    var selection3 = new List<Utilidad>();
                    if (_year != null)
                    {
                        selection3.AddRange(datasets.Cast<Utilidad>().Where(utilidad => utilidad.Year.Equals((string)_year)));
                    }
                    datasets.Clear();
                    datasets.AddRange(selection3);
                    selection3 = new List<Utilidad>();
                    if (_cycle != null)
                    {
                        selection3.AddRange(datasets.Cast<Utilidad>().Where(utilidad => utilidad.AgriculturalCycle.Equals((string)_cycle)));
                    }
                    datasets.Clear();
                    datasets.AddRange(selection3);
                    break;
            }

            Datasets = datasets;
        }
    }
}