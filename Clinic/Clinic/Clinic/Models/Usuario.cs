using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
    public class Usuario
    {
        public int iduser { get; set; }
        public string user_Name { get; set; }
        public string user_Password { get; set; }
        public string email { get; set; }
        public int user_type { get; set; }
        public int idpermisos { get; set; }
        public int valor { get; set; }
        public int idespecialidad { get; set; }
    }
}
