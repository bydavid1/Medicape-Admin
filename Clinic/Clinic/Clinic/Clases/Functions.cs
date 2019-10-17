using Clinic.Models;
using Newtonsoft.Json;
using Plugin.SecureStorage;
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

        public async Task<Response> Insert(object objeto, string controller, bool request = false)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);

            string json = JsonConvert.SerializeObject(objeto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(controller, content);

            if (response.IsSuccessStatusCode)
            {
                if (request == true)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    var result = res.ToString().Replace('"', ' ').Trim();
                    return new Response
                    {
                        IsSuccess = true,
                        Result = result
                    };
                }
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

        public async Task<bool> Update(object objeto, string controller)
        {
            client = new HttpClient();
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
            client = new HttpClient();
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
                    Message = "No se pudo encontrar resultados",
                    IsSuccess = true
                };
            }
            return new Response
            {
                IsSuccess = false,
                Message = "Ocurrio un error"
            };
        }

        public async Task<Response> Delete(string controller)
        {
            client = new HttpClient();
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

