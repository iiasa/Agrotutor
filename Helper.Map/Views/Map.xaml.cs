using System;
using System.Collections.Generic;
using Helper.Map.ViewModels;
using Xamarin.Forms;

namespace Helper.Map.Views
{
    public partial class Map : ContentPage
    {
        private MapViewModel _bindingContext;

        public List<Pin> MapPins { get; set; }

        public Map()
        {
            InitializeComponent();
            _bindingContext = (MapViewModel)BindingContext;
            _bindingContext.SetViewReference(this);
        }


    }
}
