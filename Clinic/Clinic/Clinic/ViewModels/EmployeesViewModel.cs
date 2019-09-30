using Clinic.Clases;
using Clinic.Models;
using Clinic.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Clinic.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        Connection get = new Connection();
        Functions functions;

        #region Propiedades
        ObservableCollection<Empleados> _Items;
        public ObservableCollection<Empleados> Items
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

        private bool _isVisible = false;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetValue(ref _isVisible, value); }
        }

        private string _query;
        public string Query
        {
            get { return _query; }
            set { SetValue(ref _query, value); }
        }

        private bool _noresults;
        public bool NoResults
        {
            get { return _noresults; }
            set { SetValue(ref _noresults, value); }
        }

        private bool _listVisible;
        public bool ListVisible
        {
            get { return _listVisible; }
            set { SetValue(ref _listVisible, value); }
        }
        #endregion

        #region Constructor
        public EmployeesViewModel()
        {
            IsVisible = false;
            ListVisible = true;
            functions = new Functions();
            getEmployees();
        } 
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    getEmployees();

                    IsRefreshing = false;
                });
            }
        }


        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(search);
            }
        }

        public ICommand Reconnect
        {
            get
            {
                return new RelayCommand(getEmployees);
            }
        }



        #endregion

        #region Metodos


        public async void getEmployees()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando...");
            bool result = get.TestConnection();
            if (result == true)
            {
                IsVisible = false;
                ListVisible = true;
                var response = await functions.Read<Empleados>("/Api/empleado/read.php");
                if (!response.IsSuccess)
                {
                    await loadingDialog.DismissAsync();
                    await MaterialDialog.Instance.AlertAsync(message: response.Message,
                                               title: "Error",
                                               acknowledgementText: "Ok");
                }
                else if (response.Result == null)
                {
                    await loadingDialog.DismissAsync();
                    await MaterialDialog.Instance.AlertAsync(message: response.Message,
                                               title: "Error",
                                               acknowledgementText: "Ok");
                }
                else
                {
                    await loadingDialog.DismissAsync();
                    var list = (List<Empleados>)response.Result;
                    Items = new ObservableCollection<Empleados>(list);
                }
            }
            else
            {
                await loadingDialog.DismissAsync();
                IsVisible = true;
                ListVisible = false;
            }
        }

        private async void search()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Buscando...");
            var response = await functions.Read<Empleados>("/Api/usuario/read.php?query=" + Query);
            if (response.IsSuccess && response.Result != null)
            {
                await loadingDialog.DismissAsync();
                NoResults = false;
                ListVisible = true;
                var list = (List<Empleados>)response.Result;
                Items = new ObservableCollection<Empleados>(list);
            }
            else if (response.IsSuccess && response.Result == null)
            {
                await loadingDialog.DismissAsync();
                NoResults = true;
                ListVisible = false;
            }
            else
            {
                await loadingDialog.DismissAsync();
                NoResults = false;
                                   await MaterialDialog.Instance.AlertAsync(message: response.Message,
                                               title: "Aviso",
                                               acknowledgementText: "Ok");
            }
        } 
        #endregion
    }
}
