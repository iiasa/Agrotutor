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

    public class ViewIncomePageViewModel : ViewModelBase, INavigatedAware
    {
        public static string IncomesParameterName = "BENCHMARKING_INCOME_PARAMETER";
        private double avg;

        private List<Income> datasets;

        private double max;

        private double min;

        public ViewIncomePageViewModel(IStringLocalizer<ViewIncomePageViewModel> localizer, INavigationService navigator)
            : base(navigator, localizer)
        {
            Title = "IncomePage";
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
                Min = Math.Round(value.Select(x => double.Parse(x.IncomePerHa)).Min(), 0);
                Max = Math.Round(value.Select(x => double.Parse(x.IncomePerHa)).Max(), 0);
                Avg = Math.Round(value.Select(x => double.Parse(x.IncomePerHa)).Average(), 0);
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
            if (this.Datasets == null || Datasets.Count == 0)
            {
                if (parameters.ContainsKey(IncomesParameterName))
                {
                    parameters.TryGetValue(IncomesParameterName, out List<Income> income);
                    if (income != null)
                    {
                        this.Datasets = income;
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
