namespace CimmytApp.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.ViewModels;
    using Helper.HTTP;
    using Microsoft.Extensions.Localization;
    using Prism.Navigation;

    public class ViewCostoPageViewModel : ViewModelBase, INavigatedAware
    {
        public ViewCostoPageViewModel(IStringLocalizer<ViewCostoPageViewModel> localizer)
            : base(localizer)
        {
        }

        private List<Cost> datasets;

        private bool isLoading;

        private List<Dataset> stats;

        public List<Cost> Datasets
        {
            get => this.datasets;
            set => SetProperty(ref this.datasets, value);
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => SetProperty(ref this.isLoading, value);
        }

        public List<Dataset> Stats
        {
            get => this.stats;
            set => SetProperty(ref this.stats, value);
        }

        public void CalculateStats()
        {
            Datasets = new List<Cost>(Datasets.OrderBy(x => double.Parse(x.ProductionCost)));
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
        }

        public async void LoadData()
        {
            IsLoading = true;
            Datasets = await RequestJson.Get<List<Cost>>(
                "http://104.239.158.49/api.php?type=costo&tkn=E31C5F8478566357BA6875B32DC59");
            CalculateStats();
            IsLoading = false;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            LoadData();
        }

        public class Dataset
        {
            public string Category { get; set; }

            public double Value { get; set; }
        }
    }
}