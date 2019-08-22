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
	public partial class Patients : ContentPage
	{
        Connection get = new Connection();
        private string baseurl;
        public Patients ()
		{
            MaterialControls control = new MaterialControls();
            control.ShowLoading("Obteniendo lista de pacientes");
            InitializeComponent ();
            options.Items.Add("Buscar por nombre");
            options.Items.Add("Buscar por apellido");
            options.Items.Add("Buscar por DUI");
            baseurl = get.BaseUrl;
            bool result = get.TestConnection();

            if (result == true)
            {
                getPatients();
            }
            else
            {
                control.ShowAlert("No se pudo conectar con el servidor", "Error", "Ok");
            }

            mylist.RefreshCommand = new Command(() =>
            {
                mylist.IsRefreshing = true;
                getPatients();
                mylist.IsRefreshing = false;
            });
        }

        private async void getPatients()
        {
            try
            {
                string url = baseurl+"/Api/paciente/read.php";

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var citas = JsonConvert.DeserializeObject<List<Pacientes>>(response);
                    message.IsVisible = false;
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
            Navigation.PushAsync(new AddPatients());
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
                var tapped = (list.SelectedItem as Pacientes);

                Navigation.PushAsync(new ViewPatient(tapped.idpaciente));
            }
        }

        private async void Query_Search(object sender, EventArgs e)
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

                string url = baseurl + "/Api/paciente/custom_search.php?query=" + searched + "&op=" + op;

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var citas = JsonConvert.DeserializeObject<List<Pacientes>>(response);
                    message2.IsVisible = false;
                    mylist.IsVisible  = true;
                    mylist.ItemsSource = citas;
                }
                else
                {
                    mylist.IsVisible = false;
                    message2.IsVisible = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}