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
                ActivityNameVisibility = true,
                ActivityNameListVisibility = true,
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
                    _stringLocalizer.GetString("harvest_manual"),
                    _stringLocalizer.GetString("harvest_mechanical"),
                    _stringLocalizer.GetString("harvest_other")
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