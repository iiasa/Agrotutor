namespace CimmytApp.Core.Components.Views
{
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public partial class IconWithText : ContentView
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
            get => (string)GetValue(IconWithText.IconTextProperty);
            set => SetValue(IconWithText.IconTextProperty, value);
        }

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconWithText.IconSourceProperty);
            set => SetValue(IconWithText.IconSourceProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(IconWithText.CommandProperty);
            set => SetValue(IconWithText.CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(IconWithText.CommandParameterProperty);
            set => SetValue(IconWithText.CommandParameterProperty, value);
        }

        public IconWithText ()
		{
			InitializeComponent ();

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