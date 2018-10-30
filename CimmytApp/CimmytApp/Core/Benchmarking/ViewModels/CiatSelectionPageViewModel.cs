namespace CimmytApp.Core.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.ViewModels;
    using Helper.Map;
    using Helper.Map.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    class CiatSelectionPageViewModel : ViewModelBase, INavigatedAware
    {
        public CiatSelectionPageViewModel(INavigationService navigationService, 
            IStringLocalizer<CiatSelectionPageViewModel> localizer) : base(localizer)
        {
            this.navigationService = navigationService;
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
            StartCommand = new DelegateCommand(Start);
        }

        private readonly INavigationService navigationService;
        private int pickerCropTypesSelectedIndex;

        public CropType SelectedCropType { get; set; }

        public string OldYield { get; set; }

        public Map.GeoPosition Position { get; set; }

        public string SelectedCrop { get; set; }

        public DelegateCommand ClickChooseLocation { get; set; }

        public DelegateCommand ClickGetLocation { get; set; }

        public DelegateCommand StartCommand { get; set; }

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

        public int PickerCropTypesSelectedIndex
        {
            get => this.pickerCropTypesSelectedIndex;
            set
            {
                SetProperty(ref this.pickerCropTypesSelectedIndex, value);
                if (value == -1)
                {
                    SelectedCrop = "Ninguno";
                    SelectedCropType = CropType.None;
                }
                else
                {
                    SelectedCrop = CropTypes.ElementAt(value);
                    SelectedCropType = (CropType)(value + 1);
                }
            }
        }

        private void Start()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { CiatContentPageViewModel.PARAMETER_NAME_POSITION, Position },
                { CiatContentPageViewModel.PARAMETER_NAME_CROP, SelectedCrop },
                { CiatContentPageViewModel.PARAMETER_NAME_CROP_TYPE, SelectedCropType },
                { CiatContentPageViewModel.PARAMETER_NAME_OLD_YIELD, double.Parse(OldYield) }
            };
            this.navigationService.NavigateAsync(CiatContentPageViewModel.DEFAULT_NAVIGATION_TITLE, parameters);
        }

        private void GetLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Map.MapTask.GetLocation }
            };
            this.navigationService.NavigateAsync("Map", parameters);
        }

        private void ChooseLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Map.MapTask.SelectLocation }
            };
            this.navigationService.NavigateAsync("Map", parameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue<Map.GeoPosition>("GeoPosition", out Map.GeoPosition geoPosition);
                if (geoPosition != null)
                {
                    Position = geoPosition;
                }
            }
        }
    }
}
