﻿namespace CimmytApp.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.BEM;
    using Helper.HTTP;

    /// <summary>
    /// Defines the <see cref="ViewUtilidadPageViewModel" />
    /// </summary>
    public class ViewUtilidadPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _datasets
        /// </summary>
        private ObservableCollection<Utilidad> _datasets;

        /// <summary>
        /// Gets or sets the Datasets
        /// </summary>
        public ObservableCollection<Utilidad> Datasets { get => _datasets; set => SetProperty(ref _datasets, value); }

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
            var utilidad = await RequestJson.Get<List<Utilidad>>("http://104.239.158.49/api.php?type=utilidad&tkn=E31C5F8478566357BA6875B32DC59");
            var ds = new ObservableCollection<Utilidad>();
            foreach (var dataset in utilidad)
            {
                ds.Add(dataset);
            }
            Datasets = ds;
        }
    }
}