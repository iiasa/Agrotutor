﻿using System;
using Xamarin.Forms;

namespace Helper.Datatypes
{
    public class NullableDatePicker : DatePicker
    {
        private string _format = null;
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), null);

        public DateTime? NullableDate
        {
            get => (DateTime?)GetValue(NullableDateProperty);
            set { SetValue(NullableDateProperty, value); UpdateDate(); }
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue) { if (null != _format) Format = _format; Date = NullableDate.Value; }
            else { _format = Format; Format = "pick ..."; }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Date") NullableDate = Date;
        }
    }
}