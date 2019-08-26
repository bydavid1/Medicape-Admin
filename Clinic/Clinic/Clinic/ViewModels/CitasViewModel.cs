using Clinic.Clases;
using Clinic.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clinic.ViewModels
{
   public class CitasViewModel : BaseViewModel
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        User name = new User();
        Functions functions;

        ObservableCollection<Citas> _Items;
        public ObservableCollection<Citas> Items
        {
            get { return _Items; }
            set { SetValue(ref _Items, value); }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }

        public CitasViewModel()
        {
            functions = new Functions();
            bool result = get.TestConnection();
            if (result == true)
            {
                IsRefreshing = true;
                getQuotes();
                IsRefreshing = false;
            }
            else
            {
                control.ShowAlert("No se pudo conectar con el servidor", "Error", "Ok");
            }
        }

        public async void getQuotes()
        {
            string username = name.getName();
            var response = await functions.GetCurrentId(username);

            if (!response.IsSuccess)
            {
                control.ShowAlert(response.Message, "Error", "Aceptar");
            }
            else
            {
                var response2 = await functions.Read<Citas>("/Api/citas/custom_read.php?idempleado=" + response.Result);
                if (!response2.IsSuccess)
                {
                    control.ShowAlert(response2.Message, "Error", "Aceptar");
                }
                else if(response2.Result == null)
                {
                    control.ShowAlert(response2.Message, "Error", "Aceptar");
                }
                else
                {
                    var list = (List<Citas>)response2.Result;
                    Items = new ObservableCollection<Citas>(list);
                }
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

    }
}
