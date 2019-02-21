using Agrotutor.Core.Entities;
using Microsoft.Extensions.Localization;
using Prism;

namespace Agrotutor.Modules.Map
{
    public class Helper
    {

        public static string GetMaturityTypeString(MaturityType maturityType)
        {
            var stringLocalizer = (IStringLocalizer<Helper>)PrismApplicationBase.Current.Container.Resolve(typeof(IStringLocalizer<Helper>));
            switch (maturityType)
            {
                case MaturityType.Early:
                    return stringLocalizer.GetString("early");
                case MaturityType.SemiEarly:
                    return stringLocalizer.GetString("semi_early");
                case MaturityType.Intermediate:
                    return stringLocalizer.GetString("intermediate");
                case MaturityType.SemiLate:
                    return stringLocalizer.GetString("semi_late");
                case MaturityType.Late:
                    return stringLocalizer.GetString("late");
                default:
                    return "";
            }
        }

        public static string GetClimateTypeString(ClimateType climateType)
        {
            var stringLocalizer = (IStringLocalizer<Helper>)PrismApplicationBase.Current.Container.Resolve(typeof(IStringLocalizer<Helper>));
            switch (climateType)
            {
                case ClimateType.Cold:
                    return stringLocalizer.GetString("cold");
                case ClimateType.TemperedSubtropical:
                    return stringLocalizer.GetString("tempered");
                case ClimateType.Tropical:
                    return stringLocalizer.GetString("tropical");
                case ClimateType.Hybrid:
                    return stringLocalizer.GetString("hybrid");
                default:
                    return "";
            }
        }
    }
}
