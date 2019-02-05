namespace Agrotutor.Core.Localization
{
    using System.Globalization;

    public interface ILocalizer
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo cultureInfo);
    }
}
