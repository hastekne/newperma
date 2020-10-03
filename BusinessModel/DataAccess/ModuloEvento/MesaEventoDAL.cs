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
    /// Clase para el acceso de datos a tblMesaTrabEvento
    /// </summary>
    public class MesaEventoDAL
    {
        MesaEventoML model = new MesaEventoML();

        ///<summary>
        /// Buscar por idMesaTrab en tblMesaTrabEvento
        /// </summary>
        public List<tblMesaTrabEvento> getMesaTevento(int idMesa)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMesasEvento = (from mt in db.tblMesaTrabEvento where mt.idMesaTrab == idMesa select mt).ToList();
            }
            return model.listaMesasEvento;
        }

        /// <summary>
        /// Buscar por idEvento en tblMesaTrabEvento
        /// </summary>
        public List<tblMesaTrabEvento> getMesaTeventoXevento(int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMesasEvento = (from mt in db.tblMesaTrabEvento where mt.idEvento == idEvento select mt).ToList();
            }
            return model.listaMesasEvento;
        }

        ///<summary>
        ///Insertar registro en tblMesaTrabEvento
        /// </summary>
        public bool agregarMesaEvento(int idEvento, string nombre, int cantidad, bool estado)
        {
            bool agrego = false;
            if (existeMT(idEvento, nombre) == false)
            {
                try
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblMesaTrabEvento newMesaT = new tblMesaTrabEvento()
                        {
                            idEvento = idEvento,
                            nombreMT = nombre,
                            cantidad = cantidad,
                            estado = estado,
                        };
                        db.tblMesaTrabEvento.Add(newMesaT);
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
        ///Actualizar registro en tblMesaTrabEvento
        /// </summary>
        public bool editarMesaEvento(int idMesa,string nom,int cant)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoMesaT = (from mt in db.tblMesaTrabEvento where mt.idMesaTrab == idMesa select mt).First<tblMesaTrabEvento>();
                    campoMesaT.nombreMT = nom;
                    campoMesaT.cantidad = cant;
                    edito = true;
                    db.SaveChanges();

                }
            }
            catch (Exception) { edito = false; }
            return edito;
        }

        ///<summary>
        ///Actualizar estado en tblMesaTrabEvento
        /// </summary>
        public bool editarMesaEstado(int idMesa, bool estado)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoMesaT = (from mt in db.tblMesaTrabEvento where mt.idMesaTrab == idMesa select mt).First<tblMesaTrabEvento>();
                    campoMesaT.estado = estado;
                    db.SaveChanges();

                }
            }
            catch (Exception) { edito = false; }
            return edito;
        }

        /// <summary>
        /// Calcular la cantidad aceptada en la mesa de trabajo por evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="idMT"></param>
        /// <returns></returns>
        public int cantidadAceptadaMesaT(int idEvento, int idMT)
        {
            model.cantidad = 0;
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMesasEvento = (from mt in db.tblMesaTrabEvento where mt.idEvento == idEvento && mt.idMesaTrab == idMT select mt).ToList();
            }
            foreach (var item in model.listaMesasEvento)
            {
                model.cantidad = item.cantidad;
            }
            return model.cantidad;
        }

        /// <summary>
        /// Obtiene el idMesaTrab en tblMesaTrabEvento donde idEvento == parámeto idEvento 
        /// y nombreMT == parámetro nombreMT
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="nombreMT"></param>
        /// <returns>model.idMesaTrab</returns>
        public int getIDmesaXnombreYevento(int idEvento, string nombreMT)
        {
            int idMT = 0;
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMesasEvento = (from mt in db.tblMesaTrabEvento where mt.idEvento == idEvento && mt.nombreMT == nombreMT && mt.estado == true select mt).ToList();
            }
            foreach (var item in model.listaMesasEvento)
            {
                idMT = item.idMesaTrab;
            }
            return idMT;
        }

        /// <summary>
        /// Validar si existe la Mesa de trabajo en el evento
        /// </summary>
        /// <param name="idMesa"></param>
        /// <returns></returns>
        public bool existeMT(int idEvento, string nomMT)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaMesasEvento = (from mt in db.tblMesaTrabEvento where mt.idEvento==idEvento && mt.nombreMT==nomMT select mt).ToList();
            }
            if (model.listaMesasEvento.Count != 0)
                return true;
            return false;
        }

    }
}
