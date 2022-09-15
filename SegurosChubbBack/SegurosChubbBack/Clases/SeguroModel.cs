namespace SegurosChubbBack.Clases
{
    public class SeguroModel
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; } 
        public decimal Valor_Asegurado { get; set; }
        public decimal Prima { get; set; }        
        public string Estado { get; set; }         
    }
}