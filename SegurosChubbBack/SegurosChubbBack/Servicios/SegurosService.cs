using SegurosChubbBack.Clases;
using SegurosChubbBack.DALEF;
using SegurosChubbBack.Interface;
using System.Collections.Generic;

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
        public bool EditarSeguro(SeguroModel seguro)
        {
            SeguroDAO seguroDAO = new SeguroDAO();
            return seguroDAO.EditarSeguro(seguro);
        }

        public bool EliminarSeguro(int codigo)
        {
            SeguroDAO seguroDAO = new SeguroDAO();
            return seguroDAO.EliminarSeguro(codigo);
        }
        public List<PolizaModel> ConsultaPolizas(string cedula, int codigoSeguro)
        {
            SeguroDAO seguroDAO = new SeguroDAO();
            if(!string.IsNullOrEmpty(cedula) && codigoSeguro > 0)
            {
                return seguroDAO.ConsultaPolizasFiltroAmbos(cedula, codigoSeguro);
            }else if( !string.IsNullOrEmpty(cedula) )
            {
                return seguroDAO.ConsultaPolizasPorCedula(cedula);
            }else if(codigoSeguro > 0)
            {
                return seguroDAO.ConsultaPolizasPorSeguro(codigoSeguro);
            }
            else
            {
                return seguroDAO.ConsultaPolizas();
            }
            
        }
        public SeguroModel ConsultarSeguro(int codigo)
        {
            SeguroDAO seguroDAO = new SeguroDAO();
            return seguroDAO.ConsultarSeguro(codigo);
        }

        public List<SeguroModel> ConsultarSeguros()
        {
            SeguroDAO seguroDAO = new SeguroDAO();
            return seguroDAO.ConsultarSeguros();
        }

        public bool RegistroPoliza(int codigoPersona, int codigoSeguro)
        {
            SeguroDAO seguroDAO = new SeguroDAO();
            return seguroDAO.RegistroPoliza(codigoPersona,codigoSeguro);
        }


    }
}