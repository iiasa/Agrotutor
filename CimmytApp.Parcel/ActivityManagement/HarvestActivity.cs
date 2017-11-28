namespace CimmytApp.Parcel.ActivityManagement
{
    using System.Collections.Generic;

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
                    "Cosecha ",
                    "Cosecha manual",
                    "Cosecha mecánica"
                },
                ActivityIcon = "harvest_small.png",
                ActivityTitle = "Cosecha"
            };
        }
    }
}