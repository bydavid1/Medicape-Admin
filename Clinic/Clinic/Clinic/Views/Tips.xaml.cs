using Clinic.Clases;
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

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tips : ContentPage
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        private string baseurl;
        public Tips()
        {
            InitializeComponent();
            baseurl = get.BaseUrl;
            getTips();
        }

        private async void getTips()
        {
            try
            {
                string send = baseurl + "/Api/tips/read.php";
                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(send);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(send);
                    var tips = JsonConvert.DeserializeObject<List<Models.Tips>>(response);
                    mylist.ItemsSource = tips;
                }
                else
                {
                    MaterialControls control = new MaterialControls();
                    control.ShowSnackBar("No se pudieon encontrar tips");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTips());
        }
    }
}