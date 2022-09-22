using SegurosChubbBack.Clases;
using SegurosChubbBack.Interface;
using SegurosChubbBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SegurosChubbBack.DALEF
{
    public class SeguroDAO
    {
        public bool RegistrarSeguro(SeguroModel seguro)
        {
            bool registrado = false;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var seguro_db = db.Seguros.Where(s => s.CodigoSeguro == seguro.Codigo).FirstOrDefault();
                        if (seguro_db == null)
                        {
                            int codigo = MaximoRegistro();
                            seguro_db = new Seguro
                            {
                                CodigoSeguro = codigo,
                                Descripcion = seguro.Descripcion,
                                Prima = Convert.ToDouble(seguro.Prima),
                                Estado = "A", //Siempre guarda estado activo
                                ValorAsegurado = Convert.ToDouble(seguro.Valor_Asegurado)
                            };
                            db.Seguros.Add(seguro_db);
                            db.SaveChanges();
                            trans.Commit();
                            registrado = true;
                        }
                    }
                    catch (System.Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                    return registrado;
                }
            }
        }
        public bool EditarSeguro(SeguroModel seguro)
        {
            bool registrado = false;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var seguro_db = db.Seguros.Where(s => s.CodigoSeguro == seguro.Codigo).FirstOrDefault();
                        if (seguro_db != null)
                        {
                            seguro_db.Descripcion = seguro.Descripcion;
                            seguro_db.ValorAsegurado = Convert.ToDouble(seguro.Valor_Asegurado);
                            seguro_db.Prima = Convert.ToDouble(seguro.Prima);

                            db.SaveChanges();
                            trans.Commit();
                            registrado = true;
                        }
                    }
                    catch (System.Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                    return registrado;
                }
            }
        }

        public SeguroModel ConsultarSeguro(int codigo)
        {
            SeguroModel seguro = null;
            using (var db = new SegurosChubbContext())
            {
                var seguro_base = db.Seguros.Where(s => s.CodigoSeguro == codigo && s.Estado == "A").FirstOrDefault();
                if (seguro_base != null)
                {
                    seguro = new SeguroModel
                    {
                        Codigo = seguro_base.CodigoSeguro,
                        Descripcion = seguro_base.Descripcion,
                        Prima = Convert.ToDecimal(seguro_base.Prima),
                        Valor_Asegurado = Convert.ToDecimal(seguro_base.ValorAsegurado)
                    };
                }
            }
            return seguro;
        }
        public List<SeguroModel> ConsultarSeguros()
        {
            List<SeguroModel> seguros = new List<SeguroModel>();
            SeguroModel seguro = new SeguroModel();
            using (var db = new SegurosChubbContext())
            {
                var seguros_base = db.Seguros.Where(s => s.Estado == "A").ToList();
                if (seguros_base != null)
                {
                    foreach (var seguro_base in seguros_base)
                    {
                        seguro = new SeguroModel
                        {
                            Codigo = seguro_base.CodigoSeguro,
                            Descripcion = seguro_base.Descripcion,
                            Prima = Convert.ToDecimal(seguro_base.Prima),
                            Valor_Asegurado = Convert.ToDecimal(seguro_base.ValorAsegurado)
                        };
                        seguros.Add(seguro);
                    }
                }
            }
            return seguros;
        }
        public bool EliminarSeguro(int codigo)
        {
            bool eliminado = false;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var existe_seguro_relacionado = db.Polizas.Where(s => s.CodigoSeguro == codigo).Any();

                        if (!existe_seguro_relacionado)
                        {
                            var seguro_base = db.Seguros.Where(s => s.CodigoSeguro == codigo && s.Estado == "A").FirstOrDefault();
                            if (seguro_base != null)
                            {
                                seguro_base.Estado = "I"; // inactivo
                                db.SaveChanges();
                                trans.Commit();
                                eliminado = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
            return eliminado;
        }

        public int MaximoRegistro()
        {
            int maximo = 0;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    maximo = db.Seguros.Select(s => s.CodigoSeguro).DefaultIfEmpty().Max();
                }
            }
            return maximo + 1;
        }

        public List<PolizaModel> ConsultaPolizas()
        {
            List<PolizaModel> Polizas = new List<PolizaModel>();
            PolizaModel poliza = new PolizaModel();
            using (var db = new SegurosChubbContext())
            {
                var seguros_base = (
                    from pol in db.Polizas
                    join Persona in db.Personas on pol.CodigoPersona equals Persona.CodigoPersona
                    join seguro in db.Seguros on pol.CodigoSeguro equals seguro.CodigoSeguro
                    where (pol.Estado == "A")
                    select new
                    {
                        pol.CodigoSeguro,
                        pol.CodigoPersona,
                        pol.CodigoPoliza,
                        Persona.Cedula,
                        Persona.NombreCliente,
                        seguro.Descripcion,
                        seguro.ValorAsegurado,
                        seguro.Prima,
                        pol.Estado 
                    }
                    ).ToList();
                //var seguros_base = db.Polizas.Where(s => s.Estado == "A").ToList();
                if (seguros_base != null)
                {
                    foreach (var seguro_base in seguros_base)
                    {
                        poliza = new PolizaModel
                        {
                            CodigoPoliza = seguro_base.CodigoPoliza,
                            CodigoPersona = Convert.ToInt32(seguro_base.CodigoPersona),
                            CodigoSeguro = Convert.ToInt32(seguro_base.CodigoSeguro),
                            cedulaPersona = seguro_base.Cedula,
                            NombrePersona = seguro_base.NombreCliente,
                            DescripcionSeguro = seguro_base.Descripcion,
                            Prima = Convert.ToDecimal(seguro_base.Prima),
                            Estado = seguro_base.Estado
                        };
                        Polizas.Add(poliza);
                    }
                }
            }
            return Polizas;
        }

        public List<PolizaModel> ConsultaPolizasPorCedula(string cedula)
        {
            List<PolizaModel> Polizas = new List<PolizaModel>();
            PolizaModel poliza = new PolizaModel();
            using (var db = new SegurosChubbContext())
            {
                var seguros_base = (
                    from pol in db.Polizas
                    join Persona in db.Personas on pol.CodigoPersona equals Persona.CodigoPersona
                    join seguro in db.Seguros on pol.CodigoSeguro equals seguro.CodigoSeguro
                    where (pol.Estado == "A" && Persona.Cedula == cedula)
                    select new
                    {
                        pol.CodigoSeguro,
                        pol.CodigoPersona,
                        pol.CodigoPoliza,
                        Persona.Cedula,
                        Persona.NombreCliente,
                        seguro.Descripcion,
                        seguro.ValorAsegurado,
                        seguro.Prima,
                        pol.Estado
                    }
                    ).ToList();
                //var seguros_base = db.Polizas.Where(s => s.Estado == "A").ToList();
                if (seguros_base != null)
                {
                    foreach (var seguro_base in seguros_base)
                    {
                        poliza = new PolizaModel
                        {
                            CodigoPoliza = seguro_base.CodigoPoliza,
                            CodigoPersona = Convert.ToInt32(seguro_base.CodigoPersona),
                            CodigoSeguro = Convert.ToInt32(seguro_base.CodigoSeguro),
                            cedulaPersona = seguro_base.Cedula,
                            NombrePersona = seguro_base.NombreCliente,
                            DescripcionSeguro = seguro_base.Descripcion,
                            Prima = Convert.ToDecimal(seguro_base.Prima),
                            Estado = seguro_base.Estado
                        };
                        Polizas.Add(poliza);
                    }
                }
            }
            return Polizas;
        }

        public List<PolizaModel> ConsultaPolizasPorSeguro(int codigo)
        {
            List<PolizaModel> Polizas = new List<PolizaModel>();
            PolizaModel poliza = new PolizaModel();
            using (var db = new SegurosChubbContext())
            {
                var seguros_base = (
                    from pol in db.Polizas
                    join Persona in db.Personas on pol.CodigoPersona equals Persona.CodigoPersona
                    join seguro in db.Seguros on pol.CodigoSeguro equals seguro.CodigoSeguro
                    where (pol.Estado == "A" && seguro.CodigoSeguro == codigo)
                    select new
                    {
                        pol.CodigoSeguro,
                        pol.CodigoPersona,
                        pol.CodigoPoliza,
                        Persona.NombreCliente,
                        Persona.Cedula,
                        seguro.Descripcion,
                        seguro.ValorAsegurado,
                        seguro.Prima,
                        pol.Estado
                    }
                    ).ToList();
                //var seguros_base = db.Polizas.Where(s => s.Estado == "A").ToList();
                if (seguros_base != null)
                {
                    foreach (var seguro_base in seguros_base)
                    {
                        poliza = new PolizaModel
                        {
                            cedulaPersona = seguro_base.Cedula,
                            CodigoPoliza = seguro_base.CodigoPoliza,
                            CodigoPersona = Convert.ToInt32(seguro_base.CodigoPersona),
                            CodigoSeguro = Convert.ToInt32(seguro_base.CodigoSeguro),
                            NombrePersona = seguro_base.NombreCliente,
                            DescripcionSeguro = seguro_base.Descripcion,
                            Prima = Convert.ToDecimal(seguro_base.Prima),
                            Estado = seguro_base.Estado
                        };
                        Polizas.Add(poliza);
                    }
                }
            }
            return Polizas;
        }

        public List<PolizaModel> ConsultaPolizasFiltroAmbos(string cedula, int codigoSeguro)
        {
            List<PolizaModel> Polizas = new List<PolizaModel>();
            PolizaModel poliza = new PolizaModel();
            using (var db = new SegurosChubbContext())
            {
                var seguros_base = (
                    from pol in db.Polizas
                    join Persona in db.Personas on pol.CodigoPersona equals Persona.CodigoPersona
                    join seguro in db.Seguros on pol.CodigoSeguro equals seguro.CodigoSeguro
                    where (pol.Estado == "A" && seguro.CodigoSeguro == codigoSeguro && Persona.Cedula == cedula)
                    select new
                    {
                        pol.CodigoSeguro,
                        pol.CodigoPersona,
                        pol.CodigoPoliza,
                        Persona.Cedula,
                        Persona.NombreCliente,
                        seguro.Descripcion,
                        seguro.ValorAsegurado,
                        seguro.Prima,
                        pol.Estado
                    }
                    ).ToList();
                //var seguros_base = db.Polizas.Where(s => s.Estado == "A").ToList();
                if (seguros_base != null)
                {
                    foreach (var seguro_base in seguros_base)
                    {
                        poliza = new PolizaModel
                        {
                            CodigoPoliza = seguro_base.CodigoPoliza,
                            CodigoPersona = Convert.ToInt32(seguro_base.CodigoPersona),
                            cedulaPersona = seguro_base.Cedula,
                            CodigoSeguro = Convert.ToInt32(seguro_base.CodigoSeguro),
                            NombrePersona = seguro_base.NombreCliente,
                            DescripcionSeguro = seguro_base.Descripcion,
                            Prima = Convert.ToDecimal(seguro_base.Prima),
                            Estado = seguro_base.Estado
                        };
                        Polizas.Add(poliza);
                    }
                }
            }
            return Polizas;
        }

        public int MaximoRegistroPoliza()
        {
            int maximo = 0;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    maximo = db.Polizas.Select(s => s.CodigoPoliza).DefaultIfEmpty().Max();
                }
            }
            return maximo + 1;
        }

        public bool RegistroPoliza(int codigoPersona, int codigoSeguro)
        {
            bool creado = false;
            using (var db = new SegurosChubbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        int codigo =MaximoRegistroPoliza();
                        Poliza pol = new Poliza
                        {
                            CodigoPoliza = codigo,
                            CodigoPersona = codigoPersona,
                            CodigoSeguro = codigoSeguro,
                            Estado = "A"
                        };
                        db.Polizas.Add(pol);
                        db.SaveChanges();
                        trans.Commit();
                        creado = true;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
            return creado;
        }
    }
}