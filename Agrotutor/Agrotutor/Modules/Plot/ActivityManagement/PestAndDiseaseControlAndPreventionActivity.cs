using System.Collections.Generic;
using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public class PestAndDiseaseControlAndPreventionActivity : ActivityBaseClass
    {
        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                ActivityNameTextVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedAmountVisibility = true,
                AppliedProductsVisibility = true,
                DosageVisibility = true,
                PerformanceVisibility = false,
                PlantingDensityVisibility = false,
                ProductObtainedVisibility = false,
                VarietySownVisibility = false,
                ActivityNameList = new List<string>
                {
                    _stringLocalizer.GetString("fungicides"),
                    _stringLocalizer.GetString("insecticides")
                },
                ActivityIcon = "pest_control.png",
                ActivityTitle = _stringLocalizer.GetString("pest_control")
            };
        }

        public PestAndDiseaseControlAndPreventionActivity(IStringLocalizer<ActivityDetailViewModel> stringLocalizer) : base(stringLocalizer)
        {
        }
    }
}