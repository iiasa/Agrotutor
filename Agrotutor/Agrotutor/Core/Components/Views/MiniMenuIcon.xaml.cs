namespace Agrotutor.Core.Components.Views
{
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiniMenuIcon : ContentView
    {
        public event EventHandler Clicked;

        public static readonly BindableProperty IconTextProperty =
            BindableProperty.Create(nameof(IconText), typeof(string), typeof(MiniMenuIcon), default(string));

        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(MiniMenuIcon));

        public static readonly BindableProperty CommandProperty =
             BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MiniMenuIcon));

        public static readonly BindableProperty CommandParameterProperty =
             BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MiniMenuIcon));

        public string IconText
        {
            get => (string)GetValue(MiniMenuIcon.IconTextProperty);
            set => SetValue(MiniMenuIcon.IconTextProperty, value);
        }

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(MiniMenuIcon.IconSourceProperty);
            set => SetValue(MiniMenuIcon.IconSourceProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(MiniMenuIcon.CommandProperty);
            set => SetValue(MiniMenuIcon.CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(MiniMenuIcon.CommandParameterProperty);
            set => SetValue(MiniMenuIcon.CommandParameterProperty, value);
        }

        public MiniMenuIcon()
        {
            InitializeComponent();

            this.IconTextLabel.SetBinding(Label.TextProperty, new Binding(nameof(IconText), source: this));
            this.IconImage.SetBinding(Image.SourceProperty, new Binding(nameof(IconSource), source: this));

            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Clicked?.Invoke(this, EventArgs.Empty);

                    if (Command != null)
                    {
                        if (Command.CanExecute(CommandParameter))
                            Command.Execute(CommandParameter);
                    }
                })
            });
        }
    }
}