using Clinic.Clases;
using Clinic.Models;
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
    public partial class SearchPatient : ContentPage
    {
        private int ids;
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public SearchPatient(string origin, int id)
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            ids = id;
            origen.Text = origin;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = searchBar.Text;
            string send = baseurl + "/Api/paciente/search.php?query=" + query;

            HttpClient client = new HttpClient();
            HttpResponseMessage connect = await client.GetAsync(send);

            if (connect.StatusCode == HttpStatusCode.OK)
            {
                var response = await client.GetStringAsync(send);
                var pacientes = JsonConvert.DeserializeObject<List<Pacientes>>(response);
                searchlist.IsVisible = true;
                message.IsVisible = false;
                searchlist.ItemsSource = pacientes;
            }
            else
            {
                searchlist.IsVisible = false;
                message.IsVisible = true;
            }
        }

        private void Searchlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            else
            {
                var list = (ListView)sender;
                var tapped = (list.SelectedItem as Pacientes);
                if (origen.Text == "quotes")
                {
                    Navigation.PushAsync(new addQuotes(tapped.nombres, tapped.apellidos, tapped.idpaciente));
                }
                else if (origen.Text == "consults")
                {
                    Navigation.PushAsync(new AddConsult(tapped.nombres, tapped.apellidos, tapped.idpaciente));
                }
                else if (origen.Text == "lists")
                {
                    control.ShowLoading("Registrando");
                    Lista_Item_Espera empleados = new Lista_Item_Espera
                    {
                        nombre = tapped.nombres,
                        apellido = tapped.apellidos,
                        idpaciente = tapped.idpaciente,
                        idlista = ids
                    };
                    Functions create = new Functions();
                    create.Insert(empleados, "/Api/item_espera/create.php");

                }
                else if (origen.Text == "invoice")
                {
                    Navigation.PushAsync(new AddInvoice(tapped.idpaciente));
                }
            }
               
            }
    }
}