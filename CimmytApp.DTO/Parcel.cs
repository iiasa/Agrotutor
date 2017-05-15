using System;

namespace CimmytApp.DTO
{
    using System.Collections.Generic;
    using CimmytApp.BusinessContract;
    using Xamarin.Forms;
    using SQLite.Net.Attributes;

    [Table("Parcel")]
    public class Parcel : IDataset
    {
        public int ID { get; set; }
        public string ParcelName { get; set; }
        public string Crop { get; set; }
        public string Cultivar { get; set; }
        public int AgronomicalCycle { get; set; }
        public int Year { get; set; }
        public double EstimatedParcelArea { get; set; }
        public string ProducerName { get; set; }
        public List<string> TechnologiesUsed { get; set; }
        public string OtherTechnologies { get; set; }

        public double? Performance { get; set; }
        public string Irrigation { get; set; }
        public DateTime PlantingDate { get; set; }
        public List<PesticideApplication> PesticidesApplied { get; set; }

        public GeoPosition GeoPosition { get; set; }

        public DataTemplate GetOverviewDataTemplate()
        {
            throw new System.NotImplementedException();
        }
    }
}