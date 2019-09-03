using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
   public class Consultas
    {
        public int idconsulta { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int num_Consultorio { get; set; }
        public string  nom_Doctor { get; set; }
        public int idpaciente { get; set; }
        public int idempleado { get; set; }
    }
}
