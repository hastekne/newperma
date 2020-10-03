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
    /// Clase para el Acceso de datos a tblEscuelaEvento
    /// </summary>
    public class EscuelaEventoDAL
    {
        EscuelaEventoML model = new EscuelaEventoML();

        /// <summary>
        /// Busqueda en tblEscuelaEvento por CCT
        /// </summary>
        public List<tblEscuelaEvento> getEscuelaEventoXCCT(int CCT)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEscuelasEvento = (from ee in db.tblEscuelaEvento where ee.idCCT == CCT select ee).ToList();
            }
            return model.listaEscuelasEvento;
        }

        /// <summary>
        /// Busqueda en tblEscuelaEvento por idEvento
        /// </summary>
        public List<tblEscuelaEvento> getEscuelaEventoXidEven(int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEscuelasEvento = (from ee in db.tblEscuelaEvento where ee.idEvento == idEvento select ee).ToList();
            }
            return model.listaEscuelasEvento;
        }

        ///<summary>
        ///Busqueda en tblEscuelaEvento por CCT e idEvento
        /// </summary>
        public List<tblEscuelaEvento> getEscuelaEventos(int idEvento, int cct)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEscuelasEvento = (from ee in db.tblEscuelaEvento where ee.idEvento == idEvento && ee.idCCT==cct select ee).ToList();
            }
            return model.listaEscuelasEvento;
        }

        ///<summary>
        ///Regresar cantidad dentro de tblEscuelaEvento por CCT e idEvento
        /// </summary>
        public int cantidadAceptadaEscuela(int idEvento, int cct)
        {
            int cantidad = 0;
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEscuelasEvento = (from ee in db.tblEscuelaEvento where ee.idEvento == idEvento && ee.idCCT == cct select ee).ToList();
            }
            foreach(var item in model.listaEscuelasEvento)
            {
                cantidad = item.cantidad;
            }
            return cantidad;
        }


        ///<summary>
        ///Insertar en tblEscuelaEvento
        /// </summary>
        public bool agregarCantidadEscuela(int cct, int idEvento, int cantidad)
        {
            bool agrego = false;
            if (existeEscuelaCCT(idEvento, cct) == false)
            {
                try
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblEscuelaEvento newCantidadEscuela = new tblEscuelaEvento()
                        {
                            idEvento = idEvento,
                            idCCT = cct,
                            cantidad = cantidad,
                        };
                        db.tblEscuelaEvento.Add(newCantidadEscuela);
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
        ///Actualizar cantidad dentro de tblEscuela evento, por CCT e idEvento
        /// </summary>
        public bool editarCantidadEscuela(int cct, int idEvento, int cantidad)
        {
            bool edito = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var campoCantidad = (from ee in db.tblEscuelaEvento where ee.idCCT == cct && ee.idEvento==idEvento select ee).First<tblEscuelaEvento>();
                    campoCantidad.cantidad = cantidad;
                    db.SaveChanges();

                }
            }
            catch (Exception) { edito = false; }
            return edito;
        }

        ///<summary>
        //Eliminar registro dentro de tblEscuelaEvento, por CCT e idEvento
        ///</summary>
        public bool eliminarCantEscuela(int cct, int idEvento)
        {
            bool eliminado = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var cantidadEsc = (from ee in db.tblEscuelaEvento where ee.idCCT == cct && ee.idEvento == idEvento select ee).First<tblEscuelaEvento>();
                    db.tblEscuelaEvento.Remove(cantidadEsc);
                    eliminado = true;
                    db.SaveChanges();
                }
            }
            catch (Exception) { eliminado = false; }
           
            return eliminado;
        }

        ///<summary>
        //Eliminar registro dentro de tblEscuelaEvento, por idEscuela
        ///</summary>
        public bool eliminarCantEscuela(int idEscuela)
        {
            bool eliminado = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var cantidadEsc = (from ee in db.tblEscuelaEvento where ee.idEscuela==idEscuela select ee).First<tblEscuelaEvento>();
                    db.tblEscuelaEvento.Remove(cantidadEsc);
                    eliminado = true;
                    db.SaveChanges();
                }
            }
            catch (Exception) { eliminado = false; }

            return eliminado;
        }

        ///<summary>
        ///Regresar cantidad dentro de tblEscuelaEvento por CCT e idEvento
        /// </summary>
        public bool existeEscuelaCCT(int idEvento, int cct)
        {
            bool existe = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaEscuelasEvento = (from ee in db.tblEscuelaEvento where ee.idEvento == idEvento && ee.idCCT == cct select ee).ToList();
            }
            if (model.listaEscuelasEvento.Count>0)
            {
                existe = true;
            }
            return existe;
        }

        /// <summary>
        /// Retorna lista dee la clase TablaEscuelaCantidad para mostrar las Escuelas y cantidad por el idEvento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public List<TablaEscuelaCantidad> getEventoTabla(int idEvento)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.tablaEscuelaCantida = (from e in db.tblEscuelaEvento join cct in db.tblCEO on e.idCCT equals cct.idCEO where e.idEvento==idEvento select new TablaEscuelaCantidad { idEscuela=e.idEscuela,nombreEscuela=cct.nombreCentroTrabajo,cantidad=e.cantidad,idCCT=e.idCCT,idEvento=e.idEvento}).ToList();
            }
            return model.tablaEscuelaCantida;
        }
    }
}
