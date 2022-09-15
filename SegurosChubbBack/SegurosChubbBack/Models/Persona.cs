using System;
using System.Collections.Generic;

#nullable disable

namespace SegurosChubbBack.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Polizas = new HashSet<Poliza>();
        }

        public int CodigoPersona { get; set; }
        public string Cedula { get; set; }
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }
        public int? Edad { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Poliza> Polizas { get; set; }
    }
}
