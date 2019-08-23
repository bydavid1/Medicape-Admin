using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Clases
{
    public class Functions
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        HttpClient client = new HttpClient();

        public Functions()
        {
            baseurl = get.BaseUrl;
        }

        public async void Insert(object objeto, string url)
        {

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

        public async Task<bool> Update(object objeto, string url)
        {
            client.BaseAddress = new Uri(baseurl);

            string json = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List> Read(List<Task> list, string url)
        {
            HttpResponseMessage connect = await client.GetAsync(url);

            if (connect.StatusCode == HttpStatusCode.OK)
            {
                var response = await client.GetStringAsync(url);
                var collection = JsonConvert.DeserializeObject<List>(response);
                return collection;
            }
            else if (connect.StatusCode == HttpStatusCode.NoContent)
            {

            }
        }

        public async Task<bool> Delete(string url)
        {
            client.BaseAddress = new Uri(baseurl);

            var response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
