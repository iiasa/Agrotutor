using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agrotutor.Core.Components.Views
{
	public partial class MapDataView : ContentView
	{
        public static readonly BindableProperty LabelTextProperty =
            BindableProperty.Create(nameof(LabelText), typeof(string), typeof(MapDataView), default(string));

        public static readonly BindableProperty ValueTextProperty =
            BindableProperty.Create(nameof(ValueText), typeof(string), typeof(MapDataView), default(string));

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public string ValueText
        {
            get => (string)GetValue(ValueTextProperty);
            set => SetValue(ValueTextProperty, value);
        }

        public MapDataView ()
		{
			InitializeComponent ();
            MapDataLabel.SetBinding(Label.TextProperty, new Binding(nameof(LabelText), source: this));
            MapDataValue.SetBinding(Label.TextProperty, new Binding(nameof(ValueText), source: this));
		}
	}
}