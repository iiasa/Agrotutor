using CimmytApp.BusinessContract;
using Xamarin.Forms;
using SQLite.Net.Attributes;

namespace CimmytApp.DTO
{
    [Table("Parcel")]
    public class Parcel : IDataset
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Crop { get; set; }
        public string Cultivar { get; set; }

        public DataTemplate GetOverviewDataTemplate()
        {
            throw new System.NotImplementedException();
        }
    }
}