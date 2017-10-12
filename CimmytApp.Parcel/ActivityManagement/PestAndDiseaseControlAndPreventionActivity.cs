using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimmytApp.Parcel.ActivityManagement
{
   public class PestAndDiseaseControlAndPreventionActivity : ActivityBaseClass
    {
        // public override ActivityDynamicUIVisibility ActivityDynamicUIVisibility { get; set; }

        public override void SetActivityDynamicUIVisibility()
        {
            ActivityDynamicUIVisibility = new ActivityDynamicUIVisibility
            {
                ActivityDateVisibility = true,
                ActivityNameVisibility = false,
                ActivityTotalCostVisibility = true,
                AppliedAmountVisibility = true,
                AppliedProductsVisibility = true,
                DosageVisibility = true,
                PerformanceVisibility = false,
                PlantingDensityVisibility = false,
                ProductObtainedVisibility = false,
                VarietySownVisibility = false,
                ActivityNameList = new List<string> { "Aplicación de fungicidas", "Aplicación de insecticidas" }
            };
        }
    }
}
