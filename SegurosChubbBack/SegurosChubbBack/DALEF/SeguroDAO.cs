using SegurosChubbBack.Clases;
using SegurosChubbBack.Models;
using System;
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
                            seguro_db = new Seguro
                            {
                                CodigoSeguro = MaximoRegistro(),
                                Descripcion = seguro.Descripcion,
                                Prima = Convert.ToDouble(seguro.Prima),
                                Estado = seguro.Estado,
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
        public SeguroModel ConsultarSeguro(int codigo)
        {
            SeguroModel seguro = null;
            using (var db = new SegurosChubbContext())
            {
                var seguro_base = db.Seguros.Where(s => s.CodigoSeguro == codigo && s.Estado == "A").FirstOrDefault();
                if(seguro_base != null)
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
                            if(seguro_base != null)
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
                    maximo = db.Seguros.Max().CodigoSeguro;
                    maximo = maximo == 0 ? 1 : maximo; // en caso de ser cero envia el 1 como codigo del primer registro
                }
            }
            return maximo;
        }
    }
}