using System.Collections.Generic;
using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class PestAndDiseaseControlAndPreventionActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                ActivityNameListVisibility = true,
                ActivityTotalCostVisibility = true,
                AppliedAmountVisibility = true,
                AppliedProductsVisibility = true,
                AmountSoldVisibility = false,
                SellingPriceVisibility = false,
                CommentVisibility = true,
                DosageVisibility = true,
                PerformanceVisibility = false,
                PlantingDensityVisibility = false,
                PlotAreaVisibility = false,
                ProductObtainedVisibility = false,
                VarietySownVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("pest_control_fungicide"),
                    _stringLocalizer.GetString("pest_control_pesticide"),
                    _stringLocalizer.GetString("pest_control_pheromones"),
                    _stringLocalizer.GetString("pest_control_natural_enemies"),
                    _stringLocalizer.GetString("pest_control_pest_monitoring"),
                    _stringLocalizer.GetString("pest_control_seed_treatment"),
                    _stringLocalizer.GetString("pest_control_other")
                },
                ActivityIcon = "pest_control.png",
                ActivityTitle = _stringLocalizer.GetString("pest_control")
            };
        }

        public PestAndDiseaseControlAndPreventionActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}