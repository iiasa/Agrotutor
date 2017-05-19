using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using CimmytApp.DTO;

namespace CimmytApp.ViewModels
{
    public class ParcelPageViewModel : BindableBase
    {
        public Parcel Parcel { get; private set; }

        public ParcelPageViewModel()
        {
        }
    }
}