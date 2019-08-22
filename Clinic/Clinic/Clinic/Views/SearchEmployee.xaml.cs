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
    public partial class SearchEmployee : ContentPage
    {
        Connection get = new Connection();
        private string baseurl;
        public SearchEmployee()
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = searchBar.Text;
            string send = baseurl + "/Api/empleado/search.php?query=" + query;

            HttpClient client = new HttpClient();
            HttpResponseMessage connect = await client.GetAsync(send);

            if (connect.StatusCode == HttpStatusCode.OK)
            {
                var response = await client.GetStringAsync(send);
                var pacientes = JsonConvert.DeserializeObject<List<Empleados>>(response);
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
                var tapped = (list.SelectedItem as Empleados);

                    Navigation.PushAsync(new AddUser(tapped.idempleado, tapped.email));

            }
        }
    }
}