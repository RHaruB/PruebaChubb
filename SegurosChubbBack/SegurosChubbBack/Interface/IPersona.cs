using SegurosChubbBack.Clases;

namespace SegurosChubbBack.Interface
{
    public interface IPersona
    {
        PersonaModel ConsultarPersona(int codigo);

        bool RegistrarPersona(PersonaModel persona);


    }
}
