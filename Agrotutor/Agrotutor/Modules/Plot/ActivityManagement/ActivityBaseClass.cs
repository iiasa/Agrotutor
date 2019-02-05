namespace Agrotutor.Modules.Plot.ActivityManagement
{
    public abstract class ActivityBaseClass
    {
        protected ActivityBaseClass()
        {
            SetActivityDynamicUIVisibility();
        }

        public ActivityDynamicUIVisibility ActivityDynamicUIVisibility { get; set; }

        public abstract void SetActivityDynamicUIVisibility();
    }
}