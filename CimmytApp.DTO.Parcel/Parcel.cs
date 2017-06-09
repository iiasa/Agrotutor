namespace CimmytApp.DTO.Parcel
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using SQLite.Net.Attributes;

    using Helper.BusinessContract;

    using DTO;

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

        public int CompletedPercentage => 10;
        public string OverviewString => $"{Crop}\r\n({CompletedPercentage} % complete)";
        public string IconSource => $"corn.png";

        public DataTemplate GetOverviewDataTemplate()
        {
            return null;
        }
    }
}