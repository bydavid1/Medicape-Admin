using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Clinic.Clases;
using Clinic.Models;
using Clinic.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Medicaments : ContentPage
    {
        public Medicaments()
        {
            InitializeComponent();
            BindingContext = new MedicamentsViewModel();
        }

        private void Mylist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddMedicament());
        }
    }   
}