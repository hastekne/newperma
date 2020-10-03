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
    /// Clase para el Acceso de datos a tblSubsistemaEvento
    /// </summary>
    public class SubsistemaEventoBL
    {
        SubsistemaEventoDAL dal = new SubsistemaEventoDAL();

        ///<summary>
        ///Buscar por idSubsismtemaEvento en tblSubsistemaEvento
        /// </summary>
        public List<tblSubsistemaEvento> getSubsisEventXidSubsisEv(int idSubsisEvento)
        {
            try
            {
                return dal.getSubsisEventXidSubsisEv(idSubsisEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Buscar por idEvento en tblSubsistemaEvento
        /// </summary>
        public List<tblSubsistemaEvento> getSubsisEventXidEven(int idEvento)
        {
            try
            {
                return dal.getSubsisEventXidEven(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Insertar registro en tblSubsistemaEvento
        /// </summary>
        public bool agregarSubSisEvento(int idEvento, string nomSubsis, int cant)
        {
            try
            {
                return dal.agregarSubSisEvento(idEvento,nomSubsis,cant);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Eliminar registro en tblSubsistemaEvento 
        /// </summary>
        public bool eliminarSubsisEvent(int idSubsisEvent)
        {
            try
            {
                return dal.eliminarSubsisEvent(idSubsisEvent);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Buscar por idEvento y nombre subsistema en tblSubsistemaEvento
        /// </summary>
        /// <returns></returns>
        public int cantidadAceptada(int idEvento, string nomSubsis)
        {
            try
            {
                return dal.cantidadAceptada(idEvento,nomSubsis);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
     }
}
