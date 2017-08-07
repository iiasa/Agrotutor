using CimmytApp.DTO;

namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    using DTO.Parcel;
    using CimmytApp.BusinessContract;

    using Helper.Base.DTO;

    public class AddParcelPageViewModel : BindableBase, INavigationAware
    {
        public List<string> AgriculturalCycles { get; } = new List<string> { "Primavera-Verano", "Otoño-Invierno" };

        public List<string> CropTypes { get; } = new List<string> { "Maíz", "Amaranto", "Arroz", "Canola", "Cartamo", "Calabacín", "Garbanzo", "Haba", "Soya", "Ninguno", "Otro", "Cebada", "Frijol", "Trigo", "Triticale", "Sorgo", "Alfalfa", "Avena", "Ajonjolí" };

        private List<string> _years = new List<string> { "2015", "2016", "2017" };

        public List<string> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        public List<string> IrrigationTypes { get; } = new List<string> { "Riego", "Riego de punteo", "Temporal" };

        private bool _tech1Checked;
        private bool _tech2Checked;
        private bool _tech3Checked;
        private bool _tech4Checked;
        private bool _tech5Checked;
        private bool _tech6Checked;
        private bool _tech7Checked;
        private bool _tech8Checked;

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

        public int PickerYearsSelectedIndex
        {
            get => _pickerYearsSelectedIndex;
            set
            {
                _pickerYearsSelectedIndex = value;
                Parcel.Year = Years.ElementAt(value);
            }
        }

        private int _pickerAgriculturalCycleSelectedIndex;

        public int PickerAgriculturalCycleSelectedIndex
        {
            get => _pickerAgriculturalCycleSelectedIndex;
            set
            {
                _pickerAgriculturalCycleSelectedIndex = value;
                Parcel.AgriculturalCycle = AgriculturalCycles.ElementAt(value);
                switch (value)
                {
                    case 0:
                        Years = _singleYears;
                        break;

                    case 1:
                        Years = _doubleYears;
                        break;
                }
            }
        }

        private int _pickerYearsSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        public int PickerCropTypesSelectedIndex
        {
            get => _pickerCropTypesSelectedIndex;
            set
            {
                _pickerCropTypesSelectedIndex = value;
                Parcel.Crop = CropTypes.ElementAt(value);
            }
        }

        private int _pickerIrrigationTypesSelectedIndex;

        public int PickerIrrigationTypesSelectedIndex
        {
            get => _pickerIrrigationTypesSelectedIndex;
            set
            {
                _pickerIrrigationTypesSelectedIndex = value;
                Parcel.Irrigation = IrrigationTypes.ElementAt(value);
            }
        }

        public bool Test { get; set; }

        public ICommand ClickChooseLocation { get; set; }
        public ICommand ClickSave { get; set; }

        public INavigationService _navigationService;

        public Parcel Parcel { get; set; }

        private List<string> _singleYears;
        private List<string> _doubleYears;
        private ICimmytDbOperations _cimmytDbOperations;

        public AddParcelPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;

            ClickSave = new Command(SaveParcel);
            ClickChooseLocation = new Command(ChooseLocation);

            Parcel = new Parcel();
            _singleYears = new List<string>() { "2015", "2016", "2017" };
            _doubleYears = new List<string>() { "2014-2015", "2015-2016", "2016-2017" };
        }

        private void ChooseLocation()
        {
            var parameters = new NavigationParameters { { "GetLocation", true } };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        private void SaveParcel()
        {
            _cimmytDbOperations.AddParcel(Parcel);

            var navigationParameters = new NavigationParameters
            {
                { "id", Parcel.ParcelId }
            };
            _navigationService.NavigateAsync("MainPage", navigationParameters, false);//This makes issues with Map screen
        }

        private void AgriculturalCycleChanged()
        {
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("GeoPosition"))
            {
                object geoPosition;
                parameters.TryGetValue("GeoPosition", out geoPosition);
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
    }
}