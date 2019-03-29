using System.Collections.Generic;
using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class PostHarvestStorageActivity : ActivityBaseClass
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
                PerformanceVisibility = false,
                PlotAreaVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("postharvest_storage_traditional"),
                    _stringLocalizer.GetString("postharvest_storage_hermetic_containers"),
                    _stringLocalizer.GetString("postharvest_storage_pheromones"),
                    _stringLocalizer.GetString("postharvest_storage_drying"),
                    _stringLocalizer.GetString("postharvest_storage_inert_lime"),
                    _stringLocalizer.GetString("postharvest_storage_inert_diatom_earth"),
                    _stringLocalizer.GetString("postharvest_storage_inert_silicon"),
                    _stringLocalizer.GetString("postharvest_storage_inert_other"),
                    _stringLocalizer.GetString("postharvest_storage_sheller"),
                    _stringLocalizer.GetString("postharvest_storage_sex_pheromones"),
                    _stringLocalizer.GetString("postharvest_storage_other")
                },
                ActivityIcon = "harvest_storage.png",
                ActivityTitle = _stringLocalizer.GetString("post_harvest_storage")
            };
        }

        public PostHarvestStorageActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}