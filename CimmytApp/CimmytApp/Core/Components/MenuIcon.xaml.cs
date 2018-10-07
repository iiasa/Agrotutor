namespace CimmytApp.Core.Components
{
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuIcon : ContentView
    {
        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create(nameof(IconSrc), typeof(ImageSource), null);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), null);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), null);

        public MenuIcon()
		{
			InitializeComponent();
		}

        public ImageSource IconSrc
        {
            get => this.Icon.Source;
            set => this.Icon.Source = value;
        }

        public string Text
        {
            get => this.Label.Text;
            set => this.Label.Text = value;
        }

        public ICommand Command
        {
            get => this.ClickGestureRecognizer.Command;
            set => this.ClickGestureRecognizer.Command = value;
        }

        public object CommandParameter {
            get => this.ClickGestureRecognizer.CommandParameter;
            set => this.ClickGestureRecognizer.CommandParameter = value;
        }
    }
}