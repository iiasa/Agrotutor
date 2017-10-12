using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace CimmytApp.Parcel.ActivityManagement
{
   public class ActivityDynamicUIVisibility:BindableBase
    {
        private bool _activityNameVisibility;
        private bool _activityDateVisibility;
        private bool _activityTotalCostVisibility;
        private bool _appliedProductsVisibility;
        private bool _dosageVisibility;
        private bool _appliedAmountVisibility;
        private bool _varietySownVisibility;
        private bool _plantingDensityVisibility;
        private bool _productObtainedVisibility;
        private bool _performanceVisibility;
        private bool _activityNameListVisibility;
        private List<string> _activityNameList;
        private string _activityName;

        public bool ActivityNameVisibility
        {
            get { return _activityNameVisibility; }
            set
            {
                SetProperty(ref _activityNameVisibility, value);
                ActivityNameListVisibility = !ActivityNameVisibility;
            }
        }

        public bool ActivityNameListVisibility
        {
            get { return _activityNameListVisibility; }
            set
            {
                SetProperty(ref _activityNameListVisibility, value);
            }
        }

        public List<string> ActivityNameList
        {
            get { return _activityNameList; }
            set
            {
                SetProperty(ref _activityNameList, value);
            }
        }

        public string ActivityName
        {
            get { return _activityName; }
            set
            {
                SetProperty(ref _activityName, value);
            }
        }

        public bool ActivityDateVisibility
        {
            get { return _activityDateVisibility; }
            set
            {
                SetProperty(ref _activityDateVisibility, value);
            }
        }

        public bool ActivityTotalCostVisibility
        {
            get { return _activityTotalCostVisibility; }
            set { SetProperty(ref _activityTotalCostVisibility, value); }
        }

        public bool AppliedProductsVisibility
        {
            get { return _appliedProductsVisibility; }
            set { SetProperty(ref _appliedProductsVisibility, value); }
        }

        public bool DosageVisibility
        {
            get { return _dosageVisibility; }
            set { SetProperty(ref _dosageVisibility, value); }
        }

        public bool AppliedAmountVisibility
        {
            get { return _appliedAmountVisibility; }
            set { SetProperty(ref _appliedAmountVisibility, value); }
        }

        public bool VarietySownVisibility
        {
            get { return _varietySownVisibility; }
            set { SetProperty(ref _varietySownVisibility, value); }
        }

        public bool PlantingDensityVisibility
        {
            get { return _plantingDensityVisibility; }
            set { SetProperty(ref _plantingDensityVisibility, value); }
        }

        public bool ProductObtainedVisibility
        {
            get { return _productObtainedVisibility; }
            set { SetProperty(ref _productObtainedVisibility, value); }
        }

        public bool PerformanceVisibility

        {
            get { return _performanceVisibility; }
            set { SetProperty(ref _performanceVisibility, value); }
        }
    }
}
