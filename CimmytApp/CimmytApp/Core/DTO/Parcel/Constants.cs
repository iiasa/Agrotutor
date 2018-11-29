namespace CimmytApp.Core.DTO.Parcel
{
    using System.Collections.Generic;
    using CimmytApp.DTO.Parcel;
    using Xamarin.Forms;

    public static class Constants
    {
        public static Dictionary<CropType, ImageSource> CropLogos = new Dictionary<CropType, ImageSource>
        {
            { CropType.Alfalfa, ImageSource.FromFile("crop_alfalfa.png") },
            { CropType.Amaranth, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.Barley, ImageSource.FromFile("crop_alfalfa.png") },
            { CropType.Bean, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.Canola, ImageSource.FromFile("crop_alfalfa.png") },
            { CropType.Cartamo, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.Chickpea, ImageSource.FromFile("crop_alfalfa.png") },
            { CropType.Corn, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.FavaBean, ImageSource.FromFile("crop_alfalfa.png") },
            { CropType.None, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.Oats, ImageSource.FromFile("crop_alfalfa.png") },
            { CropType.Other, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.Rice, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.Sesame, ImageSource.FromFile("crop_alfalfa.png") },
            { CropType.Sorghum, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.Soy, ImageSource.FromFile("crop_alfalfa.png") },
            { CropType.Triticale, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.Wheat, ImageSource.FromFile("crop_amaranth.png") },
            { CropType.Zucchini, ImageSource.FromFile("crop_alfalfa.png") }
        };

        public static Dictionary<ActivityType, ImageSource> ActivityLogos = new Dictionary<ActivityType, ImageSource>
        {
            { ActivityType.Commercialization, ImageSource.FromFile("money.png") },
            { ActivityType.Fertilization, ImageSource.FromFile("fertilizer.png") },
            { ActivityType.GroundPreperation, ImageSource.FromFile("shovel.png") },
            { ActivityType.Harvest, ImageSource.FromFile("harvest.png") },
            { ActivityType.Irrigation, ImageSource.FromFile("irrigation.png") },
            { ActivityType.OtherActivities, ImageSource.FromFile("farmer.png") },
            { ActivityType.PestAndDiseaseControlAndPrevention, ImageSource.FromFile("cockroach.png") },
            { ActivityType.PostHarvestStorage, ImageSource.FromFile("storage.png") },
            { ActivityType.SoilImprovers, ImageSource.FromFile("flask.png") },
            { ActivityType.Sowing, ImageSource.FromFile("sowing.png") },
            { ActivityType.WeedPreventionControl, ImageSource.FromFile("weeds.png") }
        };

        public const string StorageDirectoryName = "AgroTutor";
    }
}