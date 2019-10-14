using Clinic.Clases;
using Clinic.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Clinic.ViewModels
{
    public class EspecialtiesViewModel : BaseViewModel
    {
        Connection get = new Connection();
        Functions functions;

        #region Propiedades
        ObservableCollection<Especialidades> _Items;
        public ObservableCollection<Especialidades> Items
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

        private string _image;
        public string Image
        {
            get { return _image; }
            set { SetValue(ref _image, value); }
        }
        #endregion

        #region Constructor
        public EspecialtiesViewModel()
        {
            IsVisible = false;
            ListVisible = true;
            functions = new Functions();
            GetEspecialties();
        }

        #endregion

        private async void GetEspecialties()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando...");
            bool result = get.TestConnection();
            if (result == true)
            {
                IsVisible = false;
                ListVisible = true;
                var response = await functions.Read<Especialidades>("/Api/especialidades/read.php");
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
                    var list = (List<Especialidades>)response.Result;
                    Items = new ObservableCollection<Especialidades>();
                    foreach (var item in list)
                    {
                        if (item.publico == 0)
                        {
                            item.Image = "apagar";
                        }
                        else
                        {
                            item.Image = "encender";
                        }

                        Items.Add(item);
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


        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    GetEspecialties();

                    IsRefreshing = false;
                });
            }
        }

        public ICommand Reconnect
        {
            get
            {
                return new RelayCommand(GetEspecialties);
            }
        }

        public ICommand ImageClicked
        {
            get
            {
                return new Command((e) =>
                {
                    var item = (Especialidades)e;
                    Especialidades esp = new Especialidades
                    {
                        idespecialidad = item.idespecialidad,
                        nombre = item.nombre,
                        publico = item.publico
                    };

                    Update(esp);
                });
            }
        }

        private async void Update(Especialidades esp)
        {
            bool result = get.TestConnection();
            if (result == true)
            {
                IsVisible = false;
                ListVisible = true;
                var response = await functions.Update(esp, "/Api/especialidades/update_public.php");
                if (!response)
                {
                   await MaterialDialog.Instance.SnackbarAsync(message: "No se pudo actualizar");
                }
                else
                {
                    GetEspecialties();
                }
            }
            else
            {
                IsVisible = true;
                ListVisible = false;
            }
        }

        public ICommand Settings
        {
            get
            {
                return new Command((e) =>
                {
                    var item = (Especialidades)e;

                    Edit(item);
                });
            }
        }

        private async void Edit(Especialidades especialidad)
        {
            var actions = new string[] { "Cambiar nombre", "Editar permisos"};

            var select = await MaterialDialog.Instance.SelectActionAsync(title: "Selecciona una accion",
                                                                         actions: actions);
            if (select == 0)
            {
                var config = new MaterialInputDialogConfiguration()
                {
                    CornerRadius = 8,
                    BackgroundColor = Color.FromHex("#2c3e50"),
                    InputTextColor = Color.White,
                    InputPlaceholderColor = Color.White.MultiplyAlpha(0.6),
                    TintColor = Color.White,
                    TitleTextColor = Color.White,
                    MessageTextColor = Color.FromHex("#DEFFFFFF")
                };

                var input = await MaterialDialog.Instance.InputAsync(title: "Cambiar nombre",
                                                                     message: "Por favor ingrese el nuevo nombre de la especialidad",
                                                                     inputPlaceholder: "Especialidad",
                                                                     inputText: especialidad.nombre,
                                                                     confirmingText: "Cambiar",
                                                                     configuration: config);

                if (!string.IsNullOrEmpty(input) || input == especialidad.nombre)
                {
                    bool result = get.TestConnection();
                    if (result == true)
                    {
                        Especialidades esp = new Especialidades
                        {
                            idespecialidad = especialidad.idespecialidad,
                            nombre = input,
                            publico = especialidad.publico
                        };
                        IsVisible = false;
                        ListVisible = true;
                        var response = await functions.Update(esp, "/Api/especialidades/update.php");
                        if (!response)
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: "No se pudo actualizar");
                        }
                        else
                        {
                            GetEspecialties();
                        }
                    }
                    else
                    {
                        IsVisible = true;
                        ListVisible = false;
                    }
                }
            }
            else if (select == 1)
            {
                var permisos = new string[]
                     {
                        "Pacientes",
                        "Citas",
                        "Consultas",
                        "Empleados",
                        "Medicamentos",
                        "Usuarios",
                        "Consejos",
                        "Listas de espera"
                     };

                var choices = await MaterialDialog.Instance.SelectChoicesAsync(title: "Marque los permisos",
                                                              choices: permisos);
                string value = "";
                bool status = false;
                for (int i = 0; i < permisos.Length; i++)
                {
                    for (int j = 0; j < choices.Length; j++)
                    {
                        if (i == choices[j])
                        {
                            value += "7";
                            status = true;
                            break;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                    if (status == false)
                    {
                        value += "0";
                    }
                }
                Console.WriteLine(Convert.ToString(value));
            }
        }
    }
}

