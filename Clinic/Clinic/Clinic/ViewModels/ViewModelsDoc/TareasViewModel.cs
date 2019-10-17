using Clinic.Clases;
using Clinic.Models.DocModels;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Clinic.ViewModels.ViewModelsDoc
{
    public class TareasViewModel:BaseViewModel
    {
        Connection get = new Connection();
        Functions functions;

        #region Atributos
        private string _HoraN;
        private string _TareaN;
        private string _LugarN;
        private string _FechaN;
        #endregion

        #region Propiedades
        private ObservableCollection<TareasD> _Tareas;
        public ObservableCollection<TareasD> Tareas
        {
            get { return _Tareas; }
            set { SetValue(ref _Tareas, value); }
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

        public string TareaN
        {
            get { return _TareaN; }
            set { SetValue(ref _TareaN, value); }
        }

        public string FechaN
        {
            get { return _FechaN; }
            set { SetValue(ref _FechaN, value); }
        }

        public string LugarN
        {
            get { return _LugarN; }
            set { SetValue(ref _LugarN, value); }
        }

        public string HoraN
        {
            get { return _HoraN; }
            set { SetValue(ref _HoraN, value); }
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

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    GetTareas();

                    IsRefreshing = false;
                });
            }
        }




        public ICommand Reconnect
        {
            get
            {
                return new RelayCommand(GetTareas);
            }
        }



        #endregion

        #region Constructor
        public TareasViewModel()
        {
            IsVisible = false;
            ListVisible = true;
            functions = new Functions();
            GetTareas();
            GetTodayTareas();
        }


        #endregion



        #region Metodos

        private async void GetTareas()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando...");
            bool result = get.TestConnection();
            if (result == true)
            {
                IsVisible = false;
                ListVisible = true;
                var response = await functions.Read<TareasD>("/Api/tarea/read.php");

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
                    var list = (List<TareasD>)response.Result;
                    Tareas = new ObservableCollection<TareasD>(list);


                }
            }
            else
            {
                await loadingDialog.DismissAsync();
                IsVisible = true;
                ListVisible = false;
            }
        }

        private async void GetTodayTareas()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando...");
            bool result = get.TestConnection();
            if (result == true)
            {
                IsVisible = false;
                ListVisible = true;
                var response = await functions.Read<TareasD>("/Api/tarea/read_Date.php");

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
                    var list = (List<TareasD>)response.Result;

                    foreach (var item in list)
                    {
                        FechaN = item.fecha;
                        HoraN = item.hora;
                        TareaN = item.tarea;
                        LugarN = item.lugar;

                    }


                }
            }
            else
            {
                await loadingDialog.DismissAsync();
                IsVisible = true;
                ListVisible = false;
            }
        }


        #endregion
    }
}
