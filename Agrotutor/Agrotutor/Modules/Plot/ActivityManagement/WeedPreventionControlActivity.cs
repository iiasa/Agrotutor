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
                ActivityNameVisibility = false,
                ActivityNameTextVisibility = false,
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
                    _stringLocalizer.GetString("post_planting_herbicide"),
                    _stringLocalizer.GetString("pre_planting_herbicide"),
                    _stringLocalizer.GetString("physical_weed_control")
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