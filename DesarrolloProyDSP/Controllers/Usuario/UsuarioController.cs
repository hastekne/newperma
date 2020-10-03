using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using DesarrolloProyDSP.Models.Usuario;
using BusinessModel.Business.Usuario;
using DesarrolloProyDSP.Helpers;


namespace DesarrolloProyDSP.Controllers.Usuario
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    
    public class UsuarioController : Controller
    {
        // GET: Usuario
        UsuarioViewModel model = new UsuarioViewModel();
        PerfilViewModel modelPerf = new PerfilViewModel();
        UsuarioBL userBL = new UsuarioBL();
        PerfilBL perfilBL = new PerfilBL();
        Seguridad seguridad = new Seguridad();
        EnviarCorreo enviar = new EnviarCorreo();



        //public ActionResult Index(string nombreUsuario)
        //{
        //    //  -----------------Desplegar una lista-----------------
        //    model.listaUsuario = userBL.listUsuario(nombreUsuario);
        //    foreach (var item in model.listaUsuario)
        //    {
        //        model.nombreUsuario = item.nombreUsuario;
        //        model.correoElectronico = item.correoElectronico;
        //        model.telefono = item.telefono;
        //        model.estado = item.estado;
        //        //model.contrasenia = seguridad.DesEncriptar(item.contrasenia);
        //        model.contrasenia = item.contrasenia;
        //    }
        //    return View(model);
        //    // return View();
        //}



        //[HttpPost]
        //  public ActionResult Index(string nombreUsuario)
        //  {
        //      //----------------- Desplegar una lista-----------------
        //      model.listaUsuario = userBL.listUsuario(nombreUsuario);
        //      foreach (var item in model.listaUsuario) {

        //          model.nombreUsuario = item.nombreUsuario;
        //          model.correoElectronico = item.correoElectronico;
        //          model.telefono = item.telefono;
        //          model.estado = true;
        //         // model.contrasenia = seguridad.DesEncriptar(item.contrasenia);
        //           model.contrasenia = item.contrasenia;
        //      }
        //      return View(model);
        //      // return View();
        //  }

        /// <summary>
        /// Método de prueba
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="nombre"></param>
        /// <param name="pApellido"></param>
        /// <param name="sApellido"></param>
        /// <param name="estado"></param>
        /// <param name="perfil"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string nombreUsuario, string nombre, string pApellido, string sApellido, bool estado, int perfil)
        {
            userBL.editar(nombreUsuario, nombre, pApellido, sApellido, estado, perfil);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Ejemplo Para agregar un usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult AgregarUser()
        {
            CargarPerfiles();
            var tuple = new Tuple<UsuarioViewModel, PerfilViewModel>(model, modelPerf);


            return View(tuple);
        }

        /// <summary>
        /// Cargar perfiles solo con estado activo
        /// </summary>
        [NonAction]
        public void CargarPerfiles()
        {
            modelPerf.listaPerfil = perfilBL.getListaPerfilXestado(true, "");

        }

        /// <summary>
        /// Ejemplo para agrear usuario
        /// </summary>
        /// <param name="m"></param>
        /// <param name="Confirma"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AgregarUser([Bind(Prefix = "Item1")] UsuarioViewModel m, string Confirma, [Bind(Prefix = "Item2")] PerfilViewModel p) //(string nombreUsuario, string nombre, string pApellido, string sApellido, string contrasenia,string Confirma, string correoElectronico, string telefono, bool estado)
        {
            var tuple = new Tuple<UsuarioViewModel, PerfilViewModel>(m, p);

            //CargarPerfiles();
            if (ModelState.IsValid)
            {
                if (Confirma.Equals(m.contrasenia))
                {
                    var selectPerfil = Request.Form["selectPerfil"];
                    int perfilID = perfilBL.getIdPerfil(selectPerfil);
                    userBL.agregar(m.nombreUsuario, m.nombre, m.pApellido, m.sApellido, seguridad.Encriptar(m.contrasenia), m.correoElectronico, m.telefono, true, perfilID);
                    try
                    {
                        enviar.EnviarEmail("Bienvenido", m.contrasenia, m.correoElectronico, m.nombreUsuario);
                    }
                    catch (Exception) { throw; }
                    return RedirectToAction("AgregarUser");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "La contrasenia no caincide");
                    //var t = new Tuple<UsuarioViewModel, PerfilViewModel>(m, p);
                    CargarPerfiles();
                    modelPerf.listaPerfil = perfilBL.getListaPerfilXestado(true, "");
                    var t = new Tuple<UsuarioViewModel, PerfilViewModel>(model, modelPerf);
                    return View(t);
                }

            }

            modelPerf.listaPerfil = perfilBL.getListaPerfilXestado(true, "");
            tuple = new Tuple<UsuarioViewModel, PerfilViewModel>(model, modelPerf);
            return View(tuple);

        }

        /// <summary>
        /// Ejemplo Prueba inicio de sesión probando cookie
        /// </summary>
        /// <returns></returns>
        public ActionResult IniciarSesion()
        {
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            if (c1 != null)
            {
                return RedirectToAction("Index", "HomeBP");
            }
            else
            {
                return View(model);
            }

        }

        /// <summary>
        /// Validar el inicio de sesión con los parámetro userName y passs Ejemplo prueba
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ValidarIS(string userName, string pass)
        {
            HttpCookie cookieUsuario = new HttpCookie("CookieUsuario");
            if (userBL.validarInicioSesion(userName, seguridad.Encriptar(pass)) == true)
            {
                cookieUsuario.Value = seguridad.Encriptar(userName);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookieUsuario);
                return RedirectToAction("Index", "HomeBP");
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");

            }
        }

        /// <summary>
        /// Valida el inicio de sesion con los parámetros userName y pass
        /// genera las cookies de usuario y perfil usuario si la validación es correcta
        /// retorna el perfil del usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public string ValidaInicioS(string userName,string pass)
        {
            HttpCookie cookieUsuario = new HttpCookie("CookieUsuario");
            HttpCookie cookiePerfilU = new HttpCookie("CookiePerfilU");
            int perfilUsu = 1;
            string perfil = "";
            //if (userBL.validarInicioSesion(userName, seguridad.Encriptar(pass)) == true)
            if (userBL.validarInicioSesion(userName, seguridad.Encriptar(pass)) == true)
            {
                cookieUsuario.Value = seguridad.Encriptar(userName);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookieUsuario);

                model.listaUsuario = userBL.listUsuario(userName);
                
                
                foreach (var item in model.listaUsuario)
                {
                    perfilUsu = item.perfil;
                }

                modelPerf.listaPerfil = perfilBL.listPerfil(perfilUsu);
                foreach(var item in modelPerf.listaPerfil)
                {
                    perfil = item.perfil;
                }

                cookiePerfilU.Value = seguridad.Encriptar(perfil);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookiePerfilU);

                return perfil;
            }
            else
            {
                perfil = "NO";
                return perfil;

            }
        }

        /// <summary>
        /// Prepara la vista para el registro de usuarios
        /// </summary>
        /// <returns></returns>
        public ActionResult Registro()
        {
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
            if (c1 != null)//Validar perfil para saber a cual sesión se envia
            {
                if (seguridad.DesEncriptar(perf.Value) == "Revisor") { 
                return RedirectToAction("SesionRevisor", "BuenasPracticas");
                }
                else if(seguridad.DesEncriptar(perf.Value) == "Colaborador")
                {
                    return RedirectToAction("Sesion", "BuenasPracticas");
                }
            }
            model.nombre = "";
            model.pApellido = "";
            model.sApellido = "";
            model.correoElectronico = "";
            model.telefono = "";
            model.nombreUsuario = "";
            return View(model);
        }


        /// <summary>
        /// Registro de usuario con perfil colaborador, se activan las cookies si el registro es exitoso
        /// Se realizar las validaciones necearias de los datos capturados
        /// Se redirecciona a la View de sesión de los usuarios colaborador
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellidoP"></param>
        /// <param name="apellidoM"></param>
        /// <param name="email"></param>
        /// <param name="cEmail"></param>
        /// <param name="tel"></param>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <param name="cPass"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Registro(string nombre,string apellidoP, string apellidoM, string email, string cEmail,string tel, string userName, string pass, string cPass)
        {
            model.nombre = nombre;
            model.pApellido = apellidoP;
            model.sApellido = apellidoM;
            model.correoElectronico = email;
            model.telefono = tel;
            model.nombreUsuario = userName;
            //ModelState.AddModelError(string.Empty, "Error en el registro, volver a intentar.\n Pruebe con otro nombre de usuario.");
            //return View(model);


            HttpCookie cookieUsuario = new HttpCookie("CookieUsuario");
            HttpCookie cookiePerfilU = new HttpCookie("CookiePerfilU");


            if (ModelState.IsValid)
            {
                if (email.Equals(cEmail) && pass.Equals(cPass))
                {

                    if (userBL.agregar(userName, nombre, apellidoP, apellidoM, seguridad.Encriptar(pass), email, tel, true, 3) == true)
                    {
                        cookieUsuario.Value = seguridad.Encriptar(userName);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookieUsuario);
                        cookiePerfilU.Value = seguridad.Encriptar("Colaborador");
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookiePerfilU);
                        return RedirectToAction("Sesion", "BuenasPracticas");
                    }
                    else
                    {
                        //var m = new UsuarioViewModel();
                        ModelState.AddModelError(string.Empty, "Error en el registro, volver a intentar.\n Pruebe con otro nombre de usuario.");
                        return View(model);
                        //return Redirect(model);
                    }
                }
                else
                {
                    var m = new UsuarioViewModel();
                    ModelState.AddModelError(string.Empty, "La confirmación de contraseña o correo es incorrecta.");
                    return View(model);
                }

            }

            return View(model);
        }


        /// <summary>
        /// Valida inicio de sesión del usuario Enlace 
        /// Si la validación es correcta se obtiene el perfil del usuario 
        /// El perfil del usuario debera ser Enlace
        /// Si las cookies de inicio de sesión existen se deberan eliminar y sustituir por las nuevas 
        /// Esta sección es para controlar el logeo de un usuario de cualquiero otro perfil cambiar a usuario enlace.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public string ValidaInicioSesionEnlace(string userName, string pass)
        {
            
            int perfilUsu = 1;
            string perfil = "";
            //if (userBL.validarInicioSesion(userName, seguridad.Encriptar(pass)) == true)
            if (userBL.validarInicioSesion(userName, seguridad.Encriptar(pass)) == true)
            {
                

                model.listaUsuario = userBL.listUsuario(userName);


                foreach (var item in model.listaUsuario)
                {
                    perfilUsu = item.perfil;
                }

                modelPerf.listaPerfil = perfilBL.listPerfil(perfilUsu);
                foreach (var item in modelPerf.listaPerfil)
                {
                    perfil = item.perfil;
                }

               
                

                if (perfil == "Enlace")
                {
                    var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
                    var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
                    if (c1 != null && perf != null)
                    {
                        if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieUsuario"))
                        {
                            HttpCookie cookieUsuario = this.ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
                            cookieUsuario.Expires = DateTime.Now.AddDays(-1);
                            this.ControllerContext.HttpContext.Response.Cookies.Add(cookieUsuario);
                        }
                        if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookiePerfilU"))
                        {
                            HttpCookie cookiePerfilU = this.ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
                            cookiePerfilU.Expires = DateTime.Now.AddDays(-1);
                            this.ControllerContext.HttpContext.Response.Cookies.Add(cookiePerfilU);
                        }
                        HttpCookie cookieUsuario1 = new HttpCookie("CookieUsuario");
                        HttpCookie cookiePerfilU1 = new HttpCookie("CookiePerfilU");
                        cookieUsuario1.Value = seguridad.Encriptar(userName);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookieUsuario1);
                        cookiePerfilU1.Value = seguridad.Encriptar(perfil);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookiePerfilU1);
                    }
                    else
                    {
                        HttpCookie cookieUsuario = new HttpCookie("CookieUsuario");
                        HttpCookie cookiePerfilU = new HttpCookie("CookiePerfilU");
                        cookieUsuario.Value = seguridad.Encriptar(userName);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookieUsuario);
                        cookiePerfilU.Value = seguridad.Encriptar(perfil);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookiePerfilU);
                    }
                    
                }
                

                return perfil;
            }
            else
            {
                perfil = "NO";
                return perfil;

            }
        }



    }
    }
