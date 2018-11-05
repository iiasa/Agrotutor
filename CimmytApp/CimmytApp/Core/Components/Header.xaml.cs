namespace CimmytApp.Core.Components
{
    using Xamarin.Forms;

    public partial class Header : ContentView
    {

        public static readonly BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(MenuIcon), default(string));

        public static readonly BindableProperty HeaderIconSourceProperty =
            BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(MenuIcon));

        public string Text
        {
            get => (string)GetValue(Header.HeaderTextProperty);
            set => SetValue(Header.HeaderTextProperty, value);
        }

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(Header.HeaderIconSourceProperty);
            set => SetValue(Header.HeaderIconSourceProperty, value);
        }

        public Header ()
		{
			InitializeComponent ();

            this.HeaderTextLabel.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
            this.HeaderIcon.SetBinding(Image.SourceProperty, new Binding(nameof(IconSource), source: this));
        }
	}
}