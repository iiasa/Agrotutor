using System;
using System.Globalization;

namespace CimmytApp.Localization
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}