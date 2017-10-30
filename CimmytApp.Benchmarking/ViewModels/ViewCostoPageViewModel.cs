namespace CimmytApp.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.BEM;
    using Helper.HTTP;

    /// <inheritdoc cref="BindableBase" />
    /// <summary>
    /// Defines the <see cref="T:CimmytApp.Benchmarking.ViewModels.ViewCostoPageViewModel" />
    /// </summary>
    public class ViewCostoPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _isLoading
        /// </summary>
        private bool _isLoading;

        /// <summary>
        /// Gets or sets a value indicating whether IsLoading
        /// </summary>
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        /// <summary>
        /// Defines the _datasets
        /// </summary>
        private ObservableCollection<Costo> _datasets;

        /// <summary>
        /// Defines the _stats
        /// </summary>
        private ObservableCollection<Dataset> _stats;

        /// <summary>
        /// Gets or sets the Stats
        /// </summary>
        public ObservableCollection<Dataset> Stats { get => _stats; set => SetProperty(ref _stats, value); }

        /// <summary>
        /// Gets or sets the Datasets
        /// </summary>
        public ObservableCollection<Costo> Datasets { get => _datasets; set => SetProperty(ref _datasets, value); }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="T:Prism.Navigation.NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            LoadData();
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        public async void LoadData()
        {
            var costo = await RequestJson.Get<List<Costo>>("http://104.239.158.49/api.php?type=costo&tkn=E31C5F8478566357BA6875B32DC59");
            var ds = new ObservableCollection<Costo>();
            foreach (var dataset in costo)
            {
                ds.Add(dataset);
            }
            Datasets = ds;
            CalculateStats();
        }

        /// <summary>
        /// The CalculateStats
        /// </summary>
        public void CalculateStats()
        {
            IsLoading = true;
            Datasets = new ObservableCollection<Costo>(Datasets.OrderBy(x => int.Parse(x.ProductionCost)));
            Stats = new ObservableCollection<Dataset>
            {
                new Dataset{Value = int.Parse(Datasets.ElementAt(0)?.ProductionCost), Category = "Min"},
                new Dataset{Value = int.Parse(Datasets.ElementAt((int) Math.Floor(Datasets.Count / 4.0))?.ProductionCost), Category = "25%"},
                new Dataset{Value = int.Parse(Datasets.ElementAt((int) Math.Floor( Datasets.Count / 2.0))?.ProductionCost), Category = "50%"},
                new Dataset{Value = int.Parse(Datasets.ElementAt((int) Math.Floor(3 * Datasets.Count / 4.0))?.ProductionCost), Category = "75%"},
                new Dataset{Value = int.Parse(Datasets.ElementAt(Datasets.Count - 1)?.ProductionCost), Category = "Max"}
            };
            IsLoading = false;
        }

        /// <summary>
        /// Defines the <see cref="Dataset" />
        /// </summary>
        public class Dataset
        {
            /// <summary>
            /// Gets or sets the Category
            /// </summary>
            public string Category { get; set; }

            /// <summary>
            /// Gets or sets the Value
            /// </summary>
            public double Value { get; set; }
        }
    }
}