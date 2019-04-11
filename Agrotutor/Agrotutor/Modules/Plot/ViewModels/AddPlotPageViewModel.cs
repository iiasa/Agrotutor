using System.Threading.Tasks;
using Agrotutor.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
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
        private Random randomColor;
        public static string PositionParameterName = "Position";
        public static string PlotParameterName = "Plot";

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

        private Plot _plot;

        private DateTime plantingDate;
        private bool savingPlot;
        private bool _cultivarCharacteristicsVisible;

        public AddPlotPageViewModel(
            INavigationService navigationService,
            IStringLocalizer<AddPlotPageViewModel> stringLocalizer,
            IAppDataService appDataService) : base(navigationService, stringLocalizer)
        {
            randomColor=new Random();
            AppDataService = appDataService;

            Plot = new Plot();
            PlantingDate = DateTime.Today;

           // PickerCropTypesSelectedIndex = -1;
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
                StringLocalizer.GetString("none"),
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

        public bool CultivarCharacteristicsVisible
        {
            get => _cultivarCharacteristicsVisible;
            set => SetProperty(ref _cultivarCharacteristicsVisible, value);
        }

        public DateTime PlantingDate
        {
            get => this.plantingDate;
            set => SetProperty(ref this.plantingDate, value);
        }

        public DelegateCommand ClickSave =>
            new DelegateCommand(
                async () =>
                {
                    using (await MaterialDialog.Instance.LoadingDialogAsync(StringLocalizer.GetString("saving_plot")))
                    {
                        SavingPlot = true;
                        Plot.IsTemporaryPlot = false;
                        Plot.Activities = new List<Activity>
                        {
                            new Activity
                            {
                                ActivityType = ActivityType.Intialization,
                                Date = PlantingDate,
                               // Name = StringLocalizer.GetString("sowing"),
                               // Cost = 0
                            }
                        };
                   
                            this.Plot.PlotColor =System.Drawing.Color.FromArgb(randomColor.Next(256), randomColor.Next(256), randomColor.Next(256));
                        this.Plot.ArgbPlotColor = this.Plot.PlotColor.Value.ToArgb();
                        await AppDataService.AddPlotAsync(Plot);
                   var res=     await AppDataService.GetAllPlotsAsync();
                        SavingPlot = false;
                    }
                    await MaterialDialog.Instance.SnackbarAsync(StringLocalizer.GetString("plot_created"), 3000);
                    await Task.Delay(2000);
                    await NavigationService.NavigateAsync("app:///NavigationPage/MapPage");
                });

        public List<string> ClimateTypes { get; private set; }

        public List<string> CropTypes { get; private set; }

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
                //if (Plot.CropType == 0)
                //{
                //    if (value == -1)
                //    {
                //        Plot.CropType = CropType.None;
                //    }
                  //  else
                   // {
         if(_pickerCropTypesSelectedIndex!=-1)
                        Plot.CropType = (CropType) (value); // TODO: verify
                  // }
              //  }

                CultivarCharacteristicsVisible = (Plot.CropType == CropType.Corn);
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
        public DelegateCommand ShowAbout => new DelegateCommand(async () =>
        {
            var param = new NavigationParameters { { "page", WebContentPageViewModel.CultivarCharacteristics } };
            await NavigationService.NavigateAsync("WebContentPage", param);
        });
    }
}
