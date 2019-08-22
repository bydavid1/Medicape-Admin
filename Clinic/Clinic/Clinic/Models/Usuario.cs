using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
    class Usuario
    {
        public int iduser { get; set; }
        public string user_Name { get; set; }
        public string user_Password { get; set; }
        public string email { get; set; }
        public string user_type { get; set; }
        public int reference { get; set; }
    }
}
