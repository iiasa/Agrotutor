namespace Helper.Localization.Localization
{
    using System;
    using Helper.BusinessContract;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            string resId = null;
            if (serviceProvider != null)
            {
                IRootObjectProvider rootObjectProvider =
                    serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
#pragma warning disable CS0436 // The type 'ILocalizeConsumer' in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs' conflicts with the imported type 'ILocalizeConsumer' in 'Helper.BusinessContract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs'.
#pragma warning disable CS0436 // The type 'ILocalizeConsumer' in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs' conflicts with the imported type 'ILocalizeConsumer' in 'Helper.BusinessContract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs'.
                ILocalizeConsumer view = rootObjectProvider?.RootObject as ILocalizeConsumer;
#pragma warning restore CS0436 // The type 'ILocalizeConsumer' in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs' conflicts with the imported type 'ILocalizeConsumer' in 'Helper.BusinessContract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs'.
#pragma warning restore CS0436 // The type 'ILocalizeConsumer' in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs' conflicts with the imported type 'ILocalizeConsumer' in 'Helper.BusinessContract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'C:\Users\karner\Documents\Projects\dotNET projects\CimmytApp\Helper.Localization\Localization\ILocalizeConsumer.cs'.
                if (view != null)
                {
                    resId = view.GetResourceId();
                }
            }

            return LocalizedString.LoadString(Text, resId);
        }
    }
}