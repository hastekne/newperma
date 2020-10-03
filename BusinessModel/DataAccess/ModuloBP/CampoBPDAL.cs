using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 
using BusinessModel.Persistence.BD;
using BusinessModel.Models.ModuloBP;

namespace BusinessModel.DataAccess.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblCampoBP
    /// </summary>
    public class CampoBPDAL
    {
        CampoBPML model = new CampoBPML();

        /// <summary>
        /// Obtiene todos los registros en tblCampoBP
        /// </summary>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCamposBP()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listCamposBP = (from cbp in db.tblCampoBP select cbp).ToList();
            }
            return model.listCamposBP;
        }

        /// <summary>
        /// Obtiene los registros en tblCampo donde nombre == parámetro nombreCampo
        /// </summary>
        /// <param name="nombreCampo"></param>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCamposBP(string nombreCampo)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listCamposBP = (from cbp in db.tblCampoBP where cbp.nombre==nombreCampo select cbp).ToList();
            }
            return model.listCamposBP;
        }

        /// <summary>
        /// Obtiene los registros en tblCampo donde idCampo == parámetro idCampo
        /// </summary>
        /// <param name="idCampo"></param>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCamposBP(int idCampo)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listCamposBP = (from cbp in db.tblCampoBP where cbp.idCampo==idCampo select cbp).ToList();
            }
            return model.listCamposBP;
        }

        /// <summary>
        /// Obtiene los registros en tblCampo donde subeArchivo == parámetro subeArchivo
        /// </summary>
        /// <param name="subeArchivo"></param>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCampoBPArchivo(bool subeArchivo)
        {
            using (var db =new SeguimientoPermanenciaEntities())
            {
                model.listCamposBP = (from cbp in db.tblCampoBP where cbp.subeArchivo == subeArchivo select cbp).ToList();
            }
            return model.listCamposBP;
        }

        /// <summary>
        /// Obtiene los registros en tblCampo donde idCampo == parámetro idcampo y 
        /// subeArchivo == parámetro subeArchivo
        /// </summary>
        /// <param name="idcampo"></param>
        /// <param name="subeArchivo"></param>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCampoBPArchivo(int idcampo, bool subeArchivo)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listCamposBP = (from cbp in db.tblCampoBP where cbp.idCampo==idcampo && cbp.subeArchivo == subeArchivo select cbp).ToList();
            }
            return model.listCamposBP;
        }

        /// <summary>
        /// Obtiene los registros en tblCampo donde idCampo sea diferente a 10
        /// </summary>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCamposBPSinAnexos()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listCamposBP = (from cbp in db.tblCampoBP where cbp.idCampo != 10 select cbp).ToList();
            }
            return model.listCamposBP;
        }
    }
}
