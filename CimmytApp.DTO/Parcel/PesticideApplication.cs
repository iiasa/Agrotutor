namespace CimmytApp.DTO.Parcel
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("PesticideApplicaiton")]
    public class PesticideApplication
    {
        public string ApplicationDate { get; set; }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Parcel))]
        public int ParcelID { get; set; }

        public string ProductName { get; set; }

        //[ManyToOne]      // Many to one relationship with Parcel
        //public Parcel Parcel { get; set; }
    }
}