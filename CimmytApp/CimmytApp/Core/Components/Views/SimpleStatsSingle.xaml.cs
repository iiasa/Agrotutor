namespace CimmytApp.Core.Components.Views
{
    using Xamarin.Forms;

    public partial class SimpleStatsSingle : ContentView
    {
        public static readonly BindableProperty AverageImageSourceProperty = BindableProperty.Create(
            nameof(AverageImageSource),
            typeof(ImageSource),
            typeof(SimpleStatsSingle));

        public static readonly BindableProperty AverageTextProperty = BindableProperty.Create(
            nameof(AverageText),
            typeof(string),
            typeof(SimpleStatsSingle),
            string.Empty);

        public static readonly BindableProperty AverageValueProperty = BindableProperty.Create(
            nameof(AverageValue),
            typeof(double),
            typeof(SimpleStatsSingle));

        public static readonly BindableProperty MaxImageSourceProperty = BindableProperty.Create(
            nameof(MaxImageSource),
            typeof(ImageSource),
            typeof(SimpleStatsSingle));

        public static readonly BindableProperty MaxTextProperty = BindableProperty.Create(
            nameof(MaxText),
            typeof(string),
            typeof(SimpleStatsSingle),
            string.Empty);

        public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(
            nameof(MaxValue),
            typeof(double),
            typeof(SimpleStatsSingle));

        public static readonly BindableProperty MinImageSourceProperty = BindableProperty.Create(
            nameof(MinImageSource),
            typeof(ImageSource),
            typeof(SimpleStatsSingle));

        public static readonly BindableProperty MinTextProperty = BindableProperty.Create(
            nameof(MinText),
            typeof(string),
            typeof(SimpleStatsSingle),
            string.Empty);

        public static readonly BindableProperty MinValueProperty = BindableProperty.Create(
            nameof(MinValue),
            typeof(double),
            typeof(SimpleStatsSingle));

        public static readonly BindableProperty UnitProperty = BindableProperty.Create(
            nameof(Unit),
            typeof(string),
            typeof(SimpleStatsSingle),
            string.Empty);

        private ViewModes defaultViewMode;

        private ViewModes viewMode;

        public SimpleStatsSingle()
        {
            InitializeComponent();

            this.ImageMin.SetBinding(Image.SourceProperty, new Binding(nameof(MinImageSource), source: this));
            this.ImageAverage.SetBinding(Image.SourceProperty, new Binding(nameof(AverageImageSource), source: this));
            this.ImageMax.SetBinding(Image.SourceProperty, new Binding(nameof(MaxImageSource), source: this));

            this.lblMinValue.SetBinding(Label.TextProperty, new Binding(nameof(MinValue), source: this));
            this.lblAverageValue.SetBinding(Label.TextProperty, new Binding(nameof(AverageValue), source: this));
            this.lblMaxValue.SetBinding(Label.TextProperty, new Binding(nameof(MaxValue), source: this));

            this.lblMinUnit.SetBinding(Label.TextProperty, new Binding(nameof(Unit), source: this));
            this.lblAverageUnit.SetBinding(Label.TextProperty, new Binding(nameof(Unit), source: this));
            this.lblMaxUnit.SetBinding(Label.TextProperty, new Binding(nameof(Unit), source: this));

            this.lblMin.SetBinding(Label.TextProperty, new Binding(nameof(MinText), source: this));
            this.lblAverage.SetBinding(Label.TextProperty, new Binding(nameof(AverageText), source: this));
            this.lblMax.SetBinding(Label.TextProperty, new Binding(nameof(MaxText), source: this));
        }

        public enum ViewModes
        {
            Irrigated,

            Rainfed
        }

        public ImageSource AverageImageSource
        {
            get => (ImageSource)GetValue(SimpleStats.AverageImageSourceProperty);
            set => SetValue(SimpleStats.AverageImageSourceProperty, value);
        }

        public string AverageText
        {
            get => (string)GetValue(SimpleStatsSingle.AverageTextProperty);
            set => SetValue(SimpleStatsSingle.AverageTextProperty, value);
        }

        public double AverageValue
        {
            get => (double)GetValue(SimpleStatsSingle.AverageValueProperty);
            set => SetValue(SimpleStatsSingle.AverageValueProperty, value);
        }

        public ImageSource MaxImageSource
        {
            get => (ImageSource)GetValue(SimpleStatsSingle.MaxImageSourceProperty);
            set => SetValue(SimpleStatsSingle.MaxImageSourceProperty, value);
        }

        public string MaxText
        {
            get => (string)GetValue(SimpleStatsSingle.MaxTextProperty);
            set => SetValue(SimpleStatsSingle.MaxTextProperty, value);
        }

        public double MaxValue
        {
            get => (double)GetValue(SimpleStatsSingle.MaxValueProperty);
            set => SetValue(SimpleStatsSingle.MaxValueProperty, value);
        }

        public ImageSource MinImageSource
        {
            get => (ImageSource)GetValue(SimpleStatsSingle.MinImageSourceProperty);
            set => SetValue(SimpleStatsSingle.MinImageSourceProperty, value);
        }

        public string MinText
        {
            get => (string)GetValue(SimpleStatsSingle.MinTextProperty);
            set => SetValue(SimpleStatsSingle.MinTextProperty, value);
        }

        public double MinValue
        {
            get => (double)GetValue(SimpleStatsSingle.MinValueProperty);
            set => SetValue(SimpleStatsSingle.MinValueProperty, value);
        }

        public string Unit
        {
            get => (string)GetValue(SimpleStatsSingle.UnitProperty);
            set => SetValue(SimpleStatsSingle.UnitProperty, value);
        }
    }
}