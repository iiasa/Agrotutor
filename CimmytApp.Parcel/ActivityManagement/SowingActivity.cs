namespace CimmytApp.Parcel.ActivityManagement
{
    using System.Collections.Generic;

    public class SowingActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityNameVisibility = false,
                ActivityDateVisibility = true,
                ActivityNameTextVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = true,
                PlantingDensityVisibility = true,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    "Resiembra",
                    "Siembra"
                },
                ActivityIcon = "sowing_small.png",
                ActivityTitle = "Siembra"
            };
        }
    }
}