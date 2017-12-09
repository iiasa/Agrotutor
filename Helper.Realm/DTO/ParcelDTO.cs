

namespace Helper.Realm.DTO
{
    using System;
    using System.Collections.Generic;
    using Realms;

    public class ParcelDTO : RealmObject
    {
        [PrimaryKey]
        public int ParcelId { get; set; }
        public string ParcelName { get; set; }
        public string Crop { get; set; }
        public GeoPositionDTO Position { get; set; }
        public GeoPositionDTO[] Delineation { get; set; }
        public AgriculturalActivityDTO[] AgriculturalActivities { get; set; }
        public string ClimateType { get; set; }
        public string MaturityClass { get; set; }
        public TechnologyDTO[] TechnologiesUsed { get; set; }
    }
}
