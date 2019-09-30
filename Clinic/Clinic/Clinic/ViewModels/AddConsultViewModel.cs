using Clinic.Clases;
using Clinic.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.SecureStorage;
using System;
using System.Windows.Input;

namespace Clinic.ViewModels
{
    public class AddConsultViewModel : BaseViewModel
    {
        MaterialControls control = new MaterialControls();
        Functions element = new Functions();

        #region Atributos
        private string _nombres;
        private string _apellidos;
        private int _consultorio;
        private string _diagnostico;
        private string _tratamiento;
        private string _observaciones;
        private string _recetas;
        private string _examenes;
        private int _id;
        #endregion

        #region Propiedades
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

        public int Consultorio
        {
            get { return _consultorio; }
            set { SetValue(ref _consultorio, value); }
        }

        public string Diagnostico
        {
            get { return _diagnostico; }
            set { SetValue(ref _diagnostico, value); }
        }

        public string Tratamiento
        {
            get { return _tratamiento; }
            set { SetValue(ref _tratamiento, value); }
        }
        public string Observaciones
        {
            get { return _observaciones; }
            set { SetValue(ref _observaciones, value); }
        }
        public string Recetas
        {
            get { return _recetas; }
            set { SetValue(ref _recetas, value); }
        }

        public string Examenes
        {
            get { return _examenes; }
            set { SetValue(ref _examenes, value); }
        }


        #endregion

        #region Constructor
        public AddConsultViewModel(string nombre, string apellido, int id)
        {
            Nombres = nombre;
            Apellidos = apellido;
            _id = id;
        }
        #endregion

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
                Consultorio < 1 ||
                string.IsNullOrEmpty(Diagnostico) ||
                string.IsNullOrEmpty(Tratamiento) ||
                string.IsNullOrEmpty(Observaciones) ||
                string.IsNullOrEmpty(Recetas) ||
                string.IsNullOrEmpty(Examenes))
            {
                control.ShowAlert("Faltan datos por llenar", "Error", "Ok");
            }
            else
            {

                var idemp = Convert.ToInt32(CrossSecureStorage.Current.GetValue("iduser"));
                string date = DateTime.Today.ToString("yy/MM/dd");
                string hour = DateTime.Now.ToString("hh:mm");

                Consultas consulta = new Consultas
                {
                    nombres = Nombres,
                    apellidos = Apellidos,
                    idpaciente = _id,
                    idempleado = idemp,
                    fecha = date,
                    hora = hour,
                    num_Consultorio = Consultorio
                };


                var response1 = await element.Insert(consulta, "/Api/consultas/create.php", true);

                if (response1.IsSuccess == true)
                {
                    Expediente exp = new Expediente()
                    {
                        idconsulta = Convert.ToInt32(response1.Result),
                        diagnostico = Diagnostico,
                        tratamiento = Tratamiento,
                        receta = Recetas,
                        observaciones = Observaciones,
                        descripcion_Exam = Examenes,
                        idpaciente = _id
                    };

                    var response2 = await element.Insert(exp, "/Api/item_expediente/create.php");

                    if (response2.IsSuccess == true)
                    {
                        control.ShowAlert("Registro exitoso", "Aviso", "Ok");
                    }
                    else
                    {
                        control.ShowAlert("Ocurrio un error al registrar al expediente", "Aviso", "Ok");
                    }
                }
                else
                {
                    control.ShowAlert("Ocurrio un error al registrar la consulta", "Aviso", "Ok");
                }
            }
        }
    }
}
