using SQLite.Net.Attributes;

namespace CimmytApp.DTO
{
    
    [Table("PesticideApplicaiton")]
    public class PesticideApplication
    {

		public int ID { get; set; }
		public string ProductName { get; set; }
		public string ApplicationDate { get; set; }
	}
}