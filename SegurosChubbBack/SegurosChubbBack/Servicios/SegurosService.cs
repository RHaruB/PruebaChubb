using SegurosChubbBack.Clases;
using SegurosChubbBack.DALEF;
using SegurosChubbBack.Interface;

namespace SegurosChubbBack.Servicios
{
    public class SegurosService : ISeguros
    {
        public SegurosService()
        {

        }
        public bool RegistrarSeguro(SeguroModel seguro)
        {
            SeguroDAO seguroDAO = new SeguroDAO();
            return seguroDAO.RegistrarSeguro(seguro);
        }

        public bool EliminarSeguro(int codigo)
        {
            SeguroDAO seguroDAO = new SeguroDAO();
            return seguroDAO.EliminarSeguro(codigo);
        }

        public SeguroModel ConsultarSeguro(int codigo)
        {
            SeguroDAO seguroDAO = new SeguroDAO();
            return seguroDAO.ConsultarSeguro(codigo);
        }
    }
}