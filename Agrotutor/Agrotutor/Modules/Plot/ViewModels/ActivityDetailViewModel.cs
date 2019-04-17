

using Agrotutor.Core.Persistence;
using Agrotutor.ViewModels;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.Modules.Plot.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    using ActivityManagement;
    using Core;
    using Core.Entities;

    public class ActivityDetailViewModel : ViewModelBase, INavigatedAware
    {
        private IAppDataService _appDataService;

        private double _activityCost;

        private DateTime _activityDate;

        private double _activityDose;

        private ActivityDynamicUIVisibility _activityDynamicUIVisibility;

        private string _activityName;

        private string _activityNameText;

        private string _activityYield;

        private string _amountApplied;

        private Plot _plot;
        private string _appliedProduct;

        private List<string> _listSownVariety;

        private double _numberOfSeeds;

        private string _productObtained;

        private string _selectedSown;

        private double _weightOfSeeds;
        private string activityComment;
        private int activitySellingPrice;
        private int plotArea;
        private string amountSold;

        public ActivityDetailViewModel(INavigationService navigationService, IAppDataService appDataService,
            IStringLocalizer<ActivityDetailViewModel> localizer) : base(navigationService, localizer)
        {
            ListSownVariety = new List<string>
            {
                "Criollo",
                "Mejorado"
            };
            ActivityDate = DateTime.Now;
            ActivityNameSet = false;
            _appDataService = appDataService;
        }

        public bool ActivityNameSet { get; set; }

        public DelegateCommand ShowAbout => new DelegateCommand(async () =>
        {
            var param = new NavigationParameters { { "page", WebContentPageViewModel.Activities } };
            await NavigationService.NavigateAsync("WebContentPage", param);
        });

        /// <summary>
        ///     Gets or sets the ActivityCost
        /// </summary>
        public double ActivityCost
        {
            get => _activityCost;
            set => SetProperty(ref _activityCost, value);
        }

        public int ActivitySellingPrice
        {
            get => activitySellingPrice;
            set => SetProperty(ref activitySellingPrice, value);
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
            set
            {
                SetProperty(ref _activityName, value);
                ActivityNameSet = value != null;
            }
        }

        public string ActivityComment
        {
            get => activityComment;
            set => SetProperty(ref activityComment, value);
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
        public DelegateCommand SaveCommand => new DelegateCommand(async () =>
        {
            if (!ActivityNameSet)
            {
                await MaterialDialog.Instance.AlertAsync(
                    StringLocalizer.GetString("select_activity_name_message"),
                    StringLocalizer.GetString("select_activity_name_title"));
                return;
            }
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
            var activity = new Activity
            {
                AmountApplied = AmountApplied,
                AmountSold = AmountSold,
                AppliedProduct = AppliedProduct,
                ActivityType = ActivityType,
                Comment = ActivityComment,
                Cost = ActivityCost,
                Date = ActivityDate,
                Dose = ActivityDose,
                Name = activityName,
                NumberOfSeeds = NumberOfSeeds,
                PlotArea = PlotArea,
                ProductObtained = ProductObtained,
                SellingPrice = ActivitySellingPrice,
                Sown = SelectedSown,
                WeightOfSeeds = WeightOfSeeds,
                Yield = ActivityYield
            };
            if (Plot.Activities == null) Plot.Activities = new List<Activity>();
            Plot.Activities.Add(activity);
            await _appDataService.UpdatePlotAsync(Plot);

             await NavigationService.NavigateAsync("myapp:///NavigationPage/MapPage");
        });

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

        public int PlotArea
        {
            get => plotArea;
            set => SetProperty(ref plotArea, value);
        }

        public string AmountSold
        {
            get => amountSold;
            set => SetProperty(ref amountSold, value);
        }

        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public Plot Plot
        {
            get => this._plot;
            private set => SetProperty(ref this._plot, value);
        }

        /// <summary>
        ///     The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue<Plot>("Plot", out var plot);
                Plot = plot;
            }

            var activityName = (string)parameters["activityType"];
            ActivityType = (ActivityType)Enum.Parse(typeof(ActivityType), activityName);
            ActivityBaseClass baseClass = null;
            switch (ActivityType)
            {
                case ActivityType.SoilImprovers:
                    baseClass = new SoilImproversActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("soil_improvers");
                    break;

                case ActivityType.GroundPreperation:
                    baseClass = new GroundPreperationActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("ground_preparation");
                    break;

                case ActivityType.Sowing:
                    baseClass = new SowingActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("sowing");
                    break;

                case ActivityType.Fertilization:
                    baseClass = new FertilizationActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("fertilization");
                    break;

                case ActivityType.Irrigation:
                    baseClass = new IrrigationActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("soil_improvers");
                    break;

                case ActivityType.WeedPreventionControl:
                    baseClass = new WeedPreventionControlActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("weed_prevention");
                    break;

                case ActivityType.PestAndDiseaseControlAndPrevention:
                    baseClass = new PestAndDiseaseControlAndPreventionActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("pest_and_disease");
                    break;

                case ActivityType.Harvest:
                    baseClass = new HarvestActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("harvest");
                    break;

                case ActivityType.PostHarvestStorage:
                    baseClass = new PostHarvestStorageActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("post_harvest_storage");
                    break;

                case ActivityType.Commercialization:
                    baseClass = new CommercializationActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("commercialization");
                    break;

                case ActivityType.OtherActivities:
                    baseClass = new OtherActivitiesActivity((IStringLocalizer<ActivityDetailViewModel>)StringLocalizer);
                    ActivityName = StringLocalizer.GetString("other");
                    break;
            }

            ActivityDynamicUIVisibility = baseClass?.ActivityDynamicUIVisibility;
            base.OnNavigatedTo(parameters);
        }
    }
}
