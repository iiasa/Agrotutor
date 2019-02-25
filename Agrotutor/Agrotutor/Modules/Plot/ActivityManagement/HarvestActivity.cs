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
                ActivityNameTextVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = true,
                ProductObtainedVisibility = true,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("harvest"),
                    _stringLocalizer.GetString("manual_harvest"),
                    _stringLocalizer.GetString("mechanical_harvest")
                },
                ActivityIcon = "harvest_small.png",
                ActivityTitle = _stringLocalizer.GetString("harvest")
            };
        }

        public HarvestActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}