using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimmytApp.Parcel.ActivityManagement
{
    public abstract class ActivityBaseClass
    {
        public  ActivityDynamicUIVisibility ActivityDynamicUIVisibility { get; set; }

        protected ActivityBaseClass()
        {
            SetActivityDynamicUIVisibility();
        }

        public abstract void SetActivityDynamicUIVisibility();


    }
}
