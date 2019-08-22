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

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewExpedient : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public ViewExpedient(int id)
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            getExpedient(id);
        }

        private async void getExpedient(int id)
        {
            string send = baseurl + "/Api/item_expediente/read_one.php?idconsulta=" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage connect = await client.GetAsync(send);

            if (connect.StatusCode == HttpStatusCode.OK)
            {
                var response = await client.GetStringAsync(send);
                var consultas = JsonConvert.DeserializeObject<Expediente>(response);
                diagnostico.Text = consultas.diagnostico;
                tratamiento.Text = consultas.tratamiento;
                observaciones.Text = consultas.observaciones;
                receta.Text = consultas.receta;
                examen.Text = consultas.descripcion_Exam;
            }
            else
            {
                MaterialControls control = new MaterialControls();
                control.ShowSnackBar("Ocurrio un error al obtener el expediente");
            }
        }
    }
}