namespace Agrotutor.Core.Components.Views
{
    using System;
    using Xamarin.Forms;

    public partial class SimpleStats : ContentView
    {
        public static readonly BindableProperty AverageImageSourceProperty = BindableProperty.Create(
            nameof(AverageImageSource),
            typeof(ImageSource),
            typeof(SimpleStats));

        public static readonly BindableProperty AverageTextProperty = BindableProperty.Create(
            nameof(AverageText),
            typeof(string),
            typeof(SimpleStats),
            string.Empty);

        public static readonly BindableProperty AverageValueIrrigatedProperty = BindableProperty.Create(
            nameof(AverageValueIrrigated),
            typeof(double),
            typeof(SimpleStats));

        public static readonly BindableProperty AverageValueRainfedProperty = BindableProperty.Create(
            nameof(AverageValueRainfed),
            typeof(double),
            typeof(SimpleStats));

        public static readonly BindableProperty DefaultViewModeProperty = BindableProperty.Create(
            nameof(DefaultViewMode),
            typeof(ViewModes),
            typeof(SimpleStats),
            ViewModes.Irrigated);

        public static readonly BindableProperty MaxImageSourceProperty = BindableProperty.Create(
            nameof(MaxImageSource),
            typeof(ImageSource),
            typeof(SimpleStats));

        public static readonly BindableProperty MaxTextProperty = BindableProperty.Create(
            nameof(MaxText),
            typeof(string),
            typeof(SimpleStats),
            string.Empty);

        public static readonly BindableProperty MaxValueIrrigatedProperty = BindableProperty.Create(
            nameof(MaxValueIrrigated),
            typeof(double),
            typeof(SimpleStats));

        public static readonly BindableProperty MaxValueRainfedProperty = BindableProperty.Create(
            nameof(MaxValueRainfed),
            typeof(double),
            typeof(SimpleStats));

        public static readonly BindableProperty MinImageSourceProperty = BindableProperty.Create(
            nameof(MinImageSource),
            typeof(ImageSource),
            typeof(SimpleStats));

        public static readonly BindableProperty MinTextProperty = BindableProperty.Create(
            nameof(MinText),
            typeof(string),
            typeof(SimpleStats),
            string.Empty);

        public static readonly BindableProperty MinValueIrrigatedProperty = BindableProperty.Create(
            nameof(MinValueIrrigated),
            typeof(double),
            typeof(SimpleStats));

        public static readonly BindableProperty MinValueRainfedProperty = BindableProperty.Create(
            nameof(MinValueRainfed),
            typeof(double),
            typeof(SimpleStats));

        public static readonly BindableProperty UnitProperty = BindableProperty.Create(
            nameof(Unit),
            typeof(string),
            typeof(SimpleStats),
            string.Empty);

        public static readonly BindableProperty ViewModeProperty = BindableProperty.Create(
            nameof(ViewMode),
            typeof(ViewModes),
            typeof(SimpleStats),
            ViewModes.Irrigated);

        private ViewModes defaultViewMode;

        private ViewModes viewMode;

        public SimpleStats()
        {
            InitializeComponent();

            this.ImageMin.SetBinding(Image.SourceProperty, new Binding(nameof(MinImageSource), source: this));
            this.ImageAverage.SetBinding(Image.SourceProperty, new Binding(nameof(AverageImageSource), source: this));
            this.ImageMax.SetBinding(Image.SourceProperty, new Binding(nameof(MaxImageSource), source: this));

            this.lblMinValueRainfed.SetBinding(Label.TextProperty, new Binding(nameof(MinValueRainfed), source: this));
            this.lblAverageValueRainfed.SetBinding(
                Label.TextProperty,
                new Binding(nameof(AverageValueRainfed), source: this));
            this.lblMaxValueRainfed.SetBinding(Label.TextProperty, new Binding(nameof(MaxValueRainfed), source: this));

            this.lblMinValueIrrigated.SetBinding(
                Label.TextProperty,
                new Binding(nameof(MinValueIrrigated), source: this));
            this.lblAverageValueIrrigated.SetBinding(
                Label.TextProperty,
                new Binding(nameof(AverageValueIrrigated), source: this));
            this.lblMaxValueIrrigated.SetBinding(
                Label.TextProperty,
                new Binding(nameof(MaxValueIrrigated), source: this));

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
            get => (string)GetValue(SimpleStats.AverageTextProperty);
            set => SetValue(SimpleStats.AverageTextProperty, value);
        }

