using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Models
{
  public class Item_Factura
    {
        public int iditemfactura { get; set; }
        public int idfactura { get; set; }
        public string concepto { get; set; }
        public int cantidad { get; set; }
        public float precio { get; set; }
        public float total { get; set; }
    }
}
