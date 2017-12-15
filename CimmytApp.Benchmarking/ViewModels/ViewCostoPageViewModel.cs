namespace CimmytApp.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.DTO.BEM;
    using Helper.HTTP;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class ViewCostoPageViewModel : BindableBase, INavigationAware
    {
        private List<Costo> _datasets;

        private bool _isLoading;

        private List<Dataset> _stats;

        public List<Costo> Datasets
        {
            get => _datasets;
            set => SetProperty(ref _datasets, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public List<Dataset> Stats
        {
            get => _stats;
            set => SetProperty(ref _stats, value);
        }

        public void CalculateStats()
        {
            IsLoading = true;
            Datasets = new List<Costo>(Datasets.OrderBy(x => double.Parse(x.ProductionCost)));
            var min = double.Parse(Datasets.ElementAt(0)?.ProductionCost);
            var max = double.Parse(Datasets.ElementAt(Datasets.Count - 1)?.ProductionCost);
            var q1 = double.Parse(Datasets.ElementAt((int)Math.Floor(Datasets.Count / 4.0))?.ProductionCost);
            var q2 = double.Parse(Datasets.ElementAt((int)Math.Floor(Datasets.Count / 2.0))?.ProductionCost);
            var q3 = double.Parse(Datasets.ElementAt((int)Math.Floor(3 * Datasets.Count / 4.0))?.ProductionCost);
            Stats = new List<Dataset>
            {
                new Dataset
                {
                    Value = min,
                    Category = "Min"
                },
                new Dataset
                {
                    Value = q1,
                    Category = "25%"
                },
                new Dataset
                {
                    Value = q2,
                    Category = "50%"
                },
                new Dataset
                {
                    Value = q3,
                    Category = "75%"
                },
                new Dataset
                {
                    Value = max,
                    Category = "Max"
                }
            };
            IsLoading = false;
        }

        public async void LoadData()
        {
            Datasets = await RequestJson.Get<List<Costo>>(
                "http://104.239.158.49/api.php?type=costo&tkn=E31C5F8478566357BA6875B32DC59");
            CalculateStats();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            LoadData();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public class Dataset
        {
            public string Category { get; set; }

            public double Value { get; set; }
        }
    }
}