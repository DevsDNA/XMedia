﻿using System;
using Xamarin.Forms;

namespace XMedia.Control
{
    public class CheckBox : StackLayout
    {
        public static readonly BindableProperty CheckedValueProperty =
            BindableProperty.Create(
                nameof(CheckedValue),
                typeof(bool),                
                typeof(CheckBox),
                false,
                BindingMode.TwoWay,
                propertyChanged: OnCheckedPropertyChanged);

        public bool CheckedValue
        {
            get
            {
                var check = GetValue(CheckedValueProperty);
                return (bool)check;
            }
            set
            {
                SetValue(CheckedValueProperty, value);
            }
        }
        
        private Label labelChecked = new Label() { Text = "\u2610", FontSize = 20};
        private Label labelTitle = new Label();

        public CheckBox()
        {
            Padding = new Thickness(0);
            
            Orientation = StackOrientation.Horizontal;

            HorizontalOptions = LayoutOptions.StartAndExpand;

            Children.Add(labelChecked);
            Children.Add(labelTitle);
            
        }
        
        private static void OnCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var checkBox = bindable as CheckBox;
            checkBox.labelChecked.Text = checkBox.labelChecked.Text == "\u2611" ? "\u2610" : "\u2611";
        }
    }
}