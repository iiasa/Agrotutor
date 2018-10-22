namespace CimmytApp.Parcel.ActivityManagement
{
    using System.Collections.Generic;

    public class PestAndDiseaseControlAndPreventionActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                ActivityNameTextVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedAmountVisibility = true,
                AppliedProductsVisibility = true,
                DosageVisibility = true,
                PerformanceVisibility = false,
                PlantingDensityVisibility = false,
                ProductObtainedVisibility = false,
                VarietySownVisibility = false,
                ActivityNameList = new List<string>
                {
                    "Aplicación de fungicidas",
                    "Aplicación de insecticidas"
                },
                ActivityIcon = "cockroach_small.png",
                ActivityTitle = "Control y prevención de plagas y enfermedades"
            };
        }
    }
}