using System;
using System.Globalization;

namespace Helper.Localization.Localization
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}