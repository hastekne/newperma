using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BusinessModel.Business.ModuloBP;
using BusinessModel.Business.Global;
using BusinessModel.Business.Usuario;

using SeccionAdministrador.Models;

using System.IO;
using SeccionAdministrador.Helpers;

using BusinessModel.Models.Usuario;
using System.Text.RegularExpressions;



namespace SeccionAdministrador.Controllers
{
    public class AdminController : Controller
    {
        UsuarioBL userBL = new UsuarioBL();
        PerteneceInsSisBL insSisBL = new PerteneceInsSisBL();
        ImagenModuloBannerBL bannerBL = new ImagenModuloBannerBL();
        FuncionDesarrolloPBL funcionBL = new FuncionDesarrolloPBL();
        EjeBL ejeBL = new EjeBL();
        ModuloBL moduloBL = new ModuloBL();

        UsuarioViewModel modelUsuario = new UsuarioViewModel();
        PerteneceInsSisViewModel modelInsSis = new PerteneceInsSisViewModel();
        ImagenModuloBannerViewModel modelBanner = new ImagenModuloBannerViewModel();
        FuncionDesarrolloPViewModel modelFuncion = new FuncionDesarrolloPViewModel();
        EjeViewModel modelEje = new EjeViewModel();
        ModuloViewModel modelModulo = new ModuloViewModel();

        UsuariosRBP modelTabUsuRev = new UsuariosRBP();
        Seguridad seguridad = new Seguridad();
        EnviarCorreo enviarCorreo = new EnviarCorreo();

        //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.66", "SEG\\mc_mendoza", "M2pm2019");
        //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.69", "SEG\\mc_mendoza", "M2pm2019");
        //FTPClient con = new FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
        Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.69", "SEG\\mc_mendoza", "M2pm2019");//Mi casa

        /// <summary>
        /// Carga la vista para la sección Administrador
        /// </summary>
        /// <returns></returns>
        public ActionResult InicioAdmin()
        {
            return View();
        }

        /// <summary>
        /// Retorna la vista parcial correspondiente al menu seleccionado BP o evento
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CambiarContenido(string menu)
        {
            string vistaParcial = "";
            switch (menu)
            {
                case "BP":
                    vistaParcial = "_ModuloBP";
                    break;
                case "Evento":
                    vistaParcial = "../ModuloEventoAdmin/_ModuloEvento";
                    break;

            }


            return PartialView(vistaParcial);
        }

        /// <summary>
        /// Retorna las vistas parciales de las secciones del menú de BP 
        /// Prepara cada una de las vistas parciales solo si es la solicitada
        /// </summary>
        /// <param name="seccion"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SeccionBP(string seccion)
        {
            string vistaParcial = "";

            switch (seccion)
            {
                case "DBP":
                    CargaDatosBP();
                    vistaParcial = "ModuloBP/_DatosBP";
                    var tuple = new Tuple<ModuloViewModel, EjeViewModel, FuncionDesarrolloPViewModel>(modelModulo, modelEje, modelFuncion);
                    return PartialView(vistaParcial, tuple);
                case "Banner":
                    CargarTabBanner("01000000");
                    vistaParcial = "ModuloBP/_Banner";
                    return PartialView(vistaParcial, modelBanner);
                case "Revi":
                    vistaParcial = "ModuloBP/_Revisores";
                    CargarRevisores();
                    var tupleR = new Tuple<UsuarioViewModel, UsuariosRBP>(modelUsuario, modelTabUsuRev);
                    return PartialView(vistaParcial, tupleR);
            }

            return PartialView(vistaParcial);

        }

        /// <summary>
        /// Método utilizado para cargar la vista parcial _DatosBP
        /// Obtiene los datos de la BP
        /// </summary>
        public void CargaDatosBP()
        {
            modelModulo.listaModulos = moduloBL.getListModulo("01000000");
            foreach (var item in modelModulo.listaModulos)
            {
                modelModulo.titulo = item.titulo;

            }
            modelEje.listaEjes = ejeBL.getListaEje();
            modelFuncion.listFunciones = funcionBL.getFuncionesDesarrollo();

        }

