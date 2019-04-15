using System.Collections.Generic;
using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class SowingActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityNameVisibility = true,
                ActivityDateVisibility = true,
                ActivityNameListVisibility = true,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                AmountSoldVisibility = false,
                SellingPriceVisibility = false,
                CommentVisibility = true,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = true,
                PlantingDensityVisibility = true,
                PlotAreaVisibility = true,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("sowing_traditional"),
                    _stringLocalizer.GetString("sowing_add_cover"),
                    _stringLocalizer.GetString("sowing_add_high_market_value"),
                    _stringLocalizer.GetString("sowing_add_new"),
                    _stringLocalizer.GetString("sowing_other")
                },
                ActivityIcon = "sowing.png",
                ActivityTitle = _stringLocalizer.GetString("sowing")
            };
        }

        public SowingActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}