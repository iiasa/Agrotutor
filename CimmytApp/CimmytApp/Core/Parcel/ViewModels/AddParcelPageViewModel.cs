namespace CimmytApp.Core.Parcel.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.ViewModels;
    using Helper.Map.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Forms.GoogleMaps;
    using Position = CimmytApp.Core.Persistence.Entities.Position;

    public class AddParcelPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string PositionParameterName = "Plot";

        public AddParcelPageViewModel(INavigationService navigationService,
            IStringLocalizer<AddParcelPageViewModel> localizer, IAppDataService appDataService) : base(localizer)
        {
            this._navigationService = navigationService;
            AppDataService = appDataService;

            ClickSave = new DelegateCommand(SavePlot); //.ObservesCanExecute(o => IsSaveBtnEnabled);

            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);

            Plot = new Plot();

            PickerCropTypesSelectedIndex = -1;
            PickerClimateTypesSelectedIndex = -1;
            PickerMaturityClassesSelectedIndex = -1;
        }

        public IAppDataService AppDataService { get; }

        private readonly INavigationService _navigationService;

        private bool _isSaveBtnEnabled = true;

        private Plot _plot;

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

        private bool _userIsAtPlot;

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

        public bool InformationMissing => !IsSaveBtnEnabled;

        public List<string> MaturityClasses { get; } = new List<string>
        {
            "Temprana",
            "Semi-temprana",
            "Intermedia",
            "Semi-tardía",
            "Tardía"
        };

        public DelegateCommand ClickChooseLocation { get; set; }

        public DelegateCommand ClickGetLocation { get; set; }

        public DelegateCommand ClickDelineate { get; set; }

        public DelegateCommand ClickSave { get; set; }

        public bool IsSaveBtnEnabled
        {
            get => this._isSaveBtnEnabled;
            set => SetProperty(ref this._isSaveBtnEnabled, value);
        }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        public Plot Plot
        {
            get => this._plot;
            set
            {
                SetProperty(ref this._plot, value);
                UpdateSelections();
            }
        }

        public int PickerClimateTypesSelectedIndex
        {
            get => this._pickerClimateTypesSelectedIndex;
            set
            {
                SetProperty(ref this._pickerClimateTypesSelectedIndex, value);
                // Plot.ClimateType = value == -1 ? null : ClimateTypes.ElementAt(value); TODO fix
            }
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
            set
            {
                SetProperty(ref this._pickerMaturityClassesSelectedIndex, value);
                // Plot.MaturityClass = value == -1 ? null : MaturityClasses.ElementAt(value); todo: fix
            }
        }

        public bool UserIsAtPlot
        {
            get => this._userIsAtPlot;
            set => SetProperty(ref this._userIsAtPlot, value);
        }
        public Position Position { get; private set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(AddParcelPageViewModel.PositionParameterName))
            {
                parameters.TryGetValue<Position>(AddParcelPageViewModel.PositionParameterName, out var position);
                if (position != null)
                {
                    Position = position;
                }
            }
        }

        private void NavigateAsync(string page)
        {
            var parameters = new NavigationParameters
            {
                { "Caller", "AddPlotPage" },
                { "Plot", Plot }
            };
            this._navigationService.NavigateAsync(page, parameters);
        }

        private void SavePlot()
        {
            IsSaveBtnEnabled = false;
            // Plot.Uploaded = (int)DatasetUploadStatus.ChangesOnDevice; todo: add this?
            AppDataService.AddPlot(Plot);

            var navigationParameters = new NavigationParameters
            {
                { "id", Plot.ID }
            };
            this._navigationService.NavigateAsync("app:///MainPage", navigationParameters, true);
        }

        private void UpdateSelections() // TODO fix
        {
            // for (int i = 0; i < CropTypes.Count; i++)
            // {
            //     if (CropTypes[i] != Plot.Crop)
            //     {
            //         continue;
            //     }
            //
            //     PickerCropTypesSelectedIndex = i;
            //     break;
            // }
            //
            // for (int i = 0; i < MaturityClasses.Count; i++)
            // {
            //     if (MaturityClasses[i] != Plot.MaturityClass)
            //     {
            //         continue;
            //     }
            //
            //     PickerMaturityClassesSelectedIndex = i;
            //     break;
            // }
            //
            // for (int i = 0; i < ClimateTypes.Count; i++)
            // {
            //     if (ClimateTypes[i] != Plot.ClimateType)
            //     {
            //         continue;
            //     }
            //
            //     PickerClimateTypesSelectedIndex = i;
            //     break;
            // }
        }
    }
}