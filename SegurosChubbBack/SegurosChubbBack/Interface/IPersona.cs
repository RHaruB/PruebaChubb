using SegurosChubbBack.Clases;
using System.Collections.Generic;

namespace SegurosChubbBack.Interface
{
    public interface IPersona
    {
        PersonaModel ConsultarPersona(int codigo);

        bool RegistrarPersona(PersonaModel persona);

        bool EditarPersona(PersonaModel persona);

        bool EliminarPersona(int persona);

        List<PersonaModel> ConsultarPersonas();
    }
}
