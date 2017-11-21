namespace CimmytApp.Droid
{
    using Android.Support.Design.Widget;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;
    using Xamarin.Forms.Platform.Android.AppCompat;

    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            //var layout = (TabLayout)ViewGroup.GetChildAt(1); //should be enough but just for robustness we use loop below

            TabLayout layout = null;
            for (int i = 0; i < ChildCount; i++)
            {
                layout = GetChildAt(i) as TabLayout;

                if (layout != null)
                {
                    break;
                }
            }

            if (layout != null)
            {
                for (int tabIndex = 0; tabIndex < layout.TabCount; tabIndex++)
                {
                    SetTabIcon(layout, tabIndex);
                }
            }
        }

        private void SetTabIcon(TabLayout layout, int tabIndex)
        {
            TabLayout.Tab tab = layout.GetTabAt(tabIndex);
            tab.SetIcon(tab.Icon);

            //from local resource
            //  Context context = layout.Context;
            //   Resources resources = context.Resources;
            //  int resourceId = resources.GetIdentifier(tab.Icon, "drawable", null);
            //    context.getPackageName());
            //return resources.getDrawable(resourceId);
            // tab.SetIcon(Resource.Drawable.);
            //switch (tabIndex)
            //{
            //    case 0:
            //        tab.SetIcon(Resource.Drawable.icon_overview); //from local resource
            //        break;
            //    case 1:
            //        tab.SetIcon(Resource.Drawable.icon_map); //from Android system, depends on version !
            //        break;
            //    case 2:
            //        tab.SetIcon(Resource.Drawable.icon_info); //from Android system, depends on version !
            //        break;
            //    case 3:
            //        tab.SetIcon(Resource.Drawable.icon_calendar); //from Android system, depends on version !
            //        break;
            //    case 4:
            //        tab.SetIcon(Resource.Drawable.icon_info); //from Android system, depends on version !
            //        break;

            //}
            //  }
        }
    }
}