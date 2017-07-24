namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;
	using Prism.Navigation;
    using Xamarin.Forms;

    public class AddParcelPageViewModel : BindableBase, INavigationAware
    {
        private List<string> agriculturalCycles = new List<string> { "Spring-Summer", "Autumn-Winter" };
        public List<string> AgriculturalCycles => agriculturalCycles;

        private List<string> _cropTypes;
        public List<string> CropTypes => _cropTypes;

        public List<string> Years = new List<string> { "2015", "2016", "2017" };

        private ObservableCollection<string> _singleYears;
		private ObservableCollection<string> _doubleYears;

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
			set { 
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
                Parcel.AgriculturalCycle = AgriculturalCycles.ElementAt(value);/*
                switch(value){
                    case 0:
                        Years = _singleYears;
                        break;
                    case 1:
                        Years = _doubleYears;
                        break;
                }*/
			}
		}
		private int _pickerYearsSelectedIndex;

        private int _pickerCropTypesSelectedIndex;

        public int PickerCropTypesSelectedIndex{
            get { return _pickerCropTypesSelectedIndex; }
            set
            {
                _pickerCropTypesSelectedIndex = value;
                Parcel.Crop = CropTypes.ElementAt(value);
            }
        }

        public bool Test { get; set; }

		public ICommand ClickChooseLocation;
		public ICommand ClickSave;

        public INavigationService _navigationService;

        public Parcel Parcel { get; set; }

        public AddParcelPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Parcel = new Parcel()
            {
                EstimatedParcelArea = "20178"
			};
			_singleYears = new ObservableCollection<string>() { "2015", "2016", "2017" };
			_doubleYears = new ObservableCollection<string>() { "2014-2015", "2015-2016", "2016-2017" };

            ClickSave = new Command(SaveParcel);
            ClickChooseLocation = new Command(ChooseLocation);
        }

        private void ChooseLocation(object obj)
        {
            throw new NotImplementedException();
        }

        private void SaveParcel(object obj)
        {
            Parcel.Save();

            var navigationParameters = new NavigationParameters();

            navigationParameters.Add("", ""); //TODO

            _navigationService.NavigateAsync("ParcelPage", navigationParameters);
        }

        private void AgriculturalCycleChanged()
        {
            int i = 0;
            i++;
        }

        private void updateTechChecked(){
			var technologies = new List<string>();
			if (_tech1Checked) technologies.Add("");
            if (_tech2Checked) technologies.Add("");
			if (_tech3Checked) technologies.Add("");
			if (_tech4Checked) technologies.Add("");
			if (_tech5Checked) technologies.Add("");
			if (_tech6Checked) technologies.Add("");
            if (_tech7Checked) technologies.Add("");
			if (_tech8Checked) technologies.Add("");
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