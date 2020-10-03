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
    /// Clase para el acceso de datos a tblFuncionParticipante
    /// </summary>
    public class FuncionParticipanteDAL
    {
        FuncionParticipanteML model = new FuncionParticipanteML();

        ///<summary>
        ///Buscar por idFuncionPart en tblFunciónParticipanteDAL
        /// </summary>
        public List<tblFuncionParticipante> getFuncionEventoXidFunc(int idFuncion)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaFuncionPart = (from fe in db.tblFuncionParticipante where fe.idFuncionPart == idFuncion select fe).ToList();
            }
            return model.listaFuncionPart;
        }

        ///<summary>
        ///Buscar por idEvento en tblFunciónParticipanteDAL
        /// </summary>
        public List<tblFuncionParticipante> getFuncionEventoXidEvento(int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaFuncionPart = (from fe in db.tblFuncionParticipante where fe.idEvento == idEvento select fe).ToList();
            }
            return model.listaFuncionPart;
        }

        ///<summary>
        ///Insertar registro en tblFunciónParticipanteDAL
        /// </summary>
        public bool agregarFuncionEvento(int idEvento, string funcion)
        {
            bool agrego = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    tblFuncionParticipante newFuncion = new tblFuncionParticipante()
                    {
                        idEvento = idEvento,
                        nomFuncion=funcion,
                    };
                    db.tblFuncionParticipante.Add(newFuncion);
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

        ///<summary>
        ///Eliminar registro por idFuncionPart en tblFunciónParticipanteDAL
        /// </summary>
        public bool eliminarFuncionEvento(int idFuncion)
        {
            bool eliminado = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var funcion = (from fe in db.tblFuncionParticipante where fe.idFuncionPart == idFuncion select fe).First<tblFuncionParticipante>();
                    db.tblFuncionParticipante.Remove(funcion);
                    eliminado = true;
                    db.SaveChanges();
                }
            }
            catch (Exception) { eliminado = false; }

            return eliminado;
        }

        /// <summary>
        /// Eliminar tods las funciones por el ID del evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public bool eliminarTodasFuncionEvento(int idEvento)
        {
            bool eliminado = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var funcion = from fe in db.tblFuncionParticipante where fe.idEvento == idEvento select fe;
                    db.tblFuncionParticipante.RemoveRange(funcion);
                    eliminado = true;
                    db.SaveChanges();
                }
            }
            catch (Exception) { eliminado = false; }

            return eliminado;
        }


        ///<summary>
        ///Ver si la función existe para el evento
        ///</summary>
        ///
        public bool existeFuncion(int idEvento,string func)
        {
            bool existe = false;

            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaFuncionPart = (from fe in db.tblFuncionParticipante where fe.idEvento == idEvento && fe.nomFuncion==func select fe).ToList();
            }
            if (model.listaFuncionPart.Count > 0)
                existe = true;

            return existe;
        }

        

    }
}
