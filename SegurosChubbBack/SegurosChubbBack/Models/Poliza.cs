using System;
using System.Collections.Generic;

#nullable disable

namespace SegurosChubbBack.Models
{
    public partial class Poliza
    {
        public int CodigoPoliza { get; set; }
        public int? CodigoPersona { get; set; }
        public int? CodigoSeguro { get; set; }
        public string Estado { get; set; }

        public virtual Persona CodigoPersonaNavigation { get; set; }
        public virtual Seguro CodigoSeguroNavigation { get; set; }
    }
}