        public double AverageValueIrrigated
        {
            get => (double)GetValue(SimpleStats.AverageValueIrrigatedProperty);
            set => SetValue(SimpleStats.AverageValueIrrigatedProperty, value);
        }

        public double AverageValueRainfed
        {
            get => (double)GetValue(SimpleStats.AverageValueRainfedProperty);
            set => SetValue(SimpleStats.AverageValueRainfedProperty, value);
        }

        public ViewModes DefaultViewMode
        {
            get => this.defaultViewMode;
            set
            {
                this.defaultViewMode = value;
                ViewMode = value;
            }
        }

        public ImageSource MaxImageSource
        {
            get => (ImageSource)GetValue(SimpleStats.MaxImageSourceProperty);
            set => SetValue(SimpleStats.MaxImageSourceProperty, value);
        }

        public string MaxText
        {
            get => (string)GetValue(SimpleStats.MaxTextProperty);
            set => SetValue(SimpleStats.MaxTextProperty, value);
        }

        public double MaxValueIrrigated
        {
            get => (double)GetValue(SimpleStats.MaxValueIrrigatedProperty);
            set => SetValue(SimpleStats.MaxValueIrrigatedProperty, value);
        }

        public double MaxValueRainfed
        {
            get => (double)GetValue(SimpleStats.MaxValueRainfedProperty);
            set => SetValue(SimpleStats.MaxValueRainfedProperty, value);
        }

        public ImageSource MinImageSource
        {
            get => (ImageSource)GetValue(SimpleStats.MinImageSourceProperty);
            set => SetValue(SimpleStats.MinImageSourceProperty, value);
        }

        public string MinText
        {
            get => (string)GetValue(SimpleStats.MinTextProperty);
            set => SetValue(SimpleStats.MinTextProperty, value);
        }

        public double MinValueIrrigated
        {
            get => (double)GetValue(SimpleStats.MinValueIrrigatedProperty);
            set => SetValue(SimpleStats.MinValueIrrigatedProperty, value);
        }

        public double MinValueRainfed
        {
            get => (double)GetValue(SimpleStats.MinValueRainfedProperty);
            set => SetValue(SimpleStats.MinValueRainfedProperty, value);
        }

        public string Unit
        {
            get => (string)GetValue(SimpleStats.UnitProperty);
            set => SetValue(SimpleStats.UnitProperty, value);
        }

        public ViewModes ViewMode
        {
            get => this.viewMode;
            set
            {
                Style activeButtonStyle = (Style)Application.Current.Resources["ButtonGreen"];

                this.viewMode = value;
                switch (value)
                {
                    case ViewModes.Irrigated:
                        this.lblMinValueIrrigated.IsVisible = true;
                        this.lblAverageValueIrrigated.IsVisible = true;
                        this.lblMaxValueIrrigated.IsVisible = true;

                        this.lblMinValueRainfed.IsVisible = false;
                        this.lblAverageValueRainfed.IsVisible = false;
                        this.lblMaxValueRainfed.IsVisible = false;

                        this.BtnNonIrrigated.Style = null;
                        this.BtnIrrigated.Style = activeButtonStyle;
                        break;
                    case ViewModes.Rainfed:
                        this.lblMinValueIrrigated.IsVisible = false;
                        this.lblAverageValueIrrigated.IsVisible = false;
                        this.lblMaxValueIrrigated.IsVisible = false;

                        this.lblMinValueRainfed.IsVisible = true;
                        this.lblAverageValueRainfed.IsVisible = true;
                        this.lblMaxValueRainfed.IsVisible = true;

                        this.BtnNonIrrigated.Style = activeButtonStyle;
                        this.BtnIrrigated.Style = null;
                        break;
                }
            }
        }

        private void BtnIrrigated_OnClicked(object sender, EventArgs e)
        {
            ViewMode = ViewModes.Irrigated;
        }

        private void BtnNonIrrigated_OnClicked(object sender, EventArgs e)
        {
            ViewMode = ViewModes.Rainfed;
        }
    }
}