        /// <summary>
        /// Retorna la lista de imagenes para los módulos de BP y Eventos
        /// </summary>
        /// <param name="modulo"></param>
        public void CargarTabBanner(string modulo)
        {
            switch (modulo)
            {
                case "01000000":
                    modelBanner.listaImagenes = bannerBL.getModuloImagenes("01000000");
                    break;
                case "02000000":
                    modelBanner.listaImagenes = bannerBL.getModuloImagenes("02000000");
                    break;
            }
        }

        /// <summary>
        /// Obtiene a los usuarios dados de alta como revisores.
        /// </summary>
        public void CargarRevisores()
        {
            modelTabUsuRev.listaInfo = userBL.getUserRevisor();
            string[] array = { "UEMSTAyCM", "CECyTE", "CEB", "SABES", "UG", "CECyT 17-IPN", "UEMSTIS", "CONALEP", "CAED", "TBC", "EPRR", "BBM" };
            modelUsuario.listaInsSis = new List<string>(array);
        }

        /// <summary>
        /// Sección para la vista previa del archivo, introduccion o MBP
        /// Con base en la extension del archivo se selecciona el MIME y se envia a al método de la vista parcial
        /// </summary>
        /// <param name="seccion"></param>
        /// <returns></returns>
        public ActionResult VistaPreviaArchivo(string seccion)
        {
            string ruta = "";
            switch (seccion)
            {
                case "Intro":
                    modelModulo.listaModulos = moduloBL.getListModulo("01000000");
                    foreach (var item in modelModulo.listaModulos)
                    {
                        ruta = item.archivoIntroduccion;
                    }
                    //string extension = Path.GetExtension(ruta);
                    //if(extension==".pptx" || extension == ".docx")
                    //{
                    //    seccion = "otra";
                    //}
                    break;
                case "MBP":
                    if (con.getFileSize("MesBP/BPMes.webm") !="")
                    {
                        ruta = "MesBP\\BPMes.webm";
                    }
                    else if (con.getFileSize("MesBP/BPMes.pptx") != "")
                    {
                        ruta = "MesBP\\BPMes.pptx";
                    }
                    else if (con.getFileSize("MesBP/BPMes.ogv") != "")
                    {
                        ruta = "MesBP\\BPMes.ogv";
                    }
                    else if (con.getFileSize("MesBP/BPMes.m4v") != "")
                    {
                        ruta = "MesBP\\BPMes.m4v";
                    }
                    break;
            }
            
            if(seccion=="Intro")
            return RedirectToAction("Descarga", "VisualizacionPrevia", new { archivo = ruta, sec =seccion });
            else
            {
                string nombre = Path.GetFileName(ruta);
                byte[] filedata = System.IO.File.ReadAllBytes(con.downloadVistaPrevia(ruta));
                return File(filedata, System.Net.Mime.MediaTypeNames.Application.Octet, nombre);
                //return RedirectToAction("Plantilla", "VisualizacionPrevia");
            }

        }


        /// <summary>
        /// Recibe como parámetro el titulo y lo actualza 
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        [HttpPost]
        public string ActualizarTitulo(string titulo)
        {
            string guardado = "false";
            if (moduloBL.editarTitulo("01000000", titulo) == true)
            {
                guardado = "true";
            }
            return guardado;
        }

