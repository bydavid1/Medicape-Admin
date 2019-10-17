using Clinic.Clases;
using Clinic.Models;
using Clinic.ViewDoc;
using Clinic.ViewModels;
using Plugin.SecureStorage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            Name.Text = "Prueba de nombre";
            userName.Text = "@" + CrossSecureStorage.Current.GetValue("user");
            NavigationPage.SetHasNavigationBar(this, false);
            var idespecialidad = CrossSecureStorage.Current.GetValue("idespecialidad");
            if (idespecialidad == "0")
            {
                this.Detail = new NavigationPage(new HomeAdmin()) { BarBackgroundColor = Color.FromHex("#00cbc5") };
            }
            else
            {
                this.Detail = new NavigationPage(new HomeDocPage()) { BarBackgroundColor = Color.FromHex("#00cbc5") };
            }
            

            BindingContext = new MainPageViewModel();
            MessagingCenter.Subscribe<MasterMenu>(this, "OpenMenu", (Menu) =>
            {
                this.Detail = new NavigationPage((Page)Activator.CreateInstance(Menu.TargetType)) { BarBackgroundColor = Color.FromHex("#00cbc5") };

                IsPresented = false;
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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
    }
}
