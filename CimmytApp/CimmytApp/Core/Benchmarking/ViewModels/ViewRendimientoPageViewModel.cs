namespace CimmytApp.Core.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.DTO.BEM;
    using CimmytApp.ViewModels;
    using Helper.HTTP;
    using Microsoft.Extensions.Localization;
    using Prism.Navigation;

    /// <summary>
    ///     Defines the <see cref="ViewRendimientoPageViewModel" />
    /// </summary>
    public class ViewRendimientoPageViewModel : ViewModelBase, INavigatedAware
    {
        public ViewRendimientoPageViewModel(IStringLocalizer<ViewRendimientoPageViewModel> localizer)
            : base(localizer)
        {
        }

        /// <summary>
        ///     Defines the _datasets
        /// </summary>
        private List<Rendimiento> _datasets;

        private bool _isLoading;
        private List<Dataset> _stats;

        /// <summary>
        ///     Gets or sets the Datasets
        /// </summary>
        public List<Rendimiento> Datasets
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
            Datasets = new List<Rendimiento>(Datasets.OrderBy(x => double.Parse(x.Performance)));
            var min = double.Parse(Datasets.ElementAt(0)?.Performance);
            var max = double.Parse(Datasets.ElementAt(Datasets.Count - 1)?.Performance);
            var q1 = double.Parse(Datasets.ElementAt((int)Math.Floor(Datasets.Count / 4.0))?.Performance);
            var q2 = double.Parse(Datasets.ElementAt((int)Math.Floor(Datasets.Count / 2.0))?.Performance);
            var q3 = double.Parse(Datasets.ElementAt((int)Math.Floor(3 * Datasets.Count / 4.0))?.Performance);
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

        /// <summary>
        ///     The LoadData
        /// </summary>
        public async void LoadData()
        {
            IsLoading = true;
            Datasets = await RequestJson.Get<List<Rendimiento>>(
                "http://104.239.158.49/api.php?type=rendimiento&tkn=E31C5F8478566357BA6875B32DC59");
            CalculateStats();
            IsLoading = false;
        }

        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            LoadData();
        }
    }

    public class Dataset
    {
        /// <summary>
        ///     Gets or sets the Category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        ///     Gets or sets the Value
        /// </summary>
        public double Value { get; set; }
    }
}