﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using CimmytApp.DTO;

namespace CimmytApp.ViewModels
{
    public class AddParcelPageViewModel : BindableBase
    {
        private List<string> agriculturalCycles = new List<string> { "Spring-Summer", "Autumn-Winter" };
        public List<string> AgriculturalCycles => agriculturalCycles;

        public bool Test { get; set; }

        public ICommand AgriculturalCycleChangedCommand;
        public ICommand Moo;

        public Parcel Parcel { get; set; }

        public AddParcelPageViewModel()
        {
            AgriculturalCycleChangedCommand = new DelegateCommand(AgriculturalCycleChanged);
            Moo = new DelegateCommand(Mooo);
        }

        public void Mooo()
        {
            Debug.WriteLine("We here...");
            int i = 0;
            i++;
        }

        private void AgriculturalCycleChanged()
        {
        }
    }
}