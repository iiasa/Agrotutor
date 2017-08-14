using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CimmytApp.BusinessContract;
using CimmytApp.DTO;
using Prism.Commands;
using Xamarin.Forms;

namespace CimmytApp.Parcel.ViewModels
{
    using System;
    using Prism;
    using Prism.Events;
    using Prism.Navigation;

    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;

    using DTO.Parcel;
    using System.ComponentModel;

    public class AddParcelInformationPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware
    {
        private Parcel _parcel;
        private bool isActive;
        public DelegateCommand DeliniateParcelCommand { get; set; }
        private INavigationService _navigationService;
        private ICimmytDbOperations _cimmytDbOperations;
        private bool _needsDeliniation;

        public bool NeedsDeliniation
        {
            get { return _needsDeliniation; }
            set
            {
                SetProperty(ref _needsDeliniation, value);
            }
        }

        public AddParcelInformationPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICimmytDbOperations cimmytDbOperations) : base(eventAggregator)
        {
            DeliniateParcelCommand = new DelegateCommand(DeliniateParcel);
            _navigationService = navigationService;
            _cimmytDbOperations = cimmytDbOperations;
        }

        private void DeliniateParcel()
        {
            NeedsDeliniation = false;
            var parameters = new NavigationParameters
            {
                {"Latitude", _parcel.Latitude},
                {"Longitude", _parcel.Longitude},
                {"GetPolygon", true},
                {"parcelId",_parcel.ParcelId }
            };
            _navigationService.NavigateAsync("GenericMap", parameters);
        }

        public event EventHandler IsActiveChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsActive
        {
            get => isActive;
            set
            {
                if (isActive && !value && _parcel != null)
                {
                    PublishDataset(_parcel);
                }
                isActive = value;
            }
        }

        public Parcel Parcel
        {
            get => _parcel;
            set
            {
                _parcel = value;
                OnPropertyChanged("Parcel");
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Deliniation"))
            {
                object deliniation;
                parameters.TryGetValue("Deliniation", out deliniation);
                //   Parcel.SetDeliniation((List<GeoPosition>)deliniation);
                PolygonDto polygonObj = new PolygonDto();
                polygonObj.ListPoints = (List<GeoPosition>)deliniation;
                if (polygonObj.ListPoints != null && polygonObj.ListPoints.Count > 2)
                {
                    if (Parcel.Latitude == 0 && Parcel.Longitude == 0)
                    {
                        Parcel.Latitude = polygonObj.ListPoints.ElementAt(0).Latitude;
                        Parcel.Longitude = polygonObj.ListPoints.ElementAt(0).Longitude;
                    }
                    _cimmytDbOperations.SaveParcelPolygon(Parcel.ParcelId, polygonObj);
                    NeedsDeliniation = false;
                }
                // var res=_cimmytDbOperations.GetAllParcels();
                OnPropertyChanged("Parcel"); //TODO improve this...
                PublishDataset(Parcel);//TODO improve this..
                                       //  _cimmytDbOperations.UpdateParcel(Parcel);
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        protected override IDataset GetDataset()
        {
            return _parcel;
        }

        private void CheckDeliniation()
        {
            if (Parcel != null)
            {
                if (Parcel.Polygon != null && Parcel.Polygon.ListPoints.Count > 0)
                {
                    NeedsDeliniation = false;
                }
                else
                {
                    NeedsDeliniation = true;
                }
            }
        }

        protected virtual void OnPropertyChanged(string aName)
        {
            var iHandler = PropertyChanged;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }

        protected override void ReadDataset(IDataset dataset)
        {
            Parcel = (Parcel)dataset;
            CheckDeliniation();
        }
    }
}