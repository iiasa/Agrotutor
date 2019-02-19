using System;
using Agrotutor.Core;
using Agrotutor.Modules.Ciat.Types;
using Microsoft.Extensions.Localization;
using Prism.Commands;
using Prism.Navigation;

namespace Agrotutor.Modules.Ciat.ViewModels
{
    public class PotentialYieldPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string DataParameterName = "CIAT_DATA_PARAMETER";
        private CiatData _ciatData;
        private double _min;
        private double _max;
        private double _avg;

        public CiatData CiatData
        {
            get => _ciatData;
            set => SetProperty(ref _ciatData, value);
        }

        public PotentialYieldPageViewModel(INavigationService navigationService,
            IStringLocalizer<PotentialYieldPageViewModel> stringLocalizer)
            : base(navigationService, stringLocalizer)
        {

        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey(DataParameterName))
            {
                parameters.TryGetValue<CiatData>(DataParameterName, out var ciatData);
                CiatData = ciatData;
                IrrigatedClickedCommand.Execute();
            }
        }

        public double Min
        {
            get => _min;
            set => SetProperty(ref _min, value);
        }

        public double Max
        {
            get => _max;
            set => SetProperty(ref _max, value);
        }

        public double Avg
        {
            get => _avg;
            set => SetProperty(ref _avg, value);
        }

        public DelegateCommand NonIrrigatedClickedCommand =>
            new DelegateCommand(() =>
            {
                if (CiatData?.CiatDataNonIrrigated == null) return;
                Min = Math.Round(CiatData.CiatDataNonIrrigated.YieldMin,1);
                Max = Math.Round(CiatData.CiatDataNonIrrigated.YieldMax, 1);
                Avg = Math.Round((Min + Max) / 2, 1);
            });

        public DelegateCommand IrrigatedClickedCommand =>
            new DelegateCommand(() =>
            {
                if (CiatData?.CiatDataIrrigated == null) return;
                Min = Math.Round(CiatData.CiatDataIrrigated.YieldMin,1);
                Max = Math.Round(CiatData.CiatDataIrrigated.YieldMax,1);
                Avg = Math.Round((Min + Max) / 2,1);
            });
    }
}
