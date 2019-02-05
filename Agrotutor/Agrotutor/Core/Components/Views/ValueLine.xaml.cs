namespace Agrotutor.Core.Components.Views
{
    using System;
    using System.Windows.Input;

    using Xamarin.Forms;

    public partial class ValueLine : Grid
    {
        public event EventHandler Clicked;

        public static readonly BindableProperty DescriptionTextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(ValueLine),
            string.Empty);

        public static readonly BindableProperty ValueTextProperty = BindableProperty.Create(
            nameof(Value),
            typeof(string),
            typeof(ValueLine),
            string.Empty);

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(ImageSource),
            typeof(SimpleStats));

        public string Text
        {
            get => (string)GetValue(ValueLine.DescriptionTextProperty);
            set => SetValue(ValueLine.DescriptionTextProperty, value);
        }

        public string Value
        {
            get => (string)GetValue(ValueLine.ValueTextProperty);
            set => SetValue(ValueLine.ValueTextProperty, value);
        }

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ValueLine.ImageSourceProperty);
            set => SetValue(ValueLine.ImageSourceProperty, value);
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

        public ValueLine()
        {
            InitializeComponent();

            this.LblText.SetBinding(Label.TextProperty, new Binding(nameof(this.Text), source: this));
            this.LblValue.SetBinding(Label.TextProperty, new Binding(nameof(this.Value), source: this));
            this.Image.SetBinding(Image.SourceProperty, new Binding(nameof(this.ImageSource), source: this));

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