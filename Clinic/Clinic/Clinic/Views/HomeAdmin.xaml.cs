using Clinic.Clases;
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
        }

        void OnClick(object sender, EventArgs e)
        {
            ToolbarItem tbi = (ToolbarItem)sender;
            if (tbi.Text == "Cerrar Sesion")
            {
                MaterialControls control = new MaterialControls();
                control.ShowLoading("Cerrando sesion...");
                App.Current.Logout();
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