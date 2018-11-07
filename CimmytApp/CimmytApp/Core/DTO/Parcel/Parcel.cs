namespace CimmytApp.DTO.Parcel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.DTO.Benchmarking;
    using Helper.GeoWiki.API.GenericDatasetStorage;
    using Xamarin.Forms;

    public class Parcella
    {
        private static readonly int geoWikiDatasetGroupId = 1;

        private string _crop;

        private Position _geoPosition;

        private string _parcelName;

        public ImageSource IconSource => GetCropImage();

        public string OverviewString => $"{Crop}\r\n{ParcelName}";

        public DateTimeOffset PlantingDate { get; set; } = DateTime.Today;

        public int Uploaded { get; set; }

        public Parcella()
        {
            AgriculturalActivities = new List<Activity>();
            Delineation = new List<Position>();
        }

        public List<Activity> AgriculturalActivities { get; set; }

        public string ClimateType { get; set; }

        public string Crop
        {
            get => _crop;
            set => _crop = value;
        }

        public CropType CropType { get; set; }

        public List<Position> Delineation { get; set; }

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

        public Position Position { get; set; }
        

        //ToDo:Move to another Class

        public static async Task<List<Parcella>> LoadParcelsFromServer()
        {
            return await Storage.GetDatasets<Parcella>(16, 1, Parcella.geoWikiDatasetGroupId);
        }

        public List<DateTime> GetWindowsForFertilization()
        {
            var baseTemperature = BaseTemperature;
            var targetHeatUnits = PHU;
            if (baseTemperature == null || targetHeatUnits == null || Position == null)
                return null;
            return PhuAccumulator.GetWindowsOfOpportunity((int)baseTemperature, (int)targetHeatUnits, Position, PlantingDate.UtcDateTime);
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
            Storage.StoreDatasetAsync(this, -1, 16, 1, Parcella.geoWikiDatasetGroupId);
        }

        //ToDo:Move to another Class

        public async Task<Parcella> SubmitAsync()
        {
            if (Uploaded == (int)DatasetUploadStatus.Synchronized)
            {
                return null;
            }

            await Storage.StoreDatasetAsync(this, -1, 16, 1, Parcella.geoWikiDatasetGroupId);
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