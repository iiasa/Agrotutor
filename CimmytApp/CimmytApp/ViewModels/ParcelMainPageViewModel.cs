namespace CimmytApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using CimmytApp.DTO.Parcel;
    using Helper.Map.ViewModels;
    using Helper.Realm.BusinessContract;
    using Prism;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public class ParcelMainPageViewModel : BindableBase, INavigationAware, IActiveAware
    {
        private readonly ICimmytDbOperations _cimmytDbOperations;
        private readonly INavigationService _navigationService;
        private Parcel _parcel;

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
            catch (Exception)
            {
                // ignored
            }
        }

        public event EventHandler IsActiveChanged;

        public DelegateCommand GoBackCommand { get; set; }

        public bool IsActive { get; set; }

        public DelegateCommand<string> NavigateAsyncCommand { get; set; }

        public DelegateCommand NavigateToMapCommand { get; set; }

        public Parcel Parcel
        {
            get => _parcel;
            set => SetProperty(ref _parcel, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                var id = (string)parameters["Id"];
                if (!string.IsNullOrEmpty(id))
                {
                    Parcel = Parcel.FromDTO(_cimmytDbOperations.GetParcelById(id));
                }
            }
            catch
            {
                // ignored
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private void GoBack()
        {
            _navigationService.NavigateAsync("app:///ParcelsOverviewPage");
        }

        private void NavigateAsync(string page)
        {
            var parameters = new NavigationParameters
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

        private void NavigateToMap()
        {
            var parameters = new NavigationParameters();
            var delineation = Parcel.Delineation;
            if (delineation != null && delineation.Count > 2)
            {
                var polygon = new Polygon
                {
                    StrokeColor = Color.Green,
                    StrokeWidth = 2f
                };
                foreach (var geoPosition in delineation)
                {
                    polygon.Positions.Add(new Position((double)geoPosition.Latitude, (double)geoPosition.Longitude));
                }

                var viewPolygons = new List<Polygon>
                {
                    polygon
                };
                parameters.Add(MapViewModel.PolygonsParameterName, viewPolygons);
            }

            if (Parcel.Position != null && Parcel.Position.IsSet())
            {
                parameters.Add(MapViewModel.PointsParameterName, new List<Pin>
                {
                    new Pin
                    {
                        Position = new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude)
                    }
                });
                parameters.Add(MapViewModel.MapCenterParameterName, CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                    new Position((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude), 15)));
            }

            _navigationService.NavigateAsync("Map", parameters);
        }
    }
}