namespace CimmytApp.Core.Components
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuIcon : ContentView
    {
        public static readonly BindableProperty IconTextProperty =
            BindableProperty.Create(nameof(IconText), typeof(string), typeof(MenuIcon), default(string));

        public string IconText
        {
            get => (string)GetValue(MenuIcon.IconTextProperty);
            set => SetValue(MenuIcon.IconTextProperty, value);
        }

        // public static readonly BindableProperty TextProperty =
        //     BindableProperty.Create(nameof(Text), typeof(string), typeof(Label), "Not set");
        // private string text;
        //
        // public static readonly BindableProperty IconSourceProperty =
        //     BindableProperty.Create(nameof(IconSrc), typeof(ImageSource), null);
        //
        // public static readonly BindableProperty CommandProperty =
        //     BindableProperty.Create(nameof(Command), typeof(ICommand), null);
        //
        // public static readonly BindableProperty CommandParameterProperty =
        //     BindableProperty.Create(nameof(CommandParameter), typeof(object), null);

        public MenuIcon()
		{
			InitializeComponent();

            this.IconTextLabel.SetBinding(Label.TextProperty, new Binding(nameof(IconText), source: this));
		}
    }
}