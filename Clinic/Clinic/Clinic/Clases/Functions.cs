using Clinic.Models;
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
        Response Response;
        public Functions()
        {
            baseurl = get.BaseUrl;
            Response = new Response();
        }

        public async void Insert(object objeto, string controller)
        {

            client.BaseAddress = new Uri(baseurl);

            string json = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(controller, content);

            if (response.IsSuccessStatusCode)
            {
                control.ShowAlert("Agregado!", "Exito", "Ok");
            }
            else
            {
                control.ShowAlert("Ocurrio un error al agregar!!", "Error", "Ok");
            }
        }

        public async Task<bool> Update(object objeto, string controller)
        {
            client.BaseAddress = new Uri(baseurl);

            string json = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(controller, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Response> Read<T>(string controller)
        {
            client.BaseAddress = new Uri(baseurl);
            HttpResponseMessage connect = await client.GetAsync(controller);

            if (connect.StatusCode == HttpStatusCode.OK)
            {
                var response = await client.GetStringAsync(controller);
                var list = JsonConvert.DeserializeObject<List<T>>(response);
                return new Response
                {
                    Result = list,
                    IsSuccess = true
                };
            }
            else if (connect.StatusCode == HttpStatusCode.NoContent)
            {
                return new Response
                {
                    Message = "Sin contenido",
                    IsSuccess = true
                };
            }
            return new Response
            {
                IsSuccess = false,
                Message = "Ocurrio un error"
            };
        }

        public async Task<Response> GetCurrentId(string id)
        {
            client.BaseAddress = new Uri(baseurl);
            var controller = "/Api/usuario/read_id.php?username="+id;
            HttpResponseMessage connect = await client.GetAsync(controller);

            if (connect.StatusCode == HttpStatusCode.OK)
            {
                var response = await client.GetStringAsync(controller);
                var info = JsonConvert.DeserializeObject<Usuario>(response);
                var res = Convert.ToString(info.reference);

                return new Response
                {
                    Result = res,
                    IsSuccess = true
                };
            }

            return new Response
            {
                IsSuccess = false,
                Message = "No se pudo obtener el id"
            };
        }

        public async Task<Response> Delete(string controller)
        {
            client.BaseAddress = new Uri(baseurl);

            var response = await client.DeleteAsync(controller);

            if (response.IsSuccessStatusCode)
            {
                return new Response
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false
                };
            }
        }


    }
}

