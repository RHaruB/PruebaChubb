using SegurosChubbBack.Clases;
using System.Collections.Generic;

namespace SegurosChubbBack.Interface
{
    public interface ISeguros
    {
        bool RegistrarSeguro(SeguroModel seguro);
        bool EditarSeguro(SeguroModel seguro);
        bool EliminarSeguro(int codigo);
        SeguroModel ConsultarSeguro(int codigo);
        List<SeguroModel> ConsultarSeguros();
        List<PolizaModel> ConsultaPolizas(string cedula, int codigoSeguro);
        bool RegistroPoliza(int codigoPersona, int codigoSeguro);
    }
}
