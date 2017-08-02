using SQLite.Net.Attributes;

namespace CimmytApp.DTO.Parcel
{
    [Table("PesticideApplicaiton")]
    public class PesticideApplication
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ApplicationDate { get; set; }
    }
}