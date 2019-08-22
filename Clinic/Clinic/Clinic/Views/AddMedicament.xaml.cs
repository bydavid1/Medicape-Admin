using Clinic.Clases;
using Clinic.Models;
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
    public partial class AddMedicament : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public AddMedicament()
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(nombre.Text) &&
              String.IsNullOrWhiteSpace(codigo.Text) &&
              String.IsNullOrWhiteSpace(cant.Text) &&
              String.IsNullOrWhiteSpace(price.Text)
              )
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
            else if (String.IsNullOrWhiteSpace(nombre.Text))
            {
                control.ShowAlert("Campo Nombre medicamento es obligatorio!!", "Error", "Ok");


            }
            else if (String.IsNullOrWhiteSpace(price.Text))
            {
                control.ShowAlert("Campo Precio es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(cant.Text))
            {
                control.ShowAlert("Campo cantidad es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(codigo.Text))
            {
                control.ShowAlert("Campo codigo es obligatorio!!", "Error", "Ok");

            }
            else
            {
                try
            {
                MaterialControls control = new MaterialControls();
                control.ShowLoading("Registrando");
                string date = fecha.Date.ToString("dd/MM/yy");
                Medicamentos citas = new Medicamentos
                {
                    nom_Medicamento = nombre.Text,
                    cod_Medicamento = codigo.Text,
                    cantidad = Convert.ToInt32(cant.Text),
                    precio_U = float.Parse(price.Text),
                    fecha_V = date
                };

                HttpClient client = new HttpClient();
                string controlador = "/Api/medicamentos/create.php";
                client.BaseAddress = new Uri(baseurl);

                string json = JsonConvert.SerializeObject(citas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(controlador, content);

                if (response.IsSuccessStatusCode)
                {
                    control.ShowAlert("Registrado!!", "Exito", "Ok");
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al registrar!!", "Error", "Ok");
                }
            
            } catch (Exception ex) {
              await DisplayAlert("Ocurrio un error " + ex, "Error", "Ok");
            }
            }
        }
    }
}