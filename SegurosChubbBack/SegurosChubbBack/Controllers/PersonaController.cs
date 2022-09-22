using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SegurosChubbBack.Clases;
using SegurosChubbBack.Interface;
using System;
using System.Collections.Generic;

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
        
        [HttpGet("ConsultarPersonas")]
        public List<PersonaModel> ConsultarPersonas()
        {
            try
            {
                var resp = _Persona.ConsultarPersonas();

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

        [HttpPost("EditarPersona")]
        public bool EditarPersona(PersonaModel persona)
        {
            try
            {
                var resp = _Persona.EditarPersona(persona);

                return resp;

            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        [HttpPost("EliminarPersona")]
        public bool EliminarPersona(PersonaModel persona)
        {
            try
            {
                var resp = _Persona.EliminarPersona(persona.Codigo);

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
