using Clinic.Clases;
using Clinic.Models;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using XF.Material.Forms.UI.Dialogs.Configurations;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using Clinic.Validaciones;
using System.Collections.Generic;

namespace Clinic.ViewModels
{
    public class AddEmployeeViewModel : BaseViewModel
    {
        MaterialControls control = new MaterialControls();
        #region Propiedades
        private string _nombre;
        private string _apellido;
        private string _sexo;
        private string _estado;
        private string _especialidad;
        private string _dui;
        private string _telefono;
        private string _correo;
        private string _departamento;
        private string _municipio;
        private string _celular;
        private string _dir;
        private string _nit;
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
        public List<string> l_especialidad { get; set; }
        public string s_especialidad
        {
            get { return _especialidad; }
            set { SetValue(ref _especialidad, value); }
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
        public string e_celular
        {
            get { return _celular; }
            set { SetValue(ref _celular, value); }
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
        public string e_nit
        {
            get { return _nit; }
            set { SetValue(ref _nit, value); }
        }
        public string e_nac { get; set; }

        #endregion
        public AddEmployeeViewModel()
        {
            var listGenero = new List<string>();
            listGenero.Add("Masculino");
            listGenero.Add("Femenino");

            l_sexo = listGenero;

            var listEspecialidad= new List<string>();
            listEspecialidad.Add("Doctor");
            listEspecialidad.Add("Enfermera");

            l_especialidad = listEspecialidad;

            var listEstado = new List<string>();
            listEstado.Add("Soltero");
            listEstado.Add("Casado");
            listEstado.Add("Viudo");

            l_estado = listEstado;
        }

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
              string.IsNullOrEmpty(e_nit) ||
              string.IsNullOrEmpty(e_municipio) ||
              string.IsNullOrEmpty(e_celular)
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

                Empleados empleados = new Empleados
                {
                    nombres = e_nombre,
                    apellidos = e_apellido,
                    fecha_Nac = date,
                    sexo = s_sexo,
                    estado_Civil = s_estado,
                    especialidad = s_especialidad,
                    dui = e_dui,
                    telefono = e_telefono,
                    email = e_correo,
                    departamento = e_departamento,
                    celular = e_celular,
                    municipio = e_municipio,
                    direccion = e_dir,
                    nit = e_nit,
                    fecha_Contratacion = date
                };

                Functions element = new Functions();
               var response = await element.Insert(empleados, "/Api/empleado/create.php");

                if (response == true)
                {
                    control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                    e_nombre = string.Empty;
                    e_apellido = string.Empty;
                    e_dui = string.Empty;
                    e_telefono = string.Empty;
                    e_celular = string.Empty;
                    e_correo = string.Empty;
                    e_departamento = string.Empty;
                    e_municipio = string.Empty;
                    e_dir = string.Empty;
                    e_nit = string.Empty;
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al registrar", "Aviso", "Ok");
                }
            }
        }
    }
}
