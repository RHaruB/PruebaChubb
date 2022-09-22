namespace SegurosChubbBack.Clases
{
    public class PolizaModel
    {
        public int CodigoPoliza { get; set; }
        public int CodigoPersona { get; set; }
        public string cedulaPersona { get; set; }
        public string NombrePersona { get; set; }
        public int CodigoSeguro { get; set; }
        public string DescripcionSeguro { get; set; }
        public decimal Prima { get; set; }
        public string Estado { get; set; }
    }
}
