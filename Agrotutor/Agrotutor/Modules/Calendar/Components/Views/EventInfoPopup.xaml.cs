using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agrotutor.Modules.Calendar.ViewModels;
using Prism.Navigation;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agrotutor.Modules.Calendar.Components.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventInfoPopup : PopupPage
	{
	    private EventInfoPopupViewModel _eventInfoPopupViewModel;
		public EventInfoPopup ()
		{
			InitializeComponent ();
			((EventInfoPopupViewModel) BindingContext).SetView(this);
		}

    }
}