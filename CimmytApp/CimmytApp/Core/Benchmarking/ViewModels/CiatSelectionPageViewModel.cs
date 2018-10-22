namespace CimmytApp.Core.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using CimmytApp.DTO.Parcel;
    using Helper.Map;
    using Helper.Map.ViewModels;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    class CiatSelectionPageViewModel : BindableBase, INavigatedAware
    {
        public CiatSelectionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
            StartCommand = new DelegateCommand(Start);
        }

        private readonly INavigationService _navigationService;
        private int _pickerCropTypesSelectedIndex;

        public CropType SelectedCropType { get; set; }

        public string OldYield { get; set; }

        public GeoPosition Position { get; set; }

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
            get => _pickerCropTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerCropTypesSelectedIndex, value);
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
            this._navigationService.NavigateAsync(CiatContentPageViewModel.DEFAULT_NAVIGATION_TITLE, parameters);
        }

        private void GetLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, MapTask.GetLocation }
            };
            _navigationService.NavigateAsync("Map", parameters);
        }

        private void ChooseLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, MapTask.SelectLocation }
            };
            _navigationService.NavigateAsync("Map", parameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue<GeoPosition>("GeoPosition", out GeoPosition geoPosition);
                if (geoPosition != null)
                {
                    Position = geoPosition;
                }
            }
        }
    }
}
