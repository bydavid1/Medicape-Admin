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
	public partial class Account : ContentPage
	{
        Connection get = new Connection();
        User name = new User();
        private string baseurl;
        public Account ()
		{
            baseurl = get.BaseUrl;
			InitializeComponent ();
            getInfo();
        }

        private async void getInfo()
        {
            try
            {
                string username = name.getName();
                string url = baseurl + "/Api/usuario/read_id.php?username="+username;

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var info = JsonConvert.DeserializeObject<Usuario>(response);
                    var id = info.reference;
                    var type = info.user_type;

                    if (type != "doctor")
                    {
                        container.IsVisible = false;
                    }

                  string  url2 = baseurl + "/Api/empleado/read_one.php?idempleado=" + id;
                    HttpClient client2 = new HttpClient();
                    HttpResponseMessage connect2 = await client2.GetAsync(url2);

                    if (connect2.StatusCode == HttpStatusCode.OK)
                    {
                       var response2 = await client2.GetStringAsync(url2);
                        var personal = JsonConvert.DeserializeObject<Empleados>(response2);
                        nombres.Text = personal.nombres;
                        apellidos.Text = personal.apellidos;
                        especialidad.Text = personal.especialidad;
                        user.Text = username;
                    }
                }
                else
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Edit_Schedule());
        }
    }
}