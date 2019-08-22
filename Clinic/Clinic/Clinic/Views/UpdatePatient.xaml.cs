using Clinic.Clases;
using Clinic.Models;
using Clinic.Validaciones;
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
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePatient : ContentPage
    {
        private int ids;
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public UpdatePatient(int id)
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            p_estado.Items.Add("Casado");
            p_estado.Items.Add("Soltero");
            p_estado.Items.Add("Viudo");
            p_estado.Items.Add("Divorciado");

            p_sexo.Items.Add("Masculino");
            p_sexo.Items.Add("Femenino");
            ids = id;

            getPersonalInfo();
        }
        private async void getPersonalInfo()
        {
            string send = baseurl + "/Api/paciente/read_one.php?idpaciente=" + ids;
            HttpClient client = new HttpClient();
            HttpResponseMessage connect = await client.GetAsync(send);

            if (connect.StatusCode == HttpStatusCode.OK)
            {
                var response = await client.GetStringAsync(send);
                var paciente = JsonConvert.DeserializeObject<Pacientes>(response);
                p_nombre.Text = paciente.nombres;
                p_apellido.Text = paciente.apellidos;
                p_nac.Date.ToString(paciente.fecha_Nac);
                p_estado.SelectedItem = paciente.estado_Civil;
                p_sexo.SelectedItem = paciente.sexo;
                p_dui.Text = paciente.dui;
                p_direccion.Text = paciente.direccion;
                p_municipio.Text = paciente.municipio;
                p_departamento.Text = paciente.departamento;
                p_telefono.Text = paciente.telefono;
                p_correo.Text = paciente.email;

            }
            else
            {
                control.ShowSnackBar("Ocurrio un error");
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(p_nombre.Text) &&
               String.IsNullOrWhiteSpace(p_apellido.Text) &&
               String.IsNullOrWhiteSpace(Convert.ToString(p_sexo.SelectedItem)) &&
               String.IsNullOrWhiteSpace(Convert.ToString(p_estado.SelectedItem)) &&
               String.IsNullOrWhiteSpace(p_correo.Text) &&
               String.IsNullOrWhiteSpace(p_departamento.Text) &&
               String.IsNullOrWhiteSpace(p_direccion.Text) &&
               String.IsNullOrWhiteSpace(p_telefono.Text) &&
               String.IsNullOrWhiteSpace(p_dui.Text) &&
               String.IsNullOrWhiteSpace(p_municipio.Text))

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

                await MaterialDialog.Instance.AlertAsync(message: "Llene todos los campos",
                                                               title: "Alerta",
                                                               acknowledgementText: "Ok",
                                                               configuration: alertDialogConfiguration);
            }
            else if (String.IsNullOrWhiteSpace(p_nombre.Text))
            {
                control.ShowAlert("Campo Nombre es obligatorio!!", "Error", "Ok");


            }
            else if (String.IsNullOrWhiteSpace(p_apellido.Text))
            {
                control.ShowAlert("Campo Apellido es obligatorio!!", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(p_sexo.SelectedItem)))
            {
                control.ShowAlert("Seleccione un sexo", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(p_estado.SelectedItem)))
            {
                control.ShowAlert("Seleccione un estado", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(p_dui.Text))
            {
                control.ShowAlert("Campo Dui es obligatorio!!", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(p_telefono.Text))
            {
                control.ShowAlert("Campo telefono es obligatorio!!", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(p_correo.Text))
            {
                control.ShowAlert("Campo correo es obligatorio!!", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(p_departamento.Text))
            {
                control.ShowAlert("Campo departamento es obligatorio!!", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(p_municipio.Text))
            {
                control.ShowAlert("Campo Municipio es obligatorio!!", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(p_direccion.Text))
            {
                control.ShowAlert("Campo Direccion es obligatorio!!", "Error", "Ok");
            }
            else if (TextValidator.Ok == false || ValidateEmail.Ok == false || NumeroValidator.Ok == false)
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
                                                             acknowledgementText: "Ok",
                                                             configuration: alertDialogConfiguration);
            }
            else
            {
                try
            {
                MaterialControls control = new MaterialControls();
                control.ShowLoading("Registrando");
                string fecha = p_nac.Date.ToString("yy/MM/dd");
                Pacientes pacientes = new Pacientes
                {
                    idpaciente = ids,
                    nombres = p_nombre.Text,
                    apellidos = p_apellido.Text,
                    fecha_Nac = fecha,
                    sexo = Convert.ToString(p_sexo.SelectedItem),
                    estado_Civil = Convert.ToString(p_estado.SelectedItem),
                    dui = p_dui.Text,
                    telefono = p_telefono.Text,
                    email = p_correo.Text,
                    departamento = p_departamento.Text,
                    municipio = p_municipio.Text,
                    direccion = p_direccion.Text
                };



                HttpClient client = new HttpClient();
                string controlador = "/Api/paciente/update.php";
                client.BaseAddress = new Uri(baseurl);

                string json = JsonConvert.SerializeObject(pacientes);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(controlador, content);

                if (response.IsSuccessStatusCode)
                {
                    control.ShowAlert("Actualizado", "Exito", "Ok");
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al actualizar", "Error", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ocurrio un error " + ex, "Error", "Ok");
            }
            }
        }

        private void P_estado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}