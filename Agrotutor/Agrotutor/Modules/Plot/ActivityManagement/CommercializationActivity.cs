using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class CommercializationActivity : ActivityBaseClass
    {
        public CommercializationActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {}

        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = true,
                ActivityNameTextVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                AmountSoldVisibility = true,
                SellingPriceVisibility = true,
                CommentVisibility = true,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                PlotAreaVisibility = false,
                ProductObtainedVisibility = false,
                ActivityName = _stringLocalizer.GetString("commercialization"),
                ActivityIcon = "sales.png",
                ActivityTitle = _stringLocalizer.GetString("commercialization")
            };
        }
    }
}