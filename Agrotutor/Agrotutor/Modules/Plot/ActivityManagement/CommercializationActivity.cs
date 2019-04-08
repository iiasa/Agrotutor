using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class CommercializationActivity : ActivityBaseClass
    {
        public CommercializationActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {}

        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = true,
                ActivityNameListVisibility = true,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                AmountSoldVisibility = true,
                SellingPriceVisibility = true,
                CommentVisibility = true,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                PlotAreaVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("commercialization_direct"),
                    _stringLocalizer.GetString("commercialization_intermediary"),
                    _stringLocalizer.GetString("commercialization_other")
                },
                ActivityIcon = "sales.png",
                ActivityTitle = _stringLocalizer.GetString("commercialization")
            };
        }
    }
}