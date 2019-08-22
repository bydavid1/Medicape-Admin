using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clinic.Clases
{
    public class User
    {
        public static string nombre = "";
        public static string type = "";

        public User()
        {

            if (Application.Current.Properties.ContainsKey("name"))
            {
                var val = Application.Current.Properties["name"];
                nombre = val.ToString();
            }

            if (Application.Current.Properties.ContainsKey("type"))
            {
                var val = Application.Current.Properties["type"];
                type = val.ToString();
            }
        }
        public string getName()
        {
            return nombre;
        }

        public string getType()
        {
            return type;
        }
    }
}
