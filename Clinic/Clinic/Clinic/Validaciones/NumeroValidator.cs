using System;
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
        const string digitosRegEx = @"^[0-9]+$";

        protected override void OnAttachedTo(MaterialTextField entry)
        {
            entry.TextChanged += TextChanged;
            base.OnAttachedTo(entry);
        }

        // Solo dígitos
        void TextChanged(object sender, TextChangedEventArgs e)
        {
            bool valido = (Regex.IsMatch(e.NewTextValue, digitosRegEx, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            if (!valido)
            {
                ((MaterialTextField)sender).ErrorText = "*Campo Vacio*";
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
