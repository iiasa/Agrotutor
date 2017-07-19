namespace CimmytApp.DTO.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class AddParcelPageViewModel : BindableBase, INavigationAware
    {
        private List<string> agriculturalCycles = new List<string> { "Spring-Summer", "Autumn-Winter" };
        public List<string> AgriculturalCycles => agriculturalCycles;
        public List<string> Years;

        private List<string> _singleYears;
		private List<string> _doubleYears;

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
                        Years = _singleYears;
                        break;
                    case 1:
                        Years = _doubleYears;
                        break;
                }
			}
		}
		private int _pickerYearsSelectedIndex;

        public bool Test { get; set; }

        public ICommand Moo;

        public Parcel Parcel { get; set; }

        public AddParcelPageViewModel()
        {
            Parcel = new Parcel()
            {
                EstimatedParcelArea = 20178
			};
			_singleYears = new List<string>() { "2015", "2016", "2017" };
			_doubleYears = new List<string>() { "2014-2015", "2015-2016", "2016-2017" };
            Years = _singleYears;
        }

        private void AgriculturalCycleChanged()
        {
            int i = 0;
            i++;
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