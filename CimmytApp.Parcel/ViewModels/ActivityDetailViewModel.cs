using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
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
        private string _pageIcon;
        private string _pageTitle;

        public DelegateCommand SaveCommand { get; set; }

        public List<string> ListSownVariety
        {
            get => _listSownVariety;
            set => SetProperty(ref _listSownVariety, value);
        }

        public ActivityDynamicUIVisibility ActivityDynamicUIVisibility
        {
            get => _activityDynamicUIVisibility;
            set => SetProperty(ref _activityDynamicUIVisibility, value);
        }

        public string ActivityName
        {
            get => _activityName;
            set => SetProperty(ref _activityName, value);
        }

        public DateTime ActivityDate
        {
            get => _activityDate;
            set => SetProperty(ref _activityDate, value);
        }

        public double ActivityCost
        {
            get => _activityCost;
            set => SetProperty(ref _activityCost, value);
        }

        public string AmountApplied
        {
            get => _amountApplied;
            set => SetProperty(ref _amountApplied, value);
        }

        public string SelectedSown
        {
            get => _selectedSown;
            set => SetProperty(ref _selectedSown, value);
        }

        public string AppliedProduct
        {
            get => _appliedProduct;
            set => SetProperty(ref _appliedProduct, value);
        }

        public double ActivityDose
        {
            get => _activityDose;
            set => SetProperty(ref _activityDose, value);
        }

        public double WeightOfSeeds
        {
            get => _weightOfSeeds;
            set
            {
                _weightOfSeeds = value;
                SetProperty(ref _weightOfSeeds, value);
            }
        }

        public double NumberOfSeeds
        {
            get => _numberOfSeeds;
            set => SetProperty(ref _numberOfSeeds, value);
        }

        public string ProductObtained
        {
            get => _productObtained;
            set => SetProperty(ref _productObtained, value);
        }

        public string ActivityYield
        {
            get => _activityYield;
            set => SetProperty(ref _activityYield, value);
        }

        public DateTime MinimumCalenderDateTime
        {
            get => _minimumCalenderDateTime;
            set => SetProperty(ref _minimumCalenderDateTime, value);
        }

        public string PageIcon
        {
            get => _pageIcon;
            set => SetProperty(ref _pageIcon, value);
        }

        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        public ActivityDetailViewModel()
        {
            ListSownVariety = new List<string> { "Criollo", "Mejorado" };
            MinimumCalenderDateTime = DateTime.Now.Subtract(new TimeSpan(672, 0, 0, 0));
            SaveCommand = new DelegateCommand(SaveCommandExecution);
        }

        private void SaveCommandExecution()
        {
            //TODO
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var activityName = (String)parameters["activityType"];
            var activityType = (ActivityType)Enum.Parse(typeof(ActivityType), activityName);
            ActivityBaseClass baseClass = null;
            switch (activityType)
            {
                case ActivityType.SoilImprovers:
                    baseClass = new SoilImproversActivity();
                    PageIcon = "flask_small.png";
                    PageTitle = "Aplicación de mejoradores de suelo";
                    break;

                case ActivityType.GroundPreperation:
                    baseClass = new GroundPreperationActivity();
                    PageIcon = "shovel_small.png";
                    PageTitle = "Preparación del terreno";
                    break;

                case ActivityType.Sowing:
                    baseClass = new SowingActivity();
                    PageIcon = "sowing_small.png";
                    PageTitle = "Siembra";
                    break;

                case ActivityType.Fertilization:
                    baseClass = new FertilizationActivity();
                    PageIcon = "fertilizer_small.png";
                    PageTitle = "Fertilización";
                    break;

                case ActivityType.Irrigation:
                    baseClass = new IrrigationActivity();
                    PageIcon = "irrigation_small.png";
                    PageTitle = "Riego";
                    break;

                case ActivityType.WeedPreventionControl:
                    baseClass = new WeedPreventionControlActivity();
                    PageIcon = "weeds_small.png";
                    PageTitle = "Control de prevención de malezas";
                    break;

                case ActivityType.PestAndDiseaseControlAndPrevention:
                    baseClass = new PestAndDiseaseControlAndPreventionActivity();
                    PageIcon = "cockroach_small.png";
                    PageTitle = "Cosecha";
                    break;

                case ActivityType.Harvest:
                    baseClass = new HarvestActivity();
                    PageIcon = "harvest_small.png";
                    PageTitle = "Almacenamiento poscosecha";
                    break;

                case ActivityType.PostHarvestStorage:
                    baseClass = new PostHarvestStorageActivity();
                    PageIcon = "storage_small.png";
                    PageTitle = "Comercialización";
                    break;

                case ActivityType.Commercialization:
                    baseClass = new CommercializationActivity();
                    PageIcon = "money_small.png";
                    PageTitle = "Otras actividades";
                    break;

                case ActivityType.OtherActivities:
                    baseClass = new OtherActivitiesActivity();
                    PageIcon = "farmer_small.png";
                    PageTitle = "Control y prevención de plagas y enfermedades";
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