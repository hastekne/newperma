using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SeccionAdministrador.Models;
using SeccionAdministrador.Models.Global;
using SeccionAdministrador.Models.Eventos;

using BusinessModel.Business.Global;
using BusinessModel.Business.ModuloEvento;
using BusinessModel.Business.Usuario;

using SeccionAdministrador.Helpers;
using System.Text.RegularExpressions;
using BusinessModel.Models.Usuario;
using System.Data;

//Para la paginación en la tabla
using PagedList;
using BusinessModel.Models.ModuloEvento;

using ClosedXML.Excel;

//Librería ITextSharp
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


//Librería iTextSharp XML Worker
using iTextSharp.tool.xml;

using System.IO.Compression;


namespace SeccionAdministrador.Controllers
{
    public class ModuloEventoAdminController : Controller
    {
        ImagenModuloBannerBL bannerBL = new ImagenModuloBannerBL();
        UsuarioBL userBL = new UsuarioBL();
        PerteneceInsSisBL insSisBL = new PerteneceInsSisBL();
        MunicipioBL municipioBL = new MunicipioBL();
        EventoBL eventoBL = new EventoBL();
        MunicipioEventoBL muniEventoBL = new MunicipioEventoBL();
        FuncionParticipanteBL funcPartBL = new FuncionParticipanteBL();
        CampoEventoBL campoEvenBL = new CampoEventoBL();
        RegistroEventoBL blRegistro = new RegistroEventoBL();
        TipoRegistroBL blTipoReg = new TipoRegistroBL();
        UserEnlaceEventoBL blUserEnlaceEvento = new UserEnlaceEventoBL();
        MesaEventoBL mesaEventoBL = new MesaEventoBL();
        MesaParticipanteBL mesaParticipanteBL = new MesaParticipanteBL();
        SubsistemaEventoBL subSisEvenBL = new SubsistemaEventoBL();
        EscuelaEventoBL escualaEBL = new EscuelaEventoBL();



        ImagenModuloBannerViewModel modelBanner = new ImagenModuloBannerViewModel();
        UsuarioViewModel modelUsuario = new UsuarioViewModel();
        PerteneceInsSisViewModel modelInsSis = new PerteneceInsSisViewModel();
        MunicipioViewModel modelMunicipio = new MunicipioViewModel();
        EventoViewModel modelEvento = new EventoViewModel();
        MunicipioEventoViewModel modelMuniEvento = new MunicipioEventoViewModel();
        FuncionParticipanteViewModel modelFuncPart = new FuncionParticipanteViewModel();
        CampoEventoViewModel modelCampoEvento = new CampoEventoViewModel();
        RegistroEventoViewModel modelRegEvento = new RegistroEventoViewModel();
        TipoRegistroViewModel modelTipoRegistro = new TipoRegistroViewModel();
        UserEnlaceEventoViewModel modelUserEnlaceEvento = new UserEnlaceEventoViewModel();
        MesaEventoViewModel modelMesaEvento = new MesaEventoViewModel();
        MesaParticipanteViewModel modelMesaParticipante = new MesaParticipanteViewModel();
        SubsistemaEventoViewModel modelSubsisEvento = new SubsistemaEventoViewModel();
        EscuelaEventoViewModel modelEscuelaEve = new EscuelaEventoViewModel();

        Seguridad seguridad = new Seguridad();
        EnviarCorreo enviarCorreo = new EnviarCorreo();
        UsuariosRBP modelTabUsuRev = new UsuariosRBP();

        //FTPClient con = new FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");//SEG
        Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.66", "SEG\\mc_mendoza", "M2pm2019");//Mi casa

        /// <summary>
        /// Obtiene la vista parcial correspondiente al parámetro sección del módulo evento
        /// Prepara cada vista parcial para poder ser utilizada 
        /// </summary>
        /// <param name="seccion"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SeccionEventos(string seccion)
        {
            string vistaParcial = "";

            switch (seccion)
            {
                case "Crear":
                    vistaParcial = "_CrearEvento";
                    //Cargar municipios
                    modelMunicipio.listMunicipios = municipioBL.getMunicipio();
                    var tupleCE = new Tuple<MunicipioViewModel>(modelMunicipio);
                    return PartialView(vistaParcial, tupleCE);
                case "Evento":
                    //vistaParcial = "_Eventos";
                    //modelEvento.listaEventos = eventoBL.getTodosEvento();
                    //var tupleEve = new Tuple<EventoViewModel>(modelEvento);
                    //return PartialView(vistaParcial, tupleEve);
                    return RedirectToAction("VistaEvento");
                case "DatosEvento":
                    vistaParcial = "_VPSeccionEvento/_DatosEvento";
                    return PartialView(vistaParcial);
                case "Enlaces":
                    CargarEnlaces();
                    vistaParcial = "_Enlaces";
                    var tupleR = new Tuple<UsuarioViewModel, UsuariosRBP>(modelUsuario, modelTabUsuRev);
                    return PartialView(vistaParcial, tupleR);
                case "Banner":
                    modelBanner.listaImagenes = bannerBL.getModuloImagenes("02000000");
                    vistaParcial = "_Banner";
                    return PartialView(vistaParcial, modelBanner);
                case "Registro":
                    vistaParcial = "_Registrar";
                    modelEvento.listaEventos = eventoBL.getEvento("PUBLICADO");
                    var tupleReg = new Tuple<EventoViewModel>(modelEvento);
                    return PartialView(vistaParcial, tupleReg);
                case "MisRegistros":
                    //vistaParcial = "_MisRegistros";
                    //return PartialView(vistaParcial);
                    return RedirectToAction("VistaMisRegistros");
            }

            return PartialView(vistaParcial);

        }

        /// <summary>
        /// Retorna la vista parcial para la Sección de los datos del evento
        /// Utilizando el parámetro idEvento obtiene todos los datos utilizados en esta vista 
        /// Utiliza método getEvento para obtener los datos del evento, obtiene listMunicipios, listaSubsisEvento, tablaEscuelaCantida
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SeccionDatosEvento(int idEvento)
        {
            ViewData["idEvento"] = idEvento.ToString();
            ViewBag.idEventoTD = idEvento.ToString();
            modelEvento.listaEventos = eventoBL.getEvento(idEvento);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.idEvento = item.idEvento;
                modelEvento.nombre = item.nombre;
                TempData["EstadoEvento"] = item.estado;
                modelEvento.objetivo = item.objetivo;
                modelEvento.fRegistroInicio = item.fRegistroInicio;
                modelEvento.fRegistroFin = item.fRegistroFin;
                modelEvento.fVigenciaInicio = item.fVigenciaInicio;
                modelEvento.fVigenciaFin = item.fVigenciaFin;
                modelEvento.tipoEvento = item.tipoEvento;
            }
            //cargar todos los municipios
            modelMunicipio.listMunicipios = municipioBL.getMunicipio();
            //Cargar Subsistema y cantidad por idEvento
            modelSubsisEvento.listaSubsisEvento = subSisEvenBL.getSubsisEventXidEven(idEvento);
            //Carga las escuelas permitidas para el evento con la cantidad
            //modelEscuelaEve.listaEscuelasEvento = escualaEBL.getEscuelaEventoXidEven(idEvento);
            modelEscuelaEve.tablaEscuelaCantida = escualaEBL.getEventoTabla(idEvento);

            string vistaParcial = "_VPSeccionEvento/_DatosEvento";
            var tuple = new Tuple<EventoViewModel, MunicipioViewModel, SubsistemaEventoViewModel, EscuelaEventoViewModel>(modelEvento, modelMunicipio, modelSubsisEvento, modelEscuelaEve);
            return PartialView(vistaParcial, tuple);
        }

