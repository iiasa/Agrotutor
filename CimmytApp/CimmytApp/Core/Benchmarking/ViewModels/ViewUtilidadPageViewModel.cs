namespace CimmytApp.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.ViewModels;

    using Microsoft.Extensions.Localization;

    using Prism.Navigation;

    public class ViewUtilidadPageViewModel : ViewModelBase, INavigatedAware
    {
        private double avg;

        private List<Profit> datasets;

        private double max;

        private double min;

        public ViewUtilidadPageViewModel(IStringLocalizer<ViewUtilidadPageViewModel> localizer, INavigationService navigator)
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

        public List<Profit> Datasets
        {
            get => this.datasets;
            set
            {
                SetProperty(ref this.datasets, value);
                Min = value.Select(x => double.Parse(x.Rentability)).Min();
                Max = value.Select(x => double.Parse(x.Rentability)).Max();
                Avg = Math.Round(value.Select(x => double.Parse(x.Rentability)).Average(), 2);
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
            if (parameters.ContainsKey("Profit"))
            {
                parameters.TryGetValue("Profit", out List<Profit> profit);
                if (profit != null)
                {
                    this.datasets = profit;
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