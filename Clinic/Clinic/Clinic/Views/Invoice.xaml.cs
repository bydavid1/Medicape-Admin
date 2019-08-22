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
    public partial class Invoice : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;

        public Invoice()
        {
            control.ShowLoading("Obteniendo lista de facturas");
            InitializeComponent();
            baseurl = get.BaseUrl;
            bool result = get.TestConnection();

            if (result == true)
            {
                getInvoice();
            }
            else
            {
                control.ShowAlert("No se pudo conectar con el servidor", "Error", "Ok");
            }

            mylist.RefreshCommand = new Command(() =>
            {
                mylist.IsRefreshing = true;
                getInvoice();
                mylist.IsRefreshing = false;
            });
        }

        private async void getInvoice()
        {
            try
            {
                string url = baseurl + "/Api/factura/read.php";

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var facturas  = JsonConvert.DeserializeObject<List<Factura>>(response);
                   
                    mylist.ItemsSource = facturas;
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
            Navigation.PushAsync(new SearchPatient("invoice", 0));
        }

        private void Mylist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e != null)
            {
                var list = (ListView)sender;
                var selection = list.SelectedItem as Factura;

                Navigation.PushModalAsync(new ViewInvoice(selection.idfactura, selection.fecha, selection.hora, selection.nombre, selection.total));
            }
        }
    }
}