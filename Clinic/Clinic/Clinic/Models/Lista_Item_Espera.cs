using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
  public class Lista_Item_Espera
    {
        public int iditemlista { get; set; }
        public int idlista { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int idpaciente { get; set; }
    }
}
