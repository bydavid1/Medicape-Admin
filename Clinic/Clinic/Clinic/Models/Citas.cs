using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
    public class Citas
    {
        public int idcita { get; set; }
        public string fecha_Cita { get; set; }
        public string hora_Cita { get; set; }
        public string nombre_Paciente { get; set; }
        public string apellido_Paciente { get; set; }
        public int num_Consultorio { get; set; }
        public string nombre_Doctor { get; set; }
        public int idpaciente { get; set; }
        public int idempleado { get; set; }
    }
}
