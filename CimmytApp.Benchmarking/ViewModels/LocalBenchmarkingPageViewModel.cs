namespace CimmytApp.Benchmarking.ViewModels
{
    using Prism.Mvvm;

    using DTO.BEM;
    using Prism.Navigation;
    using System.Collections.Generic;

    public class LocalBenchmarkingPageViewModel : BindableBase, INavigationAware
    {
        private BemData _bemData;
        private object _dataset;
        private object _year;
        private object _cycle;
        private List<BemDataset> _datasets;
        public List<BemDataset> Datasets
        {
            get { return _datasets; }
            set
            {
                SetProperty(ref _datasets, value);
            }
        }



        public LocalBenchmarkingPageViewModel()
        {
            _bemData = new BemData();
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
                        foreach (Costo costo in datasets)
                        {
                            if (costo.Year.Equals((string)_year))
                            {
                                selection.Add(costo);
                            }

                        }
                    }
                    datasets.Clear();
                    datasets.AddRange(selection);
                    selection = new List<Costo>();
                    if (_cycle != null)
                    {
                        foreach (Costo costo in datasets)
                        {
                            if (costo.AgriculturalCycle.Equals((string)_cycle))
                            {
                                selection.Add(costo);
                            }

                        }
                    }
                    datasets.Clear();
                    datasets.AddRange(selection);
                    break;

                case "Ingreso":
                    datasets.AddRange(_bemData.Ingreso);
                    var selection1 = new List<Ingreso>();
                    if (_year != null)
                    {
                        foreach (Ingreso ingreso in datasets)
                        {
                            if (ingreso.Year.Equals((string)_year))
                            {
                                selection1.Add(ingreso);
                            }

                        }
                    }
                    datasets.Clear();
                    datasets.AddRange(selection1);
                    selection1 = new List<Ingreso>();
                    if (_cycle != null)
                    {
                        foreach (Ingreso ingreso in datasets)
                        {
                            if (ingreso.AgriculturalCycle.Equals((string)_cycle))
                            {
                                selection1.Add(ingreso);
                            }

                        }
                    }
                    datasets.Clear();
                    datasets.AddRange(selection1);
                    break;

                case "Rendimiento":
                    datasets.AddRange(_bemData.Rendimiento);
                    var selection2 = new List<Rendimiento>();
                    if (_year != null)
                    {
                        foreach (Rendimiento rendimiento in datasets)
                        {
                            if (rendimiento.Year.Equals((string)_year))
                            {
                                selection2.Add(rendimiento);
                            }

                        }
                    }
                    datasets.Clear();
                    datasets.AddRange(selection2);
                    selection2 = new List<Rendimiento>();
                    if (_cycle != null)
                    {
                        foreach (Rendimiento rendimiento in datasets)
                        {
                            if (rendimiento.AgriculturalCycle.Equals((string)_cycle))
                            {
                                selection2.Add(rendimiento);
                            }

                        }
                    }
                    datasets.Clear();
                    datasets.AddRange(selection2);
                    break;

                case "Utilidad":
                    datasets.AddRange(_bemData.Utilidad);
                    var selection3 = new List<Utilidad>();
                    if (_year != null)
                    {
                        foreach (Utilidad utilidad in datasets)
                        {
                            if (utilidad.Year.Equals((string)_year))
                            {
                                selection3.Add(utilidad);
                            }

                        }
                    }
                    datasets.Clear();
                    datasets.AddRange(selection3);
                    selection3 = new List<Utilidad>();
                    if (_cycle != null)
                    {
                        foreach (Utilidad utilidad in datasets)
                        {
                            if (utilidad.AgriculturalCycle.Equals((string)_cycle))
                            {
                                selection3.Add(utilidad);
                            }

                        }
                    }
                    datasets.Clear();
                    datasets.AddRange(selection3);
                    break;
            }

            Datasets = datasets;
        }
    }
}