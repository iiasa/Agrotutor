using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Xamarin.Forms;

namespace Agrotutor.Core
{
    public class ColorsDataModel
    {
        public string Name { get; set; }

        public string Picture { get; set; }

        public bool ShowGetStarted { get; set; }

        public static IList<ColorsDataModel> All { get; set; }

        static ColorsDataModel()
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

            All = new ObservableCollection<ColorsDataModel> {
                new ColorsDataModel {
                    Picture = $"{lang}_1.png"
                },
                new ColorsDataModel {
                    Picture = $"{lang}_2.png"
                },
                new ColorsDataModel {
                    Picture = $"{lang}_3.png"
                },
                new ColorsDataModel {
                    Picture = $"{lang}_4.png"
                },
                new ColorsDataModel {
                    Picture = $"{lang}_5.png"
                },
                new ColorsDataModel {
                    Picture = $"{lang}_6.png"
                },
                new ColorsDataModel {
                    ShowGetStarted = true
                }
            };
        }
    }
}