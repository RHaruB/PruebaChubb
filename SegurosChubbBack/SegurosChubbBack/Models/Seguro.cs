using System;
using System.Collections.Generic;

#nullable disable

namespace SegurosChubbBack.Models
{
    public partial class Seguro
    {
        public Seguro()
        {
            Polizas = new HashSet<Poliza>();
        }

        public int CodigoSeguro { get; set; }
        public string Descripcion { get; set; }
        public double? ValorAsegurado { get; set; }
        public double? Prima { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Poliza> Polizas { get; set; }
    }
}
