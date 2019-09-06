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
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Clinic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class addQuotes : ContentPage
	{
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        User name = new User();
        private string baseurl;
        public addQuotes (string nombre, string apellido,int id)
		{
			InitializeComponent ();
            BindingContext = new AddQuotesViewModel(nombre, apellido, id);
            baseurl = get.BaseUrl;
            idd.Text = Convert.ToString(id);
            fecha.MinimumDate = DateTime.Today;
        }

       
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(consultorio.Text)
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
            else if (String.IsNullOrWhiteSpace(consultorio.Text))
            {
                MaterialControls control = new MaterialControls();
                control.ShowAlert("Campo consultorio es obligatorio!!", "Error", "Ok");

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



                   
                        string url3 = baseurl + "/Api/empleado/read_one.php?idempleado=" + id;

                        HttpClient client3 = new HttpClient();
                        HttpResponseMessage connect3 = await client3.GetAsync(url3);

                        if (connect3.StatusCode == HttpStatusCode.OK)
                        {
                            var response3 = await client3.GetStringAsync(url3);
                            var emp = JsonConvert.DeserializeObject<Empleados>(response3);
                            var nom = emp.nombres;
                            var apell = emp.apellidos;

                            var hour = Convert.ToString(hora.Time);
                            Citas citas = new Citas
                            {
                                fecha_Cita = fecha.Date.ToString("yy/MM/dd"),
                                hora_Cita = hour,
                                nombre_Paciente = p_nombre.Text,
                                apellido_Paciente = p_apellido.Text,
                                num_Consultorio = Convert.ToInt32(consultorio.Text),
                                nombre_Doctor = (nom + " " + apell),
                                idpaciente = Convert.ToInt32(idd.Text),
                                idempleado = id
                            };



                            HttpClient client = new HttpClient();

                            string controlador = "/Api/citas/create.php";
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
                                control.ShowAlert("Ocurrio un error al registrar", "Error", "Ok");
                            }
                        }
                          
                    }

            }
            catch (Exception ex)
            {
              await  DisplayAlert("Ocurrio un error", "Error: " + ex, "Ok");
            }
            }
        }
    }
}