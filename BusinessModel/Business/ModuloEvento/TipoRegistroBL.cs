using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;
using BusinessModel.DataAccess.ModuloEvento;

namespace BusinessModel.Business.ModuloEvento
{
    /// <summary>
    /// Clase para el Acceso de datos a tblTipoRegistro
    /// </summary>
    public class TipoRegistroBL
    {
        TipoRegistroDAL dal = new TipoRegistroDAL();

        ///<summary>
        ///Buscar por idTipoRegistro en tblTipoRegistro
        /// </summary>
        public List<tblTipoRegistro> getTipoRegistro(string idTipoR)
        {
            try
            {
                return dal.getTipoRegistro(idTipoR);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        ///<summary>
        ///Insertar registro en tblTipoRegistro y regresa el id del nuevo tipoRegistro
        /// </summary>
        public string agregarTipoReg(string desc)
        {
            try
            {
                return dal.agregarTipoReg(desc);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Eliminar el tipo de registro cuando el registro al evento haya fallado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool eliminarTipoRegistro(string id)
        {
            try
            {
                return dal.eliminarTipoRegistro(id);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        }
}
