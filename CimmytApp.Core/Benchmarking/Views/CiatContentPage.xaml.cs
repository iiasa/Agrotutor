namespace CimmytApp.Core.Benchmarking.Views
{
    using System;
    using System.Linq;
    using CimmytApp.Core.Benchmarking.ViewModels;
    using CimmytApp.Core.DTO.Benchmarking;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CiatContentPage : ContentPage
    {
        private readonly CiatContentPageViewModel viewModel;

        public CiatContentPage()
        {
            InitializeComponent();
            this.viewModel = (CiatContentPageViewModel)BindingContext;
            this.viewModel.PropertyChanged += (sender, args) =>
            {
                if (!args.PropertyName.Equals(nameof(this.viewModel.CurrentData))) return;

                UpdateSelectionButtonStyle();
                UpdateColorBarValues();
                UpdateRecommendations();
            };
        }

        private void UpdateSelectionButtonStyle()
        {
            CiatData.CiatDataType type = this.viewModel.CurrentData.DataType;
            Style activeButtonStyle = (Style)Application.Current.Resources["ButtonGreen"];

            if (type == CiatData.CiatDataType.Irrigated)
            {
                this.BtnNonIrrigated.Style = null;
                this.BtnIrrigated.Style = activeButtonStyle;
            }
            else if (type == CiatData.CiatDataType.NonIrrigated)
            {
                this.BtnNonIrrigated.Style = activeButtonStyle;
                this.BtnIrrigated.Style = null;
            }
        }

        private void UpdateColorBarValues()
        {
            double yieldOld = this.viewModel.OldYield;
            double yieldMin = this.viewModel.CurrentData.YieldMin;
            double yieldMax = this.viewModel.CurrentData.YieldMax;

            double min = Math.Min(yieldOld, Math.Min(yieldMin, yieldMax));
            double max = Math.Max(yieldOld, Math.Max(yieldMin, yieldMax));
            double k = max - min;

            if (k == 0) k = 1; // TODO: check - will prevent from division by 0 exception but could break something...

            double yOld = (yieldOld - min) / k;
            double yMin = (yieldMin - min) / k;
            double yMax = (yieldMax - min) / k;


            AbsoluteLayout.SetLayoutBounds(this.MarkYieldMax, new Rectangle(0, 1-yMax, 1, 0.05));
            AbsoluteLayout.SetLayoutBounds(this.MarkYieldMin, new Rectangle(0, 1-yMin, 1, 0.05));
            AbsoluteLayout.SetLayoutBounds(this.MarkYieldOld, new Rectangle(0, 1-yOld, 1, 0.05));

        }

        private void UpdateRecommendations()
        {
            CiatData.CiatDataDetail currentData = this.viewModel?.CurrentData;
            string nitrogenText = "No recommendation on nitrogen.";
            string cultivarToAvoidText = "Avoid cultivars: -";
            string cultivarToUseText = "Use cultivars: -";
            string seedDensityText = "Seed density: -";

            if (currentData != null)
            {
                if (currentData.TotalNitrogen != null && currentData.TotalNitrogenUnit != null)
                {
                    nitrogenText = $"Apply {currentData.TotalNitrogen} {currentData.TotalNitrogenUnit} of nitrogen.";
                }

                if (currentData.SuboptimalCultivars?.Count > 0)
                {
                    cultivarToAvoidText = $"Avoid cultivars: {String.Join(", ", currentData.SuboptimalCultivars)}";
                }

                if (currentData.OptimalCultivars?.Count > 0)
                {
                    cultivarToUseText = $"Use cultivars: {String.Join(", ", currentData.OptimalCultivars)}";
                }

                if (currentData.SeedDensity != null && currentData.SeedDensityUnit != null)
                {
                    seedDensityText = $"Use a min density of {currentData.SeedDensity} {currentData.SeedDensityUnit}";
                }
            }

            this.lblNitrogen.Text = nitrogenText;
            this.lblAvoidCultivars.Text = cultivarToAvoidText;
            this.lblUseCultivars.Text = cultivarToUseText;
            this.lblSeedDensity.Text = seedDensityText;

        }
    }
}