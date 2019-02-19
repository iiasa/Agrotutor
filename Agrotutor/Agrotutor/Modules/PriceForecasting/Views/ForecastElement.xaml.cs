using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agrotutor.Modules.PriceForecasting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastElement : Grid
    {

        public static readonly BindableProperty UnitProperty = BindableProperty.Create(
            nameof(Unit),
            typeof(string),
            typeof(ForecastElement),
            string.Empty);

        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(
            nameof(Minimum),
            typeof(double),
            typeof(ForecastElement));

        public static readonly BindableProperty AverageProperty = BindableProperty.Create(
            nameof(Average),
            typeof(double),
            typeof(ForecastElement));

        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(
            nameof(Maximum),
            typeof(double),
            typeof(ForecastElement));

        public static readonly BindableProperty MonthProperty = BindableProperty.Create(
            nameof(Month),
            typeof(double),
            typeof(ForecastElement));

        private int _month;

        public string Unit { get; set; }

        public double Minimum { get; set; }
        public double Average { get; set; }
        public double Maximum { get; set; }

        private string MonthText { get; set; }

        public int Month
        {
            get => _month;
            set
            {
                _month = value;
                MonthText = $"in {value} months";
            }
        }

        public ForecastElement()
        {
            InitializeComponent();
            this.lblUnit1.SetBinding(Label.TextProperty, new Binding(nameof(Unit), source: this));
            this.lblUnit2.SetBinding(Label.TextProperty, new Binding(nameof(Unit), source: this));
            this.lblMin.SetBinding(Label.TextProperty, new Binding(nameof(Minimum), source: this));
            this.lblMax.SetBinding(Label.TextProperty, new Binding(nameof(Maximum), source: this));
            this.lblAverage.SetBinding(Label.TextProperty, new Binding(nameof(Average), source: this));
            this.lblMonths.SetBinding(Label.TextProperty, new Binding(nameof(MonthText), source: this));
        }
    }
}