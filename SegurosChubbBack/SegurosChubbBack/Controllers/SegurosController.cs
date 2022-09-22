using Microsoft.AspNetCore.Mvc;
using SegurosChubbBack.Clases;
using SegurosChubbBack.Interface;
using System;
using System.Collections.Generic;

namespace SegurosChubbBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosController : ControllerBase
    {
        private readonly ISeguros _Seguros;


        public SegurosController(ISeguros iseguros)
        {
            this._Seguros = iseguros;
        }
        
        [HttpPost("RegistrarSeguro")]
        public bool RegistrarSeguro(SeguroModel seguro)
        {
            try
            {
                var resp = _Seguros.RegistrarSeguro(seguro);
                return resp;

            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost("EditarSeguro")]
        public bool EditarSeguro(SeguroModel seguro)
        {
            try
            {
                var resp = _Seguros.EditarSeguro(seguro);
                return resp;

            }
            catch (Exception)
            {
                return false;
            }
        }

        
        [HttpPut("EliminarSeguro")]
        public bool EliminarSeguro(int codigo)
        {
            try
            {
                var resp = _Seguros.EliminarSeguro(codigo);
                return resp;

            }
            catch (Exception)
            {
                return false;
            }
        }
        
        [HttpGet("ConsultarSeguro")]
        public SeguroModel ConsultarSeguro(int codigo)
        {
            try
            {
                var resp = _Seguros.ConsultarSeguro(codigo);
                return resp;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        [HttpGet("ConsultarSeguros")]
        public List<SeguroModel> ConsultarSeguros()
        {
            try
            {
                var resp = _Seguros.ConsultarSeguros();
                return resp;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPost("ConsultaPolizas")]
        public List<PolizaModel> ConsultaPolizas(string cedula, int codigoSeguro)
        {
            try
            {
                var resp = _Seguros.ConsultaPolizas(cedula,  codigoSeguro);
                return resp;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPost("RegistroPolizas")]
        public bool RegistroPoliza(int codigoPersona, int codigoSeguro)
        {
            try
            {
                return _Seguros.RegistroPoliza(codigoPersona, codigoSeguro);
            }
            catch (Exception)
            {
                return false;
            }
        }
    
    }
}
