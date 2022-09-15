using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SegurosChubbBack.Clases;
using SegurosChubbBack.Interface;
using System;

namespace SegurosChubbBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersona _Persona;

        public PersonaController(IPersona IPersona)
        {
            this._Persona = IPersona;
        }
        #region Metodos
        [HttpGet("ConsultarPersona")]
        public PersonaModel ConsultarPersona(int codigo)
        {
            try
            {
                var resp = _Persona.ConsultarPersona(codigo);

                return resp;

            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }
        [HttpPost("RegistrarPersona")]
        public bool RegistrarPersona(PersonaModel persona)
        {
            try
            {
                var resp = _Persona.RegistrarPersona(persona);

                return resp;

            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        #endregion
    }
}
