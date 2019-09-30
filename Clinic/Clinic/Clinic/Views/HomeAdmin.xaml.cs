using Acr.UserDialogs;
using Clinic.Clases;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeAdmin : ContentPage
    {
        public HomeAdmin()
        {
            InitializeComponent();
            UserDialogs.Instance.HideLoading();
        }

        void OnClick(object sender, EventArgs e)
        {
            ToolbarItem tbi = (ToolbarItem)sender;
            if (tbi.Text == "Cerrar Sesion")
            {
                MaterialControls control = new MaterialControls();
                control.ShowLoading("Cerrando sesion...");
                var sessionDeleted = CrossSecureStorage.Current.DeleteKey("SessionActive");

                if (sessionDeleted == true)
                {
                    var idDeleted = CrossSecureStorage.Current.DeleteKey("iduser");
                    var PermisDeleted = CrossSecureStorage.Current.DeleteKey("permisos");
                    var nameDeleted = CrossSecureStorage.Current.DeleteKey("user");
                    if (idDeleted == true && PermisDeleted == true && nameDeleted == true)
                    {
                        Navigation.PushAsync(new Login());
                    }
                    else
                    {
                        control.ShowSnackBar("Algunos datos no se borraron con exito");
                        Navigation.PushAsync(new Login());
                    }
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al cerrar sesion. Reinicie la app e intentelo de nuevo", "Error", "ok");
                }
                
            }
            else if (tbi.Text == "Acerca de")
            {
                Navigation.PushModalAsync(new Info());
            }
            else if (tbi.Text == "Perfil")
            {
                Navigation.PushAsync(new Account());
            }
        }

        private void Clicked_Medicaments(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Medicaments());
        }

        private void Clicked_Tips(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Tips());
        }

        private void Clicked_Employees(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Employees());
        }
    }
}