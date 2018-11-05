namespace CimmytApp.DTO.Parcel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.DTO.Benchmarking;
    using Helper.GeoWiki.API.GenericDatasetStorage;
    using Helper.Map;
    using Helper.Realm.DTO;
    using Xamarin.Forms;

    public class Parcel
    {
        private static readonly int geoWikiDatasetGroupId = 1;

        private string _crop;

        private Core.Map.GeoPosition _geoPosition;

        private string _parcelName;

        public ImageSource IconSource => GetCropImage();

        public string OverviewString => $"{Crop}\r\n{ParcelName}";

        public DateTimeOffset PlantingDate { get; set; } = DateTime.Today;

        public int Uploaded { get; set; }

        public Parcel()
        {
            AgriculturalActivities = new List<AgriculturalActivity>();
            Delineation = new List<Core.Map.GeoPosition>();
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
            set => _crop = value;
        }

        public CropType CropType { get; set; }

        public List<Core.Map.GeoPosition> Delineation { get; set; }

        public string MaturityClass { get; set; }

        public int? PHU
        {
            get
            {
                if (CropType != CropType.Corn)
                    return null;
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
                if (CropType != CropType.Corn)
                    return null;
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
            set => _parcelName = value;
        }

        public Core.Map.GeoPosition Position { get; set; }

        public string ProducerName { get; set; }

        public List<Technology> TechnologiesUsed { get; set; }

        public static Parcel FromDTO(ParcelDTO parcelDTO)
        {
            if (parcelDTO == null)
            {
                return null;
            }

            var activities = new List<AgriculturalActivity>();
            var delineation = new List<Core.Map.GeoPosition>();
            var technologies = new List<Technology>();

            if (parcelDTO.AgriculturalActivities != null)
            {
                activities.AddRange(parcelDTO.AgriculturalActivities.Select(AgriculturalActivity.FromDTO));
            }

            if (parcelDTO.AgriculturalActivitiesList != null)
            {
                activities.AddRange(parcelDTO.AgriculturalActivitiesList.Select(AgriculturalActivity.FromDTO));
            }

            if (parcelDTO.Delineation != null)
            {
                delineation.AddRange(parcelDTO.Delineation.Select(Core.Map.GeoPosition.FromDTO));
            }

            if (parcelDTO.DelineationList != null)
            {
                delineation.AddRange(parcelDTO.DelineationList.Select(Core.Map.GeoPosition.FromDTO));
            }

            if (parcelDTO.TechnologiesUsed != null)
            {
                technologies.AddRange(parcelDTO.TechnologiesUsed.Select(technology => new Technology
                {
                    Name = technology.Name,
                    Id = technology.Id
                }));
            }

            if (parcelDTO.TechnologiesUsedList != null)
            {
                technologies.AddRange(parcelDTO.TechnologiesUsedList.Select(technology => new Technology
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
                Position = Core.Map.GeoPosition.FromDTO(parcelDTO.Position),
                TechnologiesUsed = technologies
            };

            return parcel;
        }

        //ToDo:Move to another Class

        public static async Task<List<Parcel>> LoadParcelsFromServer()
        {
            return await Storage.GetDatasets<Parcel>(16, 1, Parcel.geoWikiDatasetGroupId);
        }

        public List<DateTime> GetWindowsForFertilization()
        {
            var baseTemperature = BaseTemperature;
            var targetHeatUnits = PHU;
            if (baseTemperature == null || targetHeatUnits == null || Position == null)
                return null;
            return PhuAccumulator.GetWindowsOfOpportunity((int)baseTemperature, (int)targetHeatUnits, Position, PlantingDate.UtcDateTime);
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
            if (Uploaded == (int)DatasetUploadStatus.Synchronized)
            {
                return;
            }

            Uploaded = (int)DatasetUploadStatus.Synchronized;
            Storage.StoreDatasetAsync(this, -1, 16, 1, Parcel.geoWikiDatasetGroupId);
        }

        //ToDo:Move to another Class

        public async Task<Parcel> SubmitAsync()
        {
            if (Uploaded == (int)DatasetUploadStatus.Synchronized)
            {
                return null;
            }

            await Storage.StoreDatasetAsync(this, -1, 16, 1, Parcel.geoWikiDatasetGroupId);
            Uploaded = (int)DatasetUploadStatus.Synchronized;
            return this;
        }

        private ImageSource GetCropImage()
        {
            if (Constants.CropLogos.TryGetValue(CropType, out var ImageSource))
            {
                return ImageSource;
            }

            return ImageSource.FromFile("crop_other.png");
        }
    }
}