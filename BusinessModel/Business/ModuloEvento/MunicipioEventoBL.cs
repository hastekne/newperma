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
    /// Clase para el Acceso de datos a tblMunicipioEvento
    /// </summary>
    public class MunicipioEventoBL
    {
        MunicipioEventoDAL dal = new MunicipioEventoDAL();

        ///<summary>
        ///Buscar por idMunicipioEvento en tblMunicipioEvento
        /// </summary>
        public List<tblMunicipioEvento> getMuniEvenXidMuniE(int idMuniEven)
        {
            try
            {
                return dal.getMuniEvenXidMuniE(idMuniEven);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Buscar por idEvento en tblMunicipioEvento
        /// </summary>
        public List<tblMunicipioEvento> getMuniEvenXidEven(int idEvento)
        {
            try
            {
                return dal.getMuniEvenXidEven(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Insertar registro en tblMunicipioEvento
        /// </summary>
        public bool agregarMuniEvento(int idEvento, int idMunicipio)
        {
            try
            {
                return dal.agregarMuniEvento(idEvento, idMunicipio);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Eliminar registro en tblMunicipioEvento
        /// </summary>
        public bool eliminarMunicipioEven(int idMunicipioEvent)
        {
            try
            {
                return dal.eliminarMunicipioEven(idMunicipioEvent);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Eliminar todos los registros en tblMunicipioEvento por el ID
        /// </summary>
        public bool eliminarTodosMunicipioEven(int idEvento)
        {
            try
            {
                return dal.eliminarTodosMunicipioEven(idEvento);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        }
}
