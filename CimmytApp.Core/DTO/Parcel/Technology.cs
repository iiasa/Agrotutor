namespace CimmytApp.DTO.Parcel
{
    using System;
    using Helper.Realm.DTO;

    public class Technology
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string ParcelId { get; set; }

        public TechnologyDTO GetDTO()
        {
            return new TechnologyDTO
            {
                Id = Id,
                Name = Name,
                ParcelId = ParcelId
            };
        }

        public static Technology FromDTO(TechnologyDTO technology)
        {
            return new Technology
            {
                Id = technology.Id,
                Name = technology.Name,
                ParcelId = technology.ParcelId
            };
        }

        public override string ToString()
        {
            return Name;
        }
    }
}