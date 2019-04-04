using Agrotutor.ViewModels;
using Prism.Commands;

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
        public static string YieldsParameterName = "BENCHMARKING_YIELD_PARAMETER";
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
                Min = Math.Round(value.Select(x => double.Parse(x.Performance)).Min(), 1);
                Max = Math.Round(value.Select(x => double.Parse(x.Performance)).Max(), 1);
                Avg = Math.Round(value.Select(x => double.Parse(x.Performance)).Average(), 1);
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
            if (this.Datasets==null|| Datasets.Count==0)
            {
                if (parameters.ContainsKey(YieldsParameterName))
                {
                    parameters.TryGetValue(YieldsParameterName, out List<Yield> yield);
                    if (yield != null)
                    {
                        this.Datasets = yield;
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

            base.OnNavigatedTo(parameters);
        }
        public DelegateCommand ShowAbout => new DelegateCommand(async () =>
        {
            var param = new NavigationParameters { { "page", WebContentPageViewModel.LocalBenchmarking } };
            await NavigationService.NavigateAsync("WebContentPage", param);
        });
    }
}
