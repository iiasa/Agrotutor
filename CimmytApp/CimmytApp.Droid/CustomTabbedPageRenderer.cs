using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using CimmytApp.Droid;
using CimmytApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

//[assembly: ExportRenderer(typeof(MainPage), typeof(CustomTabbedPageRenderer))]
//[assembly: ExportRenderer(typeof(ParcelPage), typeof(CustomTabbedPageRenderer))]
namespace CimmytApp.Droid
{

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
                    break;
            }
            if (layout != null)
            {
                for (int tabIndex = 0; tabIndex < layout.TabCount; tabIndex++)
                    SetTabIcon(layout, tabIndex);
            }
        }

        private void SetTabIcon(TabLayout layout, int tabIndex)
        {
            var tab = layout.GetTabAt(tabIndex);
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