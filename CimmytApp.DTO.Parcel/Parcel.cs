using SQLiteNetExtensions.Attributes;

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
    public class Parcel : GeoPosition, IDataset, INotifyPropertyChanged
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
        private int _uploaded;
        private string _year;

        public Parcel()
        {
            PlantingDate = DateTime.Today;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //[PrimaryKey, AutoIncrement]
        //public int DBId { get; set; }

        public string AgriculturalCycle
        {
            get => _agriculturalCycle;
            set
            {
                _agriculturalCycle = value;
                OnPropertyChanged("AgriculturalCycle");
            }
        }

        public int CompletedPercentage => 10;

        public string Crop
        {
            get => _crop;
            set
            {
                _crop = value;
                OnPropertyChanged("Crop");
            }
        }

        public string Cultivar
        {
            get => _cultivar;
            set
            {
                _cultivar = value;
                OnPropertyChanged("Cultivar");
            }
        }

        public string EstimatedParcelArea
        {
            get => _estimatedParcelArea;
            set
            {
                _estimatedParcelArea = value;
                OnPropertyChanged("EstimatedParcelArea");
            }
        }

        public string EstimatedParcelAreaWithUnit => EstimatedParcelArea + " ha";

        //public GeoPosition GeoPosition
        //{
        //    get => _geoPosition;
        //    set
        //    {
        //        _geoPosition = value;
        //        OnPropertyChanged("GeoPosition");
        //    }
        //}

        public string IconSource => $"corn.png";

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        public string Irrigation
        {
            get => _irrigation;
            set
            {
                _irrigation = value;
                OnPropertyChanged("Irrigation");
            }
        }

        public string OtherTechnologies
        {
            get => _otherTechnologies;
            set
            {
                _otherTechnologies = value;
                OnPropertyChanged("OtherTechnologies");
            }
        }

        public string OverviewString => $"{Crop}\r\n{ParcelName}";

        public string ParcelName
        {
            get => _parcelName;
            set
            {
                _parcelName = value;
                OnPropertyChanged("ParcelNames");
            }
        }

        public string Performance
        {
            get => _performance;
            set
            {
                _performance = value;
                OnPropertyChanged("Performance");
            }
        }

        public string PerformanceWithUnit => Performance + " tons/ha";

        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert)]
        public List<PesticideApplication> PesticidesApplied
        {
            get => _pesticidesApplied;
            set
            {
                _pesticidesApplied = value;
                OnPropertyChanged("PesticidesApplied");
            }
        }

        public DateTime PlantingDate
        {
            get => _plantingDate;
            set
            {
                _plantingDate = value;
                OnPropertyChanged("PlantingDate");
            }
        }

        public string ProducerName
        {
            get => _producerName;
            set
            {
                _producerName = value;
                OnPropertyChanged("ProducerName");
            }
        }

        public string TechnologiesScreenList => string.Join("\r\n", TechnologiesUsed.ToArray());

        [TextBlob("TechnologiesUsedBlobbed")]
        public List<string> TechnologiesUsed
        {
            get => _technologiesUsed;
            set
            {
                _technologiesUsed = value;
                OnPropertyChanged("TechnologiesUsed");
            }
        }
        public string TechnologiesUsedBlobbed { get; set; } // serialized phone numbers
        public string Year
        {
            get => _year;
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

        public async void Submit()
        {
            if (_uploaded == 1) return;
            await Storage.StoreDatasetAsync(this, -1, 16, 1, geoWikiDatasetGroupId);
            _uploaded = (int)DatasetUploadStatus.Synchronized;
        }

        protected virtual void OnPropertyChanged(string aName)
        {
            var iHandler = PropertyChanged;
            _uploaded = (int)DatasetUploadStatus.ChangesOnDevice;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }
    }
}