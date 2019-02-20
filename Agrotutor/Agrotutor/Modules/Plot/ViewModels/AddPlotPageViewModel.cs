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
                    using (await MaterialDialog.Instance.LoadingSnackbarAsync("Loading..."))
                    {
                        SavingPlot = true;
                        Plot.Activities = new List<Activity>
                        {
                            new Activity
                            {
                                ActivityType = ActivityType.Sowing,
                                Date = PlantingDate,
                                Name = "Sowing",
                                Cost = 0
                            }
                        };
                        await AppDataService.AddPlotAsync(Plot);
                        SavingPlot = false;
                        //MainThread.BeginInvokeOnMainThread(async () =>
                        //    await NavigationService.NavigateAsync("app:///NavigationPage/MainPage"));
                    }
                    await NavigationService.NavigateAsync("app:///NavigationPage/MapPage");
                });

        public List<string> ClimateTypes { get; } = new List<string>
                                                    {
                                                        "Frío",
                                                        "Templado/Subtropical",
                                                        "Tropical",
                                                        "Híbrido"
                                                    };

        public List<string> CropTypes { get; } = new List<string>
                                                 {
                                                     "Maíz",
                                                     "Cebada",
                                                     "Frijol",
                                                     "Trigo",
                                                     "Triticale",
                                                     "Sorgo",
                                                     "Alfalfa",
                                                     "Avena",
                                                     "Ajonjolí",
                                                     "Amaranto",
                                                     "Arroz",
                                                     "Canola",
                                                     "Cartamo",
                                                     "Calabacín",
                                                     "Garbanzo",
                                                     "Haba",
                                                     "Soya",
                                                     "Ninguno",
                                                     "Otro"
                                                 };

        public List<string> MaturityClasses { get; } = new List<string>
                                                       {
                                                           "Temprana",
                                                           "Semi-temprana",
                                                           "Intermedia",
                                                           "Semi-tardía",
                                                           "Tardía"
                                                       };

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
