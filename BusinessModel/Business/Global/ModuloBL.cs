using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.DataAccess.Global;
using BusinessModel.Persistence.BD;

namespace BusinessModel.Business.Global
{

    /// <summary>
    /// Clase para el acceso a datos de tblModulo
    /// </summary>
    public class ModuloBL
    {
        ModuloDAL moduloDAL = new ModuloDAL();

        /// <summary>
        /// Obtiene todos los registros en tblModulo
        /// </summary>
        /// <returns>model.listaModulos</returns>
        public List<tblModulo> getListModulo()
        {
            try
            {
                return moduloDAL.getListModulo();
            }
            catch (Exception ex) { System.Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblModulo donde idModulo = parámetro idModulo
        /// </summary>
        /// <param name="idModulo"></param>
        /// <returns>model.listaModulos</returns>
        public List<tblModulo> getListModulo(string idModulo)
        {
            try
            {
                return moduloDAL.getListModulo(idModulo);
            }catch(Exception ex) { System.Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Genera una lista de todos los modulos dentro de la tabla tblModulo
        /// </summary>
        /// <returns>Retorna lista con Modulos</returns>
        public List<string> getSoloModulos()
        {
            try
            {
                return moduloDAL.getSoloModulos();
            }
            catch (Exception ex) { System.Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Actualización a titulo en tblModulo donde idModlo = parámetro modulo
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="newTitulo"></param>
        /// <returns></returns>
        public bool editarTitulo(string modulo, string newTitulo)
        {
            return moduloDAL.editarTitulo(modulo, newTitulo);
        }

        /// <summary>
        /// Actualización archivoIntroduccion en tblModulo donde idModlo = parámetro modulo
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="newArchivo"></param>
        /// <returns></returns>
        public bool editarArchivo(string modulo, string newArchivo)
        {
            return moduloDAL.editarArchivo(modulo, newArchivo);
        }
    }
}
