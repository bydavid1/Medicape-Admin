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
        public string e_nombre { get; set; }
        public string e_apellido { get; set; }
        public List<string> l_sexo { get; set; }
        public List<string> l_estado { get; set; }
        public string s_sexo { get; set; }
        public string s_estado { get; set; }
        public List<string> l_especialidad { get; set; }
        public string s_especialidad { get; set; }
        public string e_dui { get; set; }
        public string e_telefono { get; set; }
        public string e_correo { get; set; }
        public string e_departamento { get; set; }
        public string e_celular { get; set; }
        public string e_municipio { get; set; }
        public string e_dir { get; set; }
        public string e_nit { get; set; }
        public string e_nac { get; set; }

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

        private void Insert()
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
                element.Insert(empleados, "/Api/empleado/create.php");
            }
        }
    }
}
