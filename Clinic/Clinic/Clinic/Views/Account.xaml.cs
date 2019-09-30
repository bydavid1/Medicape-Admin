using Clinic.Clases;
using Clinic.Models;
using Newtonsoft.Json;
using Plugin.SecureStorage;
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

                  string  url2 = baseurl + "/Api/empleado/read_one.php?idempleado=" + CrossSecureStorage.Current.GetValue("iduser");
                    HttpClient client2 = new HttpClient();
                    HttpResponseMessage connect2 = await client2.GetAsync(url2);

                    if (connect2.StatusCode == HttpStatusCode.OK)
                    {
                       var response2 = await client2.GetStringAsync(url2);
                        var personal = JsonConvert.DeserializeObject<Empleados>(response2);
                        nombres.Text = personal.nombres;
                        apellidos.Text = personal.apellidos;
                        especialidad.Text = personal.especialidad;
                        //user.Text = username;
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