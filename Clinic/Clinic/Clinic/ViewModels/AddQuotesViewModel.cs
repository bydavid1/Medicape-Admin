using Clinic.Clases;
using Clinic.Models;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Clinic.ViewModels
{
    public class AddQuotesViewModel : BaseViewModel
    {

        MaterialControls control = new MaterialControls();
        Functions functions = new Functions();

        private string _nombres;
        private string _apellidos;
        private DateTime _fecha;
        private TimeSpan _hora;
        private int _consultorio;
        private int _id;

        public string Nombres
        {
            get { return _nombres; }
            set { SetValue(ref _nombres, value); }
        }

        public string Apellidos
        {
            get { return _apellidos; }
            set { SetValue(ref _apellidos, value); }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { SetValue(ref _fecha, value); }
        }

        public TimeSpan Hora
        {
            get { return _hora; }
            set { SetValue(ref _hora, value); }
        }

        public int Consultorio
        {
            get { return _consultorio; }
            set { SetValue(ref _consultorio, value); }
        }

        public int Id
        {
            get { return _id; }
            set { SetValue(ref _id, value); }
        }


        public AddQuotesViewModel(string nombre, string apellido,int id)
        {
            Nombres = nombre;
            Apellidos = apellido;
            Id = id;
        }

        public ICommand Add
        {
            get
            {
                return new RelayCommand(Insert);
            }
        }

        private async void Insert()
        {
            if (string.IsNullOrEmpty(Nombres) ||
             string.IsNullOrEmpty(Apellidos) ||
             Consultorio < 0)
            {
                control.ShowAlert("Faltan datos por llenar", "Error", "Ok");
            }
            else
            {
                    var hour = Convert.ToString(Hora);

                    Citas citas = new Citas
                    {
                        fecha_Cita = Convert.ToString(Fecha),
                        hora_Cita = hour,
                        nombre_Paciente = Nombres,
                        apellido_Paciente = Apellidos,
                        num_Consultorio = Consultorio,
                        idpaciente = Id,
                        idempleado = Convert.ToInt32(CrossSecureStorage.Current.GetValue("iduser"))
                    };

                    var response = await functions.Insert(citas,"/Api/citas/create.php");

                    if (response.IsSuccess == true)
                    {

                        control.ShowAlert("Registrado!!", "Exito", "Ok");
                    }
                    else
                    {
                        control.ShowAlert("Ocurrio un error al registrar", "Error", "Ok");
                    }
                
            }
        }
    }
}
