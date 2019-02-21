using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.Modules.Plot.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Localization;
    using Prism.Navigation;
    using Prism.Commands;

    using Core;
    using Core.Entities;
    using Core.Persistence;

    public class AddPlotPageViewModel : ViewModelBase
    {
        public static string PositionParameterName = "Position";

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

        private Plot _plot;

        private DateTime plantingDate;
        private bool savingPlot;

        public AddPlotPageViewModel(
            INavigationService navigationService,
            IStringLocalizer<AddPlotPageViewModel> stringLocalizer,
            IAppDataService appDataService) : base(navigationService, stringLocalizer)
        {

            AppDataService = appDataService;

            Plot = new Plot();
            PlantingDate = DateTime.Today;

            PickerCropTypesSelectedIndex = -1;
            PickerClimateTypesSelectedIndex = -1;
            PickerMaturityClassesSelectedIndex = -1;

            ClimateTypes = new List<string>
            {
                StringLocalizer.GetString("cold"),
                StringLocalizer.GetString("tempered"),
                StringLocalizer.GetString("tropical"),
                StringLocalizer.GetString("hybrid")
            };

            CropTypes = new List<string>
            {
                StringLocalizer.GetString("maize"),
                StringLocalizer.GetString("barley"),
                StringLocalizer.GetString("bean"),
                StringLocalizer.GetString("wheat"),
                StringLocalizer.GetString("triticale"),
                StringLocalizer.GetString("sorghum"),
                StringLocalizer.GetString("alfalfa"),
                StringLocalizer.GetString("oats"),
                StringLocalizer.GetString("sesame"),
                StringLocalizer.GetString("amaranth"),
                StringLocalizer.GetString("rice"),
                StringLocalizer.GetString("canola"),
                StringLocalizer.GetString("cartamo"),
                StringLocalizer.GetString("zucchini"),
                StringLocalizer.GetString("chickpea"),
                StringLocalizer.GetString("havabean"),
                StringLocalizer.GetString("soy"),
                StringLocalizer.GetString("none"),
                StringLocalizer.GetString("other")
            };

            MaturityClasses = new List<string>
            {
                StringLocalizer.GetString("early"),
                StringLocalizer.GetString("semi_early"),
                StringLocalizer.GetString("intermediate"),
                StringLocalizer.GetString("semi_late"),
                StringLocalizer.GetString("late")
            };
        }

        public IAppDataService AppDataService { get; }

        public bool SavingPlot { get => savingPlot; set => SetProperty(ref savingPlot, value); }


        public DateTime PlantingDate
        {
            get => this.plantingDate;
            set => SetProperty(ref this.plantingDate, value);
        }

        public DelegateCommand ClickSave =>
            new DelegateCommand(
                async () =>
                {
                    using (await MaterialDialog.Instance.LoadingSnackbarAsync(StringLocalizer.GetString("loading")))
                    {
                        SavingPlot = true;
                        Plot.Activities = new List<Activity>
                        {
                            new Activity
                            {
                                ActivityType = ActivityType.Sowing,
                                Date = PlantingDate,
                                Name = StringLocalizer.GetString("sowing"),
                                Cost = 0
                            }
                        };
                        await AppDataService.AddPlotAsync(Plot);
                        SavingPlot = false;
                    }
                    await NavigationService.NavigateAsync("app:///NavigationPage/MapPage");
                });

        public List<string> ClimateTypes { get; private set; } 

        public List<string> CropTypes { get; private set;  }

        public List<string> MaturityClasses { get; }

        public int PickerClimateTypesSelectedIndex
        {
            get => this._pickerClimateTypesSelectedIndex;
            set => SetProperty(ref this._pickerClimateTypesSelectedIndex, value);
        }

        public int PickerCropTypesSelectedIndex
        {
            get => this._pickerCropTypesSelectedIndex;
            set
            {
                SetProperty(ref this._pickerCropTypesSelectedIndex, value);
                if (value == -1)
                {
                    Plot.CropType = CropType.None;
                }
                else
                {
                    Plot.CropType = (CropType)(value + 1); // TODO: verify
                }
            }
        }

        public int PickerMaturityClassesSelectedIndex
        {
            get => this._pickerMaturityClassesSelectedIndex;
            set => SetProperty(ref this._pickerMaturityClassesSelectedIndex, value);
        }

        public Plot Plot
        {
            get => this._plot;
            set => SetProperty(ref this._plot, value);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(AddPlotPageViewModel.PositionParameterName))
            {
                parameters.TryGetValue(AddPlotPageViewModel.PositionParameterName, out Position position);
                if (position != null)
                {
                    Plot.Position = position;
                }
            }
            base.OnNavigatedTo(parameters);
        }
    }
}
