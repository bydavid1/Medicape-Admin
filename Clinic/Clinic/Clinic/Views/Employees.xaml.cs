using Clinic.Clases;
using Clinic.Models;
using Clinic.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Employees : ContentPage
	{
        public Employees ()
		{
            InitializeComponent ();
            BindingContext = new EmployeesViewModel();
        }

        private void Mylist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            else
            {
                var list = (ListView)sender;
                var tapped = (list.SelectedItem as Empleados);

                Navigation.PushAsync(new ViewEmployee(tapped.idempleado));
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddEmployee());
        }
    }
}