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
    /// Clase para el acceso de datos a tblUsuario
    /// </summary>
    public class UsuarioBL
    {
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        /// <summary>
        /// Obtiene todos los registros en tblUsuario
        /// </summary>
        /// <returns>model.listaUsuario</returns>
        public List<tblUsuario> listUsuario()
        {
            try
            {
                return usuarioDAL.getListUsuario();
            }
            catch (Exception ex) { System.Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Método que retorna la lista de un usuario en especifico.
        /// </summary>
        public List<tblUsuario> listUsuario(string usu)
        {
            try
            {
                return usuarioDAL.getListUsuario(usu);
            }
            catch (Exception ex) { System.Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Método que actualiza los datos de un usuario determinado. 
        /// </summary>
        public bool editar(string valueEdit, string nombre, string ap1, string ap2, bool estado, int perfil)
        {
            return usuarioDAL.editar(valueEdit, nombre, ap1, ap2, estado, perfil);
        }

        /// <summary>
        /// Obtiene un valor booleano que determina si un Nombre de usuario ya existe en tblUsuario
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True=Duplicado de nombreUsuario || False=No hay duplicados de nombreUsuario</returns>
        public bool validaDuplicados(UsuarioML valueEdit)
        {

            return usuarioDAL.validaDuplicados(AutoMapper.Mapper.Map<tblUsuario>(valueEdit));
        }

        /// <summary>
        /// Método que da de alta un usuario en la BD. 
        /// </summary>
        public bool agregar(string nomUser, string nombre, string p1, string p2, string pass, string email, string tel, bool estado, int perfil)
        {
            return usuarioDAL.agregar(nomUser, nombre, p1, p2, pass, email, tel, estado, perfil);
        }

        /// <summary>
        /// Valida si el usuario esta registrado, para otorgar el permiso de inicio de sesión.
        /// </summary>
        public bool validarInicioSesion(string nombreUser, string pass)
        {
            return usuarioDAL.validarInicioSesion(nombreUser, pass);
        }

        /// <summary>
        /// Obtiene los registros en tblUsuario join tblPerteneceInsSis join tblPerfil donde 
        /// idPerfil == parámetro idPerfil. Por default el parámetro idPerfil == 2
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns>model.tabRevisorUser</returns>
        public List<UsuariosRBP> getUserRevisor(int? idPerfil=0)
        {
            try
            {
                return usuarioDAL.getUserRevisor(idPerfil);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); throw;
            }

        }

        /// <summary>
        /// Actualiza en estado en tblUsuario donde nombreUsuario == parámetro user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="estado"></param>
        /// <returns>True=Update exitoso || False=Error con Update</returns>
        public bool editarEstado(string user, bool estado)
        {
            try
            {
                return usuarioDAL.editarEstado(user, estado);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Actualiza user, nombre, apellido 1, apellido 2, tel y email en tblUsuario donde nombreUsuario == parámetro user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="nombre"></param>
        /// <param name="ap1"></param>
        /// <param name="ap2"></param>
        /// <param name="tel"></param>
        /// <param name="email"></param>
        /// <returns>True=update exitoso || False=Error con update</returns>
        public bool editar(string user, string nombre, string ap1, string ap2, string tel, string email)
        {
            try
            {
                return usuarioDAL.editar(user, nombre, ap1, ap2, tel, email);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message); throw;
            }
        }

        /// <summary>
        /// Obtiene un valor booleano para determinar si el nombreUsuario eciste en tblUsuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>true=nombreUsuario existe || false=no existe el nombreUsuario</returns>
        public bool UserExist(string usuario)
        {
            try
            {
                return usuarioDAL.UserExist(usuario);
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }
    }
}
