namespace Helper.Realm.DTO
{
    using System;
    using Realms;

    public class TechnologyDTO : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string ParcelId { get; set; }
    }
}