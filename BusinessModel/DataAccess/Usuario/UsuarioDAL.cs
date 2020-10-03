using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Models.Usuario;
using BusinessModel.Persistence.BD;



namespace BusinessModel.DataAccess.Usuario
{
    /// <summary>
    /// Clase para el acceso de datos a tblUsuario
    /// </summary>
    public class UsuarioDAL
    {
        
        UsuarioML model = new UsuarioML();

        /// <summary>
        /// Obtiene todos los registros en tblUsuario
        /// </summary>
        /// <returns>model.listaUsuario</returns>
        public List<tblUsuario> getListUsuario()
        {

            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaUsuario = (from u in db.tblUsuario select u).ToList();
            }
            return model.listaUsuario;
        }

        /// <summary>
        /// Obtiene todos los registros en tblUsuario donde perfil == 2 'Revisor'
        /// </summary>
        /// <returns>model.listaUsuario</returns>
        public List<tblUsuario> getListRevi()
        {

            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaUsuario = (from u in db.tblUsuario where u.perfil==2 select u).ToList();
            }
            return model.listaUsuario;
        }

        /// <summary>
        /// Método que retorna la lista de un usuario en especifico.
        /// </summary>
        public List<tblUsuario> getListUsuario(string nomUser)
        {

            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaUsuario = (from u in db.tblUsuario where u.nombreUsuario == nomUser select u).ToList();
            }
            return model.listaUsuario;
        }

        /// <summary>
        /// Método que actualiza los datos de un usuario determinado. 
        /// </summary>
        // public bool editar(tblUsuario valueEdit)
        public bool editar(string valueEdit, string nombre, string ap1, string ap2, bool estado, int perfil)
        {

            bool actualizo = false;
            try
            {

                //SeguimientoPermanenciaEntities dbCemsysi = new SeguimientoPermanenciaEntities();
                //dbCemsysi.Entry(valueEdit).State = System.Data.Entity.EntityState.Modified;
                //dbCemsysi.SaveChanges();
                //actualizo = true;
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var usuario = (from uR in db.tblUsuario where uR.nombreUsuario == valueEdit select uR).First<tblUsuario>();
                    usuario.nombre = nombre;
                    usuario.pApellido = ap1;
                    usuario.sApellido = ap2;
                    usuario.estado = estado;
                    usuario.perfil = perfil;
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
        /// Obtiene un valor booleano que determina si un Nombre de usuario ya existe en tblUsuario
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True=Duplicado de nombreUsuario || False=No hay duplicados de nombreUsuario</returns>
        public bool validaDuplicados(tblUsuario value)
        {

            bool duplicado = false;
            try
            {

                SeguimientoPermanenciaEntities dbCemsysi = new SeguimientoPermanenciaEntities();

                if (dbCemsysi.tblUsuario.Where(m => m.nombreUsuario.Trim() == value.nombreUsuario.Trim() && m.nombre != value.nombre).Count() > 0)
                {
                    duplicado = true;
                }

            }
            catch (Exception ex)
            {
                duplicado = false;
                throw ex;
            }

            return duplicado;
        }

        //Se debe agregar el perfil del ususario, esto repercutira en la vista
        /// <summary>
        /// Método que da de alta un usuario en la BD. 
        /// </summary>
        public bool agregar(string nomUser, string nombre, string p1, string p2, string pass, string email, string tel, bool estado, int perfil)
        {
            bool agrego = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    tblUsuario usuario = new tblUsuario()
                    {
                        nombreUsuario = nomUser,
                        nombre = nombre,
                        pApellido = p1,
                        sApellido = p2,
                        contrasenia = pass,
                        correoElectronico = email,
                        telefono = tel,
                        estado = estado,
                        perfil = perfil,
                    };
                    db.tblUsuario.Add(usuario);
                    db.SaveChanges();
                }
                agrego = true;
            }
            catch (Exception)
            {
                agrego = false;
                //throw;
            }

            return agrego;
        }

        /// <summary>
        /// Valida si el usuario esta registrado, para otorgar el permiso de inicio de sesión.
        /// </summary>

        public bool validarInicioSesion(string nombreUser, string pass)
        {
            bool datosCorrectos = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    model.listaUsuario = (from u in db.tblUsuario where u.nombreUsuario == nombreUser && u.contrasenia == pass && u.estado==true select u).ToList();
                }
                if (model.listaUsuario.Count > 0)
                {
                    datosCorrectos = true;
                }
                else { datosCorrectos = false; }
            }
            catch (Exception) { datosCorrectos = false; }

            return datosCorrectos;
            
        }

        /// <summary>
        /// Obtiene los registros en tblUsuario join tblPerteneceInsSis join tblPerfil donde 
        /// idPerfil == parámetro idPerfil. Por default el parámetro idPerfil == 2
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns>model.tabRevisorUser</returns>
        public List<UsuariosRBP> getUserRevisor(int? idPerfil=0)
        {
            if (idPerfil == 0)
                idPerfil = 2;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.tabRevisorUser = (from u in db.tblUsuario join i in db.tblPerteneceInsSis on u.nombreUsuario equals i.idUserRevisor join p in db.tblPerfil on u.perfil equals p.idPerfil where p.idPerfil==idPerfil  select new UsuariosRBP {user=u.nombreUsuario, nombre=u.nombre,apP= u.pApellido,apM=u.sApellido,email=u.correoElectronico,tel=u.telefono,instSub=i.institucionSistema,estado=u.estado} ).ToList();
            }
            return model.tabRevisorUser;
        }

        /// <summary>
        /// Actualiza en estado en tblUsuario donde nombreUsuario == parámetro user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="estado"></param>
        /// <returns>True=Update exitoso || False=Error con Update</returns>
        public bool editarEstado(string user, bool estado)
        {

            bool actualizo = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var usuario = (from uR in db.tblUsuario where uR.nombreUsuario == user select uR).First<tblUsuario>();
                    usuario.estado = estado;
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
        /// Actualiza user, nombre, apellido 1, apellido 2, tel y email en tblUsuario donde nombreUsuario == parámetro user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="nombre"></param>
        /// <param name="ap1"></param>
        /// <param name="ap2"></param>
        /// <param name="tel"></param>
        /// <param name="email"></param>
        /// <returns>True=update exitoso || False=Error con update</returns>
        public bool editar(string user, string nombre, string ap1, string ap2,string tel, string email)
        {

            bool actualizo = false;
            try
            {

                using (var db = new SeguimientoPermanenciaEntities())
                {
                    var usuario = (from uR in db.tblUsuario where uR.nombreUsuario == user select uR).First<tblUsuario>();
                    usuario.nombre = nombre;
                    usuario.pApellido = ap1;
                    usuario.sApellido = ap2;
                    usuario.telefono = tel;
                    usuario.correoElectronico = email;
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
        /// Obtiene un valor booleano para determinar si el nombreUsuario eciste en tblUsuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>true=nombreUsuario existe || false=no existe el nombreUsuario</returns>
        public bool UserExist(string usuario)
        {
            bool existe = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listaUsuario = (from u in db.tblUsuario where u.nombreUsuario==usuario select u).ToList();
            }
            if (model.listaUsuario.Count > 0)
            {
                existe = true;
            }
            return existe;
        }

    }
}
