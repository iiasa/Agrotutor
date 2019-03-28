using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class GroundPreperationActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = true,
                ActivityNameListVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                AmountSoldVisibility = false,
                SellingPriceVisibility = false,
                CommentVisibility = true,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                PlotAreaVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("ground_prep_level"),
                    _stringLocalizer.GetString("ground_prep_plot_level"),
                    _stringLocalizer.GetString("ground_prep_floor_level"),
                    _stringLocalizer.GetString("ground_prep_impl_conservation"),
                    _stringLocalizer.GetString("ground_prep_fert_reformer"),
                    _stringLocalizer.GetString("ground_prep_vertical rotating"),
                    _stringLocalizer.GetString("ground_prep_permanent_beds"),
                    _stringLocalizer.GetString("ground_prep_machinery"),
                    _stringLocalizer.GetString("ground_prep_other")
                },
                ActivityIcon = "land_prep.png",
                ActivityTitle = _stringLocalizer.GetString("ground_preparation"),
            };
        }

        public GroundPreperationActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}