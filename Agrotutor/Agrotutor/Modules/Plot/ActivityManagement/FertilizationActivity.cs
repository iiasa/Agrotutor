using System.Collections.Generic;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class FertilizationActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                ActivityNameTextVisibility = false,
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
                    "Aplicación de fertilizante foliar",
                    "Aplicación de fertilizante orgánico",
                    "Fertilización química al suelo"
                },
                ActivityIcon = "fertilizer_small.png",
                ActivityTitle = "Fertilización"
            };
        }
    }
}