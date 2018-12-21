namespace CimmytApp.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.DTO.BEM;
    using CimmytApp.ViewModels;

    using Microsoft.Extensions.Localization;

    using Prism.Navigation;

    public class ViewIngresoPageViewModel : ViewModelBase, INavigatedAware
    {
        private double avg;

        private List<Income> datasets;

        private double max;

        private double min;

        public ViewIngresoPageViewModel(IStringLocalizer<ViewIngresoPageViewModel> localizer, INavigationService navigator)
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

        public List<Income> Datasets
        {
            get => this.datasets;
            set
            {
                SetProperty(ref this.datasets, value);
                Min = value.Select(x => double.Parse(x.IncomePerHa)).Min();
                Max = value.Select(x => double.Parse(x.IncomePerHa)).Max();
                Avg = Math.Round(value.Select(x => double.Parse(x.IncomePerHa)).Average(), 2);
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
            if (parameters.ContainsKey("Income"))
            {
                parameters.TryGetValue("Income", out List<Income> income);
                if (income != null)
                {
                    this.datasets = income;
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