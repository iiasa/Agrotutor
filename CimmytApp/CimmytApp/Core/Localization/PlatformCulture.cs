namespace CimmytApp.Core.Localization
{
    using System;

    public class PlatformCulture
    {

        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", nameof(platformCultureString));
            }

            // .NET expects dash, not underscore
            PlatformString = platformCultureString.Replace("_", "-");

            int dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);

            if (dashIndex > 0)
            {
                string[] parts = PlatformString.Split('-');

                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = string.Empty;
            }
        }

        public string LanguageCode { get; protected set; }

        public string LocaleCode { get; protected set; }

        public string PlatformString { get; protected set; }

        public override string ToString()
        {
            return PlatformString;
        }
    }
}