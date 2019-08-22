using Clinic.Clases;
using Clinic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddInvoice : ContentPage
    {
        private int ids;
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public AddInvoice(int id)
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            ids = id;
        }
        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            try
            {
                MaterialControls control = new MaterialControls();
                control.ShowLoading("Registrando");
                string date = DateTime.Today.ToString("yy/MM/dd");
                string hour = DateTime.Now.ToString("hh:mm");
                Factura factura = new Factura
                {
                    nombre = name.Text,
                    hora = hour,
                    fecha = date,
                    total = float.Parse(total.Text),
                    idpaciente = ids
                };

                HttpClient client = new HttpClient();
                string controlador = "/Api/factura/create.php";
                client.BaseAddress = new Uri(baseurl);

                string json = JsonConvert.SerializeObject(factura);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(controlador, content);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    var result = res.ToString().Replace('"', ' ').Trim();

                    for (int i = 1; i <= 3; i++)
                    {
                        var concept = this.FindByName<Entry>("concepto" + i);
                        var price = this.FindByName<Entry>("precio" + i);
                        var cant = this.FindByName<Entry>("cantidad" + i);
                        var su = this.FindByName<Entry>("sub" + i);

                        string h_c = concept.Text;
                        string h_p = price.Text;
                        string h_ca = cant.Text;
                        string h_s = su.Text;

                        if (!(string.IsNullOrEmpty(h_c)) || !(string.IsNullOrEmpty(h_p)) || !(string.IsNullOrEmpty(h_ca)))
                        {
                            Item_Factura item = new Item_Factura();
                         
                            item.idfactura = Convert.ToInt32(result);
                            item.concepto = h_c;
                            item.precio = float.Parse(h_p);
                            item.cantidad = int.Parse(h_ca);
                            item.total = float.Parse(h_s);
                            

                            json = JsonConvert.SerializeObject(item);
                            content = new StringContent(json, Encoding.UTF8, "application/json");
                            controlador = "/Api/item_factura/create.php";
                            response = await client.PostAsync(controlador, content);
                        }
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        control.ShowAlert("Factura registrada", "Registrado", "Ok");
                    }
                    else
                    {
                        control.ShowAlert("Ocurrio un error al registrar algno de los items", "Error", "Ok");
                    }

                }
                else
                {

                    control.ShowAlert("Ocurrio un error al registrar factura", "Error", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "" + ex, "ok");
            }
        }
    }

}