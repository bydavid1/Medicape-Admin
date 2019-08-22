using Clinic.Clases;
using Clinic.Models;
using Clinic.Validaciones;
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
    public partial class Add_Waiting_List : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public Add_Waiting_List()
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(nombre.Text) || String.IsNullOrWhiteSpace(number.Text))
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
            else if (TextValidator.Ok == false || NumeroValidator.Ok == false)
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
                await MaterialDialog.Instance.AlertAsync(message: "Al paracer algunos campos estan incorrectos",
                                                            title: "Alerta",
                                                            acknowledgementText: "Ok", configuration: alertDialogConfiguration);
            }
            else
            {
                try
            {
                control.ShowLoading("Registrando");
                Lista_Espera empleados = new Lista_Espera
                {
                    nombre = nombre.Text,
                    num_Consultorio = Convert.ToInt32(number.Text)

                };



                HttpClient client = new HttpClient();
                string controlador = "/Api/lista_espera/create.php";
                client.BaseAddress = new Uri(baseurl);

                string json = JsonConvert.SerializeObject(empleados);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(controlador, content);

                if (response.IsSuccessStatusCode)
                {
                    control.ShowAlert("Agregado!!", "Exito", "Ok");
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al agregar!!", "Error", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ocurrio un error" , "Error " + ex, "Ok");
            }
            }
        }
    }
}