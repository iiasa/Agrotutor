using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class OtherActivitiesActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = true,
                ActivityNameTextVisibility = true,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                SellingPriceVisibility = false,
                AmountSoldVisibility = false,
                CommentVisibility = true,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PlotAreaVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityName = _stringLocalizer.GetString("other_activities"),
                ActivityIcon = "add.png",
                ActivityTitle = _stringLocalizer.GetString("other_activities")
            };
        }

        public OtherActivitiesActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}