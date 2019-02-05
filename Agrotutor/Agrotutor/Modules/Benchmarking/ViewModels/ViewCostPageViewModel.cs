namespace Agrotutor.Modules.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Localization;
    using Prism.Navigation;

    using Core;
    using Core.Entities;

    public class ViewCostPageViewModel : ViewModelBase, INavigatedAware
    {
        private double avg;

        private List<Cost> datasets;

        private double max;

        private double min;

        public ViewCostPageViewModel(IStringLocalizer<ViewCostPageViewModel> localizer, INavigationService navigator)
            : base(navigator, localizer)
        {
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
                    NavigationService.GoBackAsync();
                }
            }
            else
            {
                NavigationService.GoBackAsync();
            }
        }
    }
}
