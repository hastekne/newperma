using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using BusinessModel.Models.Usuario;

namespace BusinessModel.DataAccess.Usuario
{
    /// <summary>
    /// Clase para el Acceso de datos a tblPerfil
    /// </summary>
    public class PerfilDAL
    {
        /// <summary>
        /// Método que retorna la lista de los perfiles de usuario de la BD.
        /// </summary>
        public List<tblPerfil> getListaPerfil()
        {
            PerfilML model = new PerfilML();
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaPerfil = (from p in db.tblPerfil select p).ToList();
            }
            return model.listaPerfil;
        }

        /// <summary>
        /// Obtiene los registros en tblPerfil donde estado == parámetro estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns>model.listaPerfil</returns>
        public List<tblPerfil> getListaPerfil(bool estado)
        {
            PerfilML model = new PerfilML();
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaPerfil = (from p in db.tblPerfil where p.estado==true select p).ToList();
            }
            return model.listaPerfil;
        }

        /// <summary>
        /// Método que retorna los datos de un perfil en especifico dentro de una lista.
        /// </summary>
        public List<tblPerfil> getListaPerfilXestado(bool estado, string perfil = "")
        {
            PerfilML model = new PerfilML();
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaPerfil = (from p in db.tblPerfil where p.perfil.Contains(perfil) && p.estado==estado  select p).ToList();
            }
            return model.listaPerfil;
        }

        /// <summary>
        /// Obtiene los registros en tblPerfil donde idPerfil == parámetro idPerfil
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns>model.listaPerfil</returns>
        public List<tblPerfil> getListaPerfil(int idPerfil)
        {
            PerfilML model = new PerfilML();
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listaPerfil = (from p in db.tblPerfil where p.idPerfil == idPerfil select p).ToList();
            }
                return model.listaPerfil; 
        }

        /// <summary>
        /// Obtiene el idPerfil en tblPerfil donde perfil == parámetro perfil
        /// </summary>
        /// <param name="perfil"></param>
        /// <returns>model.idPerfil</returns>
        public int getIdPerfil(string perfil)
        {
            PerfilML model = new PerfilML();
            using (var db=new SeguimientoPermanenciaEntities())
            {
                //model.idPerfil = Convert.ToInt32((from p in db.tblPerfil where p.perfil == perfil select p.idPerfil).ToString());
                model.listaPerfil = (from p in db.tblPerfil where p.perfil == perfil select p).ToList();
                if (model.listaPerfil.Count > 0)
                {
                    foreach (var item in model.listaPerfil)
                    {
                        model.idPerfil = item.idPerfil;
                    }
                }
            }

            return model.idPerfil;
        }

        /// <summary>
        /// Actializa perfil, descripción y estado en tblPerfil donde idPerfil == parámetro idPerfil
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <param name="perfil"></param>
        /// <param name="descripcion"></param>
        /// <param name="estado"></param>
        /// <returns>True=Update exitosa || False=Error Update</returns>
        public bool editar(int idPerfil, string perfil, string descripcion, bool estado)
        {
            bool actualizo = false;
            try
            {
                using (var db=new SeguimientoPermanenciaEntities())
                {
                    var per = (from p in db.tblPerfil where p.idPerfil == idPerfil select p).First<tblPerfil>();
                    per.perfil = perfil;
                    per.descripcion = descripcion;
                    per.estado = estado;
                    db.SaveChanges();
                    actualizo = true;

                }
            }
            catch (Exception ex)
            {
                actualizo = false;
                throw ex;
            }
            return actualizo;
        }

        /// <summary>
        /// Obtiene un valor true/false si el perfil existe en tblPerfil
        /// </summary>
        /// <param name="perfil"></param>
        /// <returns>True=Existe perfil || False=No existe perfil</returns>
        public bool existe (string perfil)
        {
            bool existe = false;
            PerfilML model = new PerfilML();
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaPerfil = (from p in db.tblPerfil where p.perfil == perfil select p).ToList();
            }
            if (model.listaPerfil.Count > 0)
            {
                existe = true;
            }
            return existe;
        }

        /// <summary>
        /// Nuevo registro en tblPerfil, se agrega solo si el perfil no existe
        /// </summary>
        /// <param name="perfil"></param>
        /// <param name="descripcion"></param>
        /// <param name="estado"></param>
        /// <returns>True=Registro exitoso || False=Error en registro</returns>
        public bool agregar(string perfil, string descripcion,bool estado)
        {
            bool agrego = false;
            try
            {
                if (existe(perfil) == false)
                {
                    using (var db = new SeguimientoPermanenciaEntities())
                    {
                        tblPerfil per = new tblPerfil()
                        {
                            perfil = perfil,
                            descripcion = descripcion,
                            estado = estado,
                        };
                        db.tblPerfil.Add(per);
                        db.SaveChanges();
                    }
                    agrego = true;
                }

            }catch(Exception ex)
            {
                agrego = false;
                throw ex;
            }
            return agrego;
        }
    }
}
