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
    /// Clase para el acceso de datos a tblMesaTrabEvento
    /// </summary>
    public class MesaEventoBL
    {
        MesaEventoDAL dal = new MesaEventoDAL();

        ///<summary>
        /// Buscar por idMesaTrab en tblMesaTrabEvento
        /// </summary>
        public List<tblMesaTrabEvento> getMesaTevento(int idMesa)
        {
            try
            {
                return dal.getMesaTevento(idMesa);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Buscar por idEvento en tblMesaTrabEvento
        /// </summary>
        public List<tblMesaTrabEvento> getMesaTeventoXevento(int idEvento)
        {
            try
            {
                return dal.getMesaTeventoXevento(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Insertar registro en tblMesaTrabEvento
        /// </summary>
        public bool agregarMesaEvento(int idEvento, string nombre, int cantidad, bool estado)
        {
            try
            {
                return dal.agregarMesaEvento(idEvento,nombre,cantidad,estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Actualizar registro en tblMesaTrabEvento
        /// </summary>
        public bool editarMesaEvento(int idMesa, string nom, int cant)
        {
            try
            {
                return dal.editarMesaEvento(idMesa,nom,cant);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Actualizar estado en tblMesaTrabEvento
        /// </summary>
        public bool editarMesaEstado(int idMesa, bool estado)
        {
            try
            {
                return dal.editarMesaEstado(idMesa, estado);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Calcular la cantidad aceptada en la mesa de trabajo por evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="idMT"></param>
        /// <returns></returns>
        public int cantidadAceptadaMesaT(int idEvento, int idMT)
        {
            try
            {
                return dal.cantidadAceptadaMesaT(idEvento, idMT);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
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
            try
            {
                return dal.getIDmesaXnombreYevento(idEvento, nombreMT);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

    }
}
