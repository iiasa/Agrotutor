namespace CimmytApp.Parcel.ActivityManagement
{
    using Prism.Mvvm;
    using System.Collections.Generic;

    /// <inheritdoc />
    /// <summary>
    /// Defines the <see cref="T:CimmytApp.Parcel.ActivityManagement.ActivityDynamicUIVisibility" />
    /// </summary>
    public class ActivityDynamicUIVisibility : BindableBase
    {
        /// <summary>
        /// Defines the _activityNameVisibility
        /// </summary>
        private bool _activityNameVisibility;

        /// <summary>
        /// Defines the _activityDateVisibility
        /// </summary>
        private bool _activityDateVisibility;

        /// <summary>
        /// Defines the _activityTotalCostVisibility
        /// </summary>
        private bool _activityTotalCostVisibility;

        /// <summary>
        /// Defines the _appliedProductsVisibility
        /// </summary>
        private bool _appliedProductsVisibility;

        /// <summary>
        /// Defines the _dosageVisibility
        /// </summary>
        private bool _dosageVisibility;

        /// <summary>
        /// Defines the _appliedAmountVisibility
        /// </summary>
        private bool _appliedAmountVisibility;

        /// <summary>
        /// Defines the _varietySownVisibility
        /// </summary>
        private bool _varietySownVisibility;

        /// <summary>
        /// Defines the _plantingDensityVisibility
        /// </summary>
        private bool _plantingDensityVisibility;

        /// <summary>
        /// Defines the _productObtainedVisibility
        /// </summary>
        private bool _productObtainedVisibility;

        /// <summary>
        /// Defines the _performanceVisibility
        /// </summary>
        private bool _performanceVisibility;

        /// <summary>
        /// Defines the _activityNameListVisibility
        /// </summary>
        private bool _activityNameListVisibility;

        /// <summary>
        /// Defines the _activityNameList
        /// </summary>
        private List<string> _activityNameList;

        /// <summary>
        /// Defines the _activityName
        /// </summary>
        private string _activityName;

        /// <summary>
        /// Defines the _activityTitle
        /// </summary>
        private string _activityTitle;

        /// <summary>
        /// Defines the _activityIcon
        /// </summary>
        private string _activityIcon;

        /// <summary>
        /// Gets or sets a value indicating whether ActivityNameVisibility
        /// </summary>
        public bool ActivityNameVisibility
        {
            get => _activityNameVisibility;
            set
            {
                SetProperty(ref _activityNameVisibility, value);
                ActivityNameListVisibility = !ActivityNameVisibility;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ActivityNameListVisibility
        /// </summary>
        public bool ActivityNameListVisibility { get => _activityNameListVisibility; set => SetProperty(ref _activityNameListVisibility, value); }

        /// <summary>
        /// Gets or sets the ActivityNameList
        /// </summary>
        public List<string> ActivityNameList { get => _activityNameList; set => SetProperty(ref _activityNameList, value); }

        /// <summary>
        /// Gets or sets the ActivityName
        /// </summary>
        public string ActivityName { get => _activityName; set => SetProperty(ref _activityName, value); }

        /// <summary>
        /// Gets or sets a value indicating whether ActivityDateVisibility
        /// </summary>
        public bool ActivityDateVisibility { get => _activityDateVisibility; set => SetProperty(ref _activityDateVisibility, value); }

        /// <summary>
        /// Gets or sets a value indicating whether ActivityTotalCostVisibility
        /// </summary>
        public bool ActivityTotalCostVisibility { get => _activityTotalCostVisibility; set => SetProperty(ref _activityTotalCostVisibility, value); }

        /// <summary>
        /// Gets or sets a value indicating whether AppliedProductsVisibility
        /// </summary>
        public bool AppliedProductsVisibility { get => _appliedProductsVisibility; set => SetProperty(ref _appliedProductsVisibility, value); }

        /// <summary>
        /// Gets or sets a value indicating whether DosageVisibility
        /// </summary>
        public bool DosageVisibility { get => _dosageVisibility; set => SetProperty(ref _dosageVisibility, value); }

        /// <summary>
        /// Gets or sets a value indicating whether AppliedAmountVisibility
        /// </summary>
        public bool AppliedAmountVisibility { get => _appliedAmountVisibility; set => SetProperty(ref _appliedAmountVisibility, value); }

        /// <summary>
        /// Gets or sets a value indicating whether VarietySownVisibility
        /// </summary>
        public bool VarietySownVisibility { get => _varietySownVisibility; set => SetProperty(ref _varietySownVisibility, value); }

        /// <summary>
        /// Gets or sets a value indicating whether PlantingDensityVisibility
        /// </summary>
        public bool PlantingDensityVisibility { get => _plantingDensityVisibility; set => SetProperty(ref _plantingDensityVisibility, value); }

        /// <summary>
        /// Gets or sets a value indicating whether ProductObtainedVisibility
        /// </summary>
        public bool ProductObtainedVisibility { get => _productObtainedVisibility; set => SetProperty(ref _productObtainedVisibility, value); }

        /// <summary>
        /// Gets or sets a value indicating whether PerformanceVisibility
        /// </summary>
        public bool PerformanceVisibility { get => _performanceVisibility; set => SetProperty(ref _performanceVisibility, value); }

        /// <summary>
        /// Gets or sets the ActivityTitle
        /// </summary>
        public string ActivityTitle { get => _activityTitle; set => SetProperty(ref _activityTitle, value); }

        /// <summary>
        /// Gets or sets the ActivityIcon
        /// </summary>
        public string ActivityIcon { get => _activityIcon; set => SetProperty(ref _activityIcon, value); }
    }
}