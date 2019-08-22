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
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public Medicaments()
        {
            control.ShowLoading("Obteniendo medicamentos");
            InitializeComponent();
            baseurl = get.BaseUrl;
            bool result = get.TestConnection();

            if (result == true)
            {
                getMedicaments();

            }
            else
            {
                
                control.ShowAlert("No se pudo conectar con el servidor", "Error", "Ok");
            }

            mylist.RefreshCommand = new Command(() =>
            {
                mylist.IsRefreshing = true;
                getMedicaments();
                mylist.IsRefreshing = false;
            });
        }

        private async void getMedicaments()
        {
            try
            {
                string url = baseurl + "/Api/medicamentos/read.php";

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var citas = JsonConvert.DeserializeObject<List<Medicamentos>>(response);
                    mylist.ItemsSource = citas;
                }
                else
                {
                    mylist.IsVisible = false;
                    message.IsVisible = true;
                }

            }
            catch (HttpRequestException e)
            {
                await DisplayAlert("error", "" + e, "Ok");
            }
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