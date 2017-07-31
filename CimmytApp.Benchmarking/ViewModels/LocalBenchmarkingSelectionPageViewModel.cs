namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    public class LocalBenchmarkingSelectionPageViewModel : BindableBase
    {

        public ICommand ViewDataCommand;

        public int DatasetSelectedIndex { get;  set;}
		public List<string> DatasetSelection { get; set; }


		public bool FilterYears { get; set; }
		public int YearsSelectedIndex { get; set; }
		public List<string> YearsSelection { get; set; }


		public bool FilterCycle { get; set; }
		public int CycleSelectedIndex { get; set; }
		public List<string> CycleSelection { get; set; }



        private INavigationService _navigationService;

        public LocalBenchmarkingSelectionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ViewDataCommand = new Command(ViewData);
        }

        private void ViewData(){
            NavigationParameters navigationParams = new NavigationParameters
            {
                { "dataset", DatasetSelection.ElementAt(DatasetSelectedIndex) }
            };

			if (FilterYears) navigationParams.Add("year", YearsSelection.ElementAt(YearsSelectedIndex));
            if (FilterCycle) navigationParams.Add("cycle", CycleSelection.ElementAt(CycleSelectedIndex));

            _navigationService.NavigateAsync("LocalBenchmarkingPage", navigationParams);
        }
    }
}
