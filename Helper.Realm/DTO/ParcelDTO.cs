

namespace Helper.Realm.DTO
{
    using System.Collections.Generic;
    using Realms;

    public class ParcelDTO : RealmObject
    {
        [PrimaryKey]
        public int ParcelId { get; set; }
        public string ParcelName { get; set; }
        public string Crop { get; set; }
        public GeoPositionDTO Position { get; set; }

        private List<GeoPositionDTO> _delineation;

        public IList<GeoPositionDTO> Delineation
        {
            get
            {
                return _delineation;
            }
        }

        private List<AgriculturalActivityDTO> _agriculturalActivities;

        public IList<AgriculturalActivityDTO> AgriculturalActivities
        {
            get
            {
                return _agriculturalActivities;
            }
        }

        public string ClimateType { get; set; }
        public string MaturityClass { get; set; }
        public int CropType { get; set; }

        private List<TechnologyDTO> _technologiesUsed;

        public IList<TechnologyDTO> TechnologiesUsed
        {
            get
            {
                return _technologiesUsed;
            }
        }

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
