using System.Threading.Tasks;

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
        private BemData _bemData;
        private bool _filterCycle;
        private bool _filterYears;
        private bool _dataAvailable;

        public bool DataAvailable
        {
            get => _dataAvailable;
            set => SetProperty(ref _dataAvailable, value);
        }

        public LocalBenchmarkingSelectionPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _cimmytDbOperations = cimmytDbOperations;
            _navigationService = navigationService;
            ViewDataCommand = new Command(ViewData);
            RefreshDataCommand = new Command(LoadData);

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

        private async void LoadData()
        {
            _bemData = await RefreshData();
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

        private static List<T> FilterDatasets<T>(List<T> bemDatasets, string year, string cycle) where T : BemDataset
        {
            var datasets = new List<T>();
            datasets.AddRange(bemDatasets);
            List<T> selection;
            if (year != null)
            {
                selection = new List<T>();
                selection.AddRange(datasets.Cast<T>().Where(dataset => dataset.OutYear.Equals(year)));
                datasets.Clear();
                datasets.AddRange(selection);
            }
            if (cycle != null)
            {
                selection = new List<T>();
                selection.AddRange(datasets.Cast<T>().Where(dataset => dataset.OutCycle.Equals(cycle)));
                datasets.Clear();
                datasets.AddRange(selection);
            }
            return datasets;
        }

        private async void GetData()
        {
            _bemData = _cimmytDbOperations.GetBemData();
            if (_bemData.IsEmpty)
            {
                _bemData = await RefreshData();
            }
            DataAvailable = true;
        }

        private async Task<BemData> RefreshData()
        {
            var bemData = await BemData.LoadBEMData();
            _cimmytDbOperations.SaveCostos(bemData.Costo);
            _cimmytDbOperations.SaveIngresos(bemData.Ingreso);
            _cimmytDbOperations.SaveRendimientos(bemData.Rendimiento);
            _cimmytDbOperations.SaveUtilidades(bemData.Utilidad);
            return bemData;
        }

        private void ViewData()
        {
            if (_bemData == null) return;

            var pageToCall = "";

            string year = null;
            string cycle = null;

            if (FilterYears) year = YearsSelection.ElementAt(YearsSelectedIndex);
            if (FilterCycle) cycle = CycleSelection.ElementAt(CycleSelectedIndex);

            var navigationParams = new NavigationParameters();

            switch (DatasetSelectedIndex)
            {
                case 0:
                    pageToCall = "ViewCostoPage";
                    var datasets = new List<Costo>();
                    datasets.AddRange(_bemData.Costo);
                    datasets = FilterDatasets<Costo>(datasets, year, cycle);
                    navigationParams.Add("Datasets", datasets);
                    break;

                case 1:
                    pageToCall = "ViewIngresoPage";
                    var datasets1 = new List<Ingreso>();
                    datasets1.AddRange(_bemData.Ingreso);
                    datasets1 = FilterDatasets<Ingreso>(datasets1, year, cycle);
                    navigationParams.Add("Datasets", datasets1);
                    break;

                case 2:
                    pageToCall = "ViewRendimientoPage";
                    var datasets2 = new List<Rendimiento>();
                    datasets2.AddRange(_bemData.Rendimiento);
                    datasets2 = FilterDatasets<Rendimiento>(datasets2, year, cycle);
                    navigationParams.Add("Datasets", datasets2);
                    break;

                case 3:
                    pageToCall = "ViewUtilidadPage";
                    var datasets3 = new List<Utilidad>();
                    datasets3.AddRange(_bemData.Utilidad);
                    datasets3 = FilterDatasets<Utilidad>(datasets3, year, cycle);
                    navigationParams.Add("Datasets", datasets3);
                    break;
            }

            _navigationService.NavigateAsync(pageToCall, navigationParams);
        }
    }
}