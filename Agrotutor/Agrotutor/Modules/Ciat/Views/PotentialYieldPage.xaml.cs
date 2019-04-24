using Xamarin.Forms;

namespace Agrotutor.Modules.Ciat.Views
{
    public partial class PotentialYieldPage : ContentPage
    {

        private readonly Color ButtonActiveColor = (Color)App.Current.Resources["PrimaryGreen"];
        private readonly Color ButtonInactiveColor = (Color)App.Current.Resources["PrimaryYellow"];

        public PotentialYieldPage()
        {
            InitializeComponent();
            this.BtnIrrigated.Clicked += (sender, e) =>
            {
                this.BtnIrrigated.BackgroundColor = ButtonActiveColor;
                this.BtnNonIrrigated.BackgroundColor = ButtonInactiveColor;
            };
            this.BtnNonIrrigated.Clicked += (sender, e) =>
            {
                this.BtnNonIrrigated.BackgroundColor = ButtonActiveColor;
                this.BtnIrrigated.BackgroundColor = ButtonInactiveColor;
            };
            this.BtnNonIrrigated.BackgroundColor = ButtonActiveColor;
        }
    }
}
