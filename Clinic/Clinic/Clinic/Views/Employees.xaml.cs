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
        Connection get = new Connection();
        private string baseurl;
        public Employees ()
		{
            MaterialControls control = new MaterialControls();
            control.ShowLoading("Obteniendo lista de empleados");
            InitializeComponent ();
            options.Items.Add("Buscar por nombre");
            options.Items.Add("Buscar por apellido");
            options.Items.Add("Buscar por DUI");
            baseurl = get.BaseUrl;
            bool result = get.TestConnection();

            if (result == true)
            {
                getEmployees();
            }
            else
            {
                control.ShowAlert("No se pudo conectar con el servidor", "Error", "Ok");
            }

            mylist.RefreshCommand = new Command(() =>
            {
                mylist.IsRefreshing = true;
                getEmployees();
                mylist.IsRefreshing = false;
            });
        }

        private async void getEmployees()
        {
            try
            {
              var  url = baseurl+"/Api/empleado/read.php";

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var citas = JsonConvert.DeserializeObject<List<Empleados>>(response);
                    mylist.ItemsSource = citas;
                }
                else
                {
                    mylist.IsVisible = false;
                    message.IsVisible = true;
                }

            }
            catch (HttpRequestException e)
            {
                await DisplayAlert("error", "" + e, "Ok");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddEmployee());
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

        private async void Query_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                string searched = query.Text;
                string selected = Convert.ToString(options.SelectedItem);
                string op;
                if (selected == "Buscar por nombre")
                {
                    op = "nombres";
                }
                else if (selected == "Buscar por apellido")
                {
                    op = "apellidos";
                }
                else if (selected == "Buscar por dui")
                {
                    op = "dui";
                }
                else
                {
                    op = "nombres";
                }

                string url = baseurl + "/Api/empleado/custom_search.php?query=" + searched + "&op=" + op;

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var emp = JsonConvert.DeserializeObject<List<Empleados>>(response);
                    mylist.ItemsSource = emp;
                    mylist.IsVisible = true;
                    message2.IsVisible = false;
                }
                else
                {
                    mylist.IsVisible = false;
                    message2.IsVisible = true;
                }
            }
            catch (Exception ex)
            {

             await DisplayAlert("Error", "" + ex, "Ok");
            }
        }
    }
}