using Acr.UserDialogs;
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
	public partial class LoadPage : ContentPage
	{
		public LoadPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            var id = CrossSecureStorage.Current.GetValue("iduser");
            UserDialogs.Instance.ShowLoading("Actualizando datos");
            GetInfo(id);
        }
        private async void GetInfo(string id)
        {
            try
            {
                Connection get = new Connection();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(get.BaseUrl);
                HttpResponseMessage connect = await client.GetAsync("/Api/usuario/read_id.php?iduser=" + id);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync("/Api/usuario/read_id.php?iduser=" + id);
                    var list = JsonConvert.DeserializeObject<Usuario>(response);
                    var permisosUpdated = CrossSecureStorage.Current.SetValue("permisos", Convert.ToString(list.valor));
                    var userUpdated = CrossSecureStorage.Current.SetValue("user", list.user_Name);
                    if (permisosUpdated != true && userUpdated != true)
                    {
                        MaterialControls controls = new MaterialControls();
                        controls.ShowSnackBar("No se pudieron actuaizar los datos desde el servidor");
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await Navigation.PushAsync(new MainPage());
                    }
                }
            }
            catch (Exception ex)
            {

              await  DisplayAlert("Error", "" + ex, "ok");
            }
        }
    }
}