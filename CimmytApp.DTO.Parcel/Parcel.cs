namespace CimmytApp.DTO.Parcel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Xamarin.Forms;
    using SQLite.Net.Attributes;

    using Helper.BusinessContract;
    using Helper.GeoWiki.GenericDatasetStorage;

    using DTO;
    using System.Threading.Tasks;

    [Table("Parcel")]
    public class Parcel : IDataset, INotifyPropertyChanged
    {
        private static int geoWikiDatasetGroupId = 1;

        private int _id;
        public int ID { get { return _id; } set { _id = value; OnPropertyChanged("ID"); } }
        public string ParcelName { get; set; }
        public string Crop { get; set; }
        public string Cultivar { get; set; }
        public string AgriculturalCycle { get; set; }
		public string Year { get; set; }
		public string EstimatedParcelArea { get; set; }
        public string EstimatedParcelAreaWithUnit { get { return EstimatedParcelArea + " ha"; } }
        public string ProducerName { get; set; }
        public List<string> TechnologiesUsed { get; set; }
        public string OtherTechnologies { get; set; }

		public string Performance { get; set; }
        public string PerformanceWithUnit { get { return Performance + " tons/ha"; } }
        public string Irrigation { get; set; }
        public DateTime PlantingDate { get; set; }
        public List<PesticideApplication> PesticidesApplied { get; set; }

        public GeoPosition GeoPosition { get; set; }

        public string TechnologiesScreenList {
            get { return String.Join("\r\n", TechnologiesUsed.ToArray()); }
        }

        public int CompletedPercentage => 10;
        public string OverviewString => $"{Crop}\r\n({CompletedPercentage} % complete)";
        public string IconSource => $"corn.png";

        public Parcel(){
            PlantingDate = DateTime.Today;
        }

        public DataTemplate GetOverviewDataTemplate()
        {
            return null;
        }

        public async void Submit()
        {
            await Storage.StoreDatasetAsync(this, -1, 16, 1, geoWikiDatasetGroupId);
        }

        public static async Task<List<Parcel>> LoadParcelsFromServer()
        {
            return await Storage.GetDatasets<Parcel>(16, 1, geoWikiDatasetGroupId);
        }

        public int Save()
        {
            //TODO persist and return ID
            return -1;
        }
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string aName)
		{
			PropertyChangedEventHandler iHandler = PropertyChanged;
			if (iHandler != null)
			{
				iHandler(this, new PropertyChangedEventArgs(aName));
			}
		}
	}
    }
}