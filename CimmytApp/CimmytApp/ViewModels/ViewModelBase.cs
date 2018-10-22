namespace CimmytApp.ViewModels
{
    using Microsoft.Extensions.Localization;
    using Prism.Mvvm;

    public abstract class ViewModelBase : BindableBase
    {
        protected ViewModelBase(IStringLocalizer stringLocalizer)
        {
            Localizer = stringLocalizer;
        }

        protected IStringLocalizer Localizer { get; set; }
    }
}