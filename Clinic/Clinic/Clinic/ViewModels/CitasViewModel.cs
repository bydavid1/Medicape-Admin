using Clinic.Clases;
using Clinic.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.SecureStorage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Clinic.ViewModels
{
    public class CitasViewModel : BaseViewModel
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
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

        private bool _listVisible;
        public bool ListVisible
        {
            get { return _listVisible; }
            set { SetValue(ref _listVisible, value); }
        }

        private bool _isVisible = false;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetValue(ref _isVisible, value); }
        }

        private bool _noresults;
        public bool NoResults
        {
            get { return _noresults; }
            set { SetValue(ref _noresults, value); }
        }

        public CitasViewModel()
        {
            IsVisible = false;
            functions = new Functions();
            GetQuotes();
        }

        public async void GetQuotes()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando...");
            bool result = get.TestConnection();
            if (result == true)
            {

                var response2 = await functions.Read<Citas>("/Api/citas/custom_read.php?idempleado=" + CrossSecureStorage.Current.GetValue("iduser"));
                if (!response2.IsSuccess)
                {
                    await loadingDialog.DismissAsync();
                    control.ShowAlert(response2.Message, "Error", "Aceptar");
                }
                else if (response2.Result == null)
                {
                    await loadingDialog.DismissAsync();
                    IsVisible = false;
                    NoResults = true;
                    ListVisible = false;
                }
                else
                {
                    IsVisible = false;
                    NoResults = false;
                    ListVisible = true;
                    await loadingDialog.DismissAsync();
                    var list = (List<Citas>)response2.Result;
                    Items = new ObservableCollection<Citas>(list);
                }
            }
            else
            {
                await loadingDialog.DismissAsync();
                IsVisible = true;
                ListVisible = false;
            }
        }

        public ICommand Reconnect
        {
            get
            {
                return new RelayCommand(GetQuotes);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    GetQuotes();

                    IsRefreshing = false;
                });
            }
        }


    }
}
