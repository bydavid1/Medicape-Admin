using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Clinic.Clases
{
    public class Functions
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;

        public Functions()
        {
            baseurl = get.BaseUrl;
        }

        public async void Insert(object objeto, string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);

            string json = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                control.ShowAlert("Agregado!", "Exito", "Ok");
            }
            else
            {
                control.ShowAlert("Ocurrio un error al agregar!!", "Error", "Ok");
            }
        }
    }
}
