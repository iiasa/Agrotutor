﻿using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Helper.UI
{
    public class LocalizedPicker : Picker
    {
        public Picker Picker { get; private set; }
        public BindableProperty LocalizedItems { get; }
        //public Dictionary<string, string> Items { get; set; }

        public LocalizedPicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
        }

        private static void OnItemsSourceChanged(BindableObject bindable, IEnumerable oldvalue, IEnumerable newvalue)
        {
            int i = 0;
            i++;
        }
    }
}