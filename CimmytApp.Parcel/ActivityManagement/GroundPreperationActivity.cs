namespace CimmytApp.Parcel.ActivityManagement
{
    public class GroundPreperationActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = true,

                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityName = "Preparación del terreno",
                ActivityIcon = "shovel_small.png",
                ActivityTitle = "Preparación del terreno"
            };
        }
    }
}