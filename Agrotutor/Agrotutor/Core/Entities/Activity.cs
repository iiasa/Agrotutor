namespace Agrotutor.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public ActivityType ActivityType { get; set; }
        public string AmountApplied { get; set; }
        public string AppliedProduct { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public double Dose { get; set; }
        public string Name { get; set; }
        public double NumberOfSeeds { get; set; }
        public string ParcelId { get; set; }
        public string ProductObtained { get; set; }
        public string Sown { get; set; }
        public double WeightOfSeeds { get; set; }
        public string Yield { get; set; }
        public int SellingPrice { get; set; }
        public string AmountSold { get; set; }
        public string Comment { get; set; }
        public int PlotArea { get; set; }
    }
}
