using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using CimmytApp.DTO;

namespace CimmytApp.AgronomicalRecommendations.ViewModels
{
    public class LocalAgronomicalRecommendationsPageViewModel : BindableBase
    {
        public Parcel Parcel { get; set; }

        public LocalAgronomicalRecommendationsPageViewModel()
        {
        }
    }
}