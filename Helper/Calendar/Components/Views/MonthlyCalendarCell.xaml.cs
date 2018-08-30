using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Helper.Calendar.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonthlyCalendarCell : ContentView
	{
		public MonthlyCalendarCell ()
		{
			InitializeComponent ();
		}
	}
}