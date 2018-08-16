namespace Helper.Localization
{
    using Helper.Localization.Localization;
    using Prism.Mvvm;

    public class LocalizedBindableBase : BindableBase
    {
        private TranslateExtension TranslateExtension;

        public LocalizedBindableBase()
        {
            TranslateExtension = new TranslateExtension();
        }

        protected string getText(string text_id)
        {
            //return (string)TranslateExtension.ProvideValue();
            return "";
        }
    }
}