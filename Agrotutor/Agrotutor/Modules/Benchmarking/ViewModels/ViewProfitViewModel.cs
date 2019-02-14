namespace Agrotutor.Modules.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Localization;
    using Prism.Navigation;

    using Core;
    using Core.Entities;

    public class ViewProfitPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string ProfitsParameterName = "BENCHMARKING_PROFIT_PARAMETER";
        private double avg;

        private List<Profit> datasets;

        private double max;

        private double min;

        public ViewProfitPageViewModel(IStringLocalizer<ViewProfitPageViewModel> localizer, INavigationService navigator)
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

        public List<Profit> Datasets
        {
            get => this.datasets;
            set
            {
                SetProperty(ref this.datasets, value);
                Min = value.Select(x => double.Parse(x.Rentability)).Min();
                Max = value.Select(x => double.Parse(x.Rentability)).Max();
                Avg = Math.Round(value.Select(x => double.Parse(x.Rentability)).Average(), 0);
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

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ProfitsParameterName))
            {
                parameters.TryGetValue(ProfitsParameterName, out List<Profit> profit);
                if (profit != null)
                {
                    this.Datasets = profit;
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
            base.OnNavigatedTo(parameters);
        }
    }
}
