using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
    class Expediente
    {
        public int iditemexp { get; set; }
        public string diagnostico { get; set; }
        public string tratamiento { get; set; }
        public string observaciones { get; set; }
        public string receta { get; set; }
        public string num_Expediente { get; set; }
        public string descripcion_Exam { get; set; }
        public int idpaciente { get; set; }
        public int idconsulta { get; set; }

    }
}
