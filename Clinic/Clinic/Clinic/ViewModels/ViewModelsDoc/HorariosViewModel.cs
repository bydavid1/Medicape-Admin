using Clinic.Clases;
using Clinic.Models.DocModels;
using GalaSoft.MvvmLight.Command;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using XF.Material.Forms.UI.Dialogs;

namespace Clinic.ViewModels.ViewModelsDoc
{
   public  class HorariosViewModel:BaseViewModel
    {
        Connection get = new Connection();
        Functions functions;
        MaterialControls control = new MaterialControls();
        #region Atributos
        string Fhora = "";
        int cont = 0;
        int horaF = 0;
        int Ttime = 0;
        private string _timeCitas;
        private string _numCitas;
        private string _selecH;
        private string _TotalC;
        private string _TiempoT;
        private string _Horario;
        private string _Minutos;
        private string _IniHora;
        private int IdD;




        #endregion

        #region Propiedades
        public List<string> l_Horarios { get; set; }
        public string d_TimeCitas
        {
            get { return _timeCitas; }
            set { SetValue(ref _timeCitas, value); }
        }

        public string TotalC
        {
            get { return _TotalC; }
            set { SetValue(ref _TotalC, value); }
        }

        public string Horario
        {
            get { return _Horario; }
            set { SetValue(ref _Horario, value); }
        }

        public string Minutos
        {
            get { return _Minutos; }
            set { SetValue(ref _Minutos, value); }
        }
        public string TiempoT
        {
            get { return _TiempoT; }
            set { SetValue(ref _TiempoT, value); }
        }

        public string d_NumCitas
        {
            get { return _numCitas; }
            set { SetValue(ref _numCitas, value); }
        }

        public string l_selecH
        {
            get { return _selecH; }
            set { SetValue(ref _selecH, value); }
        }
        #endregion

        #region Command
        public ICommand Create
        {
            get
            {
                return new RelayCommand(CreateData);
            }
        }


        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(CancelData);
            }
        }

        public ICommand Delete
        {
            get
            {
                return new RelayCommand(DeleteHora);
            }
        }



        #endregion

        #region Constructor
        public HorariosViewModel()
        {
            var listHorarios = new List<string>();
            listHorarios.Add("7:00 a.m");
            listHorarios.Add("8:00 a.m");
            listHorarios.Add("9:00 a.m");
            listHorarios.Add("10:00 a.m");
            listHorarios.Add("11:00 a.m");
            listHorarios.Add("1:00 p.m");
            listHorarios.Add("2:00 p.m");
            listHorarios.Add("3:00 p.m");
            listHorarios.Add("4:00 p.m");
            listHorarios.Add("5:00 p.m");

            l_Horarios = listHorarios;
            functions = new Functions();

            ReadHorario();

        }


        #endregion


        #region Metodos
        private async void CreateData()
        {
            if (string.IsNullOrEmpty(d_NumCitas.ToString()) ||
              string.IsNullOrEmpty(d_TimeCitas) ||
              string.IsNullOrEmpty(l_selecH)

              )
            {
                control.ShowAlert("Faltan datos por llenar", "Error", "Ok");
            }
            else
            {



                Ttime = Convert.ToInt32(d_NumCitas) * Convert.ToInt32(d_TimeCitas);

                if (l_selecH.Equals("7:00 a.m"))
                {
                    _IniHora = "7:00 a.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    horaF = 7 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }

                }
                else if (l_selecH.Equals("8:00 a.m"))
                {
                    _IniHora = "8:00 a.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    horaF = 8 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }
                else if (l_selecH.Equals("9:00 a.m"))
                {
                    _IniHora = "9:00 a.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    horaF = 9 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }
                else if (l_selecH.Equals("10:00 a.m"))
                {
                    _IniHora = "10:00 a.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    horaF = 10 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }
                else if (l_selecH.Equals("11:00 a.m"))
                {
                    _IniHora = "11:00 a.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    horaF = 11 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }
                else if (l_selecH.Equals("12:00 a.m"))
                {
                    _IniHora = "12:00 a.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    horaF = 12 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }
                else if (l_selecH.Equals("1:00 p.m"))
                {
                    _IniHora = "1:00 p.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    int horaF = 13 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }
                else if (l_selecH.Equals("2:00 p.m"))
                {
                    _IniHora = "2:00 p.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    int horaF = 14 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }
                else if (l_selecH.Equals("3:00 p.m"))
                {
                    _IniHora = "3:00 a.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    int horaF = 15 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }
                else if (l_selecH.Equals("4:00 p.m"))
                {
                    _IniHora = "4:00 p.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    horaF = 16 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }
                else if (l_selecH.Equals("5:00 p.m"))
                {
                    _IniHora = "5:00 p.m";
                    do
                    {
                        if (Ttime >= 60)
                        {
                            Ttime = Ttime - 60;
                            cont++;
                        }

                    }
                    while (Ttime > 60);

                    horaF = 17 + cont;
                    Fhora = horaF.ToString() + ":" + Ttime.ToString();

                    if (horaF > 12)
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "p.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                    else
                    {
                        TotalC = d_NumCitas;
                        TiempoT = Fhora.ToString();
                        Horario = "a.m";
                        Minutos = d_TimeCitas;
                        control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    }
                }

                HorariosD horaripD = new HorariosD()
                {
                    tiempo_cita = Convert.ToInt32(d_TimeCitas),
                    numero_cita = d_NumCitas,
                    hora_inicio = _IniHora,
                    hora_final = TiempoT,
                    idDoc = 1
                };

                Functions element = new Functions();
                var response = await element.Insert(horaripD, "/Api/horarios/create.php");

                if (response.IsSuccess == true)
                {
                    control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    d_TimeCitas = string.Empty;
                    d_NumCitas = string.Empty;
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al registrar", "Aviso", "Ok");
                }
            }
        }

        private async void ReadHorario()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando...");
            bool result = get.TestConnection();
            if (result == true)
            {
                IdD = 1;
                var response = await functions.Read<HorariosD>("/Api/horarios/readById.php?idDoc=" + CrossSecureStorage.Current.GetValue("iduser"));

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
                    var list = (List<HorariosD>)response.Result;

                    foreach (var item in list)
                    {
                        TotalC = item.numero_cita;
                        TiempoT = item.hora_final;
                        Minutos = item.tiempo_cita.ToString();

                    }


                }
            }
            else
            {
                await loadingDialog.DismissAsync();

            }
        }

        private async void DeleteHora()
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Desea eliminar el horario actual",
                                     title: "Confirmacion",
                                     confirmingText: "Si",
                                     dismissiveText: "No");
            if (result == true)
            {
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando...");
                bool testC = get.TestConnection();


                await MaterialDialog.Instance.AlertAsync(message: "Regstro Eliminado",
                                              title: "Aviso",
                                              acknowledgementText: "Ok");
                IdD = 1;
                /*var response = await functions.Delete<HorariosD>("")*/
            }

        }


        private async void CancelData()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}
