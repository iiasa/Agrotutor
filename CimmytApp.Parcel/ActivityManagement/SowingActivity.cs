using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimmytApp.Parcel.ActivityManagement
{
   public class SowingActivity : ActivityBaseClass
    {
       // public override ActivityDynamicUIVisibility ActivityDynamicUIVisibility { get; set; }

        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityNameVisibility = false,
               
                ActivityDateVisibility = true,
                ActivityTotalCostVisibility = true,
                AppliedProductsVisibility = false,
                DosageVisibility = false,
                AppliedAmountVisibility = false,
                VarietySownVisibility = true,
                PlantingDensityVisibility = true,
                PerformanceVisibility = false,
                ProductObtainedVisibility = false,
                ActivityNameList = new List<string> {"Resiembra", "Siembra"},


            };
        }
    }
}
