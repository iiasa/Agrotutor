using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agrotutor.Core;
using Agrotutor.Core.Entities;
using Agrotutor.Core.Persistence;
using Agrotutor.ViewModels;
using Microsoft.Extensions.Localization;
using Prism.Commands;
using Prism.Navigation;
using XF.Material.Forms.UI.Dialogs;

namespace Agrotutor.Modules.Plot.ViewModels
{
    public class AddPlotPageViewModel : ViewModelBase
    {
        public static string PositionParameterName = "Position";
        private readonly IDbService<Core.Entities.Plot> _plotDbService;
        public static string PlotParameterName = "Plot";

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

        private Core.Entities.Plot _plot;

        private DateTime plantingDate;
        private bool savingPlot;

        public AddPlotPageViewModel(
            INavigationService navigationService, IDbService<Core.Entities.Plot> plotDbService,
            IStringLocalizer<AddPlotPageViewModel> stringLocalizer) : base(navigationService, stringLocalizer)
        {
            _plotDbService = plotDbService;


            Plot = new Core.Entities.Plot();
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


        public bool SavingPlot
        {
            get => savingPlot;
            set => SetProperty(ref savingPlot, value);
        }


        public DateTime PlantingDate
        {
            get => plantingDate;
            set => SetProperty(ref plantingDate, value);
        }

        public DelegateCommand ClickSave =>
            new DelegateCommand(
                async () =>
                {
                    using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("saving_plot")))
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
                        //await AppDataService.AddPlotAsync(Plot);
                        _plotDbService.CreateItem(Plot);
                        var res = _plotDbService.ReadAllItems();
                        SavingPlot = false;
                    }

                    await MaterialDialog.Instance.SnackbarAsync(StringLocalizer.GetString("plot_created"), 3000);
                    await Task.Delay(2000);
                    await NavigationService.NavigateAsync("app:///NavigationPage/MapPage");
                });

        public List<string> ClimateTypes { get; }

        public List<string> CropTypes { get; }

        public List<string> MaturityClasses { get; }

        public int PickerClimateTypesSelectedIndex
        {
            get => _pickerClimateTypesSelectedIndex;
            set => SetProperty(ref _pickerClimateTypesSelectedIndex, value);
        }

        public int PickerCropTypesSelectedIndex
        {
            get => _pickerCropTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerCropTypesSelectedIndex, value);
                if (value == -1)
                    Plot.CropType = CropType.None;
                else
                    Plot.CropType = (CropType) (value + 1); // TODO: verify
            }
        }

        public int PickerMaturityClassesSelectedIndex
        {
            get => _pickerMaturityClassesSelectedIndex;
            set => SetProperty(ref _pickerMaturityClassesSelectedIndex, value);
        }

        public Core.Entities.Plot Plot
        {
            get => _plot;
            set => SetProperty(ref _plot, value);
        }

        public DelegateCommand ShowAbout => new DelegateCommand(async () =>
        {
            var param = new NavigationParameters {{"page", WebContentPageViewModel.CultivarCharacteristics}};
            await NavigationService.NavigateAsync("WebContentPage", param);
        });

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(PositionParameterName))
            {
                parameters.TryGetValue(PositionParameterName, out Position position);
                if (position != null) Plot.Position = position;
            }

            if (parameters.ContainsKey(AddPlotPageViewModel.PlotParameterName))
            {
                parameters.TryGetValue(AddPlotPageViewModel.PlotParameterName, out Plot plot);
                if (plot != null)
                {
                    Plot = plot;
                }
            }

            base.OnNavigatedTo(parameters);
        }
    }
}