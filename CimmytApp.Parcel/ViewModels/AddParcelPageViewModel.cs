namespace CimmytApp.Parcel.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    using BusinessContract;
    using DTO.Parcel;

    using Helper.Base.DTO;

    public class AddParcelPageViewModel : BindableBase, INavigationAware
    {
        private readonly List<string> _doubleYears = new List<string>() { "2014-2015", "2015-2016", "2016-2017" };
        private readonly List<string> _singleYears = new List<string>() { "2015", "2016", "2017" };
        private readonly ICimmytDbOperations _cimmytDbOperations;
        private readonly INavigationService _navigationService;
        private int _pickerAgriculturalCycleSelectedIndex;
        private int _pickerCropTypesSelectedIndex;
        private int _pickerIrrigationTypesSelectedIndex;
        private int _pickerYearsSelectedIndex;
        private bool _tech1Checked;
        private bool _tech2Checked;
        private bool _tech3Checked;
        private bool _tech4Checked;
        private bool _tech5Checked;
        private bool _tech6Checked;
        private bool _tech7Checked;
        private bool _tech8Checked;
        private List<string> _years;

        public AddParcelPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            Years = new List<string>();

            ClickSave = new Command(SaveParcel);
            ClickChooseLocation = new Command(ChooseLocation);

            Parcel = new Parcel();
        }

        public List<string> AgriculturalCycles { get; } = new List<string> { "Primavera-Verano", "Otoño-Invierno" };

        public ICommand ClickChooseLocation { get; set; }
        public ICommand ClickSave { get; set; }
        public List<string> CropTypes { get; } = new List<string> { "Maíz", "Cebada", "Frijol", "Trigo", "Triticale", "Sorgo", "Alfalfa", "Avena", "Ajonjolí", "Amaranto", "Arroz", "Canola", "Cartamo", "Calabacín", "Garbanzo", "Haba", "Soya", "Ninguno", "Otro" };
        public List<string> IrrigationTypes { get; } = new List<string> { "Riego", "Riego de punteo", "Temporal" };

        public Parcel Parcel { get; set; }

        public int PickerAgriculturalCycleSelectedIndex
        {
            get => _pickerAgriculturalCycleSelectedIndex;
            set
            {
                _pickerAgriculturalCycleSelectedIndex = value;
                Parcel.AgriculturalCycle = AgriculturalCycles.ElementAt(value);
                var yrs = Years;
                yrs.Clear();
                yrs.AddRange(value == 0 ? _singleYears : _doubleYears);

                Years = yrs;
            }
        }

        public int PickerCropTypesSelectedIndex
        {
            get => _pickerCropTypesSelectedIndex;
            set
            {
                _pickerCropTypesSelectedIndex = value;
                Parcel.Crop = CropTypes.ElementAt(value);
                Parcel.CropType = (CropType)(value + 1);
            }
        }

        public int PickerIrrigationTypesSelectedIndex
        {
            get => _pickerIrrigationTypesSelectedIndex;
            set
            {
                _pickerIrrigationTypesSelectedIndex = value;
                Parcel.Irrigation = IrrigationTypes.ElementAt(value);
            }
        }

        public int PickerYearsSelectedIndex
        {
            get => _pickerYearsSelectedIndex;
            set
            {
                _pickerYearsSelectedIndex = value;
                Parcel.Year = Years.ElementAt(value);
            }
        }

        public bool Tech1Checked
        {
            get => _tech1Checked;
            set
            {
                _tech1Checked = value;
                UpdateTechChecked();
            }
        }

        public bool Tech2Checked
        {
            get => _tech2Checked;
            set
            {
                _tech2Checked = value;
                UpdateTechChecked();
            }
        }

        public bool Tech3Checked
        {
            get => _tech3Checked;
            set
            {
                _tech3Checked = value;
                UpdateTechChecked();
            }
        }

        public bool Tech4Checked
        {
            get => _tech4Checked;
            set
            {
                _tech4Checked = value;
                UpdateTechChecked();
            }
        }

        public bool Tech5Checked
        {
            get => _tech5Checked;
            set
            {
                _tech5Checked = value;
                UpdateTechChecked();
            }
        }

        public bool Tech6Checked
        {
            get => _tech6Checked;
            set
            {
                _tech6Checked = value;
                UpdateTechChecked();
            }
        }

        public bool Tech7Checked
        {
            get => _tech7Checked;
            set
            {
                _tech7Checked = value;
                UpdateTechChecked();
            }
        }

        public bool Tech8Checked
        {
            get => _tech8Checked;
            set
            {
                _tech8Checked = value;
                UpdateTechChecked();
            }
        }

        public bool Test { get; set; }

        public List<string> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("GeoPosition"))
            {
                parameters.TryGetValue("GeoPosition", out object geoPosition);
                if (geoPosition != null)
                {
                    var position = (GeoPosition)geoPosition;
                    Parcel.Latitude = position.Latitude;
                    Parcel.Longitude = position.Longitude;
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private bool CheckFields()
        {
            return true;
        }

        private void ChooseLocation()
        {
            var parameters = new NavigationParameters { { "GetLocation", true } };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        private void SaveParcel()
        {
            if (CheckFields() == false)
            {
            }
            _cimmytDbOperations.AddParcel(Parcel);

            var navigationParameters = new NavigationParameters
            {
                { "id", Parcel.ParcelId }
            };
            _navigationService.NavigateAsync("MainPage", navigationParameters, true);//This makes issues with Map screen
        }

        private void UpdateTechChecked()
        {
            var technologies = new List<string>();
            if (_tech1Checked) technologies.Add("tech1");
            if (_tech2Checked) technologies.Add("tech2");
            if (_tech3Checked) technologies.Add("tech3");
            if (_tech4Checked) technologies.Add("tech4");
            if (_tech5Checked) technologies.Add("tech5");
            if (_tech6Checked) technologies.Add("tech6");
            if (_tech7Checked) technologies.Add("tech7");
            if (_tech8Checked) technologies.Add("tech8");
            Parcel.TechnologiesUsed = technologies;
        }
    }
}