namespace Helper.Localization
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using Helper.Localization.Localization;
    using Xamarin.Forms;

    public static class LocalizedString
    {
        internal const string ResourceId = "Helper.Localization.Resx.AppResources";
        private static CultureInfo _ci;

        public static string Get(string resourceName, string resourceId)
        {
            return LoadString(resourceName, resourceId);
        }

        public static string Get(string resourceName, Localization.ILocalizeConsumer context)
        {
            return LoadString(resourceName, context.GetResourceId());
        }

        public static string Get(string resourceName)
        {
            return LoadString(resourceName, null);
        }

        internal static string LoadString(string resourceName, string resourceId)
        {
            if (LocalizedString._ci == null)
            {
                SetCultureInfo();
            }
            if (resourceName == null)
            {
                return "";
            }

            var resId = LocalizedString.ResourceId;
            var usingLibResource = false;
            if (resourceId != null && !resId.Equals(resourceId))
            {
                resId = resourceId;
                usingLibResource = true;
            }
            var resourceManager = new ResourceManager(resId, typeof(TranslateExtension).GetTypeInfo().Assembly);
            var translation = resourceManager.GetString(resourceName, LocalizedString._ci);

            if (translation == null)
            {
                if (usingLibResource)
                {
                    resourceManager = new ResourceManager(LocalizedString.ResourceId,
                        typeof(TranslateExtension).GetTypeInfo().Assembly);
                    translation = resourceManager.GetString(resourceName, LocalizedString._ci);
                }

                if (translation == null)
                {
#if DEBUG
                    throw new ArgumentException(
                        $"Key '{resourceName}' was not found in resources '{LocalizedString.ResourceId}' for culture '{LocalizedString._ci.Name}'.",
                        "Text");
#else
                    translation = resourceName; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
                }
            }

            return translation;
        }

        private static void SetCultureInfo()
        {
            if (LocalizedString._ci != null)
            {
                return;
            }

            //Device.OS marked as obsolete, but proposed Device.RuntimePlatform didn't work last time I checked...
            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                LocalizedString._ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
        }
    }
}