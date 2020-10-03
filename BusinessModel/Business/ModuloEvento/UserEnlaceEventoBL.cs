using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.DataAccess.ModuloEvento;
using BusinessModel.Persistence.BD;

namespace BusinessModel.Business.ModuloEvento
{
    /// <summary>
    /// Clase para el Acceso de datos a tblUserEnlaceEvento
    /// </summary>
    public class UserEnlaceEventoBL
    {
        UserEnlaceEventoDAL dal =new  UserEnlaceEventoDAL();

        ///<summary>
        ///Buscar por idUserEnlaceEve en tblUserEnlaceEvento
        /// </summary>
        public List<tblUserEnlaceEvento> getUserEnlaceEXid(int idUserEnlaceE)
        {
            try
            {
                return dal.getUserEnlaceEXid(idUserEnlaceE);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Buscar por idEvento e idUserEnlace en tblUserEnlaceEvento
        /// </summary>
        public List<tblUserEnlaceEvento> getUserEnlaceEXidEveidUE(int idEvento, string idUser)
        {
            try
            {
                return dal.getUserEnlaceEXidEveidUE(idEvento,idUser);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Buscar por idUserEnlace en tblUserEnlaceEvento
        /// </summary>
        public List<tblUserEnlaceEvento> getUserEnlaceEidUE(string idUser)
        {
            try
            {
                return dal.getUserEnlaceEidUE(idUser);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Insertar registro en tblUserEnlaceEvento
        /// </summary>
        public bool agregarUserEnalceEve(string idTipoReg, int idEvento, string idUser)
        {
            try
            {
                return dal.agregarUserEnalceEve(idTipoReg,idEvento,idUser);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }


        }
}
