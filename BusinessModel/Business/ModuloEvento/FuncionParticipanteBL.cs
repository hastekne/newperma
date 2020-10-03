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
    /// Clase para el acceso de datos a tblFuncionParticipante
    /// </summary>
    public class FuncionParticipanteBL
    {
        FuncionParticipanteDAL dal = new FuncionParticipanteDAL();

        ///<summary>
        ///Buscar por idFuncionPart en tblFunciónParticipanteDAL
        /// </summary>
        public List<tblFuncionParticipante> getFuncionEventoXidFunc(int idFuncion)
        {
            try
            {
                return dal.getFuncionEventoXidFunc(idFuncion);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Buscar por idEvento en tblFunciónParticipanteDAL
        /// </summary>
        public List<tblFuncionParticipante> getFuncionEventoXidEvento(int idEvento)
        {
            try
            {
                return dal.getFuncionEventoXidEvento(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Insertar registro en tblFunciónParticipanteDAL
        /// </summary>
        public bool agregarFuncionEvento(int idEvento, string funcion)
        {
            try
            {
                return dal.agregarFuncionEvento(idEvento, funcion);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Eliminar registro por idFuncionPart en tblFunciónParticipanteDAL
        /// </summary>
        public bool eliminarFuncionEvento(int idFuncion)
        {
            try
            {
                return dal.eliminarFuncionEvento(idFuncion);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Eliminar tods las funciones por el ID del evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public bool eliminarTodasFuncionEvento(int idEvento)
        {
            try
            {
                return dal.eliminarTodasFuncionEvento(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

    }
}
