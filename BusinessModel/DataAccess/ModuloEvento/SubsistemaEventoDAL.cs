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
    /// Clase para el Acceso de datos a tblSubsistemaEvento
    /// </summary>
    public class SubsistemaEventoDAL
    {
        SubsistemaEventoML model = new SubsistemaEventoML();

        ///<summary>
        ///Buscar por idSubsismtemaEvento en tblSubsistemaEvento
        /// </summary>
        public List<tblSubsistemaEvento> getSubsisEventXidSubsisEv(int idSubsisEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaSubsisEvento = (from se in db.tblSubsistemaEvento where se.idSubsistemaEv == idSubsisEvento select se).ToList();
            }
            return model.listaSubsisEvento;
        }

        ///<summary>
        ///Buscar por idEvento en tblSubsistemaEvento
        /// </summary>
        public List<tblSubsistemaEvento> getSubsisEventXidEven(int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaSubsisEvento = (from se in db.tblSubsistemaEvento where se.idEvento == idEvento select se).ToList();
            }
            return model.listaSubsisEvento;
        }

        ///<summary>
        ///Insertar registro en tblSubsistemaEvento
        /// </summary>
        public bool agregarSubSisEvento(int idEvento, string nomSubsis, int cant)
        {
            bool agrego = false;
            if (existeSubSisCantEvento(nomSubsis, idEvento) == false)
            {
                try
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblSubsistemaEvento newSubSisEvent = new tblSubsistemaEvento()
                        {
                            idEvento = idEvento,
                            nombreSubsistema = nomSubsis,
                            cantidad = cant,
                        };
                        db.tblSubsistemaEvento.Add(newSubSisEvent);
                        db.SaveChanges();
                    }
                    agrego = true;
                }
                catch (Exception)
                {
                    agrego = false;
                }
            }
            

            return agrego;
        }

        ///<summary>
        ///Eliminar registro en tblSubsistemaEvento 
        /// </summary>
        public bool eliminarSubsisEvent(int idSubsisEvent)
        {
            bool eliminado = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var subsisEvent = (from se in db.tblSubsistemaEvento where se.idSubsistemaEv == idSubsisEvent select se).First<tblSubsistemaEvento>();
                    db.tblSubsistemaEvento.Remove(subsisEvent);
                    eliminado = true;
                    db.SaveChanges();
                }
            }
            catch (Exception) { eliminado = false; }

            return eliminado;
        }

        /// <summary>
        /// Buscar por idEvento y nombre subsistema en tblSubsistemaEvento
        /// </summary>
        /// <returns></returns>
        public int cantidadAceptada(int idEvento,string nomSubsis)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaSubsisEvento = (from se in db.tblSubsistemaEvento where se.idEvento == idEvento && se.nombreSubsistema==nomSubsis select se).ToList();
            }
            foreach(var item in model.listaSubsisEvento)
            {
                model.cantidad = item.cantidad;
            }
            return model.cantidad;
        }

        /// <summary>
        /// Obtiene el valor booleano true/false para saber si existe el idEvento y el nombre del
        /// Subsis en la tabla tblSubsistemaEvento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="nomSubsis"></param>
        /// <returns>True=Existe nomSubSis en Evento || False=No existe nomSubSis en Evento</returns>
        public bool existeSubSis(int idEvento, string nomSubsis)
        {
            bool existe = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaSubsisEvento = (from se in db.tblSubsistemaEvento where se.idEvento == idEvento && se.nombreSubsistema == nomSubsis select se).ToList();
            }
            if (model.listaSubsisEvento.Count>0)
            {
                existe=true;
            }
            return existe;
        }

        /// <summary>
        /// Obtiene el valor booleano true/false para la existencia de el nombreSubSis en el evento
        /// </summary>
        /// <param name="subsis"></param>
        /// <param name="idEvento"></param>
        /// <returns>True=Existe nombreSubSis en evento || False=No Existe nombreSubSis en evento</returns>
        public bool existeSubSisCantEvento(string subsis, int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaSubsisEvento = (from se in db.tblSubsistemaEvento where se.nombreSubsistema == subsis && se.idEvento==idEvento select se).ToList();
            }
            if (model.listaSubsisEvento.Count != 0)
                return true;

            return false;
        }

    }
}
