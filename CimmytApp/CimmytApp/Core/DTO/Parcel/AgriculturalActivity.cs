namespace CimmytApp.Core.DTO.Parcel
{
    using System;
    using CimmytApp.DTO.Parcel;
    using Xamarin.Forms;

    public class AgriculturalActivitey
    {
        public ImageSource Logo
        {
            get
            {
                if (Constants.ActivityLogos.TryGetValue(ActivityType, out var imageSource))
                {
                    return imageSource;
                }
                return ImageSource.FromFile("farmer.png");
            }
        }

        public ActivityType ActivityType { get; set; }

        public string AmountApplied { get; set; }

        public string AppliedProduct { get; set; }

        public double Cost { get; set; }

        public DateTimeOffset Date { get; set; }

        public double Dose { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public double NumberOfSeeds { get; set; }

        public string ParcelId { get; set; }

        public string ProductObtained { get; set; }

        public string Sown { get; set; }

        public double WeightOfSeeds { get; set; }

        public string Yield { get; set; }
    }
}