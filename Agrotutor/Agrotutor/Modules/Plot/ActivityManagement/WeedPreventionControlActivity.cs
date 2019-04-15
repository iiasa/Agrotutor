using System.Collections.Generic;
using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class WeedPreventionControlActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = true,
                ActivityNameListVisibility = true,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = true,
                AmountSoldVisibility = false,
                SellingPriceVisibility = false,
                CommentVisibility = true,
                DosageVisibility = true,
                AppliedAmountVisibility = true,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PlotAreaVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("weed_prevention_pre_sowing"),
                    _stringLocalizer.GetString("weed_prevention_post_sowing"),
                    _stringLocalizer.GetString("weed_prevention_cultural_work"),
                    _stringLocalizer.GetString("weed_prevention_other")
                },
                ActivityIcon = "weed_control.png",
                ActivityTitle = _stringLocalizer.GetString("deweeding")
            };
        }

        public WeedPreventionControlActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}