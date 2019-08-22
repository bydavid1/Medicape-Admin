using Clinic.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTips : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public AddTips()
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(title.Text) && String.IsNullOrWhiteSpace(description.Text))
            {
                var alertDialogConfiguration = new MaterialAlertDialogConfiguration
                {
                    BackgroundColor = Color.FromHex("#c62828"),
                    TitleTextColor = Color.White,
                    MessageTextColor = Color.FromHex("#DEFFFFFF"),
                    TintColor = Color.White,
                    CornerRadius = 8,
                    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
                    ButtonAllCaps = false
                };

                await MaterialDialog.Instance.AlertAsync(message: "Llene los campos",
                                                             title: "Alerta",
                                                             acknowledgementText: "Ok",
                                                             configuration: alertDialogConfiguration);

            }
            else if (String.IsNullOrWhiteSpace(description.Text))
            {
                control.ShowAlert("Campo Descripcion es obligatorio!!", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(title.Text))
            {
                control.ShowAlert("Campo Titulo es obligatorio!!", "Error", "Ok");
            }
            else
            {
                try
            {
                control.ShowLoading("Publicando..");
                Models.Tips citas = new Models.Tips
                {
                    titulo = title.Text,
                    descripcion = description.Text
                };



                HttpClient client = new HttpClient();
                string controlador = "/Api/tips/create.php";
                client.BaseAddress = new Uri(baseurl);

                string json = JsonConvert.SerializeObject(citas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(controlador, content);

                if (response.IsSuccessStatusCode)
                {
                    control.ShowAlert("Publicado!!", "Exito", "Ok");
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al publicar!!", "Error", "Ok");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ocurrio un error " + ex, "Error", "Ok");
            }
            }
        }
    }
}