        /// <summary>
        /// Retorna un string con todos los campos habilitados para el registro al evento correspondiente al parámetro idE
        /// </summary>
        /// <param name="idE"></param>
        /// <returns></returns>
        public string SeleccionarCheckSelect(string idE)
        {
            int idEvento = int.Parse(idE);
            //Comienza a concatenar en un string todos los municipios
            string listaMuniE = "muni";
            modelMuniEvento.listaMuniEvento = muniEventoBL.getMuniEvenXidEven(idEvento);
            foreach (var item in modelMuniEvento.listaMuniEvento)
            {
                string nomM = municipioBL.getNomMunicipio(item.idMunicipio);
                listaMuniE = listaMuniE + "," + nomM;
            }

            listaMuniE = listaMuniE + "," + "campo";

            //Comienza a concatenar en el strin los campos habilidatos del evento
            foreach (var campo in campoEvenBL.camposEvento(idEvento))
            {
                switch (campo)
                {
                    case "CCT":
                        listaMuniE = listaMuniE + "," + "CCT";
                        break;
                    case "nombrePlantel":
                        listaMuniE = listaMuniE + "," + "Nombre del plantel";
                        break;
                    case "municipio":
                        listaMuniE = listaMuniE + "," + "Municipio";
                        break;
                    case "regionSEG":
                        listaMuniE = listaMuniE + "," + "Región SEG";
                        break;
                    case "nivel":
                        listaMuniE = listaMuniE + "," + "Nivel";
                        break;
                    case "instiSubsis":
                        listaMuniE = listaMuniE + "," + "Institución/Subsistema";
                        break;
                    case "CURP":
                        listaMuniE = listaMuniE + "," + "CURP";
                        break;
                    case "nombre":
                        listaMuniE = listaMuniE + "," + "Nombre (s)";
                        break;
                    case "apellidoPat":
                        listaMuniE = listaMuniE + "," + "Primer apellido";
                        break;
                    case "apellidoMat":
                        listaMuniE = listaMuniE + "," + "Segundo apellido";
                        break;
                    case "correoElectronico":
                        listaMuniE = listaMuniE + "," + "Correo electrónico";
                        break;
                    case "telefono":
                        listaMuniE = listaMuniE + "," + "Teléfono de contacto";
                        break;
                    case "sexo":
                        listaMuniE = listaMuniE + "," + "Sexo";
                        break;
                    case "funcion":
                        listaMuniE = listaMuniE + "," + "Función";
                        break;
                }
            }
            if (campoEvenBL.campoHabilitado(idEvento, "mesaTrabajo") == true)
                listaMuniE = listaMuniE + "," + "Mesa de trabajo";

            //Concatenar las funciones participantes del evento
            listaMuniE = listaMuniE + "," + "func";
            modelFuncPart.listaFuncionPart = funcPartBL.getFuncionEventoXidEvento(idEvento);
            foreach (var item in modelFuncPart.listaFuncionPart) {
                listaMuniE = listaMuniE + "," + item.nomFuncion;
            }

            return listaMuniE;
        }

        /// <summary>
        /// Retorna un string con todos los municipios asignados a un evento, utiliza el parámetro idE para generar el acceso a datos
        /// </summary>
        /// <param name="idE"></param>
        /// <returns></returns>
        public string ListaMuniEvento(string idE)
        {
            int idEvento = int.Parse(idE);
            string listaMuniE = "";
            modelMuniEvento.listaMuniEvento = muniEventoBL.getMuniEvenXidEven(idEvento);
            foreach (var item in modelMuniEvento.listaMuniEvento)
            {
                string nomM = municipioBL.getNomMunicipio(item.idMunicipio);
                listaMuniE = nomM + "," + listaMuniE;
            }
            return listaMuniE;
        }

        /// <summary>
        /// Retorna un string con todos los campos del evento  activiados 
        /// </summary>
        /// <param name="idE"></param>
        /// <returns></returns>
        public string ListaCampoEvento(string idE)
        {
            int idEvento = int.Parse(idE);
            string listaCampoE = "";
            foreach (var campo in campoEvenBL.camposEvento(idEvento))
            {
                switch (campo) {
                    case "CCT":
                        listaCampoE = "CCT" + "," + listaCampoE;
                        break;
                    case "nombrePlantel":
                        listaCampoE = "Nombre del plantel" + "," + listaCampoE;
                        break;
                    case "municipio":
                        listaCampoE = "Municipio" + "," + listaCampoE;
                        break;
                    case "regionSEG":
                        listaCampoE = "Región SEG" + "," + listaCampoE;
                        break;
                    case "nivel":
                        listaCampoE = "Nivel" + "," + listaCampoE;
                        break;
                    case "instiSubsis":
                        listaCampoE = "Institución/Subsistema" + "," + listaCampoE;
                        break;
                    case "CURP":
                        listaCampoE = "CURP" + "," + listaCampoE;
                        break;
                    case "nombre":
                        listaCampoE = "Nombre (s)" + "," + listaCampoE;
                        break;
                    case "apellidoPat":
                        listaCampoE = "Primer apellido" + "," + listaCampoE;
                        break;
                    case "apellidoMat":
                        listaCampoE = "Segundo apellido" + "," + listaCampoE;
                        break;
                    case "correoElectronico":
                        listaCampoE = "Correo electrónico" + "," + listaCampoE;
                        break;
                    case "telefono":
                        listaCampoE = "Teléfono de contacto" + "," + listaCampoE;
                        break;
                    case "sexo":
                        listaCampoE = "Sexo" + "," + listaCampoE;
                        break;
                    case "funcion":
                        listaCampoE = "Función" + "," + listaCampoE;
                        break;
                }
            }
            if (campoEvenBL.campoHabilitado(idEvento, "mesaTrabajo") == true)
                listaCampoE = "Mesa de trabajo" + "," + listaCampoE;

            return listaCampoE;
        }

        /// <summary>
        /// Retorna el estado del evento utilizando el valor de una variable temporal
        /// </summary>
        /// <returns></returns>
        public string EstadoEvento()
        {
            return TempData["EstadoEvento"].ToString();
        }

        /// <summary>
        /// Retorna Vista parcial de _eventos
        /// Paginación de tabla de eventos
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult VistaEvento(int? page)
        {
            ViewBag.palabraClave = "";
            int pageSize = 15;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<EventoEnlace> stu = null;

            EventoEnlace modelEv = new EventoEnlace();
            EventoBL blEv = new EventoBL();
            List<EventoEnlace> lista = new List<EventoEnlace>();
            lista = blEv.getEventoTabla("");
            modelEv.tablaEventoEnlace = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            string vistaParcial = "_Eventos";
            var tuple = new Tuple<IPagedList<EventoEnlace>>(stu);
            return PartialView(vistaParcial, tuple);
        }

       /// <summary>
       /// Aplica filtro a la tabla páginada en la vista parcial _Eventos
       /// Realiza la paginación de los eventos validados con el filtro 
       /// </summary>
       /// <param name="page"></param>
       /// <param name="palabraclave"></param>
       /// <returns></returns>
        public ActionResult VistaEventoFiltro(int? page, string palabraclave = "")
        {
            
                ViewBag.palabraClave = palabraclave;
                //page = 1;
                int pageSize = 15;//Cantidad de renglones que se mostraran de la tabla por pag.
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                IPagedList<EventoEnlace> stu = null;

                EventoEnlace modelEv = new EventoEnlace();
                EventoBL blEv = new EventoBL();
                List<EventoEnlace> lista = new List<EventoEnlace>();
                lista = blEv.getEventoTabla(palabraclave.Trim());
                modelEv.tablaEventoEnlace = lista;

                stu = lista.ToPagedList(pageIndex, pageSize);

                string vistaParcial = "_VPSeccionEvento/_TablaEventos";
                //var tuple = new Tuple<IPagedList<EventoEnlace>>(stu);
                return PartialView(vistaParcial, stu);
            
            
        }

        //_MisRegistros
        /// <summary>
        /// Retorna la vista parcial _MisRegistros preparada con los datos de todos los registros a los cuales se a inscrito el usuario 
        /// Sección de MisRegistros con paginación
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult VistaMisRegistros(int? page)
        {
            ViewBag.palabraClaveMR = "";
            int pageSize = 15;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<MisRegistros> stu = null;

            MisRegistros modelEv = new MisRegistros();
            EventoBL blEv = new EventoBL();
            List<MisRegistros> lista = new List<MisRegistros>();
            lista = blEv.getMisRegistros("CeciMP18", ViewBag.palabraClaveMR);//Cambiar después CeciMP18 por una cookie con el login user
            modelEv.tablaMisRegistros = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            string vistaParcial = "_MisRegistros";
            var tuple = new Tuple<IPagedList<MisRegistros>>(stu);
            return PartialView(vistaParcial, tuple);
        }

        /// <summary>
        /// Retorna el filtro sobre la vista parcial _TablaRegistros, se realiza páginación
        /// </summary>
        /// <param name="page"></param>
        /// <param name="palabraclave"></param>
        /// <returns></returns>
        public ActionResult VistaMisRegistrosFiltro(int? page, string palabraclave = "")
        {

            ViewBag.palabraClaveMR = palabraclave;
            //page = 1;
            int pageSize = 15;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<MisRegistros> stu = null;

            MisRegistros modelEv = new MisRegistros();
            EventoBL blEv = new EventoBL();
            List<MisRegistros> lista = new List<MisRegistros>();
            lista = blEv.getMisRegistros("CeciMP18",palabraclave.Trim());
            modelEv.tablaMisRegistros = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            string vistaParcial = "_TablaRegistros";
            //var tuple = new Tuple<IPagedList<EventoEnlace>>(stu);
            return PartialView(vistaParcial, stu);


        }

