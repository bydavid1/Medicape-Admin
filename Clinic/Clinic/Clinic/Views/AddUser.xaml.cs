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
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUser : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Functions functions;

        public AddUser(int id, string email)
        {
            InitializeComponent();
            id_empleado.Text = Convert.ToString(id);
            correo.Text = email;
            functions = new Functions();
            control.ShowSnackBar("Se utilizara el correo ya registrado");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(us.Text) &&
              string.IsNullOrWhiteSpace(pass.Text))
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
            else if (String.IsNullOrWhiteSpace(us.Text))
            {
                control.ShowAlert("Campo Usuario es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(pass.Text))
            {
                control.ShowAlert("Campo Contraseña es obligatorio!!", "Error", "Ok");

            }
            else if (String.IsNullOrWhiteSpace(correo.Text))
            {
                control.ShowAlert("Campo email es obligatorio!!", "Error", "Ok");

            }
            else
            {
                try
                {
                    //Se piden los permisos
                    var permisos = new string[]
                        {
                        "Pacientes",
                        "Citas",
                        "Consultas",
                        "Empleados",
                        "Medicamentos",
                        "Usuarios",
                        "Consejos",
                        "Listas de espera"
                        };

                    var choices = await MaterialDialog.Instance.SelectChoicesAsync(title: "Marque los permisos",
                                                                  choices: permisos);

                    if (choices != null)
                    {
                        var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando...");
                        string value = "";
                        bool status = false;
                        for (int i = 0; i < permisos.Length; i++)
                        {
                            for (int j = 0; j < choices.Length; j++)
                            {
                                if (i == choices[j])
                                {
                                    value += "7";
                                    status = true;
                                    break;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                            if (status == false)
                            {
                                value += "0";
                            }
                        }

                        Usuario user = new Usuario
                        {
                            user_Name = us.Text,
                            user_Password = pass.Text,
                            email = correo.Text,
                            user_type = 1,
                            valor = Convert.ToInt32(value)
                        };

                        var response = await functions.Insert(user, "/Api/usuario/create.php?idempleado=" + id_empleado.Text);
                        if (response.IsSuccess == true)
                        {
                            await loadingDialog.DismissAsync();
                            control.ShowAlert("Registrado", "Perfecto!", "Ok");
                            us.Text = "";
                            pass.Text = "";
                            correo.Text = "";
                        }
                        else
                        {
                            await loadingDialog.DismissAsync();
                            control.ShowAlert("Ocurrio un error al registrar", "Error", "Ok");
                        }
                    }
                    else
                    {
                        control.ShowSnackBar("No se creo el usuario, permisos requeridos");
                    }
            }
            catch (Exception ex)
            {
              await  DisplayAlert("Ocurrio un error ", "Error" + ex, "Ok");
            }
            }
        }
    }
}