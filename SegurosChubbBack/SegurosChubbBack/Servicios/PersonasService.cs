using SegurosChubbBack.Clases;
using SegurosChubbBack.DALEF;
using SegurosChubbBack.Interface;
using SegurosChubbBack.Models;
using System.Collections.Generic;

namespace SegurosChubbBack.Servicios
{
    public class PersonasService : IPersona
    {
        public PersonasService()
        {            
        }
        public PersonaModel ConsultarPersona(int codigo)
        {
            PersonaDAO personaDAO = new PersonaDAO();
            return personaDAO.ConsultarPersona(codigo);
        }

        public bool RegistrarPersona(PersonaModel persona)
        {
            PersonaDAO personaDAO = new PersonaDAO();
            return personaDAO.RegistrarPersona(persona);
        }
        public bool EditarPersona(PersonaModel persona)
        {
            PersonaDAO personaDAO = new PersonaDAO();
            return personaDAO.EditarPersona(persona);
        }
        public bool EliminarPersona(int persona)
        {
            PersonaDAO personaDAO = new PersonaDAO();
            return personaDAO.EliminarPersona(persona);
        }
        public List<PersonaModel> ConsultarPersonas()
        {
            PersonaDAO personaDAO = new PersonaDAO();
            return personaDAO.ConsultarPersonas();
        }
    }
}
