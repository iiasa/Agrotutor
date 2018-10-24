﻿namespace CimmytApp.DTO.Parcel
{
    using System;
    using Helper.Realm.DTO;

    /// <summary>
    ///     Defines the <see cref="AgriculturalActivity" />
    /// </summary>
    public class AgriculturalActivity
    {
        public string Logo
        {
            get
            {
                switch (ActivityType)
                {
                    case ActivityType.SoilImprovers:
                        return "flask.png";

                    case ActivityType.GroundPreperation:
                        return "shovel.png";

                    case ActivityType.Sowing:
                        return "sowing.png";

                    case ActivityType.Fertilization:
                        return "fertilizer.png";

                    case ActivityType.Irrigation:
                        return "irrigation.png";

                    case ActivityType.WeedPreventionControl:
                        return "weeds.png";

                    case ActivityType.Harvest:
                        return "harvest.png";

                    case ActivityType.PostHarvestStorage:
                        return "storage.png";

                    case ActivityType.Commercialization:
                        return "money.png";

                    case ActivityType.OtherActivities:
                        return "farmer.png";

                    case ActivityType.PestAndDiseaseControlAndPrevention:
                        return "cockroach.png";

                    default:
                        return "farmer.png";
                }
            }
        }

        /// <summary>
        ///     Gets or sets the ActivityType
        /// </summary>
        public ActivityType ActivityType { get; set; }

        /// <summary>
        ///     Gets or sets the AmountApplied
        /// </summary>
        public string AmountApplied { get; set; }

        /// <summary>
        ///     Gets or sets the AppliedProduct
        /// </summary>
        public string AppliedProduct { get; set; }

        /// <summary>
        ///     Gets or sets the Cost
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        ///     Gets or sets the Date
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        ///     Gets or sets the Dose
        /// </summary>
        public double Dose { get; set; }

        /// <summary>
        ///     Gets or sets the Id
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        ///     Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the NumberOfSeeds
        /// </summary>
        public double NumberOfSeeds { get; set; }

        /// <summary>
        ///     Gets or sets the ParcelId
        /// </summary>
        public string ParcelId { get; set; }

        /// <summary>
        ///     Gets or sets the ProductObtained
        /// </summary>
        public string ProductObtained { get; set; }

        /// <summary>
        ///     Gets or sets the Sown
        /// </summary>
        public string Sown { get; set; }

        /// <summary>
        ///     Gets or sets the WeightOfSeeds
        /// </summary>
        public double WeightOfSeeds { get; set; }

        /// <summary>
        ///     Gets or sets the Yield
        /// </summary>
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