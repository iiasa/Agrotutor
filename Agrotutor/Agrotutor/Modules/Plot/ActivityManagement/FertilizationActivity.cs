using System.Collections.Generic;
using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class FertilizationActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = true,
                ActivityNameListVisibility = true,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = true,
                AmountSoldVisibility = false,
                SellingPriceVisibility = false,
                CommentVisibility = true,
                DosageVisibility = true,
                AppliedAmountVisibility = true,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                PlotAreaVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("fertilization_diag_soil"),
                    _stringLocalizer.GetString("fertilization_diag_tissue"),
                    _stringLocalizer.GetString("fertilization_diag_rich_strip"),
                    _stringLocalizer.GetString("fertilization_diag_green_seeker"),
                    _stringLocalizer.GetString("fertilization_diag_green_sat"),
                    _stringLocalizer.GetString("fertilization_diag_other"),
                    _stringLocalizer.GetString("fertilization_inorg_manual"),
                    _stringLocalizer.GetString("fertilization_inorg_mechanical"),
                    _stringLocalizer.GetString("fertilization_inorg_yunta"),
                    _stringLocalizer.GetString("fertilization_inorg_superficial"),
                    _stringLocalizer.GetString("fertilization_inorg_foliar"),
                    _stringLocalizer.GetString("fertilization_inorg_fertiriego"),
                    _stringLocalizer.GetString("fertilization_biofertilizer_manual"),
                    _stringLocalizer.GetString("fertilization_biofertilizer_mechanical"),
                    _stringLocalizer.GetString("fertilization_biofertilizer_yunta"),
                    _stringLocalizer.GetString("fertilization_biofertilizer_superficial"),
                    _stringLocalizer.GetString("fertilization_biofertilizer_foliar"),
                    _stringLocalizer.GetString("fertilization_biofertilizer_fertiriego"),
                    _stringLocalizer.GetString("fertilization_other")
                },
                ActivityIcon = "fertilization.png",
                ActivityTitle = _stringLocalizer.GetString("fertilizer")
            };
        }

        public FertilizationActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}
