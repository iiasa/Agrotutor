using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using CimmytApp.Parcel.ActivityManagement;

namespace CimmytApp.Parcel.ViewModels
{
    public class ActivityDetailViewModel : BindableBase, INavigationAware
    {
        private ActivityDynamicUIVisibility _activityDynamicUIVisibility;
        private List<string> _listSownVariety;
        private string _selectedSown;
        private string _activityName;
        private DateTime _activityDate;
        private double _activityCost;
        private string _appliedProduct;
        private double _activityDose;
        private double _weightOfSeeds;
        private double _numberOfSeeds;
        private string _amountApplied;
        private string _productObtained;
        private string _activityYield;
        private DateTime _minimumCalenderDateTime;

        public DelegateCommand SaveCommand { get; set; }
        public List<string> ListSownVariety
        {
            get { return _listSownVariety; }
            set
            {
                SetProperty(ref _listSownVariety, value);
            }
        }

        public ActivityDynamicUIVisibility ActivityDynamicUIVisibility
        {
            get { return _activityDynamicUIVisibility; }
            set
            {
      
                SetProperty(ref _activityDynamicUIVisibility, value);
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

        public DateTime ActivityDate
        {
            get { return _activityDate; }
            set
            {
                SetProperty(ref _activityDate, value);
            }
        }

        public double ActivityCost
        {
            get { return _activityCost; }
            set
            {
                SetProperty(ref _activityCost, value);
            }
        }

        public string AmountApplied
        {
            get { return _amountApplied; }
            set
            {
                SetProperty(ref _amountApplied, value);
            }
        }


        public string SelectedSown
        {
            get { return _selectedSown; }
            set
            {
                SetProperty(ref _selectedSown, value);
            }
        }

        public string AppliedProduct
        {
            get { return _appliedProduct; }
            set
            {
                SetProperty(ref _appliedProduct, value);
            }
        }

        public double ActivityDose
        {
            get { return _activityDose; }
            set
            {
                SetProperty(ref _activityDose, value);
            }
        }

        public double WeightOfSeeds
        {
            get { return _weightOfSeeds; }
            set
            {
                _weightOfSeeds = value;
                SetProperty(ref _weightOfSeeds, value);
            }
        }

        public double NumberOfSeeds
        {
            get { return _numberOfSeeds; }
            set
            {
                SetProperty(ref _numberOfSeeds, value);
            }
        }

        public string ProductObtained
        {
            get { return _productObtained; }
            set
            {
                SetProperty(ref _productObtained, value);
            }
        }

        public string ActivityYield
        {
            get { return _activityYield; }
            set
            {
                SetProperty(ref _activityYield, value);
            }
        }

        public DateTime MinimumCalenderDateTime
        {
            get { return _minimumCalenderDateTime; }
            set
            {
                SetProperty(ref _minimumCalenderDateTime, value);
            }
        }

        public ActivityDetailViewModel()
        {
            ListSownVariety=new List<string>{ "Criollo", "Mejorado" };
            MinimumCalenderDateTime=DateTime.Now.Subtract(new TimeSpan(672,0,0,0));
            SaveCommand =new DelegateCommand(SaveCommandExecution);
        }

        private void SaveCommandExecution()
        {
            
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
          
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var activityName =(String)parameters["activityType"];
         var activityType= (ActivityType)Enum.Parse(typeof(ActivityType), activityName);
            ActivityBaseClass baseClass = null;
            switch (activityType)
            {
                case ActivityType.SoilImprovers:
                    baseClass =new SoilImproversActivity();
                    break;
                case ActivityType.GroundPreperation:
                    baseClass =new GroundPreperationActivity();
                    break;
                case ActivityType.Sowing:
                    baseClass = new SowingActivity();
                    break;
                case ActivityType.Fertilization:
                    baseClass = new FertilizationActivity();
                    break;
                case ActivityType.Irrigation:
                    baseClass = new IrrigationActivity();
                    break;
                case ActivityType.WeedPreventionControl:
                    baseClass = new WeedPreventionControlActivity();
                    break;
                case ActivityType.PestAndDiseaseControlAndPrevention:
                    baseClass = new PestAndDiseaseControlAndPreventionActivity();
                    break;
                case ActivityType.Harvest:
                    baseClass = new HarvestActivity();
                    break;
                case ActivityType.PostHarvestStorage:
                    baseClass = new PostHarvestStorageActivity();
                    break;
                case ActivityType.Commercialization:
                    baseClass = new CommercializationActivity();
                    break;
                case ActivityType.OtherActivities:
                    baseClass = new OtherActivitiesActivity();
                    break;
   
            }
            if (baseClass != null)
            {
                ActivityDynamicUIVisibility = baseClass.ActivityDynamicUIVisibility;
                if (ActivityDynamicUIVisibility != null)
                    ActivityName = ActivityDynamicUIVisibility.ActivityName;
            }
        }
    }
}
