namespace CimmytApp.DTO.Parcel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Xamarin.Forms;
    using SQLite.Net.Attributes;

    using Helper.BusinessContract;
    using Helper.GeoWiki.GenericDatasetStorage;

    using DTO;
    using System.Threading.Tasks;

    [Table("Parcel")]
    public class Parcel : IDataset, INotifyPropertyChanged
    {
        private static int geoWikiDatasetGroupId = 1;

        private string _agriculturalCycle;
        private string _crop;
        private string _cultivar;
        private string _estimatedParcelArea;
        private GeoPosition _geoPosition;
        private int _id;
        private string _irrigation;
        private string _otherTechnologies;
        private string _parcelName;
        private string _performance;
        private List<PesticideApplication> _pesticidesApplied;
        private DateTime _plantingDate;
        private string _producerName;
        private List<string> _technologiesUsed;
		private string _year;
		private int _uploaded;

        public Parcel()
        {
            PlantingDate = DateTime.Today;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string AgriculturalCycle
        {
            get { return _agriculturalCycle; }
            set
            {
                _agriculturalCycle = value;
                OnPropertyChanged("AgriculturalCycle");
            }
        }

        public int CompletedPercentage => 10;

        public string Crop
        {
            get { return _crop; }
            set
            {
                _crop = value;
                OnPropertyChanged("Crop");
            }
        }

        public string Cultivar
        {
            get { return _cultivar; }
            set
            {
                _cultivar = value;
                OnPropertyChanged("Cultivar");
            }
        }

        public string EstimatedParcelArea
        {
            get { return _estimatedParcelArea; }
            set
            {
                _estimatedParcelArea = value;
                OnPropertyChanged("EstimatedParcelArea");
            }
        }

        public string EstimatedParcelAreaWithUnit => EstimatedParcelArea + " ha";

        public GeoPosition GeoPosition
        {
            get { return _geoPosition; }
            set
            {
                _geoPosition = value;
                OnPropertyChanged("GeoPosition");
            }
        }

        public string IconSource => $"corn.png";

        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        public string Irrigation
        {
            get { return _irrigation; }
            set
            {
                _irrigation = value;
                OnPropertyChanged("Irrigation");
            }
        }

        public string OtherTechnologies
        {
            get { return _otherTechnologies; }
            set
            {
                _otherTechnologies = value;
                OnPropertyChanged("OtherTechnologies");
            }
        }

        public string OverviewString => $"{Crop}\r\n{ParcelName}";

        public string ParcelName
        {
            get { return _parcelName; }
            set
            {
                _parcelName = value;
                OnPropertyChanged("ParcelNames");
            }
        }

        public string Performance
        {
            get { return _performance; }
            set
            {
                _performance = value;
                OnPropertyChanged("Performance");
            }
        }

        public string PerformanceWithUnit => Performance + " tons/ha";

        public List<PesticideApplication> PesticidesApplied
        {
            get { return _pesticidesApplied; }
            set
            {
                _pesticidesApplied = value;
                OnPropertyChanged("PesticidesApplied");
            }
        }

        public DateTime PlantingDate
        {
            get { return _plantingDate; }
            set
            {
                _plantingDate = value;
                OnPropertyChanged("PlantingDate");
            }
        }

        public string ProducerName
        {
            get { return _producerName; }
            set
            {
                _producerName = value;
                OnPropertyChanged("ProducerName");
            }
        }

        public string TechnologiesScreenList => string.Join("\r\n", TechnologiesUsed.ToArray());

        public List<string> TechnologiesUsed
        {
            get { return _technologiesUsed; }
            set
            {
                _technologiesUsed = value;
                OnPropertyChanged("TechnologiesUsed");
            }
        }

        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }

        public static async Task<List<Parcel>> LoadParcelsFromServer()
        {
            return await Storage.GetDatasets<Parcel>(16, 1, geoWikiDatasetGroupId);
        }

        public DataTemplate GetOverviewDataTemplate()
        {
            return null;
        }

        public int Save()
        {
            return -1;
        }

        public async void Submit()
        {
            if (_uploaded == 1) return;
            await Storage.StoreDatasetAsync(this, -1, 16, 1, geoWikiDatasetGroupId);
            _uploaded = 1;
            Save();
        }

        protected virtual void OnPropertyChanged(string aName)
        {
            var iHandler = PropertyChanged;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }
    }
}