        /// <summary>
        /// Sección para la vista parcial _PopUpDatosEnlace 
        /// En esta vista se cargan todos los datos del usuario enlace 
        /// A los datos se accesa con el parámetro usu
        /// </summary>
        /// <param name="usu"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult _PopUpEnlace(string usu)
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
            return PartialView("_PopUpsEvento/_PopUpDatosEnlace", tupleR);


            //return PartialView("_PopUpsEvento/_PopUpDatosEnlace");
        }

        /// <summary>
        /// Sección para subir imagenes para el banner del módulo Evento
        /// Sube la imagen al servidor FTP y guarda los datos en la BD realiazndo un nuevo registro
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubirEventoBanner(string ruta, string archivo)
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


                string rutaDestinoFTP = "Banner\\02000000\\" + nombreA;


                if (con.conexionFTP("Index.png") == true)
                {
                    if (con.conexionFTP(rutaDestinoFTP) == false)
                    {
                        if (bannerBL.agregarImagen("02000000", rutaDestinoFTP) == true)
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
            modelBanner.listaImagenes = bannerBL.getModuloImagenes("02000000");
            return PartialView("_TablaImgEventoBanner", modelBanner);
        }

        /// <summary>
        /// Sección para eliminar un registro de imagen de banner en la BD utilizando el parámetro idImg 
        /// Además borra la imagen del servidor FTP
        /// </summary>
        /// <param name="idImg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EliminarEventoImgBanner(int idImg)
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
                ModelState.AddModelError(string.Empty, "Por el momento no se pueden eliminar archivos.");
            }
            modelBanner.listaImagenes = bannerBL.getModuloImagenes("02000000");
            return PartialView("_TablaImgEventoBanner", modelBanner);
        }

        //Código para la sección de Enlaces
        /// <summary>
        /// Sección para cargar obtener los enlaces, método consumido por otros mpetodos
        /// </summary>
        public void CargarEnlaces()
        {
            modelTabUsuRev.listaInfo = userBL.getUserRevisor(4);
            string[] array = { "UEMSTAyCM", "CECyTE", "CEB", "SABES", "UG", "CECyT 17-IPN", "UEMSTIS", "CONALEP", "CAED", "TBC", "EPRR", "BBM" };
            modelUsuario.listaInsSis = new List<string>(array);
        }

        /// <summary>
        /// Retorna vista parcial _TablaDatosEnlace 
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _TablaDatosEnlace()
        {
            CargarEnlaces();
            if (TempData["mesj"].ToString() != "NO")
                ModelState.AddModelError("TabMsjEnlace", TempData["mesj"].ToString());

            var tupleR = new Tuple<UsuarioViewModel, UsuariosRBP>(modelUsuario, modelTabUsuRev);
            return PartialView("_TablaDatosEnlace", tupleR);
        }

        /// <summary>
        /// Sección para dar de alta un nuevo enlace
        /// Valida que los parámetros recibidos sean correctos
        /// Si los datos son correctos se realiza el registro a la BD y se le notifica al usuario a través de un correo electrónico su contraseña y nombre de usuario
        /// Contraseña y nombre de usuario aleatorio
        /// </summary>
        /// <param name="nomU"></param>
        /// <param name="apPU"></param>
        /// <param name="apMU"></param>
        /// <param name="emailU"></param>
        /// <param name="telU"></param>
        /// <param name="insSu"></param>
        /// <returns></returns>
        [HttpPost]
        public string NuevoEnlace(string nomU, string apPU, string apMU, string emailU, string telU, string insSu)
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
                userBL.agregar(usuario, nomU, apPU, apMU, passEn, emailU, telU, true, 4);

                insSisBL.agregarUserInsSis(usuario, insSu);

                enviarCorreo.EnviarEmail("BienvenidoEnlace", pass, emailU, usuario);
                TempData["mesj"] = "NO";
            }
            else
            {
                TempData["mesj"] = "Error en la captura de datos del Usuario Enlace.";
                registro = "false";
            }
            return registro;
        }

        /// <summary>
        /// Retorna contraseña generada de manera aleatoria
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
        /// Actualiza el estado del usuario enlace, activo o desactivo y recarga la vista parcial _TablaDatosEnlace
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EstadoEnlace(string usuario, bool estado)
        {
            userBL.editarEstado(usuario, estado);
            CargarEnlaces();
            var tupleR = new Tuple<UsuarioViewModel, UsuariosRBP>(modelUsuario, modelTabUsuRev);
            return PartialView("_TablaDatosEnlace", tupleR);
        }

        /// <summary>
        /// Actualiza los datos del enlace mediante los parámetros introducidos
        /// Realiza validaciones sobre los parámetros 
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
        public ActionResult ActualizarEnlace(string usuario, string nomU, string apPU, string apMU, string emailU, string telU, string insSu)
        {
            Regex erTel = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            Regex erEmail = new Regex(@".+\.[a-z]{2,}");
            if (erTel.IsMatch(telU) == true && erEmail.IsMatch(emailU) == true)
            {
                userBL.editar(usuario, nomU, apPU, apMU, telU, emailU);
                insSisBL.editarPerInsSis(usuario, insSu);
                CargarEnlaces();
                var tupleR = new Tuple<UsuarioViewModel, UsuariosRBP>(modelUsuario, modelTabUsuRev);
                return PartialView("_TablaDatosEnlace", tupleR);
            }
            else
            {

                return View();
            }



        }

        /// <summary>
        /// Sección para reenviar un email al usuario enlace con los datos de acceso a su sesión
        /// </summary>
        /// <param name="usu"></param>
        public void ReenviarEmailEnlace(string usu)
        {
            modelUsuario.listaUsuario = userBL.listUsuario(usu);
            foreach (var item in modelUsuario.listaUsuario)
            {
                modelUsuario.correoElectronico = item.correoElectronico;
                modelUsuario.contrasenia = seguridad.DesEncriptar(item.contrasenia);
                modelUsuario.nombreUsuario = item.nombreUsuario;
            }
            enviarCorreo.EnviarEmail("BienvenidoEnlace", modelUsuario.contrasenia, modelUsuario.correoElectronico, modelUsuario.nombreUsuario);
        }

        /// <summary>
        /// Primera sección para crear el evento 
        /// Realiza validaciones de los parametros definidos 
        /// Si todo es correcto se crea el evento
        /// Para obtener los datos de municipios, campos y funcionaes se realiza un split y se genera el registro en la tabla correspondiente
        /// </summary>
        /// <param name="nombreE"></param>
        /// <param name="objetivoE"></param>
        /// <param name="rutaIconoE"></param>
        /// <param name="rutaExcelE"></param>
        /// <param name="fvi"></param>
        /// <param name="fvf"></param>
        /// <param name="fri"></param>
        /// <param name="frf"></param>
        /// <param name="tipoEvento"></param>
        /// <param name="archivoIcono"></param>
        /// <param name="archivoExcel"></param>
        /// <param name="municipios"></param>
        /// <param name="campos"></param>
        /// <param name="funciones"></param>
        /// <returns></returns>
        [HttpPost]
        public string CrearEventoPS(string nombreE, string objetivoE, string rutaIconoE, string rutaExcelE, string fvi, string fvf, string fri, string frf, string tipoEvento,string archivoIcono, string archivoExcel,string municipios,string campos,string funciones)
        {
            string msj = "";
            string estado = "GUARDADO";
            
            //cambiar los string de fecha a DateTime
            DateTime fechaVEI = DateTime.Parse(fvi);
            DateTime fechaVEF = DateTime.Parse(fvf);
            DateTime fechaVRI = DateTime.Parse(fri);
            DateTime fechaVRF = DateTime.Parse(frf);

            //Procesar la subida de archivos
            String[] MiArrayIcon = rutaIconoE.Split(',');
            string b64Icon = MiArrayIcon[1];
            Byte[] bytesIcon = Convert.FromBase64String(b64Icon);

            String[] MiArrayExcel = rutaExcelE.Split(',');
            string b64Excel = MiArrayExcel[1];
            Byte[] bytesExcel = Convert.FromBase64String(b64Excel);

            string extensionIcon = Path.GetExtension(archivoIcono).ToLower();
            string nombreIcon = Path.GetFileName(archivoIcono).ToLower();

            string extensionExcel = Path.GetExtension(archivoExcel).ToLower();
            string nombreExcel = Path.GetFileName(archivoExcel).ToLower();

            //Se almacea en un tempPath la conversion del base64 data
            string fileNameX2Icon = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + extensionIcon.ToLower();
            System.IO.File.WriteAllBytes(fileNameX2Icon, bytesIcon);

            string fileNameX2Excel = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + extensionExcel.ToLower();
            System.IO.File.WriteAllBytes(fileNameX2Excel, bytesExcel);

            //Valida que el archivo cumpla con el tamaño máximo permitido 
            FileInfo fileInIcon = new FileInfo(fileNameX2Icon);
            var tamIcon = fileInIcon.Length;
            FileInfo fileInExcel = new FileInfo(fileNameX2Excel);
            var tamIExcel = fileInExcel.Length;
            if ((tamIcon > 100000000 || tamIcon < 0)||(tamIExcel > 100000000 || tamIExcel < 0)) //100,000,000 byte=100MB
            {
                //Mensaje error en el tamaño de los archivos
                msj = "Tamaño de la imagen mal";
            }
            else
            {


                string rutaDestinoFTPIcon = "Eventos\\IconoEvento\\" + nombreE+nombreIcon;
                string rutaDestinoFTPExcel = "Eventos\\PlantillaExcel\\" +nombreE+ nombreExcel;


                if (true == true) //con.conexionFTP("Index.png") == true
                {
                    //Si existe la conexion al servidor comenzar con la inserción de evento
                    if (eventoBL.agregarEvento(nombreE, objetivoE, rutaDestinoFTPIcon, fechaVEI, fechaVEF, fechaVRI, fechaVRF, rutaDestinoFTPExcel, tipoEvento, estado) == true)
                    {
                        con.upload(rutaDestinoFTPExcel, fileNameX2Excel);
                        con.upload(rutaDestinoFTPIcon, fileNameX2Icon);

                        //Obtener el ID del evento con el nombre del evento
                        int idEvento = eventoBL.getIdEvento(nombreE);
                        //Agregar los municipios
                        String[] MiArrayMunicipio = municipios.Split('|');
                        foreach(var muni in MiArrayMunicipio)
                        {
                            if (muni == "")
                                break;
                            int idMunicipio = municipioBL.idMunicipio(muni.Trim());
                            muniEventoBL.agregarMuniEvento(idEvento,idMunicipio); 
                        }
                        //Agregar las funciones aceptadas para el evento
                        String[] MiArrayFunciones = funciones.Split('|');
                        foreach(var func in MiArrayFunciones)
                        {
                            if (func == "")
                                break;
                            funcPartBL.agregarFuncionEvento(idEvento, func);
                        }

                        //Agregar los campos aceptados para el evento
                        List<string> listaCampos = new List<string>();
                        String[] MiArrayCampos = campos.Split('|');
                        foreach(var campo in MiArrayCampos)
                        {
                            if (campo != "")
                                listaCampos.Add(campo);
                        }
                        campoEvenBL.agregarCamposRegistro(idEvento, listaCampos.Any(x => x == "CCT"), listaCampos.Any(x => x == "Nombre del plantel"), listaCampos.Any(x => x == "Municipio"), listaCampos.Any(x => x == "Región SEG"), listaCampos.Any(x => x == "Nivel"), listaCampos.Any(x => x == "Institución/Subsistema"), listaCampos.Any(x => x == "CURP"), listaCampos.Any(x => x == "Nombre(s)"), listaCampos.Any(x => x == "Primer apellido"), listaCampos.Any(x => x == "Segundo apellido"), listaCampos.Any(x => x == "Correo electrónico"), listaCampos.Any(x => x == "Teléfono de contacto"), listaCampos.Any(x => x == "Sexo"), listaCampos.Any(x => x == "Función"), listaCampos.Any(x => x == "Mesa de trabajo"));

                        msj = "Evento Creado.";
                    }
                    else
                    {
                        msj = "Por el momento no se puede crear el evento, pruebe con otro nombre de evento o intentelo más tarde.";
                    }
                       
                  

                }
                else
                {
                    msj= "Por el momento no se puede crear el evento.";
                    ModelState.AddModelError(string.Empty, "Por el momento no se puede crear el evento.");
                }
            }

            return msj;
        }

        /// <summary>
        /// Registro Masivo al evento 
        /// Genera una dataTable y la llena con base a los datos del excel 
        /// Se valida que el excel contenga todas las columnas necesarias correspondente a los campos utilizados para registrar al evento
        /// No acepta celdas vacias 
        /// El primer registro es realizado por el método agregarTipoReg en caso de ocurrir un error en el registro masivo, se elimina el TipoReg
        /// Si el retorno del registro masivo es 'Listo' significa que el registro fue exitoso, de lo contrario retorna el error ocurrido 
        /// </summary>
        /// <param name="cantParticipantes"></param>
        /// <param name="rutaExcel"></param>
        /// <param name="archivoExcel"></param>
        /// <param name="nombreEve"></param>
        /// <returns></returns>
        [HttpPost]
        public string RegistroMasivo(string cantParticipantes,string rutaExcel, string archivoExcel,string nombreEve)
        {
            //obtener el id del evento con el nombre del Evento
            modelEvento.listaEventos = eventoBL.getEventoXnombre(nombreEve);
            int idEvento=0;
            string tipoEvento="";
            foreach (var ev in modelEvento.listaEventos)
            {
                idEvento = ev.idEvento;
                tipoEvento = ev.tipoEvento;
            }

            string data = "Registro exitoso";//"Registro exitoso"
            String[] MiArrayExcel = rutaExcel.Split(',');
            string b64Excel = MiArrayExcel[1];
            Byte[] bytesExcel = Convert.FromBase64String(b64Excel);

            string extensionExcel = Path.GetExtension(archivoExcel).ToLower();
            string nombreExcel = Path.GetFileName(archivoExcel).ToLower();

            string fileNameX2Excel = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + extensionExcel.ToLower();
            System.IO.File.WriteAllBytes(fileNameX2Excel, bytesExcel);

            //Comenzar dataTable
            DataTable dt = new DataTable();
            bool cellVacia = false;
            string filePath = string.Empty;

            //Started reading the Excel file.  
            using (XLWorkbook workbook = new XLWorkbook(fileNameX2Excel))
            {
                IXLWorksheet worksheet = workbook.Worksheet(1);
                bool FirstRow = true;
                //Range for reading the cells based on the last cell used.  
                string readRange = "1:1";

                int cantidadParticipantes = int.Parse(cantParticipantes);//obtiene de la vista la cantidad de participantes
                int cont = 0;
                for (int i = 0; i <= cantidadParticipantes; i++)
                {
                    foreach (IXLRow row in worksheet.RowsUsed(r => r.FirstCell().Address.RowNumber == 6 + cont))
                    {
                        if (FirstRow)
                        {
                            //Checking the Last cellused for column generation in datatable 
                            //var rowFromRange = ws.Range("A2:I2").FirstRow();
                            readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber - 1);//row.LastCellUsed().Address.ColumnNumber
                                                                                                                 //readRange = string.Format("{1}:{0}", "A2","C2");
                            foreach (IXLCell cell in row.Cells(readRange))
                            {
                                string nomColumna = cell.Value.ToString().Trim();
                                string[] splitCell = nomColumna.Split('-');
                                if (splitCell.Length != 0)
                                    nomColumna = splitCell[1].Trim();

                                //Dar nombre a las columnas con base a la tabla en la BD
                                switch (nomColumna)//cell.Value.ToString()
                                {
                                    case " Subsistema": //Por si pusieron espacio o no despues del ".-"
                                    case "Subsistema":
                                        dt.Columns.Add("instiSubsis");
                                        break;
                                    case "Clave del Centro de Trabajo de la escuela":
                                    case " Clave del Centro de Trabajo de la escuela":
                                        dt.Columns.Add("CCT");
                                        break;
                                    case "Nombre del plantel":
                                    case " Nombre del plantel":
                                        dt.Columns.Add("nombrePlantel");
                                        break;
                                    case "Municipio de ubicación de la escuela":
                                    case " Municipio de ubicación de la escuela":
                                        dt.Columns.Add("municipio");
                                        break;
                                    case "Delegación Regional SEG":
                                    case " Delegación Regional SEG":
                                        dt.Columns.Add("regionSEG");
                                        break;
                                    case "Nivel":
                                    case " Nivel":
                                        dt.Columns.Add("nivel");
                                        break;
                                    case "CURP":
                                    case " CURP":
                                        dt.Columns.Add("CURP");
                                        break;
                                    case "Nombre":
                                    case " Nombre":
                                        dt.Columns.Add("nombre");
                                        break;
                                    case "Primer apellido":
                                    case " Primer apellido":
                                        dt.Columns.Add("apellidoPat");
                                        break;
                                    case "Segundo apellido":
                                    case " Segundo apellido":
                                        dt.Columns.Add("apellidoMat");
                                        break;
                                    case "Correo electrónico":
                                    case " Correo electrónico":
                                        dt.Columns.Add("correoElectronico");
                                        break;
                                    case "Teléfono de contacto":
                                    case " Teléfono de contacto":
                                        dt.Columns.Add("telefono");
                                        break;
                                    case "Sexo":
                                    case " Sexo":
                                        dt.Columns.Add("sexo");
                                        break;
                                    case "Función":
                                    case " Función":
                                        dt.Columns.Add("funcion");
                                        break;
                                    default:
                                        dt.Columns.Add(cell.Value.ToString().Trim());
                                        break;
                                }

                                //dt.Columns.Add(cell.Value.ToString());


                            }

                            FirstRow = false;
                        }
                        else
                        {
                            //Adding a Row in datatable  
                            dt.Rows.Add();
                            int cellIndex = 0;
                            //Updating the values of datatable  
                            foreach (IXLCell cell in row.Cells(readRange))
                            {

                                try
                                {
                                    if (cell.Value.ToString() == "" || cell.Value.ToString() == null)
                                    {
                                        cellVacia = true;
                                    }
                                    dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                                }
                                catch
                                {
                                    if (Convert.ToString(cell.CachedValue) == "True")
                                    {
                                        dt.Rows[dt.Rows.Count - 1][cellIndex] = "VERDADERO";
                                    }
                                    else
                                    {
                                        dt.Rows[dt.Rows.Count - 1][cellIndex] = "FALSO";
                                    }
                                }

                                cellIndex++;
                                if (cellVacia == true)
                                    break;
                            }
                        }
                        if (cellVacia == true)
                            break;
                    }
                    cont++;
                    if (cellVacia == true)
                    {
                        dt = new DataTable();
                        data = "Campos obligatorios vacíos.";
                        return data;
                    }

                }



                if (FirstRow)
                {
                    data = "Empty Excel File!";
                }
            }

            //Comparar las columnas del dataTable, deben ser los mismos que los campos establecidos para el evento
            bool camposCorrectos = true;
           
            foreach (var campo in campoEvenBL.camposEvento(idEvento))
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName == campo)
                    {
                        camposCorrectos = true;
                        break;
                    }
                    else
                    {
                        camposCorrectos = false;
                    }
                }
                if (camposCorrectos == false)
                    break;
            }

            //Insertar registro en la BD
            if (camposCorrectos == true)
            {
                try
                {
                    string idTipoReg = blTipoReg.agregarTipoReg(tipoEvento);
                    if (idTipoReg != "NO")
                        data = blRegistro.registroMasivo(idEvento, idTipoReg, dt);

                    if (data != "Listo")//Eliminar el idTipoRegistro si no se cumplio con el registro de manera correcta
                        blTipoReg.eliminarTipoRegistro(idTipoReg);
                    else
                    {
                        data = "Registro exitoso";
                        //Agregar registro a tblUserEnlaceEvento
                        bool agrego = blUserEnlaceEvento.agregarUserEnalceEve(idTipoReg, idEvento, "CeciMP18");
                        while (agrego == false)
                        {
                            agrego = blUserEnlaceEvento.agregarUserEnalceEve(idTipoReg, idEvento, "CeciMP18");
                            //Mientras no se haya agregado el registro se seguira intentando
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            else
            {
                data = "El excel no tiene los campos necesarios para el registro";
            }

            //Para imprimir lo de data table, pruebas
            //data = "";
            //for(int i = 0; i < dt.Rows.Count; i++)
            //{
            //    data = data + dt.Rows[i]["CCT"].ToString()+" ";
            //}

            return data;
        }

        /// <summary>
        /// Sección de descarga de plantilla de Excel para el registro masivo a determinado evento 
        /// Genera el MIME de descarca .vlsx
        /// </summary>
        /// <param name="nombreEve"></param>
        /// <returns></returns>
        public ActionResult DescargaPlantillaExcel(string nombreEve)
        {
            string ruta = "";
            modelEvento.listaEventos = eventoBL.getEventoXnombre(nombreEve);
            foreach (var item in modelEvento.listaEventos)
            {
                ruta = item.rutaPlantillaExcel;
            }
            string nombre = "Plantilla_" + nombreEve;
            byte[] filedata = System.IO.File.ReadAllBytes(con.downloadVistaPrevia(ruta));
            return File(filedata, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombre + ".xlsx");
        }


        /// <summary>
        /// Prepara la vista _PopUpMesaT, obtiene los datos de la Mesa de trabajo utilizando los parámetros idMesaT, idEvento
        /// </summary>
        /// <param name="idMesaT"></param>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public PartialViewResult _PopUpModifEvento(int idMesaT,int idEvento)
        {
            modelMesaEvento.listaMesasEvento = mesaEventoBL.getMesaTevento(idMesaT);
            foreach(var item in modelMesaEvento.listaMesasEvento)
            {
                modelMesaEvento.idMesaTrab = item.idMesaTrab;
                modelMesaEvento.nombreMT = item.nombreMT;
                modelMesaEvento.cantidad = item.cantidad;
            }
            return PartialView("_PopUpsEvento/_PopUpMesaT",modelMesaEvento);
        }

        /// <summary>
        /// Descarga de todos los participantes en el registro masivo por excel 
        /// El excel se conforma solamente por las columnas que representan los campos necesarios para el registro 
        /// Genera un Datatable que se va llenando con base a la consulta realizada de los participantes en determinado registro y evento
        /// Utiliza el MIME para generar la descarga 
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public ActionResult DescargarExcelParticipantes(int idEvento)
        {
            //idEvento = 3;
            DataTable dt = new DataTable("Participantes del evento");
            //Lenar las columnas solo con los campos aceptados
            CampoEventoBL blCampoEvento = new CampoEventoBL();
            dt.Columns.Add("folio");
            foreach (var campo in blCampoEvento.camposEvento(idEvento))
            {
                dt.Columns.Add(campo);
            }
            foreach (var mt in mesaEventoBL.getMesaTeventoXevento(idEvento))
            {
                dt.Columns.Add("Mesa de Trabajo.- " + mt.nombreMT);
            }

            modelRegEvento.listaRegistroEvento = blRegistro.getRegistroEXidEv(idEvento);

            //LLenar los renglones del dataTable
            foreach (var item in modelRegEvento.listaRegistroEvento)
            {
                DataRow dr = dt.NewRow();

                dr["folio"] = item.folio;
                if (item.CCT != null)
                    dr["CCT"] = item.CCT;
                if (item.nombrePlantel != null)
                    dr["nombrePlantel"] = item.nombrePlantel;
                if (item.municipio != null)
                    dr["municipio"] = item.municipio;
                if (item.regionSEG != null)
                    dr["regionSEG"] = item.regionSEG;
                if (item.nivel != null)
                    dr["nivel"] = item.nivel;
                if (item.instiSubsis != null)
                    dr["instiSubsis"] = item.instiSubsis;
                if (item.CURP != null)
                    dr["CURP"] = item.CURP;
                if (item.nombre != null)
                    dr["nombre"] = item.nombre;
                if (item.apellidoPat != null)
                    dr["apellidoPat"] = item.apellidoPat;
                if (item.apellidoMat != null)
                    dr["apellidoMat"] = item.apellidoMat;
                if (item.correoElectronico != null)
                    dr["correoElectronico"] = item.correoElectronico;
                if (item.telefono != null)
                    dr["telefono"] = item.telefono;
                if (item.sexo != null)
                    dr["sexo"] = item.sexo;
                if (item.funcion != null)
                    dr["funcion"] = item.funcion;
                foreach (var mtp in mesaEventoBL.getMesaTeventoXevento(idEvento))
                {
                    if (mesaParticipanteBL.existeRegistroMT(item.folio, mtp.nombreMT) == true)
                    {
                        dr["Mesa de Trabajo.- " + mtp.nombreMT] = "SI";
                    }
                    else
                    {
                        dr["Mesa de Trabajo.- " + mtp.nombreMT] = "NO";
                    }
                }


                dt.Rows.Add(dr);

            }
            //Cambiar el nombre de las columnas para generar el excel
            if (dt.Columns.Contains("folio"))
                dt.Columns["folio"].ColumnName = "Folio";
            if (dt.Columns.Contains("CCT"))
                dt.Columns["CCT"].ColumnName = "Clave de Centro de Trabajo";
            if (dt.Columns.Contains("nombrePlantel"))
                dt.Columns["nombrePlantel"].ColumnName = "Plantel";
            if (dt.Columns.Contains("municipio"))
                dt.Columns["municipio"].ColumnName = "Municipio";
            if (dt.Columns.Contains("regionSEG"))
                dt.Columns["regionSEG"].ColumnName = "Región SEG";
            if (dt.Columns.Contains("nivel"))
                dt.Columns["nivel"].ColumnName = "Nivel";
            if (dt.Columns.Contains("instiSubsis"))
                dt.Columns["instiSubsis"].ColumnName = "Institución/Subsistema";
            if (dt.Columns.Contains("CURP"))
                dt.Columns["CURP"].ColumnName = "CURP";
            if (dt.Columns.Contains("nombre"))
                dt.Columns["nombre"].ColumnName = "Nombre";
            if (dt.Columns.Contains("apellidoPat"))
                dt.Columns["apellidoPat"].ColumnName = "Apellido Paterno";
            if (dt.Columns.Contains("apellidoMat"))
                dt.Columns["apellidoMat"].ColumnName = "Apellido Materno";
            if (dt.Columns.Contains("correoElectronico"))
                dt.Columns["correoElectronico"].ColumnName = "Correo eléctronico";
            if (dt.Columns.Contains("telefono"))
                dt.Columns["telefono"].ColumnName = "Teléfono";
            if (dt.Columns.Contains("sexo"))
                dt.Columns["sexo"].ColumnName = "Sexo";
            if (dt.Columns.Contains("funcion"))
                dt.Columns["funcion"].ColumnName = "Función";

            //Generar el excel y descarga
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ParticipantesEvento.xlsx");
                }
            }
        }

        /// <summary>
        /// Genera la vista previa de la imagen de icono
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        public ActionResult VistaPreviaIcono(string evento)
        {
            string ruta = "";
            modelEvento.listaEventos = eventoBL.getEventoXnombre(evento);
            foreach(var item in modelEvento.listaEventos)
            {
                ruta = item.rutaIcono;
            }
            //ruta = "Eventos\\IconoEvento\\Segundo evento con plantillacapturapantallaexcel.png";
            return RedirectToAction("Index", "VisualizacionPrevia", new { archivo = ruta});
        }

        /// <summary>
        /// Subir los archivos de icono o la plantilla de excel para el evento
        /// Los archivos son subidos al servidor FTP, si ya existe un archivo se elimina y se vuelve a subir al servidor y se realiza 
        /// una actualizacion en la BD, si no existe se sube y se registra en la BD
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="ruta"></param>
        /// <param name="archivo"></param>
        /// <param name="idE"></param>
        /// <returns></returns>
        [HttpPost]
        public string SubirIE(string seccion, string ruta, string archivo, int idE)
        {
           
            string subido = "";
            try
            {
                modelEvento.listaEventos = eventoBL.getEvento(idE);
                foreach (var item in modelEvento.listaEventos)
                {
                    modelEvento.nombre = item.nombre;
                    modelEvento.rutaIcono = item.rutaIcono;
                    modelEvento.rutaPlantillaExcel = item.rutaPlantillaExcel;
                }

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
                    subido = "Tamaño de la imagen mayor al permitido";
                }
                else
                {
                    string rutaDestinoFTP = "";
                    if (seccion == "Icono")
                        rutaDestinoFTP = "Eventos\\IconoEvento\\Icono" + idE.ToString() + modelEvento.nombre + extension;
                    else
                        rutaDestinoFTP = "Eventos\\PlantillaExcel\\Excel" + idE.ToString() + modelEvento.nombre + extension;

                    if (con.conexionFTP("Index.png") == true)
                    {
                        if (seccion == "Icono")
                        {

                            
                            if (eventoBL.editarRutaIcono(idE, rutaDestinoFTP) == false)
                            {
                                
                                ModelState.AddModelError(string.Empty, "No se pudo subir el archivo");
                                subido = "No se pudo subir el Icono";
                            }
                            else
                            {
                                con.delete(modelEvento.rutaIcono);
                                con.upload(rutaDestinoFTP, fileNameX2);
                                subido = "Subida de icono exitosa";
                            }
                        }
                        else
                        {
                           
                            if (eventoBL.editarRutaPExcel(idE, rutaDestinoFTP) == false)
                            {
                                ModelState.AddModelError(string.Empty, "No se pudo subir el archivo");
                                subido = "No se pudo subir el archivo";
                            }
                            else
                            {
                                con.delete(modelEvento.rutaPlantillaExcel);
                                con.upload(rutaDestinoFTP, fileNameX2);
                                subido = "Subida de Excel exitoso";
                            }
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Por el momento no se pueden subir archivos.");
                        subido = "Por el momento no se pueden subir archivos.";
                    }
                }
            }catch(Exception ex)
            {
                subido = ex.Message;
            }
            return subido;
        }

        /// <summary>
        /// Actualiza los datos del evento 
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="estado"></param>
        /// <param name="nombreE"></param>
        /// <param name="objetivoE"></param>
        /// <param name="fvi"></param>
        /// <param name="fvf"></param>
        /// <param name="fri"></param>
        /// <param name="frf"></param>
        /// <param name="tipoEvento"></param>
        /// <param name="municipios"></param>
        /// <param name="campos"></param>
        /// <param name="funciones"></param>
        /// <returns></returns>
        [HttpPost]
        public string ActualizarEvento(int idEvento,string estado,string nombreE, string objetivoE,  string fvi, string fvf, string fri, string frf, string tipoEvento, string municipios, string campos, string funciones)
        {
            string msj = "";
            

            //cambiar los string de fecha a DateTime
            DateTime fechaVEI = DateTime.Parse(fvi);
            DateTime fechaVEF = DateTime.Parse(fvf);
            DateTime fechaVRI = DateTime.Parse(fri);
            DateTime fechaVRF = DateTime.Parse(frf);


            if (eventoBL.modificarEvento(idEvento, nombreE, objetivoE, fechaVEI, fechaVEF, fechaVRI, fechaVRF, tipoEvento, estado) == true)
            {


                muniEventoBL.eliminarTodosMunicipioEven(idEvento);
                funcPartBL.eliminarTodasFuncionEvento(idEvento);
                campoEvenBL.eliminarTodosCamposEvento(idEvento);


                //Agregar los municipios
                String[] MiArrayMunicipio = municipios.Split('|');
                foreach (var muni in MiArrayMunicipio)
                {
                    if (muni == "")
                        break;
                    int idMunicipio = municipioBL.idMunicipio(muni.Trim());
                    muniEventoBL.agregarMuniEvento(idEvento, int.Parse(muni.Trim()));
                }

                //Agregar las funciones aceptadas para el evento
                String[] MiArrayFunciones = funciones.Split('|');
                foreach (var func in MiArrayFunciones)
                {
                    if (func == "")
                        break;
                    funcPartBL.agregarFuncionEvento(idEvento, func);
                }

                //Agregar los campos aceptados para el evento
                List<string> listaCampos = new List<string>();
                String[] MiArrayCampos = campos.Split('|');
                foreach (var campo in MiArrayCampos)
                {
                    if (campo != "")
                        listaCampos.Add(campo);
                }
                campoEvenBL.agregarCamposRegistro(idEvento, listaCampos.Any(x => x == "CCT"), listaCampos.Any(x => x == "Nombre del plantel"), listaCampos.Any(x => x == "Municipio"), listaCampos.Any(x => x == "Región SEG"), listaCampos.Any(x => x == "Nivel"), listaCampos.Any(x => x == "Institución/Subsistema"), listaCampos.Any(x => x == "CURP"), listaCampos.Any(x => x == "Nombre(s)"), listaCampos.Any(x => x == "Primer apellido"), listaCampos.Any(x => x == "Segundo apellido"), listaCampos.Any(x => x == "Correo electrónico"), listaCampos.Any(x => x == "Teléfono de contacto"), listaCampos.Any(x => x == "Sexo"), listaCampos.Any(x => x == "Función"), listaCampos.Any(x => x == "Mesa de trabajo"));

                msj = "Evento " + estado;
                    }
                    else
                    {
                        msj = "Por el momento no se puede modificar los datos del evento, intentelo más tarde";
                    }



                
            

            return msj;
        }

        /// <summary>
        /// Sección que prepara la vista parcial _TablaCantMT con las mesas de trabajo del evento
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public PartialViewResult CargarTablaCantMT(string idEvento)
        {
            int idE = int.Parse(idEvento);
            modelMesaEvento.listaMesasEvento = mesaEventoBL.getMesaTeventoXevento(idE);
            string vistaParcial = "_VPSeccionEvento/_TablaCantMT";
            return PartialView(vistaParcial,modelMesaEvento);
        }

        /// <summary>
        /// Sección para agregar cantidad a un subsistema en especifico en un determinado evento
        /// Actualiza la vista parcial _TablaCantSubSis
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="subsistema"></param>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public PartialViewResult AgregarSubSisE(int idEvento, string subsistema,int cantidad)
        {
           
                subSisEvenBL.agregarSubSisEvento(idEvento, subsistema, cantidad);

                modelSubsisEvento.listaSubsisEvento = subSisEvenBL.getSubsisEventXidEven(idEvento);
                string vistaParcial = "_VPSeccionEvento/_TablaCantSubSis";
                return PartialView(vistaParcial, modelSubsisEvento);
           
            
        }

        /// <summary>
        /// Elimina un subsistema en un determinado evento 
        /// </summary>
        /// <param name="idSubsistemaEv"></param>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public PartialViewResult EliminarSubsisCant(int idSubsistemaEv,int idEvento)
        {
            subSisEvenBL.eliminarSubsisEvent(idSubsistemaEv);
            modelSubsisEvento.listaSubsisEvento = subSisEvenBL.getSubsisEventXidEven(idEvento);
            string vistaParcial = "_VPSeccionEvento/_TablaCantSubSis";
            return PartialView(vistaParcial, modelSubsisEvento);
        }

        /// <summary>
        /// Sección para agregar cantidad a una escuela en especifico en un determinado evento
        /// Actualiza la vista parcial _TablaCantEscuela
        /// Utiliza la CCT para esta sección de escuela
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="CCT"></param>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public PartialViewResult AgregarEscuelaE(int idEvento, string CCT, int cantidad)
        {
            CEOBL ceobl = new CEOBL();
            int idCCT = ceobl.getIdCEO(CCT);
            escualaEBL.agregarCantidadEscuela(idCCT, idEvento, cantidad);

            modelEscuelaEve.tablaEscuelaCantida = escualaEBL.getEventoTabla(idEvento);
            string vistaParcial = "_VPSeccionEvento/_TablaCantEscuela";
            return PartialView(vistaParcial, modelEscuelaEve);
        }

        /// <summary>
        /// Sección para validar una CCT e identificar si existe o no en la BD
        /// </summary>
        /// <param name="CCT"></param>
        /// <returns></returns>
        public bool ValidarCCT(string CCT)
        {
            CEOBL ceoBL = new CEOBL();
            CEOViewModel modelCEO = new CEOViewModel();

            bool existe = false;
            modelCEO.listCEO = ceoBL.getCEO(CCT);

            if (modelCEO.listCEO.Count > 0)
            {
                existe = true;
            }

            return existe;
        }

        /// <summary>
        /// Sección para eliminar una escuela de un evento 
        /// Retorna vistaParcial _TablaCantEscuela
        /// </summary>
        /// <param name="idCCT"></param>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public PartialViewResult EliminarCantEscuela(int idCCT, int idEvento)
        {
            escualaEBL.eliminarCantEscuela(idCCT,idEvento);

            modelEscuelaEve.tablaEscuelaCantida = escualaEBL.getEventoTabla(idEvento);
            string vistaParcial = "_VPSeccionEvento/_TablaCantEscuela";
            return PartialView(vistaParcial, modelEscuelaEve);
        }

        /// <summary>
        /// Sección para agregar cantidad a una mesa de trabajo en especifico en un determinado evento
        /// Actualiza la vista parcial _TablaCantMT
        /// </summary>
        /// <param name="idEvento"></param>
        /// <param name="mesaTrabajo"></param>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public PartialViewResult AgregarMT(int idEvento,string mesaTrabajo,int cantidad)
        {
            mesaEventoBL.agregarMesaEvento(idEvento, mesaTrabajo, cantidad, true);

            modelMesaEvento.listaMesasEvento = mesaEventoBL.getMesaTeventoXevento(idEvento);
            string vistaParcial = "_VPSeccionEvento/_TablaCantMT";
            return PartialView(vistaParcial, modelMesaEvento);
        }

        /// <summary>
        /// Sección para actualizar el estado de la mesad de trabajo
        /// </summary>
        /// <param name="idMesaT"></param>
        /// <param name="estado"></param>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public PartialViewResult EstadoMT(int idMesaT, string estado, int idEvento)
        {
            if (estado == "true")
                mesaEventoBL.editarMesaEstado(idMesaT, true);
            else
                mesaEventoBL.editarMesaEstado(idMesaT, false);

            modelMesaEvento.listaMesasEvento = mesaEventoBL.getMesaTeventoXevento(idEvento);
            string vistaParcial = "_VPSeccionEvento/_TablaCantMT";
            return PartialView(vistaParcial, modelMesaEvento);
        }

        /// <summary>
        /// Sección para actualizar los datos de la mesa de trabajo
        /// </summary>
        /// <param name="idMesaT"></param>
        /// <param name="nombreMT"></param>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public string ModificarDatosMT(int idMesaT,string nombreMT,int cantidad)
        {
            string mensaje = "";
            if (mesaEventoBL.editarMesaEvento(idMesaT, nombreMT, cantidad) == true)
            {
                mensaje = "Listo Actualización";
            }
            else
            {
                mensaje = "Ocurrio un error, pruebe con otro nombre o intentelo más tarde";
            }
            return mensaje;
        }


        //GENERAR PDF Y DESCARGAR .ZIP
        //Generar el .zip con los PDF
        /// <summary>
        /// Este es el método que es llamado desde la vista recibe como parametro el tipo de registro y el idEvento
        /// Utiliza el metodo HTMLtoPDF(evento,idTipoRegistro) para generar la descarga de los documentos en .pdf en un archivo comprimido
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="evento"></param>
        /// <returns></returns>
        public ActionResult PDFtoZIP(string tr,int evento)
        {
            //int ide = int.Parse(evento);

            HTMLToPdf(evento, tr);
            return View();
        }

        /// <summary>
        /// Sección donde se generan los multiples PDF y se guardar en un archivo .zip
        /// Se debe de agregar el ensamblador necesario
        /// Obtiene los datos de cada participante correspondiente al registro para generar el PDF 
        /// Se envia al método que contiene la estructura del PDF 
        /// Se usa el método ShowPDF para lo siguiente:
        /// Los PDF son almacenados en una carpeta con nombre aleatorio que será eliminada al finalizar el proceso
        /// Una vez llena la carpeta con los PDF se comprime y se utiliza el método para generar el MIME y la descarga del .zip
        /// </summary>
        /// <param name="evento"></param>
        /// <param name="tipoR"></param>
        public void HTMLToPdf(int evento, string tipoR)//FilePath
        {
            //Document document = new Document();
            string filePath = string.Empty;
            //PdfWriter.GetInstance(document, new FileStream(Request.PhysicalApplicationPath + "\\Chap0101.pdf", FileMode.Create));
            //PdfWriter.GetInstance(document, new FileStream(FilePath, FileMode.Create));
            //string path = Server.MapPath("~/Uploadss/");
            Random ran = new Random();
            string tempP = "/Uploads" + ran.Next(9999).ToString();
            string path = Server.MapPath(tempP);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            modelRegEvento.listaRegistroEvento = blRegistro.getRegistroEXidEvidTR(evento, tipoR);
            //for (int i = 1; i < 3; i++)//Cambiar por foreach con el list de la consulta de registros
            foreach(var item in modelRegEvento.listaRegistroEvento)
            {
                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                
                filePath = path + "\\"+item.folio.ToString("0000000.##")+ ".pdf";
                //PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                XMLWorkerHelper xml = XMLWorkerHelper.GetInstance();
                document.Open();
                Image pdfImage = Image.GetInstance(Server.MapPath("constanciaFondo.jpg"));

                pdfImage.ScaleToFit(550, 600);

                pdfImage.Alignment = iTextSharp.text.Image.UNDERLYING;
                pdfImage.SetAbsolutePosition(25, 385);

                document.Add(pdfImage);

                //iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
                //HTMLWorker hw = new HTMLWorker(document);
                //hw.Parse(new StringReader(ContenidoHTML()));//Pasar a ContenidoHTML los datos del registro
                //iTextSharp.text.pdf.PdfWriter writer=new PdfWriter;
                string nomCompleto = item.nombre + " " + item.apellidoPat + " " + item.apellidoMat;
                string nomE = "";
                int folio = item.folio;
                int tipoFecha = 0;
                bool existeMT = false;
                string d1 = "", d2 = "";
                string m1 = "", m2 = "";
                string year = "";
                modelEvento.listaEventos = eventoBL.getEvento(evento);
                foreach(var itemE in modelEvento.listaEventos)
                {
                    nomE = itemE.nombre;
                    d1 = itemE.fVigenciaInicio.ToString("dd");
                    m1 = itemE.fVigenciaInicio.ToString("MMM");
                    d2 = itemE.fVigenciaFin.ToString("dd");
                    m2 = itemE.fVigenciaFin.ToString("MMM");
                    year = itemE.fVigenciaFin.ToString("yyyy");

                }
                if (d1 != d2 && m1 == m2)
                    tipoFecha = 1;
                else if ((d1 == d2 || d1 != d2) && m1 != m2)
                    tipoFecha = 2;
                else if (d1 == d2 && m1 == m2)
                    tipoFecha = 3;
                //Existe MT
                modelMesaParticipante.listaMesaParticipante = mesaParticipanteBL.getMesaPartiXfolio(folio);
                //foreach(var itemMTP in modelMesaParticipante.listaMesaParticipante)
                //{
                //    existeMT = true;
                //    itemMTP.
                //}
                if (modelMesaParticipante.listaMesaParticipante.Count != 0)
                    existeMT = true;

                string html = ContenidoHTML(nomCompleto,nomE,folio.ToString("0000000.##"),tipoFecha,d1,m1,d2,m2,year, existeMT);
                xml.ParseXHtml( writer, document, stringToStream(html), System.Text.Encoding.UTF8);

                document.Close();

            }


            //.zip
            //string startPath = @"C:\\Users\\mc_mendoza\\Documents\\CarpetaServFTP\\Banner\\02000000";

            //string zipPath = @"C: \Users\mc_mendoza\Desktop\result.zip";
            //Temporal zipPath
            Random ranZ = new Random();
            string tempPZip = "/ZIP" + ranZ.Next(9999).ToString();
            string pathZIP = Server.MapPath(tempPZip);
            if (!Directory.Exists(pathZIP))
            {
                Directory.CreateDirectory(pathZIP);
            }
            else
            {
                Directory.Delete(pathZIP, true);
                Directory.CreateDirectory(pathZIP);
            }
            string zipPath = pathZIP + "\\result.zip";
            string startPath = path;

            ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);
            //

            Directory.Delete(path, true);
            //Directory.Delete(pathZIP, true);
            ShowPdf(zipPath, pathZIP); //Metodo que inserta el MIME a la página 
        }

        /// <summary>
        /// Se genera el MIME para la descarga de los PDF 
        /// Se elimina la carpeta con todo su contenido en la ruta del parámetro deleteFile
        /// </summary>
        /// <param name="s"></param>
        /// <param name="deleteFile"></param>
        private void ShowPdf(string s,string deleteFile)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=" + "Participantes.zip");
            Response.ContentType = "application/zip";
            Response.WriteFile(s);
            Response.Flush();
            Response.Clear();
            Directory.Delete(deleteFile, true);
        }

        /// <summary>
        /// Código HTML para generar las constancias de participantes 
        /// En el código HTML se agrega la información de los participantes como el 
        /// folio, nombre completo, fecha del evento, nombre del evento y en caso de estar inscrito a mesas de trabajo se agrega una sesión para 
        /// mostrar el nombre de las mesas de trabajo
        /// </summary>
        /// <param name="nomCompleto"></param>
        /// <param name="nombreEve"></param>
        /// <param name="folio"></param>
        /// <param name="tipoFecha"></param>
        /// <param name="d1"></param>
        /// <param name="m1"></param>
        /// <param name="d2"></param>
        /// <param name="m2"></param>
        /// <param name="year"></param>
        /// <param name="existeMT"></param>
        /// <returns></returns>
        public string ContenidoHTML(string nomCompleto,string nombreEve,string folio,int tipoFecha,string d1,string m1,string d2,string m2,string year,bool existeMT)
        {
             String HTML = "<head><meta charset='utf-8'/>" +
                "<style>" +
        ".fondo{" +
            "width: 792px;" +
            "height: 612px;" +
        //"background - image: url('constancia-02.jpg');" +//Ver lo de agregar la imagen
        "}" +
        ".participante{" +
            "font-size: 25pt;" +
            "font-weight: 700;" +
            "color: #264863;" +
            "font-family: 'Carrois Gothic', sans-serif;" +
            "padding-top: 260px;" +
            "align-content: center;" +
            "text-align: center;" +
        "}" +
        ".evento {" +
            "font-size: 18pt;" +
            "font-weight: 50;" +
            "font-family: 'Carrois Gothic', sans-serif;" +
            "color: #264863;" +
            "align-content: center;" +
            "text-align: center;" +
        "}" +
        ".nombreEvento{" +
            "font-size: 18pt;" +
            "font-family: 'Carrois Gothic', sans-serif;" +
            "text-transform: uppercase;" +
            "color: #264863;" +
            "align-content: center;" +
            "text-align: center;" +
        "}" +
        ".datos{" +
            "font-size: 15pt;" +
            "font-weight: 50;" +
            "font-family: 'Carrois Gothic', sans-serif;" +
            "color: #264863;" +
            "align-content: center;" +
            "text-align: center;" +
        "}" +
        ".aligmesa{" +
            "font-size: 13pt;" +
            "align-content: center;" +
            "text-align: center;" +
            "font-family: 'Carrois Gothic', sans-serif;" +
            "color: #264863;" +
            "height: 120px;" +
        "}" +
        ".mesa{" +
            "margin: 20px auto 5px;" +
            "font-size: 15pt;" +
            "font-weight: 700;" +
            "font-family: 'Carrois Gothic', sans-serif;" +
            "color: #264863;" +
            "align-content: center;" +
            "text-align: center;" +
            "border-top: dashed thin;" +
            "border-bottom: dashed thin;" +
            "width: 350px;" +
        "}" +
        ".folio{" +
            "text-align: right;" +
            "margin-right: 120px;" +
            "font-size: 16pt;" +
            "font-weight: 700;" +
            "color: #264863;" +
            "font-family: 'Carrois Gothic', sans-serif;" +
            "z-index: 10;" +
        "}" +
"</style>" +

"</head>" +
                "<body>" +

                    "<div class='participante'>" + nomCompleto + "</div>" +
                    "<div class='evento'> Por su participación activa al evento<br/>" +
                    "<span class='nombreEvento'>" + nombreEve + "</span> </div>";

            if (tipoFecha == 1)
            {
                HTML = HTML + "<div class='datos'> del <label>"+d1+"</label> al <label>"+d2+"</label> de <label>"+m1+"</label> de <label>"+year+"</label>,<br/>";
            }
            else if (tipoFecha == 2)
            {
                HTML = HTML + "<div class='datos'> del <label>"+d1+"</label> de <label>"+m1+ "</label> al <label>" + d2 + "</label> de <label>" + m2 + "</label> de <label>"+year+"</label>,<br/>";
            }
            else
            {
                HTML = HTML + "<div class='datos'> del <label>"+d1+"</label> de <label>"+m1+"</label> de <label>"+year+"</label>,<br/>";
            }


            HTML = HTML + "en Guanajuato.</div>";

            if (existeMT == true) {
                HTML = HTML + "<div class='aligmesa'>" +
                "<div style='font-size: 15pt;font-weight: 700;'>Mesas de Trabajo</div>";
                
                modelMesaParticipante.listaMesaParticipante = mesaParticipanteBL.getMesaPartiXfolio(int.Parse(folio));
                foreach (var item in modelMesaParticipante.listaMesaParticipante)
                {
                    modelMesaEvento.listaMesasEvento = mesaEventoBL.getMesaTevento(item.idMesaTrab);
                    foreach (var itemMT in modelMesaEvento.listaMesasEvento)
                    {
                        string nombreMT = itemMT.nombreMT;
                        HTML = HTML + "<div>"+nombreMT+"</div>";
                    }
                }
                HTML =HTML+"</div>";
                
            }

            HTML =HTML+"<div class='folio' style='width: 620px;'>FOLIO "+folio+"</div>" +
             

            "</body> ";
           
            return HTML;
        }

        /// <summary>
        ///  Utilizado para agregar el código HTML al método xml.ParseXHtml de la libreria utilizada
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public Stream stringToStream(string txt)
        {
            var stream = new MemoryStream();
            var w = new StreamWriter(stream);
            w.Write(txt);
            w.Flush();
            stream.Position = 0;
            return stream;
        }

    }
}