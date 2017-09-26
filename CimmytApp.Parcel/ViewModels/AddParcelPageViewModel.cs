﻿using System;
using Helper.BusinessContract;
using Helper.Localization;
using Prism.Commands;

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
        private readonly ICimmytDbOperations _cimmytDbOperations;
        private readonly List<string> _doubleYears = new List<string>() { "2014-2015", "2015-2016", "2016-2017" };
        private readonly INavigationService _navigationService;
        private readonly List<string> _singleYears = new List<string>() { "2015", "2016", "2017" };
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
        private bool _userIsAtParcel;
        private List<string> _years;
        private bool _isSaveBtnEnabled = true;
        private bool _activity1Checked;
        private bool _activity9Checked;
        private bool _activity8Checked;
        private bool _activity7Checked;
        private bool _activity6Checked;
        private bool _activity5Checked;
        private bool _activity4Checked;
        private bool _activity3Checked;
        private bool _activity2Checked;

        public AddParcelPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
            Years = new List<string>();

            ClickSave = new DelegateCommand(SaveParcel).ObservesCanExecute(o => IsSaveBtnEnabled);
            ClickChooseLocation = new Command(ChooseLocation);

            Parcel = new Parcel();
        }

        public bool IsSaveBtnEnabled
        {
            get
            {
                return _isSaveBtnEnabled;
            }
            set
            {
                SetProperty(ref _isSaveBtnEnabled, value);
            }
        }

        public bool InformationMissing { get { return !IsSaveBtnEnabled; } }
        public List<string> AgriculturalCycles { get; } = new List<string> { "Primavera-Verano", "Otoño-Invierno" };

        public ICommand ClickChooseLocation { get; set; }
        public DelegateCommand ClickSave { get; set; }
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

        public bool Activity1Checked
        {
            get => _activity1Checked;
            set
            {
                SetProperty(ref _activity1Checked, value);
                UpdateActivityChecked();
            }
        }

        public bool Activity2Checked
        {
            get => _activity2Checked;
            set
            {
                SetProperty(ref _activity2Checked, value);
                UpdateActivityChecked();
            }
        }

        public bool Activity3Checked
        {
            get => _activity3Checked;
            set
            {
                SetProperty(ref _activity3Checked, value);
                UpdateActivityChecked();
            }
        }

        public bool Activity4Checked
        {
            get => _activity4Checked;
            set
            {
                SetProperty(ref _activity4Checked, value);
                UpdateActivityChecked();
            }
        }

        public bool Activity5Checked
        {
            get => _activity5Checked;
            set
            {
                SetProperty(ref _activity5Checked, value);
                UpdateActivityChecked();
            }
        }

        public bool Activity6Checked
        {
            get => _activity6Checked;
            set
            {
                SetProperty(ref _activity6Checked, value);
                UpdateActivityChecked();
            }
        }

        public bool Activity7Checked
        {
            get => _activity7Checked;
            set
            {
                SetProperty(ref _activity7Checked, value);
                UpdateActivityChecked();
            }
        }

        public bool Activity8Checked
        {
            get => _activity8Checked;
            set
            {
                SetProperty(ref _activity8Checked, value);
                UpdateActivityChecked();
            }
        }

        public bool Activity9Checked
        {
            get => _activity9Checked;
            set
            {
                SetProperty(ref _activity9Checked, value);
                UpdateActivityChecked();
            }
        }

        public bool Test { get; set; }

        public bool UserIsAtParcel
        {
            get => _userIsAtParcel;
            set => SetProperty(ref _userIsAtParcel, value);
        }

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
                if (geoPosition == null) return;
                var position = (GeoPosition)geoPosition;
                Parcel.Latitude = position.Latitude;
                Parcel.Longitude = position.Longitude;
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private bool CheckFields()
        {
            if (Parcel.EstimatedParcelArea == null) return false;
            if (Parcel.EstimatedParcelArea.Equals(string.Empty)) return false;
            if (Parcel.ProducerName == null) return false;
            if (Parcel.ProducerName.Equals(string.Empty)) return false;
            if (Parcel.ParcelName == null) return false;
            if (Parcel.ParcelName.Equals(string.Empty)) return false;
            if (Parcel.Cultivar == null) return false;
            if (Parcel.Cultivar.Equals(string.Empty)) return false;
            return true;
        }

        private void ChooseLocation()
        {
            var parameters = new NavigationParameters { { "GetLocation", true } };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        private void SaveParcel()
        {
            IsSaveBtnEnabled = false;
            _cimmytDbOperations.AddParcel(Parcel);

            var navigationParameters = new NavigationParameters
            {
                { "id", Parcel.ParcelId }
            };
            _navigationService.NavigateAsync("MainPage", navigationParameters, true);
        }

        private void UpdateTechChecked()
        {
            var technologies = new List<string>();
            if (_tech1Checked) technologies.Add("Cambio a variedades mejoradas, nuevas y adaptadas a las zonas con potencial para incrementar el rendimiento ");
            if (_tech2Checked) technologies.Add("Interpretación y uso del análisis de suelo");
            if (_tech3Checked) technologies.Add("Uso del sensor infrarrojo para fertilización óptima");
            if (_tech4Checked) technologies.Add("Uso de biofertilizantes");
            if (_tech5Checked) technologies.Add("Mejoradores de suelo para complementar fertilización");
            if (_tech6Checked) technologies.Add("Mínimo movimiento de suelo, retención de residuos y rotación de cultivos");
            if (_tech7Checked) technologies.Add("Introducción de nuevos cultivos en la rotación (ejemplo: cultivos de forraje)");
            if (_tech8Checked) technologies.Add("Tecnología para mejorar el almacenamiento del grano");
            Parcel.TechnologiesUsed = technologies;
        }

        private void UpdateActivityChecked()
        {
            var activities = new List<string>();
            if (_activity1Checked) activities.Add("Preparación del terreno");
            if (_activity2Checked) activities.Add("Aplicación de fertilizante foliar");
            if (_activity3Checked) activities.Add("Aplicación de fertilizante orgánico");
            if (_activity4Checked) activities.Add("Fertilización química al suelo");
            if (_activity5Checked) activities.Add("Aplicación de herbicidas después de la siembra");
            if (_activity6Checked) activities.Add("Aplicación de herbicidas presiembra");
            if (_activity7Checked) activities.Add("Labores culturales y control físico de malezas");
            if (_activity8Checked) activities.Add("Aplicación de fungicidas");
            if (_activity9Checked) activities.Add("Aplicación de insecticidas");
            Parcel.Activities = activities;
        }
    }
}