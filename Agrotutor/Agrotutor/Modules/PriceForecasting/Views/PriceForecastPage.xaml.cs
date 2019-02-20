using System;
using Agrotutor.Modules.PriceForecasting.ViewModels;
using Xamarin.Forms;

namespace Agrotutor.Modules.PriceForecasting.Views
{
    public partial class PriceForecastPage : ContentPage
    {
        private PriceForecastPageViewModel viewModel;

        public PriceForecastPage()
        {
            InitializeComponent();
            this.viewModel = (PriceForecastPageViewModel)BindingContext;
            this.viewModel.PropertyChanged += (sender, args) =>
            {
                if (!args.PropertyName.Equals(nameof(this.viewModel.FurtherItems))) return;

                UpdateForecastItems();
            };
        }

        private void UpdateForecastItems()
        {
            this.ForecastStack.Children.Clear();

            foreach (var item in viewModel.FurtherItems)
            {
                try
                {
                    var headerLabel = new Label{Text = $"in {item.ID} months", FontSize = 18, TextColor = Color.ForestGreen};
                    var expectedLabel = new Label {Text = $"{item.Price} ($/kg)"};
                    var rangeLabel = new Label { Text = $"{item.MinPrice} to {item.MaxPrice} ($/kg)"};
                    var layout = new StackLayout();

                    layout.Children.Add(headerLabel);
                    layout.Children.Add(expectedLabel);
                    layout.Children.Add(rangeLabel);


                    ForecastStack.Children.Add(layout);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
