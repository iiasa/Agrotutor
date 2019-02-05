using System.Collections.Generic;

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
                ActivityNameTextVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    "Almacenamiento poscosecha con tecnologías herméticas",
                    "Almacenamiento poscosecha tradicional"
                },
                ActivityIcon = "storage_small.png",
                ActivityTitle = "Almacenamiento poscosecha"
            };
        }
    }
}