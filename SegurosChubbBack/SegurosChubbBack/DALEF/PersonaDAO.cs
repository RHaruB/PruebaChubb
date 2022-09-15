using SegurosChubbBack.Clases;
using SegurosChubbBack.Models;
using System;
using System.Linq;

namespace SegurosChubbBack.DALEF
{
    public class PersonaDAO
    {
        public bool RegistrarPersona(PersonaModel persona)
        {
            bool exito = false;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var db_Persona = db.Personas.Where(s => s.CodigoPersona == persona.Codigo).FirstOrDefault();
                        if (db_Persona == null)
                        {
                            db_Persona = new Persona
                            {
                                CodigoPersona = MaximoRegistro(),
                                NombreCliente = persona.Nombre,
                                Cedula = persona.Cedula,
                                Edad = persona.Edad,
                                Estado = "A",
                                Telefono = persona.Telefono
                            };
                            db.Personas.Add(db_Persona);
                            db.SaveChanges();

                            trans.Commit();
                            exito = true;
                        }

                    }
                    catch (System.Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
            return exito;
        }

        public PersonaModel ConsultarPersona(int codigo)
        {
            PersonaModel persona = null;
            using (var db = new SegurosChubbContext())
            {
                var db_Persona = db.Personas.Where(s => s.CodigoPersona == codigo).FirstOrDefault();
                if (db_Persona != null)
                {
                    persona = new PersonaModel
                    {
                        Codigo = db_Persona.CodigoPersona,
                        Cedula = db_Persona.Cedula,
                        Edad = Convert.ToInt32(db_Persona.Edad),
                        Nombre = db_Persona.NombreCliente,
                        Telefono = db_Persona.Telefono
                    };
                }
            }
            return persona;
        }

        public int MaximoRegistro()
        {
            int maximo = 0;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    maximo = db.Personas.Max().CodigoPersona;
                    maximo = maximo == 0 ? 1 : maximo; // en caso de ser cero envia el 1 como codigo del primer registro
                }
            }
            return maximo;
        }
    }
}