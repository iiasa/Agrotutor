namespace CimmytApp.Core.Components.Views
{
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public partial class MenuIcon : ContentView
    {
        public event EventHandler Clicked;

        public static readonly BindableProperty IconTextProperty =
            BindableProperty.Create(nameof(IconText), typeof(string), typeof(MenuIcon), default(string));

        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(MenuIcon));

        public static readonly BindableProperty CommandProperty =
             BindableProperty.Create(nameof(MenuIcon.Command), typeof(ICommand), typeof(MenuIcon));

        public static readonly BindableProperty CommandParameterProperty =
             BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MenuIcon));

        public string IconText
        {
            get => (string)GetValue(MenuIcon.IconTextProperty);
            set => SetValue(MenuIcon.IconTextProperty, value);
        }

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(MenuIcon.IconSourceProperty);
            set => SetValue(MenuIcon.IconSourceProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(MenuIcon.CommandProperty);
            set => SetValue(MenuIcon.CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(MenuIcon.CommandParameterProperty);
            set => SetValue(MenuIcon.CommandParameterProperty, value);
        }

        public MenuIcon()
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