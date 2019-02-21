using Agrotutor.Modules.Plot.ViewModels;
using Microsoft.Extensions.Localization;

namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public abstract class ActivityBaseClass
    {
        protected IStringLocalizer<ActivityDetailViewModel> _stringLocalizer;
        protected ActivityBaseClass(IStringLocalizer<ActivityDetailViewModel> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            SetActivityDynamicUIVisibility();
        }

        public ActivityDynamicUIVisibility ActivityDynamicUIVisibility { get; set; }

        public abstract void SetActivityDynamicUIVisibility();
    }
}