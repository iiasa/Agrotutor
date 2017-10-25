namespace CimmytApp.Parcel.ActivityManagement
{
    using System.Collections.Generic;

    public class PostHarvestStorageActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string> { "Almacenamiento poscosecha con tecnologías herméticas", "Almacenamiento poscosecha tradicional" },
                ActivityIcon = "storage_small.png",
                ActivityTitle = "Almacenamiento poscosecha"
            };
        }
    }
}