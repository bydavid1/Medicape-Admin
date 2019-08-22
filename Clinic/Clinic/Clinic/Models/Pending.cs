using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
    class Pending
    {
        public int idpending { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string tipo { get; set; }
        public int idpaciente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        public int idempleado { get; set; }
    }
}
