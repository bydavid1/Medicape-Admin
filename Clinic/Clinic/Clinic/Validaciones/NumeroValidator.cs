﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace Clinic.Validaciones
{
    class NumeroValidator : Behavior<MaterialTextField>
    {
        public static bool Ok { get; set; }
        //const string digitosRegEx = @"^[0-9]+$";
        //const string digitosRegEx = @"^\\d{4}-\\d{4}$";
        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
            }
        }

        protected override void OnAttachedTo(MaterialTextField entry)
        {
            entry.TextChanged += TextChanged;
            base.OnAttachedTo(entry);
        }

        // Solo dígitos
        void TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as MaterialTextField;
            var text = entry.Text;

            if (!string.IsNullOrWhiteSpace(Mask))

                if (text.Length == _mask.Length)
                    entry.MaxLength = _mask.Length;

            if ((e.OldTextValue == null) || (e.OldTextValue.Length <= e.NewTextValue.Length))

                for (int i = Mask.Length; i >= text.Length; i--)
                {
                    if (Mask[(text.Length - 1)] != 'X')
                    {
                        text = text.Insert((text.Length - 1), Mask[(text.Length - 1)].ToString());
                    }
                }
            entry.Text = text;
            //bool valido = (Regex.IsMatch(e.NewTextValue, digitosRegEx, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            Regex reg = new Regex("^\\d{4}-\\d{4}$");

            bool valido = reg.IsMatch(e.NewTextValue);
            if (!valido)
            {


                ((MaterialTextField)sender).ErrorText = "*Campo incompleto*";
                ((MaterialTextField)sender).ErrorColor = Color.FromHex("#c62828");
                ((MaterialTextField)sender).HasError = true;
                Ok = false;
            }
            else
            {
                ((MaterialTextField)sender).HasError = false;
                Ok = true;

            }
        }

        protected override void OnDetachingFrom(MaterialTextField entry)
        {
            entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
