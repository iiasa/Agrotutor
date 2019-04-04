using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class SoilImproversActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityNameVisibility = true,
                ActivityDateVisibility = true,
                ActivityNameListVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = true,
                AmountSoldVisibility = false,
                SellingPriceVisibility = false,
                CommentVisibility = true,
                DosageVisibility = true,
                AppliedAmountVisibility = true,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                PlotAreaVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("soil_improvers_organic_fertilizer"),
                    _stringLocalizer.GetString("soil_improvers_dolomite_lime"),
                    _stringLocalizer.GetString("soil_improvers_stubble_handling"),
                    _stringLocalizer.GetString("soil_improvers_other")
                },
                ActivityIcon = "soil_improv.png",
                ActivityTitle = _stringLocalizer.GetString("soil_improvers")
            };
        }

        public SoilImproversActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}