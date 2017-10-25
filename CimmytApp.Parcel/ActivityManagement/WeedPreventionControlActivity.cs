namespace CimmytApp.Parcel.ActivityManagement
{
    using System.Collections.Generic;

    public class WeedPreventionControlActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = true,
                DosageVisibility = true,
                AppliedAmountVisibility = true,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    "Aplicación de herbicidas después de la siembra",
                    "Aplicación de herbicidas presiembra",
                    "Labores culturales y control físico de malezas"
                },
                ActivityIcon = "weeds_small.png",
                ActivityTitle = "Control de prevención de malezas"
            };
        }
    }
}