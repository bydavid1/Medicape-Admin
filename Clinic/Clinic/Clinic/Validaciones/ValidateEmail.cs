using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace Clinic.Validaciones
{
    public class ValidateEmail : Behavior<MaterialTextField>
    {
        public static bool Ok { get; set; }

        const string emailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

        protected override void OnAttachedTo(MaterialTextField entry)
        {
            entry.TextChanged += TextChanged;
            base.OnAttachedTo(entry);
        }

        // Valida si el texto introducido es un correo electrónico
        void TextChanged(object sender, TextChangedEventArgs e)
        {
            bool valido = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));

            if (!valido)
            {
                ((MaterialTextField)sender).ErrorText = "*Correo Invalido*";
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
