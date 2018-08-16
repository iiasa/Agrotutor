namespace Helper.Localization.Localization
{
    using System.Globalization;

    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}