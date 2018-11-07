namespace CimmytApp.Core.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Acr.UserDialogs;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    /// <summary>
    ///     Defines the <see cref="ParcelsOverviewPageViewModel" />
    /// </summary>
    public class ParcelsOverviewPageViewModel : ViewModelBase
    {

        /// <summary>
        ///     Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        ///     Defines the _addParcelHintIsVisible
        /// </summary>
        private bool _addParcelHintIsVisible = true;

        /// <summary>
        ///     Defines the _isParcelListEnabled
        /// </summary>
        private bool _isParcelListEnabled = true;

        private ObservableCollection<PlotViewModel> _observableParcel;

        /// <summary>
        ///     Defines the _oldParcel
        /// </summary>
        private PlotViewModel _oldParcel;

        /// <summary>
        ///     Defines the _parcels
        /// </summary>
        private List<Plot> _plots;

        /// <summary>
        ///     Defines the _parcelsListIsVisible
        /// </summary>
        private bool _parcelsListIsVisible;

        /// <summary>
        ///     Defines the _showUploadButton
        /// </summary>
        private bool _showUploadButton;

        public IAppDataService AppDataService { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParcelsOverviewPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" /></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations" /></param>
        public ParcelsOverviewPageViewModel(INavigationService navigationService,
            IStringLocalizer<ParcelsOverviewPageViewModel> localizer, IAppDataService appDataService): base(localizer)
        {
            this._navigationService = navigationService;
            this.AppDataService = appDataService;
            AddParcelCommand = new DelegateCommand(NavigateToAddParcelPage);
            UploadCommand = new DelegateCommand(UploadPlots);
            ParcelDetailCommand =
                new DelegateCommand<object>(
                    NavigateToParcelDetailPage); //.ObservesCanExecute(o => IsParcelListEnabled);
            ParcelEditCommand =
                new DelegateCommand<object>(NavigateToParcelEditPage); //.ObservesCanExecute(o => IsParcelListEnabled);
            ParcelDeleteCommand =
                new DelegateCommand<object>(
                    ShowParcelDeletePrompt); //.ObservesCanExecute(o => IsParcelListEnabled);
            BackToMainPageCommand = new DelegateCommand(BackToMainPage);
            GoBackCommand = new DelegateCommand(GoBack);
            RefreshParcelsCommand = new DelegateCommand(RefreshParcels);

            Plots = new List<Plot>();
        }

        /// <summary>
        ///     Gets or sets the AddParcelCommand
        /// </summary>
        public DelegateCommand AddParcelCommand { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether AddParcelHintIsVisible
        /// </summary>
        public bool AddParcelHintIsVisible
        {
            get => this._addParcelHintIsVisible;
            set => SetProperty(ref this._addParcelHintIsVisible, value);
        }

        /// <summary>
        ///     Gets or sets the BackToMainPageCommand
        /// </summary>
        public DelegateCommand BackToMainPageCommand { get; set; }

        public DelegateCommand GoBackCommand { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether IsParcelListEnabled
        /// </summary>
        public bool IsParcelListEnabled
        {
            get => this._isParcelListEnabled;
            set => SetProperty(ref this._isParcelListEnabled, value);
        }

        /// <summary>
        ///     Gets or sets the ObservableParcel
        /// </summary>
        public ObservableCollection<PlotViewModel> ObservableParcel
        {
            get => this._observableParcel;
            set => SetProperty(ref this._observableParcel, value);
        }

        /// <summary>
        ///     Gets or sets the ParcelDeleteCommand
        /// </summary>
        public DelegateCommand<object> ParcelDeleteCommand { get; set; }

        /// <summary>
        ///     Gets or sets the ParcelDetailCommand
        /// </summary>
        public DelegateCommand<object> ParcelDetailCommand { get; set; }

        /// <summary>
        ///     Gets or sets the ParcelEditCommand
        /// </summary>
        public DelegateCommand<object> ParcelEditCommand { get; set; }

        /// <summary>
        ///     Gets or sets the Parcels
        /// </summary>
        public List<Plot> Plots
        {
            get => this._plots;
            set
            {
                SetProperty(ref this._plots, value);
                ParcelsListIsVisible = value.Count > 0;
                AddParcelHintIsVisible = !ParcelsListIsVisible;
                ShowUploadButton = ParcelsListIsVisible;

                SetObservableParcel();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether ParcelsListIsVisible
        /// </summary>
        public bool ParcelsListIsVisible
        {
            get => this._parcelsListIsVisible;
            set => SetProperty(ref this._parcelsListIsVisible, value);
        }

        public DelegateCommand RefreshParcelsCommand { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether ShowUploadButton
        /// </summary>
        public bool ShowUploadButton
        {
            get => this._showUploadButton;
            set => SetProperty(ref this._showUploadButton, value);
        }

        /// <summary>
        ///     Gets or sets the UploadCommand
        /// </summary>
        public DelegateCommand UploadCommand { get; set; }

        /// <summary>
        ///     The HideOrShowParcel
        /// </summary>
        /// <param name="parcel">The <see cref="ParcelViewModel" /></param>
        public void HideOrShowParcel(PlotViewModel plot)
        {
            if (this._oldParcel == plot)
            {
                plot.IsOptionsVisible = !plot.IsOptionsVisible;
                UpdateObservaleParcel(plot);
            }
            else
            {
                if (this._oldParcel != null)
                {
                    this._oldParcel.IsOptionsVisible = false;
                    UpdateObservaleParcel(this._oldParcel);
                }
                plot.IsOptionsVisible = true;
                UpdateObservaleParcel(plot);
            }
            this._oldParcel = plot;
        }

        /// <summary>
        ///     The BackToMainPage
        /// </summary>
        private void BackToMainPage()
        {
            this._navigationService.GoBackAsync();
        }

        private void GoBack()
        {
            this._navigationService.NavigateAsync("app:///MainPage");
        }

        /// <summary>
        ///     The NavigateToAddParcelPage
        /// </summary>
        private void NavigateToAddParcelPage()
        {
            this._navigationService.NavigateAsync("AddParcelPage");
        }

        /// <summary>
        ///     The NavigateToParcelDeletePage
        /// </summary>
        /// <param name="obj">The <see cref="object" /></param>
        private async void ShowParcelDeletePrompt(object id) //TODO: fix type
        {

            var deleteConfirmed = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "Do you really want to delete this plot?",
                OkText = "Yes",
                CancelText = "Cancel",
                Title = "Delete plot?"
            });

            if (deleteConfirmed)
            {
                Plot plotToRemove = Plots.Where(x => x.ID == (int)id).SingleOrDefault(null);
                if (plotToRemove != null)
                {
                    await AppDataService.RemovePlot(plotToRemove);
                }
                Plots.Remove(Plots.Where(x => x.ID == (int)id).SingleOrDefault(null));
            }
        }

        /// <summary>
        ///     The NavigateToParcelDetailPage
        /// </summary>
        /// <param name="id">The <see cref="object" /></param>
        private void NavigateToParcelDetailPage(object id)
        {
            try
            {
                // IsParcelListEnabled = false;
                var navigationParameters = new NavigationParameters
                {
                    { "Id", (string)id }
                };
                this._navigationService.NavigateAsync("ParcelMainPage", navigationParameters);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///     The NavigateToParcelEditPage
        /// </summary>
        /// <param name="obj">The <see cref="object" /></param>
        private void NavigateToParcelEditPage(object id)
        {
            try
            {
                // IsParcelListEnabled = false;
                var navigationParameters = new NavigationParameters
                {
                    { "Id", (string)id },
                    { "EditEnabled", true },
                    { "Caller", "ParcelsOverviewPage" }
                };
                this._navigationService.NavigateAsync("ParcelPage", navigationParameters);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async void RefreshParcels()
        {
            var plots = await this.AppDataService.GetAllPlots();
            UploadCommand.Execute();
        }

        /// <summary>
        ///     The SetObservableParcel
        /// </summary>
        private void SetObservableParcel()
        {
            ObservableParcel = new ObservableCollection<PlotViewModel>();
            foreach (var plot in Plots)
            {
                ObservableParcel.Add(new PlotViewModel
                {
                    Plot = plot,
                    IsOptionsVisible = false
                });
            }
        }

        /// <summary>
        ///     The UpdateObservaleParcel
        /// </summary>
        /// <param name="parcel">The <see cref="ParcelViewModel" /></param>
        private void UpdateObservaleParcel(PlotViewModel parcel)
        {
            var index = ObservableParcel.IndexOf(parcel);
            if (index == -1)
            {
                return; // Prevents a crash, but there is a case where it's -1 and shouldn't be
            }

            ObservableParcel.Remove(parcel);
            ObservableParcel.Insert(index, parcel);
        }

        /// <summary>
        ///     The UploadParcels
        /// </summary>
        private void UploadPlots()
        {
            foreach (var plot in Plots)
            {
                plot.Submit();
            }

            Plots = Plots; // Just for triggering setproperty
            ShowUploadButton = false;
        }
    }
}