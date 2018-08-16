namespace Helper.Realm.DTO
{
    using System;
    using System.Collections.Generic;
    using Realms;

    public class ParcelDTO : RealmObject
    {
        public IList<AgriculturalActivityDTO> AgriculturalActivities { get; } = new List<AgriculturalActivityDTO>();

        public IList<GeoPositionDTO> Delineation { get; } = new List<GeoPositionDTO>();

        public IList<TechnologyDTO> TechnologiesUsed { get; } = new List<TechnologyDTO>();

        [Ignored]
        public List<AgriculturalActivityDTO> AgriculturalActivitiesList { get; set; }

        [Ignored]
        public List<GeoPositionDTO> DelineationList { get; set; }

        [Ignored]
        public List<TechnologyDTO> TechnologiesUsedList { get; set; }

        public string ClimateType { get; set; }

        public string Crop { get; set; }

        public int CropType { get; set; }

        public string MaturityClass { get; set; }

        [PrimaryKey]
        public string ParcelId { get; set; } = Guid.NewGuid().ToString();

        public string ParcelName { get; set; }

        public GeoPositionDTO Position { get; set; }

        public DateTimeOffset PlantingDate { get; set; }
    }
}