using System.Collections.Generic;
using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class HarvestActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                ActivityNameListVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                AmountSoldVisibility = false,
                SellingPriceVisibility = false,
                CommentVisibility = true,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = true,
                PlotAreaVisibility = false,
                ProductObtainedVisibility = true,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("irrigation_sprinkler"),
                    _stringLocalizer.GetString("irrigation_drip"),
                    _stringLocalizer.GetString("irrigation_rolled"),
                    _stringLocalizer.GetString("irrigation_other")
                },
                ActivityIcon = "harvest.png",
                ActivityTitle = _stringLocalizer.GetString("harvest")
            };
        }

        public HarvestActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}