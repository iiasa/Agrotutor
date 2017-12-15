namespace Helper.Realm.DTO
{
    using System;
    using System.Collections.Generic;
    using Realms;

    public class ParcelDTO : RealmObject
    {
        private List<AgriculturalActivityDTO> _agriculturalActivities;

        private List<GeoPositionDTO> _delineation;

        private List<TechnologyDTO> _technologiesUsed;

        public IList<AgriculturalActivityDTO> AgriculturalActivities => _agriculturalActivities;
        public IList<GeoPositionDTO> Delineation => _delineation;
        public IList<TechnologyDTO> TechnologiesUsed => _technologiesUsed;

        public string ClimateType { get; set; }

        public string Crop { get; set; }

        public int CropType { get; set; }

        public string MaturityClass { get; set; }

        [PrimaryKey]
        public string ParcelId { get; set; } = Guid.NewGuid().ToString();

        public string ParcelName { get; set; }

        public GeoPositionDTO Position { get; set; }

        public void SetAgriculturalActivities(List<AgriculturalActivityDTO> activities)
        {
            _agriculturalActivities = activities;
        }

        public void SetDelineation(List<GeoPositionDTO> delineation)
        {
            _delineation = delineation;
        }

        public void SetTechnologies(List<TechnologyDTO> technologies)
        {
            _technologiesUsed = technologies;
        }
    }
}