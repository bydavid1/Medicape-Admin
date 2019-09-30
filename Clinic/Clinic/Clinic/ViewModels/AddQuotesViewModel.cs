using Clinic.Clases;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Clinic.ViewModels
{
    public class AddQuotesViewModel : BaseViewModel
    {

        MaterialControls control = new MaterialControls();
        Functions element = new Functions();

        private string _nombres;
        private string _apellidos;
        private DateTime _fecha;
        private TimeSpan _hora;
        private int _consultorio;

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

        public AddQuotesViewModel(string nombre, string apellido,int id)
        {
            Nombres = nombre;
            Apellidos = apellido;
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
                    ///Falta algo  y no se que es
            }
        }
    }
}
