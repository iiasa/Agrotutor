using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

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
                ActivityNameListVisibility = true,
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
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("other_multiuse_conservation"),
                    _stringLocalizer.GetString("other_multiuse_traditional"),
                    _stringLocalizer.GetString("other_multiuse_miaf"),
                    _stringLocalizer.GetString("other_agriculture_contract"),
                    _stringLocalizer.GetString("other_conservation_filtering"),
                    _stringLocalizer.GetString("other_conservation_living_barriers"),
                    _stringLocalizer.GetString("other_conservation_dykes"),
                    _stringLocalizer.GetString("other_conservation_contreo"),
                    _stringLocalizer.GetString("other_conservation_other"),
                    _stringLocalizer.GetString("other_crop_rotation"),
                    _stringLocalizer.GetString("other_other")
                },
                ActivityIcon = "add.png",
                ActivityTitle = _stringLocalizer.GetString("other_activities")
            };
        }

        public OtherActivitiesActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}