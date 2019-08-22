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
	public partial class Home : ContentPage
	{
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        private string url;
        public Home ()
		{

            control.ShowLoading("Obteniendo datos...");
            InitializeComponent ();
            baseurl = get.BaseUrl;
            bool result = get.TestConnection();

            if (result == true)
            {
                getQuotes();
            }
            else
            {
                control.ShowAlert("No se pudo conectar con el servidor", "Error", "Ok");
            }
        }


        private async void getQuotes()
        {
            try
            {
                url = baseurl+"/Api/citas/read_date.php";

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var citas = JsonConvert.DeserializeObject<List<Citas>>(response);
                    mylist.ItemsSource = citas;
                }
                else
                {
                    mylist.IsVisible = false;
                    message.IsVisible = true;
                }
                
            }
            catch (Exception e)
            {
               await DisplayAlert("error", ""+e, "Ok");
            }
        }

        void OnClick(object sender, EventArgs e)
        {
            ToolbarItem tbi = (ToolbarItem)sender;
            if (tbi.Text == "Cerrar Sesion")
            {
                MaterialControls control = new MaterialControls();
                control.ShowLoading("Cerrando sesion...");
                App.Current.Logout();
            }
            else if (tbi.Text == "Acerca de")
            {
                Navigation.PushModalAsync(new Info());
            }
            else if (tbi.Text == "Perfil")
            {
                Navigation.PushAsync(new Account());
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pending_Quotes());
        }
    }
}