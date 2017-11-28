namespace Helper.Localization
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using Helper.BusinessContract;
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

#pragma warning disable CS0436 // The type 'ILocalizeConsumer' in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs' conflicts with the imported type 'ILocalizeConsumer' in 'Helper.BusinessContract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs'.
        public static string Get(string resourceName, ILocalizeConsumer context)
#pragma warning restore CS0436 // The type 'ILocalizeConsumer' in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs' conflicts with the imported type 'ILocalizeConsumer' in 'Helper.BusinessContract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs'.
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

            string resId = LocalizedString.ResourceId;
            bool usingLibResource = false;
            if (resourceId != null && !resId.Equals(resourceId))
            {
                resId = resourceId;
                usingLibResource = true;
            }
            ResourceManager resourceManager =
                new ResourceManager(resId, typeof(TranslateExtension).GetTypeInfo().Assembly);
            string translation = resourceManager.GetString(resourceName, LocalizedString._ci);

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
#pragma warning disable CS0618 // 'Device.OS' is obsolete: 'Use RuntimePlatform instead.'
#pragma warning disable CS0618 // 'Device.OS' is obsolete: 'Use RuntimePlatform instead.'
#pragma warning disable CS0612 // 'TargetPlatform' is obsolete
#pragma warning disable CS0612 // 'TargetPlatform' is obsolete
            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
#pragma warning restore CS0612 // 'TargetPlatform' is obsolete
#pragma warning restore CS0612 // 'TargetPlatform' is obsolete
#pragma warning restore CS0618 // 'Device.OS' is obsolete: 'Use RuntimePlatform instead.'
#pragma warning restore CS0618 // 'Device.OS' is obsolete: 'Use RuntimePlatform instead.'
            {
                LocalizedString._ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
        }
    }
}