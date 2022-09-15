using SegurosChubbBack.Clases;
using SegurosChubbBack.DALEF;
using SegurosChubbBack.Interface;

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
    }
}
