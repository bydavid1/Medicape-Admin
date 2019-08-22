using Clinic.Behavior;
using Clinic.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clinic.Clases
{
    public class LoginInterface : CarouselPage
    {
        ContentPage login;

        public LoginInterface(ILoginManager ilm)
        {
            login = new Login(ilm);
            this.Children.Add(login);
            MessagingCenter.Subscribe<ContentPage>(this, "Login", (sender) =>
            {
                this.SelectedItem = login;
            });
        }
    }
}
