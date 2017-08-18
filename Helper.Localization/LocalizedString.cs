using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Helper.Localization.Localization;
using Xamarin.Forms;

namespace Helper.Localization
{
    using BusinessContract;

    public static class LocalizedString
    {
        private static CultureInfo _ci;
        internal const string ResourceId = "Helper.Localization.Resx.AppResources";

        public static string Get(string resourceName, string resourceId)
        {
            return LoadString(resourceName, resourceId);
        }

        public static string Get(string resourceName, ILocalizeConsumer context)
        {
            return LoadString(resourceName, context.GetResourceId());
        }

        public static string Get(string resourceName)
        {
            return LoadString(resourceName, null);
        }

        internal static string LoadString(string resourceName, string resourceId)
        {
            if (_ci == null) SetCultureInfo();
            if (resourceName == null) return "";
            var resId = ResourceId;
            var usingLibResource = false;
            if (resourceId != null && !resId.Equals(resourceId))
            {
                resId = resourceId;
                usingLibResource = true;
            }
            var resourceManager = new ResourceManager(resId, typeof(TranslateExtension).GetTypeInfo().Assembly);
            var translation = resourceManager.GetString(resourceName, _ci);

            if (translation == null)
            {
                if (usingLibResource)
                {
                    resourceManager = new ResourceManager(ResourceId,
                        typeof(TranslateExtension).GetTypeInfo().Assembly);
                    translation = resourceManager.GetString(resourceName, _ci);
                }

                if (translation == null)
                {
#if DEBUG
                    throw new ArgumentException(
                        $"Key '{resourceName}' was not found in resources '{ResourceId}' for culture '{_ci.Name}'.",
                        "Text");
#else
				translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
                }
            }
            return translation;
        }

        private static void SetCultureInfo()
        {
            if (_ci != null) return;
            //Device.OS marked as obsolete, but proposed Device.RuntimePlatform didn't work last time I checked...
            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                _ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
        }
    }
}