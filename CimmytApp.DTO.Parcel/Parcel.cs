using System.Runtime.Serialization;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using SQLite.Net.Attributes;
using Helper.BusinessContract;
using Helper.GeoWiki.GenericDatasetStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CimmytApp.DTO.Parcel
{
    [Table("Parcel")]
    public class Parcel : GeoPosition, IDataset, INotifyPropertyChanged
    {
        private static int geoWikiDatasetGroupId = 1;

        private string _agriculturalCycle;
        private string _crop;
        private CropType _croptype;
        private string _cultivar;
        private string _estimatedParcelArea;
        private GeoPosition _geoPosition;
        private int _id;
        private string _irrigation;
        private string _otherTechnologies;
        private string _parcelName;
        private string _performance;
        private List<PesticideApplication> _pesticidesApplied;
        private string _deliniation;
        private DateTime _plantingDate;
        private string _producerName;
        private List<string> _technologiesUsed;
        private int _uploaded;
        private string _year;
        private PolygonDto _polygon;

        public Parcel()
        {
            PlantingDate = DateTime.Today;
            //_technologiesUsed = new List<string>();
            //_polygon=new PolygonDto();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string AgriculturalCycle
        {
            get => _agriculturalCycle;
            set
            {
                _agriculturalCycle = value;
                OnPropertyChanged("AgriculturalCycle");
            }
        }

        //[PrimaryKey, AutoIncrement]
        //public int DBId { get; set; }
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

        public CropType CropType
        {
            get => _croptype;
            set => _croptype = value;
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

        [ForeignKey(typeof(PolygonDto))]
        public int PolygonID { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public PolygonDto Polygon
        {
            get { return _polygon; }
            set
            {
                _polygon = value;
                OnPropertyChanged("Polygon");
            }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<PesticideApplication> PesticidesApplied
        {
            get => _pesticidesApplied;
            set
            {
                _pesticidesApplied = value;
                OnPropertyChanged("PesticidesApplied");
            }
        }

        public int Uploaded
        {
            get => _uploaded;
            set => _uploaded = value;
        }

        public string EstimatedParcelAreaWithUnit => EstimatedParcelArea + " ha";

        public string IconSource
        {
            get
            {
                switch (_croptype)
                {
                    case CropType.Corn:
                        return "crop_corn.png";

                    case CropType.Barley:
                        return "crop_barley.png";

                    case CropType.Bean:
                        return "crop_bean.png";

                    case CropType.Wheat:
                        return "crop_wheat.png";

                    case CropType.Triticale:
                        return "crop_triticale.png";

                    case CropType.Sorghum:
                        return "crop_sorghum.png";

                    case CropType.Alfalfa:
                        return "crop_alfalfa.png";

                    case CropType.Oats:
                        return "crop_oats.png";

                    case CropType.Sesame:
                        return "crop_sesame.png";

                    case CropType.Amaranth:
                        return "crop_amaranth.png";

                    case CropType.Rice:
                        return "crop_rice.png";

                    case CropType.Canola:
                        return "crop_canola.png";

                    case CropType.Cartamo:
                        return "crop_cartamo.png";

                    case CropType.Zucchini:
                        return "crop_zucchini.png";

                    case CropType.Chickpea:
                        return "crop_chickpea.png";

                    case CropType.FavaBean:
                        return "crop_bean.png";

                    case CropType.Soy:
                        return "crop_soy.png";

                    case CropType.None:
                        return "crop_none.png";

                    case CropType.Other:
                        return "crop_other.png";

                    default:
                        return "crop_other.png";
                }
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

        public string OverviewString => _uploaded == (int)DatasetUploadStatus.Synchronized ? $"{Crop}\r\n{ParcelName}" : $"* {Crop}\r\n{ParcelName}";

        [PrimaryKey, AutoIncrement]
        public int ParcelId { get; set; }

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

        public DateTime PlantingDate
        {
            get => _plantingDate;
            set
            {
                //_plantingDate = value;

                _plantingDate = value.ToLocalTime();
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

        [IgnoreDataMember]
        public string TechnologiesScreenList
        {
            get
            {
                if (TechnologiesUsed != null && TechnologiesUsed.Count > 0)
                {
                    var technologiesScreenList = string.Join("\r\n", TechnologiesUsed.ToArray());
                    return technologiesScreenList;
                }
                return null;
            }
        }

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

        public string TechnologiesUsedBlobbed { get; set; }

        public string Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }

        //ToDo:Move to another Class
        public static async Task<List<Parcel>> LoadParcelsFromServer()
        {
            return await Storage.GetDatasets<Parcel>(16, 1, geoWikiDatasetGroupId);
        }

        //ToDo:Move to another Class
        public DataTemplate GetOverviewDataTemplate()
        {
            return null;
        }

        //ToDo:Move to another Class
        public void Submit()
        {
            if (_uploaded == (int)DatasetUploadStatus.Synchronized) return;
            _uploaded = (int)DatasetUploadStatus.Synchronized;
            Storage.StoreDatasetAsync(this, -1, 16, 1, geoWikiDatasetGroupId);
        }

        //ToDo:Move to another Class
        public async Task<Parcel> SubmitAsync()
        {
            if (_uploaded == (int)DatasetUploadStatus.Synchronized) return null;
            await Storage.StoreDatasetAsync(this, -1, 16, 1, geoWikiDatasetGroupId);
            _uploaded = (int)DatasetUploadStatus.Synchronized;
            return this;
        }

        protected virtual void OnPropertyChanged(string aName)
        {
            var iHandler = PropertyChanged;
            _uploaded = (int)DatasetUploadStatus.ChangesOnDevice;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }

        //ToDo:Move to another Class
        public List<GeoPosition> GetDeliniation()
        {
            return string.IsNullOrEmpty(_deliniation) ? null : JsonConvert.DeserializeObject<List<GeoPosition>>(_deliniation);
        }

        //ToDo:Move to another Class
        public void SetDeliniation(List<GeoPosition> deliniation)
        {
            _deliniation = JsonConvert.SerializeObject(deliniation);
        }
    }
}