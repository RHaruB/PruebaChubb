using SegurosChubbBack.Clases;

namespace SegurosChubbBack.Interface
{
    public interface ISeguros
    {
        bool RegistrarSeguro(SeguroModel seguro);
        bool EliminarSeguro(int codigo);
        SeguroModel ConsultarSeguro(int codigo);
    }
}
