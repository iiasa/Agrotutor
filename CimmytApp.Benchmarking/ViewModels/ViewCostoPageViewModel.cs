using System;
using System.Linq;

namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.BEM;

    public class ViewCostoPageViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Costo> _datasets;

        private ObservableCollection<Dataset> _stats;

        public ObservableCollection<Dataset> Stats
        {
            get => _stats;
            set => SetProperty(ref _stats, value);
        }

        public ObservableCollection<Costo> Datasets
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
            var ds = new ObservableCollection<Costo>();
            if (datasets == null) return;
            foreach (var dataset in (List<Costo>)datasets)
            {
                ds.Add(dataset);
            }
            Datasets = ds;
            CalculateStats();
        }

        public void CalculateStats()
        {
            Datasets = new ObservableCollection<Costo>(Datasets.OrderBy(x => int.Parse(x.ProductionCost)));
            Stats = new ObservableCollection<Dataset>
            {
                new Dataset{Value = int.Parse(Datasets.ElementAt(0)?.ProductionCost), Category = "Min"},
                new Dataset{Value = int.Parse(Datasets.ElementAt((int) Math.Floor(Datasets.Count / 4.0))?.ProductionCost), Category = "25%"},
                new Dataset{Value = int.Parse(Datasets.ElementAt((int) Math.Floor( Datasets.Count / 2.0))?.ProductionCost), Category = "50%"},
                new Dataset{Value = int.Parse(Datasets.ElementAt((int) Math.Floor(3 * Datasets.Count / 4.0))?.ProductionCost), Category = "75%"},
                new Dataset{Value = int.Parse(Datasets.ElementAt(Datasets.Count - 1)?.ProductionCost), Category = "Max"}
            };
        }

        public class Dataset
        {
            public string Category { get; set; }
            public double Value { get; set; }
        }
    }
}