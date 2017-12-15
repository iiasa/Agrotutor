﻿namespace Helper.Localization.Localization
{
    using System;
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
                var rootObjectProvider = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
                var view = rootObjectProvider?.RootObject as ILocalizeConsumer;
                if (view != null)
                {
                    resId = view.GetResourceId();
                }
            }

            return LocalizedString.LoadString(Text, resId);
        }
    }
}