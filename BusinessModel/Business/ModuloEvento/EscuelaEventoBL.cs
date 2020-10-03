using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.DataAccess.ModuloEvento;
using BusinessModel.Persistence.BD;
using BusinessModel.Models.ModuloEvento;

namespace BusinessModel.Business.ModuloEvento
{
    /// <summary>
    /// Clase para el Acceso de datos a tblEscuelaEvento
    /// </summary>
    public class EscuelaEventoBL
    {
        EscuelaEventoDAL dal = new EscuelaEventoDAL();

        /// <summary>
        /// Busqueda en tblEscuelaEvento por CCT
        /// </summary>
        public List<tblEscuelaEvento> getEscuelaEventoXCCT(int CCT)
        {
            try
            {
                return dal.getEscuelaEventoXCCT(CCT);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Busqueda en tblEscuelaEvento por idEvento
        /// </summary>
        public List<tblEscuelaEvento> getEscuelaEventoXidEven(int idEvento)
        {
            try
            {
                return dal.getEscuelaEventoXidEven(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Busqueda en tblEscuelaEvento por CCT e idEvento
        /// </summary>
        public List<tblEscuelaEvento> getEscuelaEventos(int idEvento, int cct)
        {
            try
            {
                return dal.getEscuelaEventos(idEvento, cct);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Regresar cantidad dentro de tblEscuelaEvento por CCT e idEvento
        /// </summary>
        public int cantidadAceptadaEscuela(int idEvento, int cct)
        {
            try
            {
                return dal.cantidadAceptadaEscuela(idEvento, cct);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }


        ///<summary>
        ///Insertar en tblEscuelaEvento
        /// </summary>
        public bool agregarCantidadEscuela(int cct, int idEvento, int cantidad)
        {
            try
            {
                return dal.agregarCantidadEscuela(cct, idEvento, cantidad);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }


        ///<summary>
        ///Actualizar cantidad dentro de tblEscuela evento, por CCT e idEvento
        /// </summary>
        public bool editarCantidadEscuela(int cct, int idEvento, int cantidad)
        {
            try
            {
                return dal.editarCantidadEscuela(cct, idEvento, cantidad);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }


        ///<summary>
        //Eliminar registro dentro de tblEscuelaEvento, por CCT e idEvento
        ///</summary>
        public bool eliminarCantEscuela(int cct, int idEvento)
        {
            try
            {
                return dal.eliminarCantEscuela(cct, idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        //Eliminar registro dentro de tblEscuelaEvento, por idEscuela
        ///</summary>
        public bool eliminarCantEscuela(int idEscuela)
        {
            try
            {
                return dal.eliminarCantEscuela(idEscuela);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Retorna lista dee la clase TablaEscuelaCantidad para mostrar las Escuelas y cantidad por el idEvento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public List<TablaEscuelaCantidad> getEventoTabla(int idEvento)
        {
            try
            {
                return dal.getEventoTabla(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

    }
}