        /// <summary>
        /// Sección para subir la introducción o Mejor BP en el módulo de BP 
        /// Se valida si los archivos ya existen,en caso de existir se debe de eliminar 
        /// El archivo es subido al servidor FTP, si el registro existe en la BD se actualiza en cas contrario se inserta el nuevo registro
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="ruta"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        [HttpPost]
        public string Subir(string seccion, string ruta, string archivo)
        {
            string subido = "false";
            String[] MiArray = ruta.Split(',');
            string b64 = MiArray[1];
            Byte[] bytes = Convert.FromBase64String(b64);

            string extension = Path.GetExtension(archivo).ToLower();


            //Se almacea en un tempPath la conversion del base64 data
            string fileNameX2 = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + extension.ToLower();
            System.IO.File.WriteAllBytes(fileNameX2, bytes);

            //Valida que el archivo cumpla con el tamaño máximo permitido 
            FileInfo fileIn = new FileInfo(fileNameX2);
            var tam = fileIn.Length;
            if (tam > 100000000 || tam < 0) //100,000,000 byte=100MB
            {
                //Mensaje, tamaño de la imagen mayor al permitido

            }
            else
            {
                string rutaDestinoFTP = "";
                if (seccion == "Intro")
                    rutaDestinoFTP = "Introduccion\\" + "01000000" + extension;
                else
                    rutaDestinoFTP = "MesBP\\" + "BPMes" + extension;

                if (con.conexionFTP("Index.png") == true)
                {
                    if (seccion == "Intro")
                    {
                        modelModulo.listaModulos = moduloBL.getListModulo("01000000");
                        foreach (var item in modelModulo.listaModulos)
                        {
                            modelModulo.archivoIntroduccion = item.archivoIntroduccion;
                        }
                        con.delete(modelModulo.archivoIntroduccion);
                        if (moduloBL.editarArchivo("01000000", rutaDestinoFTP) == false)
                        {
                            ModelState.AddModelError(string.Empty, "No se pudo subir el archivo");
                        }
                        else
                        {
                            con.upload(rutaDestinoFTP, fileNameX2);
                            subido = "true";
                        }
                    }
                    else
                    {
                        EliminarMBP();
                        con.upload(rutaDestinoFTP, fileNameX2);
                        subido = "true";
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Por el momento no se pueden subir archivos.");
                }
            }
            return subido;
        }

        /// <summary>
        /// Sección para eliminar una mejor práctica 
        /// </summary>
        public void EliminarMBP()
        {
            //if (con.conexionFTP("MesBP/BPMes.pptx") == true)
            //{
            //    con.delete("MesBP/BPMes.pptx");
            //}
            //else if (con.conexionFTP("MesBP/BPMes.webm") == true)
            //{
            //    con.delete("MesBP/BPMes.webm");
            //}
            //else if (con.conexionFTP("MesBP/BPMes.ogv") == true)
            //{
            //    con.delete("MesBP/BPMes.ogv");
            //}
            //else if (con.conexionFTP("MesBP/BPMes.m4v") == true)
            //{
            //    con.delete("MesBP/BPMes.m4v");
            //}

            if (con.getFileSize("MesBP/BPMes.webm") != "")
            {
                con.delete("MesBP/BPMes.webm");
            }
            else if (con.getFileSize("MesBP/BPMes.pptx") != "")
            {
                con.delete("MesBP/BPMes.pptx");
            }
            else if (con.getFileSize("MesBP/BPMes.ogv") != "")
            {
                con.delete("MesBP/BPMes.ogv");
            }
            else if (con.getFileSize("MesBP/BPMes.m4v") != "")
            {
                con.delete("MesBP/BPMes.m4v");
            }
        }

        /// <summary>
        /// Sección para agregar un nuevo eje o una función de desarrollo para la BP 
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="nuevo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Nuevo(string seccion, string nuevo)
        {
            if (seccion == "eje")
            {
                ejeBL.agregar(nuevo, true);
                modelEje.listaEjes = ejeBL.getListaEje();
                return PartialView("ModuloBP/_TablaEje", modelEje);
            }
            else
            {
                funcionBL.agregarFD(nuevo, true);
                modelFuncion.listFunciones = funcionBL.getFuncionesDesarrollo();
                return PartialView("ModuloBP/_TablaFuncionesD", modelFuncion);
            }
        }

        /// <summary>
        /// Sección para actualizar los datos de la BP, actualiza los ejes o las Funciones de desarrollo
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="id"></param>
        /// <param name="nuevo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ActualizarDatosBP(string seccion, int id, string nuevo)
        {
            if (seccion == "eje")
            {
                ejeBL.editarEje(id, nuevo);
                modelEje.listaEjes = ejeBL.getListaEje();
                return PartialView("ModuloBP/_TablaEje", modelEje);
            }
            else
            {
                funcionBL.editarFD(id, nuevo);
                modelFuncion.listFunciones = funcionBL.getFuncionesDesarrollo();
                return PartialView("ModuloBP/_TablaFuncionesD", modelFuncion);
            }
        }

        /// <summary>
        /// Sección para actualizar los estados de eje o de las funciones de desarrollo 
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="id"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EstadoDatosBP(string seccion, int id, bool estado)
        {
            if (seccion == "eje")
            {
                ejeBL.actualizarEstado(id, estado);
                modelEje.listaEjes = ejeBL.getListaEje();
                return PartialView("ModuloBP/_TablaEje", modelEje);
            }
            else
            {
                funcionBL.actualizarEstado(id, estado);
                modelFuncion.listFunciones = funcionBL.getFuncionesDesarrollo();
                return PartialView("ModuloBP/_TablaFuncionesD", modelFuncion);
            }
        }

        /// <summary>
        /// Sección para la carga de una imagen al servidor FTP y su registro a la base de datos 
        /// Imagen utilizada para la sección de banner de la BP
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubirBPBanner(string ruta, string archivo)
        {
            String[] MiArray = ruta.Split(',');
            string b64 = MiArray[1];
            Byte[] bytes = Convert.FromBase64String(b64);

            string extension = Path.GetExtension(archivo).ToLower();
            string nombreA = Path.GetFileName(archivo).ToLower();


            //Se almacea en un tempPath la conversion del base64 data
            string fileNameX2 = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + extension.ToLower();
            System.IO.File.WriteAllBytes(fileNameX2, bytes);

            //Valida que el archivo cumpla con el tamaño máximo permitido 
            FileInfo fileIn = new FileInfo(fileNameX2);
            var tam = fileIn.Length;
            if (tam > 100000000 || tam < 0) //100,000,000 byte=100MB
            {
                //Mensaje, tamaño de la imagen mayor al permitido

            }
            else
            {


                string rutaDestinoFTP = "Banner\\01000000\\" + nombreA;


                if (con.conexionFTP("Index.png") == true)
                {
                    if (con.conexionFTP(rutaDestinoFTP) == false)
                    {
                        if (bannerBL.agregarImagen("01000000", rutaDestinoFTP) == true)
                            con.upload(rutaDestinoFTP, fileNameX2);
                    }
                    else
                    {
                        //El nombre de la imagen no se puede repetir
                        ModelState.AddModelError(string.Empty, "El archivo que desea subir ya existe.");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Por el momento no se pueden subir archivos.");
                }
            }
            CargarTabBanner("01000000");
            return PartialView("ModuloBP/_TablaImgBanner", modelBanner);
        }


        /// <summary>
        /// Sección para eliminar una imagen de banner en el módulo de BP
        /// </summary>
        /// <param name="idImg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EliminarBPBanner(int idImg)
        {
            if (con.conexionFTP("Index.png") == true)
            {
                foreach (var item in bannerBL.getListImagen(idImg))
                {
                    modelBanner.imagen = item.imagen;
                }


                if (bannerBL.EliminarImagen(idImg) == true)
                {
                    con.delete(modelBanner.imagen);
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Por el momento no se pueden subir archivos.");
            }
            CargarTabBanner("01000000");
            return PartialView("ModuloBP/_TablaImgBanner", modelBanner);
        }

        /// <summary>
        /// Actualiza el estado de un revisor, determina si el estado es activo o inhabilitado
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EstadoRevisor(string usuario, bool estado)
        {
            userBL.editarEstado(usuario, estado);
            CargarRevisores();
            var tupleR = new Tuple<UsuarioViewModel, UsuariosRBP>(modelUsuario, modelTabUsuRev);
            return PartialView("ModuloBP/_TablaRevisores", tupleR);
        }

        /// <summary>
        /// Sección para actualizar los datos de un usuario revisor 
        /// Se valida que los parámetros recividos sean correctos
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="nomU"></param>
        /// <param name="apPU"></param>
        /// <param name="apMU"></param>
        /// <param name="emailU"></param>
        /// <param name="telU"></param>
        /// <param name="insSu"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ActualizarRevi(string usuario, string nomU, string apPU, string apMU, string emailU, string telU, string insSu)
        {
            //string select = "selectInsSis_" + usuario;
            //string select = "selectInsSisRev";
            Regex erTel = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            Regex erEmail = new Regex(@".+\.[a-z]{2,}");
            if (erTel.IsMatch(telU) == true && erEmail.IsMatch(emailU) == true)
            {
                userBL.editar(usuario, nomU, apPU, apMU, telU, emailU);
                insSisBL.editarPerInsSis(usuario, insSu);
                CargarRevisores();
                var tupleR = new Tuple<UsuarioViewModel, UsuariosRBP>(modelUsuario, modelTabUsuRev);
                return PartialView("ModuloBP/_TablaRevisores", tupleR);
            }
            else
            {
                
                return View();
            }
                

            
        }

        /// <summary>
        /// Sección para generar una contraseña de manera aleatoria utilizada por el módulo NuevoRevisor
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerarPass(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        /// <summary>
        /// Sección para crear un usuario con perfil revisor 
        /// La dirección de correo será a la cual le llegue su usuario y contraseña para el acceso a la sesión de BP usuario revisor
        /// Se genera la contraseña utilizando GenerarPass()
        /// </summary>
        /// <param name="nomU"></param>
        /// <param name="apPU"></param>
        /// <param name="apMU"></param>
        /// <param name="emailU"></param>
        /// <param name="telU"></param>
        /// <param name="insSu"></param>
        /// <returns></returns>
        [HttpPost]
        public string NuevoRevisor(string nomU, string apPU, string apMU, string emailU, string telU, string insSu)
        {
            string usuario = "";
            string pass = "";
            string registro = "true";
            Regex erTel = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            Regex erEmail = new Regex(@".+\.[a-z]{2,}");
            if (erTel.IsMatch(telU) == true && erEmail.IsMatch(emailU) == true)
            {
                Random random = new Random();
                int num = random.Next(1000);
                usuario = nomU.Substring(0, 2).ToUpper() + apPU.Substring(0, 2) + apMU.Substring(0, 2) + num.ToString();
                while (userBL.UserExist(usuario) == true)
                {
                    usuario = nomU.Substring(0, 2).ToUpper() + apPU.Substring(0, 2) + apMU.Substring(0, 2) + num.ToString();
                }
                pass = GenerarPass(8);
                string passEn = seguridad.Encriptar(pass);
                userBL.agregar(usuario, nomU, apPU, apMU, passEn, emailU, telU, true, 2);

                insSisBL.agregarUserInsSis(usuario, insSu);

                enviarCorreo.EnviarEmail("Bienvenido", pass, emailU, usuario);
                TempData["mesj"] = "NO";
            }
            else
            {
                //Mensaje de datos incorrectos para dar de alta un Revisor.
                TempData["mesj"] = "Error en la captura de datos del Usuario Revisor.";
                //ModelState.AddModelError("TabMsjRevi", "Error en la captura de datos del Usuario Revisor.");
                registro = "false";


            }

            //CargarRevisores();
            //var tupleR = new Tuple<UsuarioViewModel, UsuariosRBP>(modelUsuario, modelTabUsuRev);
            ////return PartialView("ModuloBP/_TablaRevisores", tupleR);

            return registro;
        }

        /// <summary>
        /// Sección que obtiene la vista parcial mostrando la tabla de revisores
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _TablaRevisores()
        {
            CargarRevisores();
            if (TempData["mesj"].ToString() != "NO")
                ModelState.AddModelError("TabMsjRevi", TempData["mesj"].ToString());

            var tupleR = new Tuple<UsuarioViewModel, UsuariosRBP>(modelUsuario, modelTabUsuRev);
            return PartialView("ModuloBP/_TablaRevisores", tupleR);
        }

        /// <summary>
        /// Sección que muestra el popUP vista parsial con los datos del revisor 
        /// </summary>
        /// <param name="usu"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult _PopUpRevisor(string usu)
        {
            string[] array = { "UEMSTAyCM", "CECyTE", "CEB", "SABES", "UG", "CECyT 17-IPN", "UEMSTIS", "CONALEP", "CAED", "TBC", "EPRR", "BBM" };
            modelUsuario.listaInsSis = new List<string>(array);
            modelUsuario.nombreUsuario = usu;
            modelUsuario.listaUsuario = userBL.listUsuario(usu);
            foreach (var item in modelUsuario.listaUsuario)
            {
                modelUsuario.nombre = item.nombre;
                modelUsuario.pApellido = item.pApellido;
                modelUsuario.sApellido = item.sApellido;
                modelUsuario.telefono = item.telefono;
                modelUsuario.correoElectronico = item.correoElectronico;

            }
            modelInsSis.listaInsSis = insSisBL.getInstiSub(usu);
            foreach (var item2 in modelInsSis.listaInsSis)
            {
                modelInsSis.institucionSistema = item2.institucionSistema;
            }
            var tupleR = new Tuple<UsuarioViewModel, PerteneceInsSisViewModel>(modelUsuario, modelInsSis);
            return PartialView("ModuloBP/_PopUpDatosRevisor", tupleR);

        }

        /// <summary>
        /// Sección para reenviar email al usuario revisor 
        /// Toma los datos del usuario desde la BD
        /// </summary>
        /// <param name="usu"></param>
        public void ReenviarEmail(string usu)
        {
            modelUsuario.listaUsuario = userBL.listUsuario(usu);
            foreach(var item in modelUsuario.listaUsuario)
            {
                modelUsuario.correoElectronico = item.correoElectronico;
                modelUsuario.contrasenia = seguridad.DesEncriptar(item.contrasenia);
                modelUsuario.nombreUsuario = item.nombreUsuario;
            }
            enviarCorreo.EnviarEmail("Bienvenido", modelUsuario.contrasenia, modelUsuario.correoElectronico, modelUsuario.nombreUsuario);
        }

        /// <summary>
        /// Sección que retorna la vista parcial del PopUpEje 
        /// Obtiene los datos del eje correspondiente el parámetro id recivido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult _PopUpEje(int id)
        {
            modelEje.nombre = ejeBL.getNombreEje(id);
            modelEje.idEje = id;
            return PartialView("ModuloBP/_PopUpModEje",modelEje);

        }

        /// <summary>
        /// Sección que obtiene la vista parcial para el popUp Función desarrollo 
        /// Obtiene los datos de la función desarrollo utilizando el parámetro id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult _PopUpFuncionD(int id)
        {
            modelFuncion.nombreFunc = funcionBL.getNomFunc(id);
            modelFuncion.idFuncionD = id;
            return PartialView("ModuloBP/_PopUpModFuncionD", modelFuncion);
        }

        /// <summary>
        /// Ejemplo prueba, no es utilizado en el proyecto
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminEventos()
        {
            return View();
        }

    }
}