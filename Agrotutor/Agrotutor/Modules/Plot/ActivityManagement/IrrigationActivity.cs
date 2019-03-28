using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class IrrigationActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = true,
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
                PlotAreaVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("harvest_manual"),
                    _stringLocalizer.GetString("harvest_mechanical"),
                    _stringLocalizer.GetString("harvest_other")
                },
                ActivityIcon = "irrigation.png",
                ActivityTitle = _stringLocalizer.GetString("irrigation")
            };
        }

        public IrrigationActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}