namespace CimmytApp.DTO.Parcel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Helper.GeoWiki.API.GenericDatasetStorage;
    using Helper.Map;
    using Helper.Realm.DTO;
    using Xamarin.Forms;

    public class Parcel : INotifyPropertyChanged
    {
        private static readonly int geoWikiDatasetGroupId = 1;

        private string _crop;

        private GeoPosition _geoPosition;

        private string _parcelName;

        private int _uploaded;

        public event PropertyChangedEventHandler PropertyChanged;

        public int CompletedPercentage => 10;

        public string IconSource => GetCropImage();

        public string OverviewString => $"{Crop}\r\n{ParcelName}";

        public DateTimeOffset PlantingDate { get; set; } = DateTime.Today;

        public Parcel()
        {
            AgriculturalActivities = new List<AgriculturalActivity>();
            Delineation = new List<GeoPosition>();
            TechnologiesUsed = new List<Technology>();
        }

        public string TechnologiesScreenList
        {
            get
            {
                if (TechnologiesUsed == null || TechnologiesUsed.Count <= 0)
                {
                    return null;
                }

                return string.Join("\r\n", TechnologiesUsed.ToList());
            }
        }

        public List<AgriculturalActivity> AgriculturalActivities { get; set; }

        public string ClimateType { get; set; }

        public string Crop
        {
            get => _crop;
            set
            {
                _crop = value;
                OnPropertyChanged("Crop");
            }
        }

        public CropType CropType { get; set; }

        public List<GeoPosition> Delineation { get; set; }

        public string MaturityClass { get; set; }

        public int? PHU
        {
            get
            {
                switch (MaturityClass)
                {
                    case "Temprana":
                        return 1680;

                    case "Semi-temprana":
                        return 1890;

                    case "Intermedia":
                        return 2100;

                    case "Semi-tardía":
                        return 2310;

                    case "Tardía":
                        return 2520;

                    default:
                        return null;
                }
            }
        }

        public int? BaseTemperature
        {
            get
            {
                switch (ClimateType)
                {
                    case "Frío":
                        return 4;

                    case "Templado/Subtropical":
                        return 7;

                    case "Tropical":
                        return 9;

                    case "Híbrido":
                        return 10;

                    default:
                        return null;
                }
            }
        }

        public string ParcelId { get; set; } = Guid.NewGuid().ToString();

        public string ParcelName
        {
            get => _parcelName;
            set
            {
                _parcelName = value;
                OnPropertyChanged("ParcelNames");
            }
        }

        public GeoPosition Position { get; set; }

        public string ProducerName { get; set; }

        public List<Technology> TechnologiesUsed { get; set; }

        public static Parcel FromDTO(ParcelDTO parcelDTO)
        {
            if (parcelDTO == null)
            {
                return null;
            }

            var activities = new List<AgriculturalActivity>();
            var delineation = new List<GeoPosition>();
            var technologies = new List<Technology>();

            if (parcelDTO.AgriculturalActivities != null)
            {
                activities.AddRange(parcelDTO.AgriculturalActivities.Select(AgriculturalActivity.FromDTO));
            }

            if (parcelDTO.Delineation != null)
            {
                delineation.AddRange(parcelDTO.Delineation.Select(GeoPosition.FromDTO));
            }

            if (parcelDTO.TechnologiesUsed != null)
            {
                technologies.AddRange(parcelDTO.TechnologiesUsed.Select(technology => new Technology
                {
                    Name = technology.Name,
                    Id = technology.Id
                }));
            }

            var parcel = new Parcel
            {
                AgriculturalActivities = activities,
                ClimateType = parcelDTO.ClimateType,
                Crop = parcelDTO.Crop,
                CropType = (CropType)parcelDTO.CropType,
                Delineation = delineation,
                MaturityClass = parcelDTO.MaturityClass,
                ParcelId = parcelDTO.ParcelId,
                ParcelName = parcelDTO.ParcelName,
                PlantingDate = parcelDTO.PlantingDate,
                Position = GeoPosition.FromDTO(parcelDTO.Position),
                TechnologiesUsed = technologies
            };

            return parcel;
        }

        //ToDo:Move to another Class

        public static async Task<List<Parcel>> LoadParcelsFromServer()
        {
            return await Storage.GetDatasets<Parcel>(16, 1, Parcel.geoWikiDatasetGroupId);
        }

        public ParcelDTO GetDTO()
        {
            var dto = new ParcelDTO
            {
                ClimateType = ClimateType,
                Crop = Crop,
                CropType = (int)CropType,
                MaturityClass = MaturityClass,
                ParcelId = ParcelId,
                ParcelName = ParcelName,
                PlantingDate = PlantingDate,
                Position = Position?.GetDTO(ParcelId),
                AgriculturalActivitiesList =
                    AgriculturalActivities.Select(activity => activity.GetDTO(ParcelId)).ToList(),
                DelineationList = Delineation.Select(position => position.GetDTO(ParcelId)).ToList()
            };

            var technologies = new List<TechnologyDTO>();
            technologies.AddRange(TechnologiesUsed.Select(technology => new TechnologyDTO
            {
                Name = technology.Name,
                Id = technology.Id,
                ParcelId = ParcelId
            }));
            dto.TechnologiesUsedList = technologies;
            return dto;
        }

        public DataTemplate GetOverviewDataTemplate()
        {
            return null;
        }

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

        protected virtual void OnPropertyChanged(string aName)
        {
            var iHandler = PropertyChanged;
            _uploaded = (int)DatasetUploadStatus.ChangesOnDevice;
            iHandler?.Invoke(this, new PropertyChangedEventArgs(aName));
        }

        private string GetCropImage()
        {
            switch (CropType)
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
}