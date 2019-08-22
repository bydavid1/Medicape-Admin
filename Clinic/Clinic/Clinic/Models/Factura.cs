using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
    class Factura
    {
        public int idfactura { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string nombre { get; set; }
        public int idpaciente { get; set; }
        public float total { get; set; }
    }
}
