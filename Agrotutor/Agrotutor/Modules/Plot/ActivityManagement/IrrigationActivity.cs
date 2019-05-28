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
                PlotAreaVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("irrigation_sprinkler"),
                    _stringLocalizer.GetString("irrigation_drip"),
                    _stringLocalizer.GetString("irrigation_rolled"),
                    _stringLocalizer.GetString("irrigation_other")
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