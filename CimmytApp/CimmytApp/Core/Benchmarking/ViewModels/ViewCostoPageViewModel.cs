namespace CimmytApp.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.ViewModels;

    using Microsoft.Extensions.Localization;

    using Prism.Navigation;

    public class ViewCostoPageViewModel : ViewModelBase, INavigatedAware
    {
        private double avg;

        private List<Cost> datasets;

        private double max;

        private double min;

        public ViewCostoPageViewModel(IStringLocalizer<ViewCostoPageViewModel> localizer, INavigationService navigator)
            : base(localizer)
        {
            Navigator = navigator;
            Min = 0;
            Max = 0;
            Avg = 0;
        }

        public double Avg
        {
            get => this.avg;
            set => SetProperty(ref this.avg, value);
        }

        public List<Cost> Datasets
        {
            get => this.datasets;
            set
            {
                SetProperty(ref this.datasets, value);
                Min = value.Select(x => double.Parse(x.ProductionCost)).Min();
                Max = value.Select(x => double.Parse(x.ProductionCost)).Max();
                Avg = Math.Round(value.Select(x => double.Parse(x.ProductionCost)).Average(), 2);
            }
        }

        public double Max
        {
            get => this.max;
            set => SetProperty(ref this.max, value);
        }

        public double Min
        {
            get => this.min;
            set => SetProperty(ref this.min, value);
        }

        public INavigationService Navigator { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Cost"))
            {
                parameters.TryGetValue("Cost", out List<Cost> cost);
                if (cost != null)
                {
                    this.datasets = cost;
                }
                else
                {
                    Navigator.GoBackAsync();
                }
            }
            else
            {
                Navigator.GoBackAsync();
            }
        }
    }
}