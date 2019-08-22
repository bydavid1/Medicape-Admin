using Clinic.Clases;
using Clinic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ViewPatient : TabbedPage
    {
        private int ids;
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public ViewPatient(int id)
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            ids = id;
            getPersonalInfo();
            getExpedient();
            getVital();
        }

        private async void getVital()
        {
            string send = baseurl + "/Api/perfil_paciente/read_one.php?idpaciente=" + ids;
            HttpClient client = new HttpClient();
            HttpResponseMessage connect = await client.GetAsync(send);

            if (connect.StatusCode == HttpStatusCode.OK)
            {
                var response = await client.GetStringAsync(send);
                var perfil = JsonConvert.DeserializeObject<VitalSigns>(response);
                height.Text = Convert.ToString(perfil.altura);
                weight.Text = Convert.ToString(perfil.peso);
                temp.Text = Convert.ToString(perfil.temperatura);
                b_pres.Text = Convert.ToString(perfil.presion);
                fc.Text = Convert.ToString(perfil.frec_Cardiaca);
            }
            else
            {
                control.ShowSnackBar("Este paciente no tiene registros completos");
            }
        }

        private async void getExpedient()
        {
            string send = baseurl + "/Api/consultas/custom_read.php?idpaciente=" + ids;
            HttpClient client = new HttpClient();
            HttpResponseMessage connect = await client.GetAsync(send);

            if (connect.StatusCode == HttpStatusCode.OK)
            {
                var response = await client.GetStringAsync(send);
                var consultas = JsonConvert.DeserializeObject<List<Consultas>>(response);
                mylist.ItemsSource = consultas;
            }
            else
            {
                control.ShowSnackBar("Este paciente no tiene registros completos");
            }
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
                _name.Text = (paciente.nombres + " " + paciente.apellidos);
                _fecha.Text = paciente.fecha_Nac;
                _sexo.Text = paciente.sexo;
                _dui.Text = paciente.dui;
                _direccion.Text = paciente.direccion;
                _municipio.Text = paciente.municipio;
                _departamento.Text = paciente.departamento;
                _telefono.Text = paciente.telefono;
                _correo.Text = paciente.email;
                _estado.Text = paciente.estado_Civil;
            }
            else
            {
                
                control.ShowSnackBar("Este paciente no tiene registros completos");
            }
        }

        private void Mylist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            else
            {
                var list = (ListView)sender;
                var tapped = (list.SelectedItem as Consultas);

                Navigation.PushAsync(new ViewExpedient(tapped.idconsulta));
            }
        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdatePatient(ids));
        }

        private void ToolbarItem_Activated_1(object sender, EventArgs e)
        {
            control.ShowLoading("Actualizando...");
            getPersonalInfo();
            getExpedient();
            getVital();
        }
    }
}