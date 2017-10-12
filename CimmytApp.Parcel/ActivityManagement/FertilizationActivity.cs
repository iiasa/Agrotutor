using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimmytApp.Parcel.ActivityManagement
{
   public class FertilizationActivity : ActivityBaseClass
    {
        // public override ActivityDynamicUIVisibility ActivityDynamicUIVisibility { get; set; }

        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = true,
                DosageVisibility = true,
                AppliedAmountVisibility = true,
                VarietySownVisibility = false,
                PlantingDensityVisibility = false,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string> { "Resiembra", "Siembra" },
            };
        }
    }
}
