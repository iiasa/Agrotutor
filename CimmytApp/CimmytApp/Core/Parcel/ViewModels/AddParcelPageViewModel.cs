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

    public class AddParcelPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string PositionParameterName = "Position";

        public AddParcelPageViewModel(INavigationService navigationService,
            IStringLocalizer<AddParcelPageViewModel> localizer, IAppDataService appDataService) : base(localizer)
        {
            this._navigationService = navigationService;
            AppDataService = appDataService;

            Plot = new Plot();

            PickerCropTypesSelectedIndex = -1;
            PickerClimateTypesSelectedIndex = -1;
            PickerMaturityClassesSelectedIndex = -1;
        }

        public IAppDataService AppDataService { get; }

        private readonly INavigationService _navigationService;

        private Plot _plot;

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

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

        public DelegateCommand ClickSave =>
            new DelegateCommand(()=> {
                // Plot.Uploaded = (int)DatasetUploadStatus.ChangesOnDevice; todo: add this?
                AppDataService.AddPlot(Plot);
                this._navigationService.GoBackAsync();

            });

        public Plot Plot
        {
            get => this._plot;
            set
            {
                SetProperty(ref this._plot, value);
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

        public Persistence.Entities.Position Position { get; private set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(AddParcelPageViewModel.PositionParameterName))
            {
                parameters.TryGetValue<Persistence.Entities.Position>(AddParcelPageViewModel.PositionParameterName, out var position);
                if (position != null)
                {
                    Position = position;
                }
            }
        }
    }
}