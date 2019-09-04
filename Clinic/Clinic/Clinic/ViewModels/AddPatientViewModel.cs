using Clinic.Clases;
using Clinic.Models;
using Clinic.Validaciones;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Clinic.ViewModels
{
    public class AddPatientViewModel : BaseViewModel
    {
        MaterialControls control = new MaterialControls();
        #region Propiedades
        private string _nombre;
        private string _apellido;
        private string _sexo;
        private string _estado;
        private string _dui;
        private string _telefono;
        private string _correo;
        private string _departamento;
        private string _municipio;
        private string _dir;
        private DateTime _selected;
        #endregion

        #region Propiedades
        public string e_nombre
        {
            get { return _nombre; }
            set { SetValue(ref _nombre, value); }
        }
        public string e_apellido
        {
            get { return _apellido; }
            set { SetValue(ref _apellido, value); }
        }
        public List<string> l_sexo { get; set; }
        public List<string> l_estado { get; set; }
        public string s_sexo
        {
            get { return _sexo; }
            set { SetValue(ref _sexo, value); }
        }
        public string s_estado
        {
            get { return _estado; }
            set { SetValue(ref _estado, value); }
        }
        public string e_dui
        {
            get { return _dui; }
            set { SetValue(ref _dui, value); }
        }
        public string e_telefono
        {
            get { return _telefono; }
            set { SetValue(ref _telefono, value); }
        }
        public string e_correo
        {
            get { return _correo; }
            set { SetValue(ref _correo, value); }
        }
        public string e_departamento
        {
            get { return _departamento; }
            set { SetValue(ref _departamento, value); }
        }
        public string e_municipio
        {
            get { return _municipio; }
            set { SetValue(ref _municipio, value); }
        }
        public string e_dir
        {
            get { return _dir; }
            set { SetValue(ref _dir, value); }
        }
        public string e_nac { get; set; }

        public DateTime e_selected
        {
            get { return _selected; }
            set { SetValue(ref _selected, value); OnPropertyChanged("e_selected"); }
        }

        #endregion

        #region Constructor
        public AddPatientViewModel()
        {
            var listGenero = new List<string>();
            listGenero.Add("Masculino");
            listGenero.Add("Femenino");

            l_sexo = listGenero;


            var listEstado = new List<string>();
            listEstado.Add("Soltero");
            listEstado.Add("Casado");
            listEstado.Add("Viudo");

            l_estado = listEstado;
        }
        #endregion

        public ICommand Create
        {
            get
            {
                return new RelayCommand(Insert);
            }
        }

        private async void Insert()
        {
            if (string.IsNullOrEmpty(e_nombre) ||
              string.IsNullOrEmpty(e_apellido) ||
              string.IsNullOrEmpty(s_sexo) ||
              string.IsNullOrEmpty(s_estado) ||
              string.IsNullOrEmpty(s_estado) ||
              string.IsNullOrEmpty(e_correo) ||
              string.IsNullOrEmpty(e_departamento) ||
              string.IsNullOrEmpty(e_dir) ||
              string.IsNullOrEmpty(e_telefono) ||
              string.IsNullOrEmpty(e_dui) ||
              string.IsNullOrEmpty(e_municipio)
              )
            {
                control.ShowAlert("Faltan datos por llenar", "Error", "Ok");
            }
            else if (TextValidator.Ok == false || ValidateEmail.Ok == false || NumeroValidator.Ok == false)
            {
                control.ShowAlert("Al parecer hay algunos errores", "Error", "Ok");
            }
            else
            {
                string date = DateTime.Today.ToString("yy/MM/dd");
                string bornDate = e_selected.ToString("yy/MM/dd");

                Pacientes pacientes = new Pacientes
                {
                    nombres = e_nombre,
                    apellidos = e_apellido,
                    fecha_Nac = bornDate,
                    sexo = s_sexo,
                    estado_Civil = s_estado,
                    dui = e_dui,
                    telefono = e_telefono,
                    email = e_correo,
                    departamento = e_departamento,
                    municipio = e_municipio,
                    direccion = e_dir
                };

                Functions element = new Functions();
                var response = await element.Insert(pacientes, "/Api/paciente/create.php");

                if (response == true)
                {
                    control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    e_nombre = string.Empty;
                    e_apellido = string.Empty;
                    e_dui = string.Empty;
                    e_telefono = string.Empty;
                    e_correo = string.Empty;
                    e_departamento = string.Empty;
                    e_municipio = string.Empty;
                    e_dir = string.Empty;
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al registrar", "Aviso", "Ok");
                }
            }
        }
    }
}
