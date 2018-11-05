namespace CimmytApp.Core.Parcel.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.Parcel;
    using CimmytApp.ViewModels;
    using Helper.Map;
    using Helper.Map.ViewModels;
    using Helper.Realm.BusinessContract;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;
    using Xamarin.Forms.GoogleMaps;

    public class AddParcelPageViewModel : ViewModelBase, INavigatedAware
    {

        public AddParcelPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations,
            IStringLocalizer<AddParcelPageViewModel> localizer, IAppDataService appDataService) : base(localizer)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            AppDataService = appDataService;

            ClickSave = new DelegateCommand(SaveParcel); //.ObservesCanExecute(o => IsSaveBtnEnabled);
            ClickChooseLocation = new DelegateCommand(ChooseLocation);
            ClickGetLocation = new DelegateCommand(GetLocation);
            ClickDelineate = new DelegateCommand(Delineate);

            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);

            Parcel = new Parcel();

            PickerCropTypesSelectedIndex = -1;
            PickerClimateTypesSelectedIndex = -1;
            PickerMaturityClassesSelectedIndex = -1;
        }

        private readonly ICimmytDbOperations _cimmytDbOperations;

        public IAppDataService AppDataService { get; }

        private readonly INavigationService _navigationService;

        private bool _isSaveBtnEnabled = true;

        private Parcel _parcel;

        private int _pickerClimateTypesSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        private int _pickerMaturityClassesSelectedIndex;

        private bool _userIsAtParcel;

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
            get => _isSaveBtnEnabled;
            set => SetProperty(ref _isSaveBtnEnabled, value);
        }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                SetProperty(ref _parcel, value);
                UpdateSelections();
            }
        }

        public int PickerClimateTypesSelectedIndex
        {
            get => _pickerClimateTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerClimateTypesSelectedIndex, value);
                Parcel.ClimateType = value == -1 ? null : ClimateTypes.ElementAt(value);
            }
        }

        public int PickerCropTypesSelectedIndex
        {
            get => _pickerCropTypesSelectedIndex;
            set
            {
                SetProperty(ref _pickerCropTypesSelectedIndex, value);
                if (value == -1)
                {
                    Parcel.Crop = "Ninguno";
                    Parcel.CropType = CropType.None;
                }
                else
                {
                    Parcel.Crop = CropTypes.ElementAt(value);
                    Parcel.CropType = (CropType)(value + 1);
                }
            }
        }

        public int PickerMaturityClassesSelectedIndex
        {
            get => _pickerMaturityClassesSelectedIndex;
            set
            {
                SetProperty(ref _pickerMaturityClassesSelectedIndex, value);
                Parcel.MaturityClass = value == -1 ? null : MaturityClasses.ElementAt(value);
            }
        }

        public bool UserIsAtParcel
        {
            get => _userIsAtParcel;
            set => SetProperty(ref _userIsAtParcel, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue<Parcel>("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcel = parcel;
                }
            }
            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue<List<AgriculturalActivity>>("Activities", out var activities);
                if (Parcel.AgriculturalActivities == null)
                {
                    Parcel.AgriculturalActivities = activities;
                }
                else
                {
                    if (activities != null)
                    {
                        activities.AddRange(Parcel.AgriculturalActivities);
                        Parcel.AgriculturalActivities = activities;
                    }
                }
            }
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue<Core.Map.GeoPosition>("GeoPosition", out var geoPosition);
                if (geoPosition != null)
                {
                    Parcel.Position = geoPosition;
                }
            }

            if (parameters.ContainsKey("Delineation"))
            {
                parameters.TryGetValue<List<Core.Map.GeoPosition>>("Delineation", out var delineation);
                Parcel.Delineation = delineation;

                //_cimmytDbOperations.SaveParcelPolygon(Parcel.ParcelId, polygonObj); TODO ensure saving
            }

            if (parameters.ContainsKey(ParcelConstants.TechnologiesParameterName))
            {
                parameters.TryGetValue<List<Technology>>(ParcelConstants.TechnologiesParameterName,
                    out var technologies);
                if (Parcel != null)
                {
                    Parcel.TechnologiesUsed = technologies;
                }
            }
        }

        private void GetLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.GetLocation }
            };
            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        private void ChooseLocation()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.SelectLocation }
            };
            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        private void Delineate()
        {
            var parameters = new NavigationParameters
            {
                { MapViewModel.MapTaskParameterName, Core.Map.MapTask.SelectPolygon }
            };
            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                parameters.Add(MapViewModel.MapCenterParameterName,
                    CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                        new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }
            _navigationService.NavigateAsync("Map", parameters);
        }

        private void NavigateAsync(string page)
        {
            var parameters = new NavigationParameters
            {
                { "Caller", "AddParcelPage" },
                { "Parcel", Parcel }
            };
            _navigationService.NavigateAsync(page, parameters);
        }

        private void SaveParcel()
        {
            IsSaveBtnEnabled = false;
            Parcel.Uploaded = (int)DatasetUploadStatus.ChangesOnDevice;
            _cimmytDbOperations.SaveParcel(Parcel.GetDTO());
            SavePlot();

            var navigationParameters = new NavigationParameters
            {
                { "id", Parcel.ParcelId }
            };
            _navigationService.NavigateAsync("app:///MainPage", navigationParameters, true);
        }

        private async void SavePlot()
        {
            await AppDataService.AddPlot(new Plot
            {
                Name = "Test"
            });
        }

        private void UpdateSelections()
        {
            for (int i = 0; i < CropTypes.Count; i++)
            {
                if (CropTypes[i] != Parcel.Crop)
                {
                    continue;
                }

                PickerCropTypesSelectedIndex = i;
                break;
            }

            for (int i = 0; i < MaturityClasses.Count; i++)
            {
                if (MaturityClasses[i] != Parcel.MaturityClass)
                {
                    continue;
                }

                PickerMaturityClassesSelectedIndex = i;
                break;
            }

            for (int i = 0; i < ClimateTypes.Count; i++)
            {
                if (ClimateTypes[i] != Parcel.ClimateType)
                {
                    continue;
                }

                PickerClimateTypesSelectedIndex = i;
                break;
            }
        }
    }
}