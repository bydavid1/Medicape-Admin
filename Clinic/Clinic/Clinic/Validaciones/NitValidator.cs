using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace Clinic.Validaciones
{
    public class NitValidator : Behavior<MaterialTextField>
    {
        public static bool Ok { get; set; }
        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
            }
        }
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
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
            Regex reg = new Regex("^\\d{4}-\\d{6}-\\d{3}-\\d{1}");
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

    }
}
