using SegurosChubbBack.Clases;
using SegurosChubbBack.Models;
using System;
using System.Collections.Generic;
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
                        int codigo = MaximoRegistro();
                        var db_Persona = db.Personas.Where(s => s.Cedula == persona.Cedula).FirstOrDefault();
                        if (db_Persona == null)
                        {
                            db_Persona = new Persona
                            {
                                CodigoPersona = codigo,
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
                    catch (System.Exception ex)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
            return exito;
        }

        public bool EditarPersona(PersonaModel persona)
        {
            bool exito = false;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var db_Persona = db.Personas.Where(s => s.CodigoPersona == persona.Codigo).FirstOrDefault();
                        if (db_Persona != null)
                        {
                            db_Persona.NombreCliente = persona.Nombre;
                            db_Persona.Telefono = persona.Telefono;
                            db_Persona.Cedula = persona.Cedula;
                            db_Persona.Edad = persona.Edad;
                            

                            db.SaveChanges();

                            trans.Commit();
                            exito = true;
                        }

                    }
                    catch (System.Exception )
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
            return exito;
        }
        
        public bool EliminarPersona(int persona)
        {
            bool exito = false;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        
                        
                        var db_Persona = db.Personas.Where(s => s.CodigoPersona == persona).FirstOrDefault();
                        if (db_Persona != null)
                        {
                            var db_persona_poliza = db.Polizas.Where(s => s.CodigoPersona == db_Persona.CodigoPersona).Any();
                            if (!db_persona_poliza)
                            {
                                db_Persona.Estado = "I";

                                db.SaveChanges();

                                trans.Commit();
                                exito = true;
                            }
                            else
                            {
                                exito = false;
                            }
                            
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
                    maximo = db.Personas.Select(s=>s.CodigoPersona).DefaultIfEmpty().Max();                    
                }
            }
            return maximo +1;
        }

        public List<PersonaModel> ConsultarPersonas()
        {
            List<PersonaModel> personas = new List<PersonaModel>();
            using (var db = new SegurosChubbContext())
            {
                var db_Personas = db.Personas.Where(s=>s.Estado == "A").ToList();
                foreach(var db_persona in db_Personas)
                {
                    PersonaModel persona = new PersonaModel
                    {
                        Cedula = db_persona.Cedula,
                        Codigo = db_persona.CodigoPersona,
                        Nombre = db_persona.NombreCliente,
                        Edad = Convert.ToInt32( db_persona.Edad),
                        Telefono = db_persona.Telefono
                    };
                    personas.Add(persona);
                }
            }
            return personas;
        }
    }
}