namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.BEM;
    using Helper.HTTP;

    /// <summary>
    /// Defines the <see cref="ViewIngresoPageViewModel" />
    /// </summary>
    public class ViewIngresoPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _datasets
        /// </summary>
        private ObservableCollection<Ingreso> _datasets;

        /// <summary>
        /// Gets or sets the Datasets
        /// </summary>
        public ObservableCollection<Ingreso> Datasets { get => _datasets; set => SetProperty(ref _datasets, value); }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            LoadData();
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        public async void LoadData()
        {
            var ingreso = await RequestJson.Get<List<Ingreso>>("http://104.239.158.49/api.php?type=ingreso&tkn=E31C5F8478566357BA6875B32DC59");
            var ds = new ObservableCollection<Ingreso>();
            foreach (var dataset in ingreso)
            {
                ds.Add(dataset);
            }
            Datasets = ds;
        }
    }
}