using Clinic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models.DocModels
{
   public class HorariosD
    {
        public int Idhorario { get; set; }
        public int tiempo_cita { get; set; }
        public string numero_cita { get; set; }
        public string hora_inicio { get; set; }
        public string hora_final { get; set; }
        public int idDoc { get; set; }

    }
}
