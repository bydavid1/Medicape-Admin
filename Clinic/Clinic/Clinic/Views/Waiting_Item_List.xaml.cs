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
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Waiting_Item_List : ContentPage
    {
        private int ids;
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public Waiting_Item_List(int id)
        {
            ids = id;
            control.ShowLoading("Obteniendo lista de espera");
            InitializeComponent();
            baseurl = get.BaseUrl;
            bool result = get.TestConnection();

            if (result == true)
            {
                getLists(ids);
            }
            else
            {
                control.ShowAlert("No se pudo conectar con el servidor", "Error", "Ok");
            }
        }

        private async void getLists(int id)
        {
            try
            {
                string url = baseurl + "/Api/item_Espera/read.php?idlista="+id;

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var lista = JsonConvert.DeserializeObject<List<Lista_Item_Espera>>(response);
                    mylist.ItemsSource = lista;
                }
                else
                {
              
                    message.IsVisible = true;
                }

            }
            catch (HttpRequestException e)
            {
                await DisplayAlert("error", "" + e, "Ok");
            }
        }

        private void Mylist_Refreshing(object sender, EventArgs e)
        {
            getLists(ids);
            mylist.EndRefresh();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPatient("lists", ids));
        }

        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            try
            {

                var snackbarConfiguration = new MaterialSnackbarConfiguration()
                {
                    BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_BACKGROUND),
                    ButtonAllCaps = true,
                    TintColor = Color.White,
                    MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8)
                };

               var res = await MaterialDialog.Instance.SnackbarAsync(message: "Confirme para eliminar",
                                            actionButtonText: "Eliminar",
                                            configuration: snackbarConfiguration);
                if (res == true)
                {
                    control.ShowLoading("Eliminando");
                    string url = baseurl + "/Api/lista_espera/delete.php?idlista=" + ids;

                    HttpClient client = new HttpClient();
                    HttpResponseMessage connect = await client.GetAsync(url);
                    if (connect.StatusCode == HttpStatusCode.OK)
                    {
                        url = baseurl + "/Api/item_espera/delete.php?idlista=" + ids;
                       connect = await client.GetAsync(url);
                        if (connect.StatusCode == HttpStatusCode.OK)
                        {
                            control.ShowAlert("Lista de espera eliminada", "Eliminada", "Ok");
                            getLists(ids);

                        }
                        else
                        {
                            control.ShowAlert("No se pudo eliminar los items", "Error", "Ok");
                        }
                    }
                    else
                    {
                        control.ShowAlert("No se pudo eliminar la lista", "Error", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error: " + ex, "ok");
            }
        }
    }
}