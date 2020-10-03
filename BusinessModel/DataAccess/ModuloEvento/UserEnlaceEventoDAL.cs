using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Models.ModuloEvento;
using BusinessModel.Persistence.BD;

namespace BusinessModel.DataAccess.ModuloEvento
{
    /// <summary>
    /// Clase para el Acceso de datos a tblUserEnlaceEvento
    /// </summary>
    public class UserEnlaceEventoDAL
    {
        UserEnlaceEventoML model = new UserEnlaceEventoML();

        ///<summary>
        ///Buscar por idUserEnlaceEve en tblUserEnlaceEvento
        /// </summary>
        public List<tblUserEnlaceEvento> getUserEnlaceEXid(int idUserEnlaceE)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listUserEnlaceEvento = (from uee in db.tblUserEnlaceEvento where uee.idUserEnlaceEve == idUserEnlaceE select uee).ToList();
            }
            return model.listUserEnlaceEvento;
        }

        ///<summary>
        ///Buscar por idEvento e idUserEnlace en tblUserEnlaceEvento
        /// </summary>
        public List<tblUserEnlaceEvento> getUserEnlaceEXidEveidUE(int idEvento, string idUser)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listUserEnlaceEvento = (from uee in db.tblUserEnlaceEvento where uee.idEvento == idEvento && uee.idUserEnlace==idUser select uee).ToList();
            }
            return model.listUserEnlaceEvento;
        }

        ///<summary>
        ///Buscar por idUserEnlace en tblUserEnlaceEvento
        /// </summary>
        public List<tblUserEnlaceEvento> getUserEnlaceEidUE(string idUser)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listUserEnlaceEvento = (from uee in db.tblUserEnlaceEvento where uee.idUserEnlace == idUser select uee).ToList();
            }
            return model.listUserEnlaceEvento;
        }

        ///<summary>
        ///Insertar registro en tblUserEnlaceEvento
        /// </summary>
        public bool agregarUserEnalceEve(string idTipoReg,int idEvento,string idUser)
        {
            bool agrego = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    tblUserEnlaceEvento newUserEnlaceEvento = new tblUserEnlaceEvento()
                    {
                        idTipoRegistro=idTipoReg,
                        idEvento = idEvento,
                        idUserEnlace=idUser,
                    };
                    db.tblUserEnlaceEvento.Add(newUserEnlaceEvento);
                    db.SaveChanges();
                }
                agrego = true;
            }
            catch (Exception)
            {
                agrego = false;
            }

            return agrego;
        }
    }
}
