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
    public partial class AddUser : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public AddUser(int id, string email)
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            id_empleado.Text = Convert.ToString(id);
            correo.Text = email;
            control.ShowSnackBar("Se utilizara el correo ya registrado");
            t_user.Items.Add("archivo");
            t_user.Items.Add("doctor");
            t_user.Items.Add("enfermero");
            t_user.Items.Add("farmacia");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(us.Text) &&
              String.IsNullOrWhiteSpace(pass.Text) &&
              String.IsNullOrWhiteSpace(Convert.ToString(t_user.SelectedItem))
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
            else if (String.IsNullOrWhiteSpace(us.Text))
            {
                control.ShowAlert("Campo Usuario es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(pass.Text))
            {
                control.ShowAlert("Campo Contraseña es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(t_user.SelectedItem)))
            {
                control.ShowAlert("Campo Tipo de usuario es obligatorio!!", "Error", "Ok");

            }
            else
            {
                try
                {
                control.ShowLoading("Registrando");


                Usuario citas = new Usuario
                {
                    user_Name = us.Text,
                    user_Password = pass.Text,
                    email = correo.Text,
                    user_type = Convert.ToString(t_user.SelectedItem)
                };



                HttpClient client = new HttpClient();
                string controlador = "/Api/usuario/create.php";
                client.BaseAddress = new Uri(baseurl);

                string json = JsonConvert.SerializeObject(citas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(controlador, content);

                if (response.IsSuccessStatusCode)
                {
                    control.ShowAlert("Registrado", "Exito", "Ok");
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al registrar", "Error", "Ok");
                }
            }
            catch (Exception ex)
            {
              await  DisplayAlert("Ocurrio un error " + ex, "Error", "Ok");
            }
            }
        }
    }
}