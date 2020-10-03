using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.DataAccess.ModuloEvento;
using BusinessModel.Persistence.BD;

namespace BusinessModel.Business.ModuloEvento
{
    /// <summary>
    /// Clase para el acceso de datos a tblRegistroEvento
    /// </summary>
    public class RegistroEventoBL
    {
        RegistroEventoDAL dal = new RegistroEventoDAL();

        ///<summary>
        ///Buscar por folio en tblRegistroEvento
        /// </summary>
        public List<tblRegistroEvento> getRegistroEXfolio(int folio)
        {
            try
            {
                return dal.getRegistroEXfolio(folio);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Buscar por idEvento en tblRegistroEvento
        /// </summary>
        public List<tblRegistroEvento> getRegistroEXidEv(int idEvento)
        {
            try
            {
                return dal.getRegistroEXidEv(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Buscar por idTipoRegistro en tblRegistroEvento
        /// </summary>
        public List<tblRegistroEvento> getRegistroEXidTR(string idTR)
        {
            try
            {
                return dal.getRegistroEXidTR(idTR);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }


        ///<summary>
        ///Buscar por idEvento e idTipoRegistro en tblRegistroEvento
        /// </summary>
        public List<tblRegistroEvento> getRegistroEXidEvidTR(int idEvento, string idTR)
        {
            try
            {
                return dal.getRegistroEXidEvidTR(idEvento,idTR);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }


        ///<summary>
        ///Insertar Registro en tblRegistroEvento para Registro INDIVIDUAL
        /// </summary>
        public string agregarUserEnalceEve(int idEvento, string idTR, string cct, string nomplantel, string municipio, string regSEG, string nivel, string instisub, string curp, string nom, string ap1, string ap2, string email, string tel, string sexo, string funcion, List<int> listaIDmesa)
        {
            try
            {
                return dal.agregarUserEnalceEve(idEvento, idTR, cct, nomplantel, municipio, regSEG, nivel, instisub, curp, nom, ap1, ap2, email, tel, sexo, funcion, listaIDmesa);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Realizar Registro masivo a traves de una transacción con un DataTable
        /// </summary>
        public string registroMasivo(int idEvento, string idTR, DataTable dt)
        {
            try
            {
                return dal.registroMasivo(idEvento, idTR,dt);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }


    }
}
