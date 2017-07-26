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

    public class AddParcelPageViewModel : BindableBase, INavigationAware
    {
        private List<string> agriculturalCycles = new List<string> { "Spring-Summer", "Autumn-Winter" };
        public List<string> AgriculturalCycles => agriculturalCycles;

        private List<string> _cropTypes = new List<string>{"Maize", "Barley", "Potato"};
        public List<string> CropTypes => _cropTypes;

        private List<string> _years = new List<string> { "2015", "2016", "2017" };
		public List<string> Years => _years;

		private List<string> _irrigationTypes = new List<string> { "Irrigation", "Sprinkler irrigation", "Temporal" };
		public List<string> IrrigationTypes => _irrigationTypes;


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
            get { return _tech1Checked; }
            set
            {
                _tech1Checked = value;
                updateTechChecked();
            }
        }

        public bool Tech2Checked
        {
            get { return _tech2Checked; }
            set
            {
                _tech2Checked = value;
                updateTechChecked();
            }
        }

        public bool Tech3Checked
        {
            get { return _tech3Checked; }
            set
            {
                _tech3Checked = value;
                updateTechChecked();
            }
        }

        public bool Tech4Checked
        {
            get { return _tech4Checked; }
            set
            {
                _tech4Checked = value;
                updateTechChecked();
            }
        }

        public bool Tech5Checked
        {
            get { return _tech5Checked; }
            set
            {
                _tech5Checked = value;
                updateTechChecked();
            }
        }

        public bool Tech6Checked
        {
            get { return _tech6Checked; }
            set
            {
                _tech6Checked = value;
                updateTechChecked();
            }
        }

        public bool Tech7Checked
        {
            get { return _tech7Checked; }
            set
            {
                _tech7Checked = value;
                updateTechChecked();
            }
        }

        public bool Tech8Checked
        {
            get { return _tech8Checked; }
            set
            {
                _tech8Checked = value;
                updateTechChecked();
            }
        }

        public int PickerYearsSelectedIndex
        {
            get { return _pickerYearsSelectedIndex; }
            set
            {
                _pickerYearsSelectedIndex = value;
                Parcel.Year = Years.ElementAt(value);
            }
        }

        private int _pickerAgriculturalCycleSelectedIndex;

        public int PickerAgriculturalCycleSelectedIndex
        {
            get { return _pickerAgriculturalCycleSelectedIndex; }
            set
            {
                _pickerAgriculturalCycleSelectedIndex = value;
                Parcel.AgriculturalCycle = AgriculturalCycles.ElementAt(value);
                switch(value){
                    case 0:
                        _years = _singleYears;
                        break;

                    case 1:
                        _years = _doubleYears;
                        break;
                }
            }
        }

		private int _pickerYearsSelectedIndex;

		private int _pickerCropTypesSelectedIndex;

		public int PickerCropTypesSelectedIndex
		{
			get { return _pickerCropTypesSelectedIndex; }
			set
			{
				_pickerCropTypesSelectedIndex = value;
				Parcel.Crop = CropTypes.ElementAt(value);
			}
		}

		private int _pickerIrrigationTypesSelectedIndex;

		public int PickerIrrigationTypesSelectedIndex
		{
			get { return _pickerIrrigationTypesSelectedIndex; }
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

        public AddParcelPageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;

			ClickSave = new Command(SaveParcel);
			ClickChooseLocation = new Command(ChooseLocation);

            Parcel = new Parcel();
            _singleYears = new List<string>() { "2015", "2016", "2017" };
            _doubleYears = new List<string>() { "2014-2015", "2015-2016", "2016-2017" };
        }

        private void ChooseLocation(object obj)
        {
            throw new NotImplementedException();
        }

        private void SaveParcel(object obj)
        {
            Parcel.Save();

            var navigationParameters = new NavigationParameters();

            navigationParameters.Add("id", Parcel.ID);

            _navigationService.NavigateAsync("ParcelPage", navigationParameters);
        }

        private void AgriculturalCycleChanged()
        {
            int i = 0;
            i++;
        }

        private void updateTechChecked()
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
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}