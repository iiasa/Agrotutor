namespace CimmytApp.Core.DTO.Parcel
{
    using System;
    using CimmytApp.DTO.Parcel;
    using Helper.Realm.DTO;
    using Xamarin.Forms;

    public class AgriculturalActivity
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

        public static AgriculturalActivity FromDTO(AgriculturalActivityDTO activity)
        {
            if (activity == null)
            {
                return null;
            }

            return new AgriculturalActivity
            {
                ActivityType = (ActivityType)activity.ActivityType,
                AmountApplied = activity.AmountApplied,
                AppliedProduct = activity.AppliedProduct,
                Cost = activity.Cost,
                Date = activity.Date,
                Dose = activity.Dose,
                Id = activity.Id,
                Name = activity.Name,
                NumberOfSeeds = activity.NumberOfSeeds,
                ParcelId = activity.ParcelId,
                ProductObtained = activity.ProductObtained,
                Sown = activity.Sown,
                WeightOfSeeds = activity.WeightOfSeeds,
                Yield = activity.Yield
            };
        }

        public AgriculturalActivityDTO GetDTO(string parcelId)
        {
            return new AgriculturalActivityDTO
            {
                ActivityType = (int)ActivityType,
                AmountApplied = AmountApplied,
                AppliedProduct = AppliedProduct,
                Cost = Cost,
                Date = Date,
                Dose = Dose,
                Id = Id,
                Name = Name,
                NumberOfSeeds = NumberOfSeeds,
                ParcelId = parcelId,
                ProductObtained = ProductObtained,
                Sown = Sown,
                WeightOfSeeds = WeightOfSeeds,
                Yield = Yield
            };
        }
    }
}