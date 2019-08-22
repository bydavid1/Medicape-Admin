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

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddVitalSigns : ContentPage
    {
        Connection get = new Connection();
        private string baseurl;
        public AddVitalSigns(int id)
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            id_paciente.Text = Convert.ToString(id);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                MaterialControls control = new MaterialControls();
                control.ShowLoading("Registrando...");
                string date = DateTime.Today.ToString("yy/MM/dd");
                string hour = DateTime.Now.ToString("hh:mm");
                VitalSigns consultas = new VitalSigns
                {
                    altura = float.Parse(alt.Text),
                    peso = float.Parse(pes.Text),
                    fecha = date,
                    temperatura = float.Parse(temp.Text),
                    frec_Cardiaca = float.Parse(frec.Text),
                    presion = float.Parse(pres.Text),
                    pulso = float.Parse(pul.Text),
                    idpaciente = Convert.ToInt32(id_paciente.Text)
                };



                HttpClient client = new HttpClient();
                string controlador = "/Api/perfil_paciente/create.php";
                client.BaseAddress = new Uri(baseurl);

                string json = JsonConvert.SerializeObject(consultas);
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
              await DisplayAlert("Ocurrio un error" + ex, "Error", "Ok");
            }
        }
    }
}