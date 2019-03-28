using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class IrrigationActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = true,
                ActivityNameTextVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                AmountSoldVisibility = false,
                SellingPriceVisibility = false,
                CommentVisibility = true,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PlotAreaVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityName = _stringLocalizer.GetString("irrigation"),
                ActivityIcon = "irrigation.png",
                ActivityTitle = _stringLocalizer.GetString("irrigation")
            };
        }

        public IrrigationActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}