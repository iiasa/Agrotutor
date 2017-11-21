namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.Parcel.ActivityManagement;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    /// <inheritdoc cref="BindableBase" />
    /// <summary>
    ///     Defines the <see cref="T:CimmytApp.Parcel.ViewModels.ActivityDetailViewModel" />
    /// </summary>
    public class ActivityDetailViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        ///     Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        ///     Defines the _activityCost
        /// </summary>
        private double _activityCost;

        /// <summary>
        ///     Defines the _activityDate
        /// </summary>
        private DateTime _activityDate;

        /// <summary>
        ///     Defines the _activityDose
        /// </summary>
        private double _activityDose;

        /// <summary>
        ///     Defines the _activityDynamicUIVisibility
        /// </summary>
        private ActivityDynamicUIVisibility _activityDynamicUIVisibility;

        /// <summary>
        ///     Defines the _activityName
        /// </summary>
        private string _activityName;

        /// <summary>
        ///     Defines the _activityNameText
        /// </summary>
        private string _activityNameText;

        /// <summary>
        ///     Defines the _activityYield
        /// </summary>
        private string _activityYield;

        /// <summary>
        ///     Defines the _amountApplied
        /// </summary>
        private string _amountApplied;

        /// <summary>
        ///     Defines the _appliedProduct
        /// </summary>
        private string _appliedProduct;

        /// <summary>
        ///     Defines the _listSownVariety
        /// </summary>
        private List<string> _listSownVariety;

        /// <summary>
        ///     Defines the _minimumCalenderDateTime
        /// </summary>
        private DateTime _minimumCalenderDateTime;

        /// <summary>
        ///     Defines the _numberOfSeeds
        /// </summary>
        private double _numberOfSeeds;

        /// <summary>
        ///     Defines the _productObtained
        /// </summary>
        private string _productObtained;

        /// <summary>
        ///     Defines the _selectedSown
        /// </summary>
        private string _selectedSown;

        /// <summary>
        ///     Defines the _weightOfSeeds
        /// </summary>
        private double _weightOfSeeds;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivityDetailViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" /></param>
        public ActivityDetailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ListSownVariety = new List<string>
            {
                "Criollo",
                "Mejorado"
            };
            MinimumCalenderDateTime = DateTime.Now.Subtract(new TimeSpan(672, 0, 0, 0));
            SaveCommand = new DelegateCommand(SaveCommandExecution);
        }

        /// <summary>
        ///     Gets or sets the ActivityCost
        /// </summary>
        public double ActivityCost
        {
            get => _activityCost;
            set => SetProperty(ref _activityCost, value);
        }

        /// <summary>
        ///     Gets or sets the ActivityDate
        /// </summary>
        public DateTime ActivityDate
        {
            get => _activityDate;
            set => SetProperty(ref _activityDate, value);
        }

        /// <summary>
        ///     Gets or sets the ActivityDose
        /// </summary>
        public double ActivityDose
        {
            get => _activityDose;
            set => SetProperty(ref _activityDose, value);
        }

        /// <summary>
        ///     Gets or sets the ActivityDynamicUIVisibility
        /// </summary>
        public ActivityDynamicUIVisibility ActivityDynamicUIVisibility
        {
            get => _activityDynamicUIVisibility;
            set => SetProperty(ref _activityDynamicUIVisibility, value);
        }

        /// <summary>
        ///     Gets or sets the ActivityName
        /// </summary>
        public string ActivityName
        {
            get => _activityName;
            set => SetProperty(ref _activityName, value);
        }

        /// <summary>
        ///     Gets or sets the ActivityNameText
        /// </summary>
        public string ActivityNameText
        {
            get => _activityNameText;
            set => SetProperty(ref _activityNameText, value);
        }

        /// <summary>
        ///     Gets or sets the ActivityType
        /// </summary>
        public ActivityType ActivityType { get; private set; }

        /// <summary>
        ///     Gets or sets the ActivityYield
        /// </summary>
        public string ActivityYield
        {
            get => _activityYield;
            set => SetProperty(ref _activityYield, value);
        }

        /// <summary>
        ///     Gets or sets the AmountApplied
        /// </summary>
        public string AmountApplied
        {
            get => _amountApplied;
            set => SetProperty(ref _amountApplied, value);
        }

        /// <summary>
        ///     Gets or sets the AppliedProduct
        /// </summary>
        public string AppliedProduct
        {
            get => _appliedProduct;
            set => SetProperty(ref _appliedProduct, value);
        }

        /// <summary>
        ///     Gets or sets the ListSownVariety
        /// </summary>
        public List<string> ListSownVariety
        {
            get => _listSownVariety;
            set => SetProperty(ref _listSownVariety, value);
        }

        /// <summary>
        ///     Gets or sets the MinimumCalenderDateTime
        /// </summary>
        public DateTime MinimumCalenderDateTime
        {
            get => _minimumCalenderDateTime;
            set => SetProperty(ref _minimumCalenderDateTime, value);
        }

        /// <summary>
        ///     Gets or sets the NumberOfSeeds
        /// </summary>
        public double NumberOfSeeds
        {
            get => _numberOfSeeds;
            set => SetProperty(ref _numberOfSeeds, value);
        }

        /// <summary>
        ///     Gets or sets the ProductObtained
        /// </summary>
        public string ProductObtained
        {
            get => _productObtained;
            set => SetProperty(ref _productObtained, value);
        }

        /// <summary>
        ///     Gets or sets the SaveCommand
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        ///     Gets or sets the SelectedSown
        /// </summary>
        public string SelectedSown
        {
            get => _selectedSown;
            set => SetProperty(ref _selectedSown, value);
        }

        /// <summary>
        ///     Gets or sets the WeightOfSeeds
        /// </summary>
        public double WeightOfSeeds
        {
            get => _weightOfSeeds;
            set
            {
                _weightOfSeeds = value;
                SetProperty(ref _weightOfSeeds, value);
            }
        }

        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            string activityName = (string)parameters["activityType"];
            ActivityType = (ActivityType)Enum.Parse(typeof(ActivityType), activityName);
            ActivityBaseClass baseClass = null;
            switch (ActivityType)
            {
                case ActivityType.SoilImprovers:
                    baseClass = new SoilImproversActivity();
                    break;

                case ActivityType.GroundPreperation:
                    baseClass = new GroundPreperationActivity();
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
                {
                    ActivityName = ActivityDynamicUIVisibility.ActivityName;
                }
            }
        }

        /// <summary>
        ///     The SaveCommandExecution
        /// </summary>
        private void SaveCommandExecution()
        {
            string activityName;
            if (ActivityDynamicUIVisibility.ActivityNameListVisibility ||
                ActivityDynamicUIVisibility.ActivityNameVisibility)
            {
                activityName = ActivityName;
            }
            else
            {
                activityName = ActivityNameText;
            }
            AgriculturalActivity activity = new AgriculturalActivity
            {
                AmountApplied = AmountApplied,
                AppliedProduct = AppliedProduct,
                ActivityType = ActivityType,
                Cost = ActivityCost,
                Date = ActivityDate,
                Dose = ActivityDose,
                Name = activityName,
                NumberOfSeeds = NumberOfSeeds,
                ProductObtained = ProductObtained,
                Sown = SelectedSown,
                WeightOfSeeds = WeightOfSeeds,
                Yield = ActivityYield
            };

            NavigationParameters parameters = new NavigationParameters
            {
                { "Activity", activity }
            };
            _navigationService.GoBackAsync(parameters);
        }
    }
}