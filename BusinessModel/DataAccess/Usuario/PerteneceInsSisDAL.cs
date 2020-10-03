using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Models.Usuario;
using BusinessModel.Persistence.BD;

namespace BusinessModel.DataAccess.Usuario
{
    /// <summary>
    /// Clase para el acceso de datos a tblPerteneceInsSis
    /// </summary>
    public class PerteneceInsSisDAL
    {
        PerteneceInsSisML model = new PerteneceInsSisML();

        /// <summary>
        /// Obtiene todos los registros en tblPerteneceInsSis
        /// </summary>
        /// <returns>model.listaInsSis</returns>
        public List<tblPerteneceInsSis> getInstiSub()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaInsSis = (from insu in db.tblPerteneceInsSis select insu).ToList();
            }
            return model.listaInsSis;
        }

        /// <summary>
        /// Obtiene todos los datos en tblPerteneceInsSis donde idUserRevisor == parámetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>model.listaInsSis</returns>
        public List<tblPerteneceInsSis> getInstiSub(string revisor)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaInsSis = (from insu in db.tblPerteneceInsSis where insu.idUserRevisor==revisor select insu).ToList();
            }
            return model.listaInsSis;
        }

        /// <summary>
        /// Actializa la institucionSistema en tblPerteneceInsSis donde idUserRevisor == parámetro revisor
        /// </summary>
        /// <param name="revisor"></param>
        /// <param name="insSis"></param>
        /// <returns>True=Update exitoso || False=Falla en Update</returns>
        public bool editarPerInsSis(string revisor, string insSis)
        {

            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var insSis2 = (from pis in db.tblPerteneceInsSis where pis.idUserRevisor == revisor select pis).First<tblPerteneceInsSis>();
                    insSis2.institucionSistema = insSis;
                    db.SaveChanges();
                }
                actualizo = true;
            }
            catch (Exception ex)
            {
                actualizo = false;
                throw ex;
            }

            return actualizo;
        }

        /// <summary>
        /// Obtiene un valor booleano si existe o no el idUserRevisor en tblPerteneceInsSis
        /// </summary>
        /// <param name="revisor"></param>
        /// <returns>True=Existe revisor en tblPerteneceInsSis || False=No existe revisor en tblPerteneceInsSis</returns>
        public bool existeUserInsSis(string revisor)
        {
            bool existe = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaInsSis = (from pis in db.tblPerteneceInsSis where pis.idUserRevisor == revisor select pis).ToList();
            }
            if (model.listaInsSis.Count > 0)
            {
                existe = true;
            }
            return existe;
        }

        /// <summary>
        /// Nuevo Registro en tblPerteneceInsSis,se genera el registro solo si existeUserInsSis(revisor)==false
        /// </summary>
        /// <param name="revisor"></param>
        /// <param name="insSis"></param>
        /// <returns>True=Registro exitoso || False=Error en el registro</returns>
        public bool agregarUserInsSis(string revisor, string insSis)
        {
            bool agrego = false;
            try
            {
                if (existeUserInsSis(revisor) == false)
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblPerteneceInsSis newInsSis = new tblPerteneceInsSis()
                        {
                            idUserRevisor = revisor,
                            institucionSistema = insSis,
                        };
                        db.tblPerteneceInsSis.Add(newInsSis);
                        db.SaveChanges();
                    }
                    agrego = true;
                }

            }
            catch (Exception)
            {
                agrego = false;
                throw;
            }

            return agrego;
        }
    }
}
