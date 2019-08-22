using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
    class VitalSigns
    {
        public int idperfil { get; set; }
        public string fecha { get; set; }
        public float altura { get; set; }
        public float peso { get; set; }
        public float temperatura { get; set; }
        public float presion { get; set; }
        public float frec_Cardiaca { get; set; }
        public float pulso { get; set; }
        public int idpaciente { get; set; }
    }
}
