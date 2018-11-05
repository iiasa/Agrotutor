namespace CimmytApp.Core.Persistence.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.Core.Map;
    using CimmytApp.DTO.Parcel;

    public class Plot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public CropType CropType { get; set; }

        public ClimateType ClimateType { get; set; }

        public MaturityType MaturityType { get; set; }

        public List<AgriculturalActivity> AgriculturalActivities { get; set; }

        public GeoPosition GeoPosition { get; set; }

        public List<GeoPosition> Delineation { get; set; }

        //TODO: add pictures
        //Todo: add videos
    }


}
