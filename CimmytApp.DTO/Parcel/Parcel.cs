namespace CimmytApp.DTO.Parcel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using Helper.BusinessContract;
    using Helper.GeoWiki.API.GenericDatasetStorage;
    using Helper.Map;
    using Newtonsoft.Json;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;
    using Xamarin.Forms;

    /// <summary>
    ///     Defines the <see cref="Parcel" />
    /// </summary>
    [Table("Parcel")]
    public class Parcel : GeoPosition, IDataset, INotifyPropertyChanged
    {
        /// <summary>
        ///     Defines the geoWikiDatasetGroupId
        /// </summary>
        private static readonly int geoWikiDatasetGroupId = 1;

        /// <summary>
        ///     Defines the _activities
        /// </summary>
        private List<string> _activities;

        /// <summary>
        ///     Defines the _agriculturalCycle
        /// </summary>
        private string _agriculturalCycle;

        /// <summary>
        ///     Defines the _crop
        /// </summary>
        private string _crop;

        /// <summary>
        ///     Defines the _croptype
        /// </summary>
        private CropType _croptype;

        /// <summary>
        ///     Defines the _cultivar
        /// </summary>
        private string _cultivar;

        /// <summary>
        ///     Defines the _delineation
        /// </summary>
        private string _delineation;

        /// <summary>
        ///     Defines the _estimatedParcelArea
        /// </summary>
        private string _estimatedParcelArea;

        /// <summary>
        ///     Defines the _geoPosition
        /// </summary>
        private GeoPosition _geoPosition;

        /// <summary>
        ///     Defines the _id
        /// </summary>
        private int _id;

        /// <summary>
        ///     Defines the _irrigation
        /// </summary>
        private string _irrigation;

        /// <summary>
        ///     Defines the _otherTechnologies
        /// </summary>
        private string _otherTechnologies;

        /// <summary>
        ///     Defines the _parcelName
        /// </summary>
        private string _parcelName;

        /// <summary>
        ///     Defines the _performance
        /// </summary>
        private string _performance;

        /// <summary>
        ///     Defines the _pesticidesApplied
        /// </summary>
        private List<PesticideApplication> _pesticidesApplied;

        /// <summary>
        ///     Defines the _plantingDate
        /// </summary>
        private DateTime _plantingDate;

        /// <summary>
        ///     Defines the _polygon
        /// </summary>
        private PolygonDto _polygon;

        /// <summary>
        ///     Defines the _producerName
        /// </summary>
        private string _producerName;

        /// <summary>
        ///     Defines the _technologiesUsed
        /// </summary>
        private List<string> _technologiesUsed;

        /// <summary>
        ///     Defines the _uploaded
        /// </summary>
        private int _uploaded;

        /// <summary>
        ///     Defines the _year
        /// </summary>
        private string _year;

        private List<AgriculturalActivity> _agriculturalActivities;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Parcel" /> class.
        /// </summary>
        public Parcel()
        {
            PlantingDate = DateTime.Today;
        }

        /// <summary>
        ///     Defines the PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets the CompletedPercentage
        /// </summary>
        public int CompletedPercentage => 10;

        /// <summary>
        ///     Gets the EstimatedParcelAreaWithUnit
        /// </summary>
        public string EstimatedParcelAreaWithUnit => EstimatedParcelArea + " ha";

        /// <summary>
        ///     Gets the IconSource
        /// </summary>
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

        /// <summary>
        ///     Gets the OverviewString
        /// </summary>
        public string OverviewString => $"{Crop}\r\n{ParcelName}";

        /// <summary>
        ///     Gets the PerformanceWithUnit
        /// </summary>
        public string PerformanceWithUnit => Performance + " tons/ha";

        public string DelineationString
        {
            get => _delineation;
            set => _delineation = value;
        }

        /// <summary>
        ///     Gets the TechnologiesScreenList
        /// </summary>
        [IgnoreDataMember]
        public string TechnologiesScreenList
        {
            get
            {
                if (TechnologiesUsed != null && TechnologiesUsed.Count > 0)
                {
                    string technologiesScreenList = string.Join("\r\n", TechnologiesUsed.ToArray());
                    return technologiesScreenList;
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets or sets the Activities
        /// </summary>
        [TextBlob("ActivitiesBlobbed")]
        public List<string> Activities
        {
            get => _activities;
            set => _activities = value;
        }

        /// <summary>
        ///     Gets or sets the ActivitiesBlobbed
        /// </summary>
        public string ActivitiesBlobbed { get; set; }

        /// <summary>
        ///     Gets or sets the AgriculturalActivities
        /// </summary>
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<AgriculturalActivity> AgriculturalActivities
        {
            get
            {
                if (_agriculturalActivities == null && !string.IsNullOrEmpty(ActivitiesBlobbed))
                {
                    _agriculturalActivities =
                        JsonConvert.DeserializeObject<List<AgriculturalActivity>>(ActivitiesBlobbed);
                }
                return _agriculturalActivities;
            }
            set
            {
                _agriculturalActivities = value;
                ActivitiesBlobbed = JsonConvert.SerializeObject(_agriculturalActivities);
            }
        }

        /// <summary>
        ///     Gets or sets the AgriculturalCycle
        /// </summary>
        public string AgriculturalCycle
        {
            get => _agriculturalCycle;
            set
            {
                _agriculturalCycle = value;
                OnPropertyChanged("AgriculturalCycle");
            }
        }

        /// <summary>
        ///     Gets or sets the ClimateType
        /// </summary>
        public string ClimateType { get; set; }

        /// <summary>
        ///     Gets or sets the Crop
        /// </summary>
        public string Crop
        {
            get => _crop;
            set
            {
                _crop = value;
                OnPropertyChanged("Crop");
            }
        }

        /// <summary>
        ///     Gets or sets the CropType
        /// </summary>
        public CropType CropType
        {
            get => _croptype;
            set => _croptype = value;
        }

        /// <summary>
        ///     Gets or sets the Cultivar
        /// </summary>
        public string Cultivar
        {
            get => _cultivar;
            set
            {
                _cultivar = value;
                OnPropertyChanged("Cultivar");
            }
        }

        /// <summary>
        ///     Gets or sets the EstimatedParcelArea
        /// </summary>
        public string EstimatedParcelArea
        {
            get => _estimatedParcelArea;
            set
            {
                _estimatedParcelArea = value;
                OnPropertyChanged("EstimatedParcelArea");
            }
        }

        /// <summary>
        ///     Gets or sets the HarvestingType
        /// </summary>
        public string HarvestingType { get; set; }

        /// <summary>
        ///     Gets or sets the Irrigation
        /// </summary>
        public string Irrigation
        {
            get => _irrigation;
            set
            {
                _irrigation = value;
                OnPropertyChanged("Irrigation");
            }
        }

        /// <summary>
        ///     Gets or sets the MaturityClass
        /// </summary>
        public string MaturityClass { get; set; }

        /// <summary>
        ///     Gets or sets the OtherTechnologies
        /// </summary>
        public string OtherTechnologies
        {
            get => _otherTechnologies;
            set
            {
                _otherTechnologies = value;
                OnPropertyChanged("OtherTechnologies");
            }
        }

        /// <summary>
        ///     Gets or sets the ParcelId
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int ParcelId { get; set; }

        /// <summary>
        ///     Gets or sets the ParcelName
        /// </summary>
        public string ParcelName
        {
            get => _parcelName;
            set
            {
                _parcelName = value;
                OnPropertyChanged("ParcelNames");
            }
        }

        /// <summary>
        ///     Gets or sets the Performance
        /// </summary>
        public string Performance
        {
            get => _performance;
            set
            {
                _performance = value;
                OnPropertyChanged("Performance");
            }
        }

        /// <summary>
        ///     Gets or sets the PesticidesApplied
        /// </summary>
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

        /// <summary>
        ///     Gets or sets the PlantingDate
        /// </summary>
        public DateTime PlantingDate
        {
            get => _plantingDate;
            set
            {
                _plantingDate = value.ToLocalTime();
                OnPropertyChanged("PlantingDate");
            }
        }

        /// <summary>
        ///     Gets or sets the Polygon
        /// </summary>
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public PolygonDto Polygon
        {
            get => _polygon;
            set
            {
                _polygon = value;
                OnPropertyChanged("Polygon");
            }
        }

        /// <summary>
        ///     Gets or sets the PolygonID
        /// </summary>
        [ForeignKey(typeof(PolygonDto))]
        public int PolygonID { get; set; }

        /// <summary>
        ///     Gets or sets the ProducerName
        /// </summary>
        public string ProducerName
        {
            get => _producerName;
            set
            {
                _producerName = value;
                OnPropertyChanged("ProducerName");
            }
        }

        /// <summary>
        ///     Gets or sets the SowingType
        /// </summary>
        public string SowingType { get; set; }

        /// <summary>
        ///     Gets or sets the StorageType
        /// </summary>
        public string StorageType { get; set; }

        /// <summary>
        ///     Gets or sets the TechnologiesUsed
        /// </summary>
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

        /// <summary>
        ///     Gets or sets the TechnologiesUsedBlobbed
        /// </summary>
        public string TechnologiesUsedBlobbed { get; set; }

        /// <summary>
        ///     Gets or sets the Uploaded
        /// </summary>
        public int Uploaded
        {
            get => _uploaded;
            set => _uploaded = value;
        }

        /// <summary>
        ///     Gets or sets the Year
        /// </summary>
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
        /// <summary>
        ///     The LoadParcelsFromServer
        /// </summary>
        /// <returns>The <see cref="Task{List{Parcel}}" /></returns>
        public static async Task<List<Parcel>> LoadParcelsFromServer()
        {
            return await Storage.GetDatasets<Parcel>(16, 1, Parcel.geoWikiDatasetGroupId);
        }

        //ToDo:Move to another Class
        /// <summary>
        ///     The GetDelineation
        /// </summary>
        /// <returns>The <see cref="List{GeoPosition}" /></returns>
        public List<GeoPosition> GetDelineation()
        {
            return string.IsNullOrEmpty(DelineationString)
                ? null
                : JsonConvert.DeserializeObject<List<GeoPosition>>(DelineationString);
        }

        //ToDo:Move to another Class
        /// <summary>
        ///     The GetOverviewDataTemplate
        /// </summary>
        /// <returns>The <see cref="DataTemplate" /></returns>
        public DataTemplate GetOverviewDataTemplate()
        {
            return null;
        }

        //ToDo:Move to another Class
        /// <summary>
        ///     The SetDelineation
        /// </summary>
        /// <param name="delineation">The <see cref="List{GeoPosition}" /></param>
        public void SetDelineation(List<GeoPosition> delineation)
        {
            DelineationString = JsonConvert.SerializeObject(delineation);
        }

        //ToDo:Move to another Class
        /// <summary>
        ///     The Submit
        /// </summary>
        public void Submit()
        {
            if (_uploaded == (int)DatasetUploadStatus.Synchronized)
            {
                return;
            }

            _uploaded = (int)DatasetUploadStatus.Synchronized;
            Storage.StoreDatasetAsync(this, -1, 16, 1, Parcel.geoWikiDatasetGroupId);
        }

        //ToDo:Move to another Class
        /// <summary>
        ///     The SubmitAsync
        /// </summary>
        /// <returns>The <see cref="Task{Parcel}" /></returns>
        public async Task<Parcel> SubmitAsync()
        {
            if (_uploaded == (int)DatasetUploadStatus.Synchronized)
            {
                return null;
            }

            await Storage.StoreDatasetAsync(this, -1, 16, 1, Parcel.geoWikiDatasetGroupId);
            _uploaded = (int)DatasetUploadStatus.Synchronized;
            return this;
        }

        /// <summary>
        ///     The OnPropertyChanged
        /// </summary>
        /// <param name="aName">The <see cref="string" /></param>
        protected virtual void OnPropertyChanged(string aName)
        {
            PropertyChangedEventHandler iHandler = PropertyChanged;
            _uploaded = (int)DatasetUploadStatus.ChangesOnDevice;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }
    }
}