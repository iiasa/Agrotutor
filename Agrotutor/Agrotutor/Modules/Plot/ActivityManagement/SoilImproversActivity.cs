namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class SoilImproversActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityNameVisibility = true,
                ActivityDateVisibility = true,
                ActivityNameTextVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = true,
                DosageVisibility = true,
                AppliedAmountVisibility = true,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityName = "Aplicación de mejoradores de suelo",
                ActivityIcon = "flask_small.png",
                ActivityTitle = "Aplicación de mejoradores de suelo"
            };
        }
    }
}