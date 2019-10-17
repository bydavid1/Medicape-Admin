using Clinic.Behavior;
using Clinic.Clases;
using Clinic.Models;
using Clinic.ViewModels;
using Newtonsoft.Json;
using Plugin.SecureStorage;
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
    public partial class Login : ContentPage
    {
        public static string Username = "";
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public Login()
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            control.ShowLoading("Iniciando sesion...");
            var usuario = user.Text;
            var contraseña = password.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                //message.IsVisible = true;
            }
            else
            {
                bool inf = get.TestConnection();
                if (inf == true)
                {

                    Usuario users = new Usuario()
                    {
                        user_Name = usuario,
                        user_Password = contraseña
                    };

                    HttpClient client = new HttpClient();

                    string controlador = "/Api/usuario/auth_admin.php";
                    client.BaseAddress = new Uri(baseurl);

                    string json = JsonConvert.SerializeObject(users);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(controlador, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Usuario>(res);
                        var iduser = Convert.ToString(result.iduser);
                        var permisos = Convert.ToString(result.valor);
                        var userName = Convert.ToString(result.user_Name);
                        var idespecialidad = Convert.ToString(result.idespecialidad);
                        var sessionCreated =  CrossSecureStorage.Current.SetValue("SessionActive", "true");
                        var idCreated = CrossSecureStorage.Current.SetValue("iduser", iduser);
                        var permisosCreated = CrossSecureStorage.Current.SetValue("permisos", permisos);
                        var userCreated = CrossSecureStorage.Current.SetValue("user", userName);
                        var idespcreated = CrossSecureStorage.Current.SetValue("idespecialidad", idespecialidad);
                        var sessionToken = CrossSecureStorage.Current.GetValue("SessionActive");
                        if (sessionToken == "true" && sessionCreated == true && idCreated == true && permisosCreated == true && userCreated == true && idespcreated == true)
                        {
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            control.ShowAlert("Hubo un error al crear la sesion", "Error", "Ok");
                        }
                    }
                    else
                    {
                          control.ShowAlert("Los datos estan incorrectos", "Error", "Ok");
                    }
                }
                else
                {
                    control.ShowAlert("No se pudo conectar con el servidor", "Error", "Ok");
                }
            }
        }
    }
}