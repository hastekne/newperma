using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Models.Global;
using BusinessModel.Persistence.BD;

namespace BusinessModel.DataAccess.Global
{
    /// <summary>
    /// Clase con métodos para el acceso a tblCEO
    /// Clase temporal, el acceso a datos sera por merdio de la API
    /// </summary>
    public class CEODAL
    {
        CEOML model = new CEOML();

        /// <summary>
        /// Select * tblCEO 
        /// </summary>
        /// <returns>model.listCEO</returns>
        public List<tblCEO> getCEO()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listCEO = (from c in db.tblCEO select c).ToList();
            }
            return model.listCEO;
        }

        /// <summary>
        /// Select * from tblCEO where idCEO= parámetro idCEO específicado
        /// </summary>
        /// <param name="idCEO"></param>
        /// <returns>model.listCEO</returns>
        public List<tblCEO> getCEO(int idCEO)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listCEO = (from c in db.tblCEO where c.idCEO==idCEO select c).ToList();
            }
            return model.listCEO;
        }

        /// <summary>
        /// Select * from tblCEO where claveCentroTrabajo= parámetro claveCT específicado
        /// </summary>
        /// <param name="claveCT"></param>
        /// <returns>model.listCEO</returns>
        public List<tblCEO> getCEO(string claveCT)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listCEO = (from c in db.tblCEO where c.claveCentroTrabajo == claveCT select c).ToList();
            }
            return model.listCEO;
        }

        /// <summary>
        /// Select * from tblCEO where nombreCentroTrabajo= parámetro nombre específicado
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>model.listCEO</returns>
        public List<tblCEO> BusXnombreCT(string nombre)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listCEO = (from c in db.tblCEO where c.nombreCentroTrabajo == nombre select c).ToList();
            }
            return model.listCEO;
        }

        /// <summary>
        /// Obtiene el idCEO con el parámetro claveCEO
        /// </summary>
        /// <param name="claveCEO"></param>
        /// <returns>model.idCEO</returns>
        public int getIdCEO(string claveCEO)
        {
            using(var db=new SeguimientoPermanenciaEntities())
            {
                //model.idCEO = Int32.Parse((from c in db.tblCEO where c.claveCentroTrabajo == claveCEO select c.idCEO).ToString());
                model.listCEO = (from c in db.tblCEO where c.claveCentroTrabajo == claveCEO select c).ToList();
                foreach(var item in model.listCEO)
                {
                    model.idCEO = item.idCEO;
                }
            }
            return model.idCEO;
        }

        /// <summary>
        /// Obtiene la Clave del centro de trabajo con el idCEO
        /// </summary>
        /// <param name="idCEO"></param>
        /// <returns>model.claveCentroTrabajo</returns>
        public string getCVCEO(int idCEO)
        {
            string cvCEO = "";
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listCEO = (from c in db.tblCEO where c.idCEO==idCEO select c).ToList();
            }
            foreach(var item in model.listCEO)
            {
                cvCEO = item.claveCentroTrabajo;
            }
            return cvCEO;
        }
    }
}
