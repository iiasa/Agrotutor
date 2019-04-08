using System;
using System.Linq;
using Agrotutor.Modules.Ciat.Types;
using Agrotutor.Modules.Ciat.ViewModels;

namespace Agrotutor.Modules.Ciat.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CiatPage : ContentPage
    {
        private readonly CiatPageViewModel viewModel;

        public CiatPage()
        {
            InitializeComponent();
            this.viewModel = (CiatPageViewModel)BindingContext;
            this.viewModel.PropertyChanged += (sender, args) =>
            {
                if (!args.PropertyName.Equals(nameof(this.viewModel.CurrentData))) return;
                
                UpdateRecommendations();
            };
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

                if (currentData.SuboptimalCultivars!=null&&currentData.SuboptimalCultivars.Any())
                {
                    cultivarToAvoidText = $"Avoid cultivars: {string.Join(", ", currentData.SuboptimalCultivars)}";
                }

                if (currentData.OptimalCultivars != null && currentData.OptimalCultivars.Any())
                {
                    cultivarToUseText = $"Use cultivars: {string.Join(", ", currentData.OptimalCultivars)}";
                }

                if (currentData.SeedDensityUnit != null)
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
