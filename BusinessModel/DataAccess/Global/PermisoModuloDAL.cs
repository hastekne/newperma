using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Models.Global;
using BusinessModel.Persistence.BD;
namespace BusinessModel.DataAccess.Global
{
    /// <summary>
    /// Clase para el acceso de datos a tblPermisoModulo
    /// </summary>
    public class PermisoModuloDAL
    {
        PermisoModuloML model = new PermisoModuloML();

        /// <summary>
        /// Obtiene todos los registros de tblPermisoModulo
        /// </summary>
        /// <returns>model.listPermisosMod</returns>
        public List<tblPermisoModulo> getPermisosMod()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listPermisosMod = (from pm in db.tblPermisoModulo select pm).ToList();
            }
            return model.listPermisosMod;
        }

        /// <summary>
        /// Obtiene los registros de tblPermisoModulo donde modulo = parámetro modulo
        /// </summary>
        /// <param name="modulo"></param>
        /// <returns>model.listPermisosMod</returns>
        public List<tblPermisoModulo> getPermisosMod(string modulo)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listPermisosMod = (from pm in db.tblPermisoModulo where pm.modulo==modulo  select pm).ToList();
            }
            return model.listPermisosMod;
        }

        /// <summary>
        /// Obtiene los registros de tblPermisoModulo donde usuario = parámetro usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>model.listPermisosMod</returns>
        public List<tblPermisoModulo> getPermisosUsu(string usuario)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listPermisosMod = (from pm in db.tblPermisoModulo where pm.usuario == usuario select pm).ToList();
            }
            return model.listPermisosMod;
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
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.permiso = Boolean.Parse((from pm in db.tblPermisoModulo where pm.modulo == modulo && pm.usuario == usuario select pm.permiso).ToString());
            }
            return model.permiso;
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

            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var per = (from pm in db.tblPermisoModulo where pm.modulo == modulo && pm.usuario==usuario select pm).First<tblPermisoModulo>();
                    per.permiso = permiso;
                    db.SaveChanges();
                }
                actualizo = true;
            }
            catch (Exception ex)
            {
                actualizo = false;
                throw ex;
            }

            return actualizo;
        }

        /// <summary>
        /// Obtiene TRUE o FALSE si un permiso existe para un determinado modulo y usuario
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="usuario"></param>
        /// <returns>TRUE==Permiso existe || FALSE== No existe Permiso</returns>
        public bool existePermisoUsuario(string modulo, string usuario)
        {
            bool existe = false;
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listPermisosMod = (from pm in db.tblPermisoModulo where pm.modulo == modulo && pm.usuario == usuario select pm).ToList();
            }
            if (model.listPermisosMod.Count > 0)
            {
                existe = true;
            }
                return existe;
        }

        /// <summary>
        /// Agrega un nuevo registro a tblPermiso
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="usuario"></param>
        /// <returns>TRUE==Registro exitoso || FALSE==Falla en el registro</returns>
        public bool agregarpermiso(string modulo, string usuario)
        {
            bool agrego = false;
            try
            {
                if (existePermisoUsuario(modulo, usuario)==false)
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblPermisoModulo newPer = new tblPermisoModulo()
                        {
                            modulo = modulo,
                            usuario = usuario,
                            permiso = true,
                        };
                        db.tblPermisoModulo.Add(newPer);
                        db.SaveChanges();
                    }
                    agrego = true;
                }
                
            }
            catch (Exception)
            {
                agrego = false;
                throw;
            }

            return agrego;
        }
    }
}
