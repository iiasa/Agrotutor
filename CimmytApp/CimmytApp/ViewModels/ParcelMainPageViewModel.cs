namespace CimmytApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using CimmytApp.BusinessContract;
    using CimmytApp.DTO.Parcel;
    using Helper.Map;
    using Helper.Map.ViewModels;
    using Prism;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using TK.CustomMap;
    using TK.CustomMap.Overlays;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    /// <summary>
    ///     Defines the <see cref="ParcelMainPageViewModel" />
    /// </summary>
    public class ParcelMainPageViewModel : BindableBase, INavigationAware, IActiveAware
    {
        /// <summary>
        ///     Defines the _cimmytDbOperations
        /// </summary>
        private readonly ICimmytDbOperations _cimmytDbOperations;

        /// <summary>
        ///     Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        ///     Defines the _parcel
        /// </summary>
        private Parcel _parcel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParcelMainPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" /></param>
        /// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations" /></param>
        public ParcelMainPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations)
        {
            _navigationService = navigationService;
            NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
            NavigateToMapCommand = new DelegateCommand(NavigateToMap);
            GoBackCommand = new DelegateCommand(GoBack);

            try
            {
                _cimmytDbOperations = cimmytDbOperations;
            }
            catch (Exception e)
            {
            }
        }

        private void GoBack()
        {
            _navigationService.NavigateAsync("app:///ParcelsOverviewPage");
        }

        /// <summary>
        ///     Defines the IsActiveChanged
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Gets or sets the NavigateAsyncCommand
        /// </summary>
        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        /// <summary>
        ///     Gets or sets the NavigateToMapCommand
        /// </summary>
        public DelegateCommand NavigateToMapCommand { get; set; }

        /// <summary>
        ///     Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel
        {
            get => _parcel;
            set => SetProperty(ref _parcel, value);
        }

        public DelegateCommand GoBackCommand { get; set; }

        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                int id = (int)parameters["Id"];

                Parcel = Parcel.FromDTO(_cimmytDbOperations.GetParcelById(id));
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     The NavigateAsync
        /// </summary>
        /// <param name="page">The <see cref="string" /></param>
        private void NavigateAsync(string page)
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "Parcel", Parcel }
            };
            if (page == "ParcelPage")
            {
                parameters.Add("EditEnabled", false);
                parameters.Add("Caller", "ParcelMainPage");
            }
            _navigationService.NavigateAsync(page, parameters);
        }

        /// <summary>
        ///     The NavigateToMap
        /// </summary>
        private void NavigateToMap()
        {
            NavigationParameters parameters = new NavigationParameters();
            PolygonDto delineation = Parcel.Polygon;
            if (delineation != null && delineation.ListPoints.Count > 2)
            {
                TKPolygon polygon = new TKPolygon
                {
                    StrokeColor = Color.Green,
                    StrokeWidth = 2f,
                    Color = Color.Red
                };
                foreach (GeoPosition geoPosition in delineation.ListPoints)
                {
                    polygon.Coordinates.Add(new Position((double)geoPosition.Latitude, (double)geoPosition.Longitude));
                }

                ObservableCollection<TKPolygon> viewPolygons = new ObservableCollection<TKPolygon>
                {
                    polygon
                };
                parameters.Add(GenericMapViewModel.PolygonsParameterName, viewPolygons);
            }

            if ((bool)Parcel.Position?.IsSet())
            {
                parameters.Add(GenericMapViewModel.PointsParameterName, new ObservableCollection<TKCustomMapPin>
                {
                    new TKCustomMapPin
                    {
                        Position = Parcel.Position.ToPosition()
                    }
                });
            }

            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}