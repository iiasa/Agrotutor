using System;
using System.Collections.Generic;
using System.Threading;
using Agrotutor.Core.Localization;
using Agrotutor.Modules.PriceForecasting.ViewModels;
using Microsoft.Extensions.Localization;
using Xamarin.Forms;

namespace Agrotutor.Modules.PriceForecasting.Views
{
    public partial class PriceForecastPage : ContentPage
    {
        private PriceForecastPageViewModel viewModel;

        public PriceForecastPage()
        {
            InitializeComponent();
            viewModel = (PriceForecastPageViewModel)BindingContext;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (!args.PropertyName.Equals(nameof(viewModel.FurtherItems))) return;

                UpdateForecastItems();
            };
        }

        private void UpdateForecastItems()
        {
            ForecastStack.Children.Clear();

            var supportedLang = new List<string>
            {
                "en", "es"
            };
            var lang = "en";
            try
            {
                var currentLang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                if (supportedLang.Contains(currentLang))
                {
                    lang = currentLang;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var str1 = (lang == "es") ? "en" : "in";
            var str2 = (lang == "es") ? "meses" : "months";
            
            foreach (var item in viewModel.FurtherItems)
            {
                try
                {
                    var headerLabel = new Label{Text = $"{str1} {item.Month} {str2}", FontSize = 18, TextColor = Color.ForestGreen, Margin = new Thickness(0,0,0,6)};
                    var expectedLabel = new Label {Text = $"{item.RoundedPrice}", Margin = new Thickness(0,0,0,23)};
                    var rangeLabel = new Label { Text = $"{item.RoundedMinPrice} to {item.RoundedMaxPrice}"};
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
