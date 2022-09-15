using Microsoft.AspNetCore.Mvc;
using SegurosChubbBack.Clases;
using SegurosChubbBack.Interface;
using System;

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
        [HttpGet("RegistrarSeguro")]
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
                throw;
            }
        }
        [HttpGet("EliminarSeguro")]
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
                throw;
            }
        }
        [HttpGet("ConsultarSeguro")]
        public SeguroModel ConsultarSeguro(int codigo)
        {
            {
                try
                {
                    var resp = _Seguros.ConsultarSeguro(codigo);
                    return resp;

                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
        }
    }
}
