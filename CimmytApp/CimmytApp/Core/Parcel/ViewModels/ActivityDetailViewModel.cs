namespace CimmytApp.Core.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.Parcel.ActivityManagement;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class ActivityDetailViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private double _activityCost;

        private DateTimeOffset _activityDate;

        private double _activityDose;

        private ActivityDynamicUIVisibility _activityDynamicUIVisibility;

        private string _activityName;

        private string _activityNameText;

        private string _activityYield;

        private string _amountApplied;

        private string _appliedProduct;

        private List<string> _listSownVariety;

        private double _numberOfSeeds;

        private string _productObtained;

        private string _selectedSown;

        private double _weightOfSeeds;

        public ActivityDetailViewModel(INavigationService navigationService,
            IStringLocalizer<ActivityDetailViewModel> localizer) : base(localizer)
        {
            _navigationService = navigationService;
            ListSownVariety = new List<string>
            {
                "Criollo",
                "Mejorado"
            };
            ActivityDate = DateTimeOffset.Now;
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
        public DateTimeOffset ActivityDate
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
            var activityName = (string)parameters["activityType"];
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

        public void OnNavigatingTo(NavigationParameters parameters)
        {
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
            var activity = new AgriculturalActivity
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

            var parameters = new NavigationParameters
            {
                { "Activity", activity }
            };
            _navigationService.GoBackAsync(parameters);
        }
    }
}