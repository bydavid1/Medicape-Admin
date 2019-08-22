using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
    class Medicamentos
    {
        public int idmedicamentos { get; set; }
        public string nom_Medicamento { get; set; }
        public string cod_Medicamento { get; set; }
        public int cantidad { get; set; }
        public float precio_U { get; set; }
        public string fecha_V { get; set; }
    }
}
