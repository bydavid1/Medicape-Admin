using Clinic.Clases;
using Clinic.Models;
using Clinic.ViewModels;
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
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddConsult : ContentPage
    {
        private int ids;
        public AddConsult(string nombre, string apellido, int id)
        {
            InitializeComponent();
            BindingContext = new AddConsultViewModel(nombre, apellido, id);
            ids = id;
        }


        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            int id = ids;
            Navigation.PushAsync(new AddVitalSigns(id));
        }
    }
}