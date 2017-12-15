namespace Helper.Realm.DTO
{
    using System;
    using Realms;

    public class AgriculturalActivityDTO : RealmObject
    {
        public int ActivityType { get; set; }

        public string AmountApplied { get; set; }

        public string AppliedProduct { get; set; }

        public double Cost { get; set; }

        public DateTimeOffset Date { get; set; }

        public double Dose { get; set; }

        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public double NumberOfSeeds { get; set; }

        public string ProductObtained { get; set; }

        public string Sown { get; set; }

        public double WeightOfSeeds { get; set; }

        public string Yield { get; set; }

        public string ParcelId { get; set; }
    }
}