using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace Clinic.Validaciones
{
    public class TextValidator : Behavior<MaterialTextField>
    {
        public static bool Ok { get; set; }
        protected override void OnAttachedTo(MaterialTextField entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(MaterialTextField entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            Regex reg = new Regex("[0-9]");
            bool isValid = reg.IsMatch(args.NewTextValue);

            if (!isValid)
            {
                ((MaterialTextField)sender).HasError = false;
                Ok = true;

            }
            else
            {
                ((MaterialTextField)sender).ErrorText = "*No se permiten numeros*";
                ((MaterialTextField)sender).ErrorColor = Color.FromHex("#c62828");
                ((MaterialTextField)sender).HasError = true;

                Ok = false;


            }
        }
    }
}
