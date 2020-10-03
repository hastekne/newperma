using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Models.Usuario;
using BusinessModel.DataAccess.Usuario;
using BusinessModel.Persistence.BD;

namespace BusinessModel.Business.Usuario
{
    /// <summary>
    /// Clase para el Acceso de datos a tblPerfil
    /// </summary>
    public class PerfilBL
    {
        PerfilDAL perfilDAL = new PerfilDAL();

        /// <summary>
        /// Método que retorna la lista de los perfiles de usuario de la BD.
        /// </summary>
        public List<tblPerfil> listPerfil()
        {
            try
            {
                return perfilDAL.getListaPerfil();
            }
            catch (Exception ex) { System.Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblPerfil donde idPerfil == parámetro idPerfil
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns>model.listaPerfil</returns>
        public List<tblPerfil> listPerfil(int idPerfil)
        {
            try
            {
                return perfilDAL.getListaPerfil(idPerfil);
            }
            catch (Exception ex) { System.Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Método que retorna los datos de un perfil en especifico dentro de una lista.
        /// </summary>
        public List<tblPerfil> getListaPerfilXestado(bool estado,string perfil="")
        {
            try
            {
                return perfilDAL.getListaPerfilXestado(estado,perfil);
            }
            catch (Exception ex) { System.Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el idPerfil en tblPerfil donde perfil == parámetro perfil
        /// </summary>
        /// <param name="perfil"></param>
        /// <returns>model.idPerfil</returns>
        public int getIdPerfil(string perfil)
        {
            try
            {
                return perfilDAL.getIdPerfil(perfil);
            }catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message); throw;
            }

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
            return perfilDAL.editar(idPerfil, perfil, descripcion, estado);
        }

        /// <summary>
        /// Nuevo registro en tblPerfil, se agrega solo si el perfil no existe
        /// </summary>
        /// <param name="perfil"></param>
        /// <param name="descripcion"></param>
        /// <param name="estado"></param>
        /// <returns>True=Registro exitoso || False=Error en registro</returns>
        public bool agregar(string perfil, string descripcion, bool estado)
        {
            return perfilDAL.agregar(perfil, descripcion,estado);
        }
    }
}
