using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimmytApp.Parcel.ActivityManagement
{
   public class PostHarvestStorageActivity : ActivityBaseClass
    {
        // public override ActivityDynamicUIVisibility ActivityDynamicUIVisibility { get; set; }

        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string> { "Almacenamiento poscosecha con tecnologías herméticas", "Almacenamiento poscosecha tradicional" }
            };
        }
    }
}
