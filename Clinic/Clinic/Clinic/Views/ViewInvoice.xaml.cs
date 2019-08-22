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
    public partial class ViewInvoice : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public ViewInvoice(int id, string fecha, string hora, string nombre, float total)
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            getInvoice(id, fecha, hora, nombre, total);
        }
        private async void getInvoice(int id, string fecha, string hora, string nombre, float total)
        {
            try
            {
                string send = baseurl + "/Api/item_factura/read.php?idfactura=" + id;
                var totaly = "$" + total;
                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(send);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(send);
                    var factura = JsonConvert.DeserializeObject<List<Item_Factura>>(response);
                    mylist.ItemsSource = factura;
                    date.Text = fecha;
                    name.Text = nombre;
                    hour.Text = hora;
                    tot.Text = totaly;
                }
                else
                {
                    MaterialControls control = new MaterialControls();
                    control.ShowSnackBar("Ocurrio un error al recuperar la informacion");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error: " + ex, "Ok");

            }
        }
    }
}