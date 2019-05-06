using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Xamarin.Forms;

namespace Agrotutor.Core
{
    public class WelcomeModel
    {
        public string Name { get; set; }

        public string Picture { get; set; }

        public string Label { get; set; }

        public bool ShowGetStarted { get; set; }

        public static IList<WelcomeModel> All { get; set; }

        static WelcomeModel()
        {
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

            All = new ObservableCollection<WelcomeModel> {
                new WelcomeModel {
                    Picture = $"{lang}_1.jpg",
                    Label = "Click on the pre-loaded layers of AgroTutor to find further practical information close to you. Also, explore the options, crop calendar and weather reports available in the main bar"
                },
                new WelcomeModel {
                    Picture = $"{lang}_2.jpg"
                },
                new WelcomeModel {
                    Picture = $"{lang}_3.jpg"
                },
                new WelcomeModel {
                    Picture = $"{lang}_4.jpg"
                },
                new WelcomeModel {
                    Picture = $"{lang}_5.jpg"
                },
                new WelcomeModel {
                    Picture = $"{lang}_6.jpg"
                },
                new WelcomeModel {
                    ShowGetStarted = true
                }
            };
        }
    }
}