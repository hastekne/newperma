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
    /// Clase para el acceso de datos a tblPermisoModulo
    /// </summary>
    public class PermisoModuloBL
    {
        PermisoModuloDAL permisoDAL = new PermisoModuloDAL();

        /// <summary>
        /// Obtiene todos los registros de tblPermisoModulo
        /// </summary>
        /// <returns>model.listPermisosMod</returns>
        public List<tblPermisoModulo> getPermisosMod()
        {
            try
            {
                return permisoDAL.getPermisosMod();
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblPermisoModulo donde modulo = parámetro modulo
        /// </summary>
        /// <param name="modulo"></param>
        /// <returns>model.listPermisosMod</returns>
        public List<tblPermisoModulo> getPermisosMod(string modulo)
        {
            try
            {
                return permisoDAL.getPermisosMod(modulo);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblPermisoModulo donde usuario = parámetro usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>model.listPermisosMod</returns>
        public List<tblPermisoModulo> getPermisosUsu(string usuario)
        {
            try
            {
                return permisoDAL.getPermisosUsu(usuario);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obiene el valor de model.permiso donde modulo = parámetro modulo y 
        /// usuario = parámetro usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="modulo"></param>
        /// <returns>model.permiso (TRUE||FALSE)</returns>
        public bool tienePermisoUsu(string usuario, string modulo)
        {
            try
            {
                return permisoDAL.tienePermisoUsu(usuario,modulo);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Actualiza el permiso de un modulo utilizando modulo=parámetro modulo
        /// y usuario=parámetro usuario
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="usuario"></param>
        /// <param name="permiso"></param>
        /// <returns>TRUE==Update exitoso || FALSE==Falla en Update</returns>
        public bool editarPermiso(string modulo, string usuario, bool permiso)
        {
            try
            {
                return permisoDAL.editarPermiso(modulo, usuario,permiso);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene TRUE o FALSE si un permiso existe para un determinado modulo y usuario
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="usuario"></param>
        /// <returns>TRUE==Permiso existe || FALSE== No existe Permiso</returns>
        public bool existePermisoUsuario(string modulo, string usuario)
        {
            try
            {
                return permisoDAL.existePermisoUsuario(modulo, usuario);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Agrega un nuevo registro a tblPermiso
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="usuario"></param>
        /// <returns>TRUE==Registro exitoso || FALSE==Falla en el registro</returns>
        public bool agregarpermiso(string modulo, string usuario)
        {
            try
            {
                return permisoDAL.agregarpermiso(modulo, usuario);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
