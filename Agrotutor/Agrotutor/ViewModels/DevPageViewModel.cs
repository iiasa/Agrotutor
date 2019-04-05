using Agrotutor.Modules.Weather.Awhere.API;
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
                var history = await WeatherAPI.GetObservationsAsync(20, -100, creds, DateTime.Parse("2018-12-01"), DateTime.Parse("2019-04-01"));
                var normals = await WeatherAPI.GetNormsAsync(20, -100, 03, 01, creds);
            }catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        });
    }
}
