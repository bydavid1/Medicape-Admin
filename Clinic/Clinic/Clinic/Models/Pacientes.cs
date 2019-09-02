using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
   public class Pacientes
    {
        public int idpaciente { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string fecha_Nac { get; set; }
        public string sexo { get; set; }
        public string estado_Civil { get; set; }
        public string dui { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string departamento { get; set; }
        public string municipio { get; set; }
        public string direccion { get; set; }
    }
}
