namespace Agrotutor.Modules.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Localization;
    using Prism.Navigation;

    using Core;
    using Core.Entities;

    public class ViewYieldPageViewModel : ViewModelBase, INavigatedAware
    {
        private double avg;

        private List<Yield> datasets;

        private double max;

        private double min;

        public ViewYieldPageViewModel(
            IStringLocalizer<ViewYieldPageViewModel> localizer,
            INavigationService navigator)
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

        public List<Yield> Datasets
        {
            get => this.datasets;
            set
            {
                SetProperty(ref this.datasets, value);
                Min = value.Select(x => double.Parse(x.Performance)).Min();
                Max = value.Select(x => double.Parse(x.Performance)).Max();
                Avg = Math.Round(value.Select(x => double.Parse(x.Performance)).Average(), 2);
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
            if (parameters.ContainsKey("Yield"))
            {
                parameters.TryGetValue("Yield", out List<Yield> yield);
                if (yield != null)
                {
                    this.datasets = yield;
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
