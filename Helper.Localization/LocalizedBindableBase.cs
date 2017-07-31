namespace Helper.Localization
{
    using Helper.Localization.Localization;

    public class LocalizedBindableBase : BindableBase
    {
        TranslateExtension TranslateExtension;

        public LocalizedBindableBase()
        {
            TranslateExtension = new TranslateExtension();
        }

        protected string getText(string text_id){
            //return (string)TranslateExtension.ProvideValue();
            return "";
        }
    }
}
