using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using CimmytApp.DTO;

namespace CimmytApp.ViewModels
{
    public class AddParcelPageViewModel : BindableBase
    {
        public Dictionary<string, string> AgriculturalCycles { get; private set; }
        public Parcel Parcel { get; set; }

        public AddParcelPageViewModel()
        {
            AgriculturalCycles = new Dictionary<string, string>
            {
                /* {"agricultural_cycle_1", Resx.AppResources.agricultural_cycle_1 },
                 {"agricultural_cycle_2", Resx.AppResources.agricultural_cycle_2 }*/
            };
        }
    }
}