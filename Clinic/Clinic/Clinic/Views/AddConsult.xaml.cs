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
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddConsult : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        User name = new User();
        private string baseurl;

        public AddConsult(string nombre, string apellido, int id)
        {
            InitializeComponent();
            p_nombre.Text = nombre;
            p_apellido.Text = apellido;
            id_paciente.Text = Convert.ToString(id);
            baseurl = get.BaseUrl;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(doctor.Text) &&
                String.IsNullOrWhiteSpace(consultorio.Text) &&
               String.IsNullOrWhiteSpace(diag.Text) &&
               String.IsNullOrWhiteSpace(trat.Text) &&
               String.IsNullOrWhiteSpace(rec.Text) &&
               String.IsNullOrWhiteSpace(obs.Text)
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
            else if (String.IsNullOrWhiteSpace(doctor.Text))
            {
                control.ShowAlert("Campo doctor es obligatorio!!", "Error", "Ok");
            }
            else if (String.IsNullOrWhiteSpace(consultorio.Text))
            {
                control.ShowAlert("Campo consultorio es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(diag.Text))
            {
                control.ShowAlert("Campo diagnostico es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(trat.Text))
            {
                control.ShowAlert("Campo tratamiento es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(rec.Text))
            {
                control.ShowAlert("Campo receta es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(obs.Text))
            {
                control.ShowAlert("Campo observaciones es obligatorio!!", "Error", "Ok");

            }
            else
            {
                try
            {
                control.ShowLoading("Registrando");
                    string username = name.getName();
                    string url2 = baseurl + "/Api/usuario/read_id.php?username=" + username;

                    HttpClient client2 = new HttpClient();
                    HttpResponseMessage connect2 = await client2.GetAsync(url2);

                    if (connect2.StatusCode == HttpStatusCode.OK)
                    {
                        var response2 = await client2.GetStringAsync(url2);
                        var info = JsonConvert.DeserializeObject<Usuario>(response2);
                        var id = info.reference;

                        string date = DateTime.Today.ToString("yy/MM/dd");
                        string hour = DateTime.Now.ToString("hh:mm");
                        Consultas consultas = new Consultas
                        {
                            nombres = p_nombre.Text,
                            apellidos = p_apellido.Text,
                            fecha = date,
                            hora = hour,
                            num_Consultorio = Convert.ToInt32(consultorio.Text),
                            nom_Doctor = doctor.Text,
                            idpaciente = Convert.ToInt32(id_paciente.Text),
                            idempleado = id
                        };



                        HttpClient client = new HttpClient();
                        string controlador = "/Api/consultas/create.php";
                        client.BaseAddress = new Uri(baseurl);

                        string json = JsonConvert.SerializeObject(consultas);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(controlador, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string res = await response.Content.ReadAsStringAsync();
                            var result = res.ToString().Replace('"', ' ').Trim();
                            Expediente expediente = new Expediente
                            {
                                diagnostico = diag.Text,
                                tratamiento = trat.Text,
                                receta = rec.Text,
                                observaciones = obs.Text,
                                descripcion_Exam = exam.Text,
                                idpaciente = Convert.ToInt32(id_paciente.Text),
                                idconsulta = Convert.ToInt32(result)
                            };

                            controlador = "/Api/item_expediente/create.php";
                            client.BaseAddress = new Uri(baseurl);

                            json = JsonConvert.SerializeObject(expediente);
                            content = new StringContent(json, Encoding.UTF8, "application/json");

                            response = await client.PostAsync(controlador, content);

                            if (response.IsSuccessStatusCode)
                            {
                                control.ShowAlert("Guardado", "Exito", "Ok");
                                diag.Text = "";
                                trat.Text = "";
                                rec.Text = "";
                                obs.Text = "";
                                exam.Text = "";
                                p_nombre.Text = "";
                                p_apellido.Text = "";
                            }
                            else
                            {
                                control.ShowAlert("Ocurrio un error al registrar el expediente", "Error", "Ok");
                            }
                        }
                        else
                        {
                            control.ShowAlert("Ocurrio un error al registrar la consulta", "Error", "Ok");
                        }
                    }
            }
            catch (Exception ex)
            {
              await  DisplayAlert("Ocurrio un error" + ex, "Error", "Ok");
            }
            }
        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(id_paciente.Text);
            Navigation.PushAsync(new AddVitalSigns(id));
        }
    }
}