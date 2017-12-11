namespace Helper.Realm.DTO
{
    using Realms;

    public class TechnologyDTO : RealmObject
    {
        [PrimaryKey]
        public int? Id { get; set; }

        public string Name
        {
            get;
            set;
        }
    }
}