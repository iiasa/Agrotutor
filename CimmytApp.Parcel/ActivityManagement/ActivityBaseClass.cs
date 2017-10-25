namespace CimmytApp.Parcel.ActivityManagement
{
    public abstract class ActivityBaseClass
    {
        public ActivityDynamicUIVisibility ActivityDynamicUIVisibility { get; set; }

        protected ActivityBaseClass()
        {
            SetActivityDynamicUIVisibility();
        }

        public abstract void SetActivityDynamicUIVisibility();
    }
}