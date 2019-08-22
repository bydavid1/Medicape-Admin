using Clinic.Clases;
using Clinic.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clinic.ViewModels
{
   public class CitasViewModel : INotifyPropertyChanged
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        User name = new User();
        private string baseurl;


        public CitasViewModel()
        {
            baseurl = get.BaseUrl;
            bool result = get.TestConnection();
            if (result == true)
            {
                getQuotes();
            }
            else
            {
                control.ShowAlert("No se pudo conectar con el servidor", "Error", "Ok");
            }
        }

        public async void getQuotes()
        {
                string username = name.getName();
                string url = baseurl + "/Api/usuario/read_id.php?username=" + username;

                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var info = JsonConvert.DeserializeObject<Usuario>(response);
                    var id = info.reference;

                    string url2 = baseurl + "/Api/citas/custom_read.php?idempleado=" + id;

                    HttpClient client2 = new HttpClient();
                    HttpResponseMessage connect2 = await client2.GetAsync(url2);

                    if (connect2.StatusCode == HttpStatusCode.OK)
                    {
                        var response2 = await client2.GetStringAsync(url2);
                        var citas = JsonConvert.DeserializeObject<List<Citas>>(response2);
                        Items = new ObservableCollection<Citas>();
                    foreach (var item in citas)
                    {
                        Items.Add(item);
                    }
                    }
                }
        }

        ObservableCollection<Citas> _Items;
        public ObservableCollection<Citas> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                if (value != null)
                {
                    _Items = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                     getQuotes();

                    IsRefreshing = false;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
