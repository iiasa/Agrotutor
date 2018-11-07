namespace CimmytApp.Core.Persistence.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.DTO.Parcel;
    using Helper.GeoWiki.API.GenericDatasetStorage;

    public class Plot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public CropType CropType { get; set; }

        public ClimateType ClimateType { get; set; }

        public MaturityType MaturityType { get; set; }

        public List<Activity> Activities { get; set; }

        public Position Position { get; set; }

        public List<Position> Delineation { get; set; }

        public BemData BemData { get; set; }

        //TODO: add pictures
        //Todo: add videos
        public void Submit() // TODO: remove this from here
        {
            Storage.StoreDatasetAsync(this, -1, 16, 1, 1);
        }
    }


}
