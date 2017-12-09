using System;
using Realms;

namespace Helper.Realm.DTO
{
    public class AgriculturalActivityDTO : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int ActivityType { get; set; }
        public string AmountApplied { get; set; }
        public string AppliedProduct { get; set; }
        public double Cost { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Dose { get; set; }
        public string Name { get; set; }
        public double NumberOfSeeds { get; set; }
        public string ProductObtained { get; set; }
        public string Sown { get; set; }
        public double WeightOfSeeds { get; set; }
        public string Yield { get; set; }
    }
}
