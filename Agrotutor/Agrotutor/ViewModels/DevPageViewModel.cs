using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agrotutor.ViewModels
{
    public class DevPageViewModel : BindableBase
    {
        public DevPageViewModel()
        {

        }

        public DelegateCommand Weather => new DelegateCommand(async () => 
        {
            var creds = new UserCredentials{
                Username = Constants.AWhereWeatherAPIUsername,
                Password = Constants.AWhereWeatherAPIPassword
            };

            try
            {
                var forecast = await WeatherAPI.GetForecastAsync(20, -100, creds);

                var history = await WeatherAPI.GetObservationsAsync(20, -100, creds, DateTime.Parse("2018-03-01"), DateTime.Parse("2019-04-01"));
                forecast = await WeatherAPI.GetForecastAsync(20, -100, creds);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        });
    }
}
