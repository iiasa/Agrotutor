namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;

    using BusinessContract;
    using DTO.BEM;

    public class LocalBenchmarkingSelectionPageViewModel : BindableBase
    {
        private readonly ICimmytDbOperations _cimmytDbOperations;
        private readonly INavigationService _navigationService;
        private bool _filterCycle;
        private bool _filterYears;

        public LocalBenchmarkingSelectionPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _cimmytDbOperations = cimmytDbOperations;
            _navigationService = navigationService;
            ViewDataCommand = new Command(ViewData);
            RefreshDataCommand = new Command(RefreshData);

            DatasetSelection = new List<string>
            {
                "Costo",
                "Ingreso",
                "Rendimiento",
                "Utilidad"
            };

            YearsSelection = new List<string>{
                "2016",
                "2017"
            };

            CycleSelection = new List<string>{
                "Primavera-Verano",
                "Otoño-Invierno"
            };
        }

        public int CycleSelectedIndex { get; set; }
        public List<string> CycleSelection { get; set; }
        public int DatasetSelectedIndex { get; set; }
        public List<string> DatasetSelection { get; set; }

        public bool FilterCycle
        {
            get => _filterCycle;
            set => SetProperty(ref _filterCycle, value);
        }

        public bool FilterYears
        {
            get => _filterYears;
            set => SetProperty(ref _filterYears, value);
        }

        public ICommand RefreshDataCommand { get; set; }
        public ICommand ViewDataCommand { get; set; }
        public int YearsSelectedIndex { get; set; }
        public List<string> YearsSelection { get; set; }

        private async void RefreshData()
        {
            var bemData = await BemData.LoadBEMData();
            _cimmytDbOperations.SaveCostos(bemData.Costo);
            _cimmytDbOperations.SaveIngresos(bemData.Ingreso);
            _cimmytDbOperations.SaveRendimientos(bemData.Rendimiento);
            _cimmytDbOperations.SaveUtilidades(bemData.Utilidad);
        }

        private void ViewData()
        {
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