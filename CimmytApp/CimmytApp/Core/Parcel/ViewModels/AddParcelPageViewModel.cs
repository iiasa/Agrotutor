namespace CimmytApp.Core.Parcel.ViewModels
{
    using System.Collections.Generic;

    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.ViewModels;

    using Microsoft.Extensions.Localization;

    using Prism.Commands;
    using Prism.Navigation;

    public class AddParcelPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string PositionParameterName = "Position";

        private readonly INavigationService _navigationService;

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

        private Plot _plot;

        public AddParcelPageViewModel(
            INavigationService navigationService,
            IStringLocalizer<AddParcelPageViewModel> localizer,
            IAppDataService appDataService)
            : base(localizer)
        {
            this._navigationService = navigationService;
            AppDataService = appDataService;

            Plot = new Plot();

            PickerCropTypesSelectedIndex = -1;
            PickerClimateTypesSelectedIndex = -1;
            PickerMaturityClassesSelectedIndex = -1;
        }

        public IAppDataService AppDataService { get; }

        public DelegateCommand ClickSave =>
            new DelegateCommand(
                () =>
                {
                    // Plot.Uploaded = (int)DatasetUploadStatus.ChangesOnDevice; todo: add this?
                    AppDataService.AddPlot(Plot);
                    this._navigationService.GoBackAsync();
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(AddParcelPageViewModel.PositionParameterName))
            {
                parameters.TryGetValue(AddParcelPageViewModel.PositionParameterName, out Position position);
                if (position != null)
                {
                    Plot.Position = position;
                }
            }
        }
    }
}