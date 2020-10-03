using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BusinessModel.Business.ModuloEvento;
using BusinessModel.Business.Global;
using BusinessModel.Business.Usuario;
using BusinessModel.Persistence.BD;
using BusinessModel.Models.ModuloEvento;

using DesarrolloProyDSP.Models.ModuloEvento;
using DesarrolloProyDSP.Models.Usuario;
using DesarrolloProyDSP.Models.Global;
using DesarrolloProyDSP.Helpers;
using System.IO; 


using System.Data;
using ClosedXML.Excel;
using iTextSharp.tool.xml;
using System.IO.Compression;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PagedList;

namespace DesarrolloProyDSP.Controllers.ModuloEvento
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class EventoController : Controller
    {
        Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.66", "SEG\\mc_mendoza", "M2pm2019");
        //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");

        EventoViewModel modelEvento = new EventoViewModel();
        ImagenModuloBannerViewModel modelBanner = new ImagenModuloBannerViewModel();
        CampoEventoViewModel modelCampoEvento = new CampoEventoViewModel();
        TipoRegistroViewModel modelTipoReg = new TipoRegistroViewModel();
        RegistroEventoViewModel modelRegistroEv = new RegistroEventoViewModel();
        UserEnlaceEventoViewModel modelUserEnlaceEvento = new UserEnlaceEventoViewModel();
        MesaParticipanteViewModel modelMesaParticipante = new MesaParticipanteViewModel();
        MesaEventoViewModel modelMesaEvento = new MesaEventoViewModel();
        FuncionParticipanteViewModel modelFuncionPart = new FuncionParticipanteViewModel();
        CEOViewModel modelCEO = new CEOViewModel();
        EscuelaEventoViewModel modelEscuelaEvento = new EscuelaEventoViewModel();

        EventoBL eventoBL = new EventoBL();
        ImagenModuloBannerBL bannerBL = new ImagenModuloBannerBL();
        CampoEventoBL campoEvenBL = new CampoEventoBL();
        TipoRegistroBL tipoRegBL = new TipoRegistroBL();
        RegistroEventoBL registroEvBL = new RegistroEventoBL();
        UserEnlaceEventoBL userEnlaceEventoBL = new UserEnlaceEventoBL();
        MesaParticipanteBL mesaParticipanteBL = new MesaParticipanteBL();
        MesaEventoBL mesaEventoBL = new MesaEventoBL();
        FuncionParticipanteBL funcionParticipanteBL = new FuncionParticipanteBL();
        CEOBL ceoBL = new CEOBL();
        EscuelaEventoBL escuelaEventoBL = new EscuelaEventoBL();

        Seguridad seguridad = new Seguridad();

        /// <summary>
        /// Obtiene la imagenes del módulo de Eventos
        /// Obtiene la lista de los eventos disponibles a para registro utilizando el método 
        /// getHomeEvento y como parámetro pasa la fecha actual.
        /// </summary>
        /// <returns></returns>
        public ActionResult Eventos()
        {
            ViewBag.menu = "Evento";
            modelEvento.listaImagenes = bannerBL.getModuloImagenes("02000000");
            //return View();
            modelEvento.listaEventosHome = eventoBL.getHomeEvento(DateTime.Now);
            var tuple = new Tuple<EventoViewModel>(modelEvento);
            return View(tuple);
        }

        //[HttpPost]
        /// <summary>
        /// Prepara la vista para los Datos del evento 
        /// Sección en donde se obtiene el objetivo del evento, icono, tipo de evento, fecha de vigencia del evento
        /// Se realizan validaciones para saber si el usuario puede estar en esta vista, de no ser valido se redirecciona a la vista de Eventos
        /// 
        /// </summary>
        /// <param name="nomE"></param>
        /// <returns></returns>
        public ActionResult DatosEvento(string nomE)
        {
            ViewBag.menu = "Evento";
            if (nomE == "" || nomE == null)
            {
                return RedirectToAction("Eventos");
            }
            modelEvento.estado = eventoBL.estadoEvento(nomE);
            if (modelEvento.estado == "" || modelEvento.estado == "FINALIZADO")
            {
                return RedirectToAction("Eventos");
            }
            //string nomE = "HOME EVENTO 3 PRUEBA DE FECHAS";
            int idEvento = eventoBL.getIdEvento(nomE);
            modelEvento.listaEventos = eventoBL.getEvento(idEvento);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.idEvento = item.idEvento;
                modelEvento.nombre = item.nombre;
                modelEvento.objetivo = item.objetivo;
                modelEvento.tipoEvento = item.tipoEvento;
                modelEvento.fVigenciaInicio = item.fVigenciaInicio;
                modelEvento.fVigenciaFin = item.fVigenciaFin;
                modelEvento.rutaIcono = item.rutaIcono;
            }
            if (modelEvento.listaEventos.Count == 0)
            {
                return RedirectToAction("Eventos");
            }

            return View(modelEvento);
        }

        /// <summary>
        /// Retorna true o false con base a la sesion de usuario enlace activa 
        /// Usada en la sección de registro a evento restringuido que es solo para usuarios enlace 
        /// Si la validación falla se le pedira al usuario que inicie sesión
        /// </summary>
        /// <returns></returns>
        public string SesionEnlaceActiva()
        {
            string sesionActiva = "false";

            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
            if (c1 != null && perf != null)
            {
                if (seguridad.DesEncriptar(perf.Value) == "Enlace")
                    sesionActiva = "true";
            }

            return sesionActiva;
        }

        /// <summary>
        /// Prepara la vista, muestra titulo, objetivo, fechas de vigencia e icono del evento 
        /// Vista para elegir el tipo de registro 'Individual' o 'Grupal'
        /// Se realizan las validaciones de cookies necesarias y de nombre de evento para identificar 
        /// si el usuario tiene acceso a esta vista, de lo contrario se redirecciona a la vista Eventos
        /// </summary>
        /// <param name="nomE"></param>
        /// <returns></returns>
        public ActionResult Registrar(string nomE)
        {

            ViewBag.menu = "Evento";
            if (nomE == "" || nomE == null)
            {
                return RedirectToAction("Eventos");
            }
            modelEvento.estado = eventoBL.estadoEvento(nomE);
            if (modelEvento.estado == "" || modelEvento.estado == "FINALIZADO")
            {
                return RedirectToAction("Eventos");
            }

            int idEvento = eventoBL.getIdEvento(nomE);
            modelEvento.listaEventos = eventoBL.getEvento(idEvento);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.idEvento = item.idEvento;
                modelEvento.nombre = item.nombre;
                modelEvento.objetivo = item.objetivo;
                modelEvento.tipoEvento = item.tipoEvento;
                modelEvento.fVigenciaInicio = item.fVigenciaInicio;
                modelEvento.fVigenciaFin = item.fVigenciaFin;
                modelEvento.rutaIcono = item.rutaIcono;
            }
            if (modelEvento.listaEventos.Count == 0)
            {
                return RedirectToAction("Eventos");
            }
            if (modelEvento.tipoEvento == "Restringido")
            {
                var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
                var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
                if (c1 != null && perf != null)
                {

                    if (seguridad.DesEncriptar(perf.Value) != "Enlace")
                    {
                        return RedirectToAction("Eventos");
                    }
                }
                else
                {
                    return RedirectToAction("Eventos");
                }
            }

            return View(modelEvento);
        }

        /// <summary>
        /// Prepara la vista para el registro másivo al evento 
        /// Realiza las validaciones necesarias de cookies y nomE
        /// Obtiene los datos del evento como obtjetivo, tipo de evento, vigencia e icono
        /// </summary>
        /// <param name="nomE"></param>
        /// <returns></returns>
        public ActionResult RegistroMasivo(string nomE)
        {
            ViewBag.menu = "Evento";

            ViewBag.menu = "Evento";
            if (nomE == "" || nomE == null)
            {
                return RedirectToAction("Eventos");
            }
            modelEvento.estado = eventoBL.estadoEvento(nomE);
            if (modelEvento.estado == "" || modelEvento.estado == "FINALIZADO")
            {
                return RedirectToAction("Eventos");
            }

            int idEvento = eventoBL.getIdEvento(nomE);
            modelEvento.listaEventos = eventoBL.getEvento(idEvento);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.idEvento = item.idEvento;
                modelEvento.nombre = item.nombre;
                modelEvento.objetivo = item.objetivo;
                modelEvento.tipoEvento = item.tipoEvento;
                modelEvento.fVigenciaInicio = item.fVigenciaInicio;
                modelEvento.fVigenciaFin = item.fVigenciaFin;
                modelEvento.rutaIcono = item.rutaIcono;
            }
            if (modelEvento.listaEventos.Count == 0)
            {
                return RedirectToAction("Eventos");
            }
            //Validar si tipo de evento es restringido que existan las cookies de sesion y perfil sea enlace
            if (modelEvento.tipoEvento == "Restringido")
            {
                var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
                var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
                if (c1 != null && perf != null)
                {

                    if (seguridad.DesEncriptar(perf.Value) != "Enlace")
                    {
                        return RedirectToAction("Eventos");
                    }
                }
                else
                {
                    return RedirectToAction("Eventos");
                }
            }

            return View(modelEvento);
        }

        /// <summary>
        /// Sección para descargar la plantilla en excel para el registro al evento en registro masivo
        /// Retorna un File para generar la descarga 
        /// Recibe el nombre del evento para obtener la ruta de la plantilla en el servidor FTP
        /// </summary>
        /// <param name="nombreEve"></param>
        /// <returns></returns>
        public ActionResult DescargaPlantillaEReg(string nombreEve)
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

        //Registro masivo 
        /// <summary>
        /// Retorna un 'Listo' si el registro fue exitoso, de lo contrario retorna el error 
        /// Recibe como parametros la cantidad de participantes, la plantilla subida por el usuario, el archivo y el nombre del evento
        /// Utiliza la libreria ClosedXML
        /// Genera una datatable a partir de la lectura del excel 
        /// Hace la validación que la datatable contenga todos los campos habilitados para el registro al evento
        /// No acepta celdas vacias 
        /// Se prepara la datatable con los nombres de las columnas iguales a los campos de la tabla en la BD.
        /// Para el registro primero se realiza el registro a la tabla tblTipoRegistro, en caso de ocurrir un error se elimina el registro
        /// Después se utiliza el método registroMasivo(idEvento, idTipoReg, datatable), sera el encargado de realizar el registro masivo.
        /// Si el registro es exitoso se valida si el registro es Restringido o si existen las cookies de logeo y el usuario es enlace para relacionar el registro con el usuario y almacenar la información en tblUserEnlaceEvento
        /// </summary>
        /// <param name="cantParticipantes"></param>
        /// <param name="rutaExcel"></param>
        /// <param name="archivoExcel"></param>
        /// <param name="nombreEve"></param>
        /// <returns></returns>
        [HttpPost]
        public string RegistroEMasivoUser(string cantParticipantes, string rutaExcel, string archivoExcel, string nombreEve)
        {
            //obtener el id del evento con el nombre del Evento
            modelEvento.listaEventos = eventoBL.getEventoXnombre(nombreEve);
            int idEvento = 0;
            string tipoEvento = "";
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
                    string idTipoReg = tipoRegBL.agregarTipoReg(tipoEvento);
                    if (idTipoReg != "NO")
                        data = registroEvBL.registroMasivo(idEvento, idTipoReg, dt);

                    if (data != "Listo")
                    {//Eliminar el idTipoRegistro si no se cumplio con el registro de manera correcta
                        tipoRegBL.eliminarTipoRegistro(idTipoReg);
                        data = "Error";
                    }
                    else
                    {
                        data = idTipoReg;
                        //Agregar registro a tblUserEnlaceEvento
                        var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
                        var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
               
                        if (tipoEvento == "Restringido" || (c1 != null && perf != null))
                        {
                           //c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
                            bool agrego = userEnlaceEventoBL.agregarUserEnalceEve(idTipoReg, idEvento, seguridad.DesEncriptar(c1.Value));
                            while (agrego == false)
                            {
                                agrego = userEnlaceEventoBL.agregarUserEnalceEve(idTipoReg, idEvento, seguridad.DesEncriptar(c1.Value));
                                //Mientras no se haya agregado el registro se seguira intentando
                            }
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
        /// Prepara la vista para descargar los comprobantes de registro masivo al evento 
        /// Realiza las validaciones necesarias de cookies y parámetros para identificar si el usuario tiene acceso a la página
        /// 
        /// </summary>
        /// <param name="nombreE"></param>
        /// <param name="itr"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public ActionResult DescargaRegistroMasivo(string nombreE, string itr,string t)
        {

            ViewBag.menu = "Evento";
            if (nombreE == "" || nombreE == null || itr == "" || itr == null)
            {
                return RedirectToAction("Eventos");
            }
            modelEvento.estado = eventoBL.estadoEvento(nombreE);
            if((modelEvento.estado==""||modelEvento.estado== "FINALIZADO")&&t=="1")
            {
                return RedirectToAction("Eventos");
            }

            ViewData["tipo"] = t;
            modelEvento.idEvento = eventoBL.getIdEvento(nombreE);
            modelEvento.nombre = nombreE;
            modelRegistroEv.idTipoRegistro = itr;

            modelEvento.listaEventos = eventoBL.getEvento(modelEvento.idEvento);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.tipoEvento = item.tipoEvento;
            }

            //Validar si tipo de evento es restringido que existan las cookies de sesion y perfil sea enlace
            if (modelEvento.tipoEvento == "Restringido")
            {
                var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
                var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
                if (c1 != null && perf != null)
                {

                    if (seguridad.DesEncriptar(perf.Value) != "Enlace")
                    {
                        return RedirectToAction("Eventos");
                    }
                }
                else
                {
                    return RedirectToAction("Eventos");
                }
            }

            var tuple = new Tuple<EventoViewModel, RegistroEventoViewModel>(modelEvento, modelRegistroEv);
            return View(tuple);
        }

        //Crear .zip con constancias en PDF 
        /// <summary>
        /// Este es el método que es llamado desde la vista recibe como parametro el tipo de registro y el idEvento
        /// Utiliza el metodo HTMLtoPDF(evento,idTipoRegistro) para generar la descarga de los documentos en .pdf en un archivo comprimido
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="evento"></param>
        /// <returns></returns>
        public ActionResult PDFtoZIP(string tr, int evento)
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
            modelRegistroEv.listaRegistroEvento = registroEvBL.getRegistroEXidEvidTR(evento, tipoR);
            //for (int i = 1; i < 3; i++)//Cambiar por foreach con el list de la consulta de registros
            foreach (var item in modelRegistroEv.listaRegistroEvento)
            {
                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                filePath = path + "\\" + item.folio.ToString("0000000.##") + ".pdf";
                //PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                XMLWorkerHelper xml = XMLWorkerHelper.GetInstance();
                document.Open();
                //-------Sección para agregar una imagen de fondo//---------------------
                //Image pdfImage = Image.GetInstance(Server.MapPath("constanciaFondo.jpg"));

                //pdfImage.ScaleToFit(550, 600);

                //pdfImage.Alignment = iTextSharp.text.Image.UNDERLYING;
                //pdfImage.SetAbsolutePosition(25, 385);

                //document.Add(pdfImage);
                //----------------------------------------------------------------------


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
                foreach (var itemE in modelEvento.listaEventos)
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

                
                //string html = ContenidoHTML(nomCompleto, nomE, folio.ToString("0000000.##"), tipoFecha, d1, m1, d2, m2, year, existeMT);
                string html = ContenidoHTMLPruebaRegistro(nomCompleto, nomE, folio.ToString("0000000.##"), tipoFecha, d1, m1, d2, m2, year, existeMT);
                xml.ParseXHtml(writer, document, stringToStream(html), System.Text.Encoding.UTF8);

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
        private void ShowPdf(string s, string deleteFile)
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

        //Contenido HTML con constancia
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
        public string ContenidoHTML(string nomCompleto, string nombreEve, string folio, int tipoFecha, string d1, string m1, string d2, string m2, string year, bool existeMT)
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
                HTML = HTML + "<div class='datos'> del <label>" + d1 + "</label> al <label>" + d2 + "</label> de <label>" + m1 + "</label> de <label>" + year + "</label>,<br/>";
            }
            else if (tipoFecha == 2)
            {
                HTML = HTML + "<div class='datos'> del <label>" + d1 + "</label> de <label>" + m1 + "</label> al <label>" + d2 + "</label> de <label>" + m2 + "</label> de <label>" + year + "</label>,<br/>";
            }
            else
            {
                HTML = HTML + "<div class='datos'> del <label>" + d1 + "</label> de <label>" + m1 + "</label> de <label>" + year + "</label>,<br/>";
            }


            HTML = HTML + "en Guanajuato.</div>";

            if (existeMT == true)
            {
                HTML = HTML + "<div class='aligmesa'>" +
                "<div style='font-size: 15pt;font-weight: 700;'>Mesas de Trabajo</div>";

                modelMesaParticipante.listaMesaParticipante = mesaParticipanteBL.getMesaPartiXfolio(int.Parse(folio));
                foreach (var item in modelMesaParticipante.listaMesaParticipante)
                {
                    modelMesaEvento.listaMesasEvento = mesaEventoBL.getMesaTevento(item.idMesaTrab);
                    foreach (var itemMT in modelMesaEvento.listaMesasEvento)
                    {
                        string nombreMT = itemMT.nombreMT;
                        HTML = HTML + "<div>" + nombreMT + "</div>";
                    }
                }
                HTML = HTML + "</div>";

            }

            HTML = HTML + "<div class='folio' style='width: 620px;'>FOLIO " + folio + "</div>" +


            "</body> ";

            return HTML;
        }

        //Contenido HTML simple, Comprobante de Registro
        /// Código HTML para generar los comprobantes de registro de participantes 
        /// En el código HTML se agrega la información de los participantes como el 
        /// folio, nombre completo, fecha del evento, nombre del evento y en caso de estar inscrito a mesas de trabajo se agrega una sesión para 
        /// mostrar el nombre de las mesas de trabajo
        public string ContenidoHTMLPruebaRegistro(string nomCompleto, string nombreEve, string folio, int tipoFecha, string d1, string m1, string d2, string m2, string year, bool existeMT)
        {
            String HTML = "<head>" +
                "<meta charset='utf-8'/>" +
                "<style>" +
                "@page { " +
                    "margin: 0.6in 0.40in 0.25in;}" +
                ".participante{" +
                    "font-size: 16pt;font-family: 'Carrois Gothic', sans-serif;color: #888;}" +
                ".aligmesa{" +
                    "font-size: 13pt;align-content: center;text-align: center;font-family: 'Carrois Gothic', sans-serif;color: #264863;height: 120px;}" +
                ".mesa{" +
                    "margin: 20px  auto 5px;font-size: 15pt;font-weight: 700;font-family: 'Carrois Gothic', sans-serif;color: #004d99;align-content: center;text-align: center;border-top: dashed thin;border-bottom: dashed thin;width: 350px;}" +
                ".folio{" +
                    "text-align: right;margin-right: 130px;font-size: 16pt;font-weight: 700;color: ##004d99;font-family: 'Carrois Gothic', sans-serif;z-index: 10;}" +
                "body {" +
                    "font-family: sans-serif;margin: 1.5cm 0;text-align: justify;padding-bottom: 20px;}" +
                "#header,#footer {" +
                    "position: fixed;left: 0;right: 0;color: #aaa;font-size: 1.0em;background-color: #FFFFFF;padding-right: 10px;padding-bottom: 5px;}" +
                "#footer {" +
                    "bottom: -6px;border-top: 0.1pt solid #aaa;text-align: center;}" +
                "#header table,#footer table {" +
                    "width: 100%;border-collapse: collapse;border: none;}" +
                "#header td,#footer td {" +
                    "padding: 0;width: 50%;}" +
                "h1{" +
                    "display: block;font-size: 1.5em;font-weight: 700;color: #003A7D;text-transform: uppercase;font-family: 'Carrois Gothic', sans-serif;text-align: justify !important;}" +
                ".topTabla{" +
                    "font-family: 'Roboto', sans-serif;margin-top: -45px;font-size : 1rem !important;color : #FFFFFF !important;}" +
                "#pageCounter {" +
                    "counter-reset: pageTotal;}" +
                "#pageCounter span {counter-increment: pageTotal; }" +
                "#pageNumbers {counter-reset: currentPage;}" +
                "#pageNumbers div:before { " +
                    "counter-increment: currentPage;" +
                    "content: 'Pág. ' counter(currentPage) ' de ';" +
                    "align-content: center;}" +
                "#pageNumbers div:after { content: counter(pageTotal); }" +
                "@media print {" +
                    "h1{" +
                    "margin-top: -60px;" +
                    "font-size: 1.5em;" +
                    "font-weight: 700;" +
                    "color: #003A7D;" +
                    "text-transform: uppercase;" +
                    "font-family: 'Carrois Gothic', sans-serif;+" +
                    "text-align: justify !important;}}" +
                    "</style>" +

                    //"<title>PDF Registro Masivo a Evento</title>" +
                    "</head>" +
                    "<body>" +
                        "<div class='page'>" +
                            "<h1><label>"+nombreEve+"</label></h1>" +
                            "<div>";

            if (tipoFecha == 1)
            {
                HTML = HTML + "<p>Fecha del evento: <span>"+d1+"</span> al <span>"+d2+"</span> <span>"+m1+" del "+year+"</span></p>";
            }
            else if (tipoFecha == 2)
            {
                HTML = HTML + "<p>Fecha del evento: <span>" + d1 + " de <label>"+m1+"</label></span> al <span>" + d2 + "</span> de <span>" + m2 + " del " + year + "</span></p>";
            }
            else
            {
                HTML = HTML + "<p>Fecha del evento: <span>" + d1 + "</span> de <span>" + m1 + " del " + year + "</span></p>";
            }
            //"<p>Fecha del evento: <span>22</span> al <span>25</span> <span>Marzo</span></p>"+
            HTML = HTML + "</div>" +
                            "<div class='participante' class='mesa' align='center'>Participante: " + nomCompleto + "</div><br/>";

            if (existeMT == true)
            {
                HTML = HTML + "<table width='100%' class='salto_tabla'>" +
                "<tbody>" +
                    "<tr width='100%' align='center'>" +
                        "<th align='center'>Mesas de Trabajo</th>" +
                    "</tr>";

                modelMesaParticipante.listaMesaParticipante = mesaParticipanteBL.getMesaPartiXfolio(int.Parse(folio));
                foreach (var item in modelMesaParticipante.listaMesaParticipante)
                {
                    modelMesaEvento.listaMesasEvento = mesaEventoBL.getMesaTevento(item.idMesaTrab);
                    foreach (var itemMT in modelMesaEvento.listaMesasEvento)
                    {
                        string nombreMT = itemMT.nombreMT;
                       
                        HTML = HTML + "<tr align='center'>" +
                   "<td><label>"+ nombreMT + "</label></td>" +
               "</tr>";
                    }
                }
               

                   
                HTML=HTML+"</tbody>" +
            "</table>";

               
                

            }


            HTML =HTML+"<div class='folio'>Folio: "+folio+"</div>" +
                          "</div>"+
                          "</body>";
            return HTML;
        }

        /// <summary>
        /// Utilizado para agregar el código HTML al método xml.ParseXHtml de la libreria utilizada
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

        /// <summary>
        /// Prepara la vista para mostrar el PDF a imprimir o guardar del registro masivo 
        /// Obtiene los datos de todos los participantes con el método DataTableParticipantesRegEvento los almacena en una dataTable local 
        /// y regresa una tupla como modelo a la vista
        /// </summary>
        /// <param name="e"></param>
        /// <param name="itr"></param>
        /// <returns></returns>
        public ActionResult pdf_RegistroM(string e, string itr)
        {
            modelEvento.idEvento = eventoBL.getIdEvento(e);
            modelEvento.nombre = e;
            modelRegistroEv.idTipoRegistro = itr;

            modelEvento.listaEventos = eventoBL.getEventoXnombre(e);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.fVigenciaInicio = item.fVigenciaInicio;
                modelEvento.fVigenciaFin = item.fVigenciaFin;
            }

            DataTable dt = DataTableParticipantesRegEvento(modelEvento.idEvento, modelRegistroEv.idTipoRegistro);

            var tuple = new Tuple<EventoViewModel, RegistroEventoViewModel, DataTable>(modelEvento, modelRegistroEv, dt);
            return View(tuple);
        }

        /// <summary>
        /// Retorna una DataTable con todos los datos de los participantes registrados al evento masivo
        /// La DataTable esta conformada solo con los campos que han sido habilitados para el registro al evento
        /// </summary>
        /// <param name="idE"></param>
        /// <param name="idTR"></param>
        /// <returns></returns>
        public DataTable DataTableParticipantesRegEvento(int idE, string idTR)
        {

            DataTable dt = new DataTable("Participantes del evento");

            //Lenar las columnas solo con los campos aceptados
            dt.Columns.Add("folio");
            foreach (var campo in campoEvenBL.camposEvento(idE))
            {
                dt.Columns.Add(campo);
            }


            modelRegistroEv.listaRegistroEvento = registroEvBL.getRegistroEXidEvidTR(idE, idTR);

            foreach (var item in modelRegistroEv.listaRegistroEvento)
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



                dt.Rows.Add(dr);

            }


            //Cambiar nombre de la columna del dT
            if (dt.Columns.Contains("folio"))
                dt.Columns["folio"].ColumnName = "Folio";
            if (dt.Columns.Contains("CCT"))
                dt.Columns["CCT"].ColumnName = "Clave CT";
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
            return dt;
        }

        /// <summary>
        /// Prepara la vista para el registro Individual al Evento 
        /// Obtiene los datos que serán mostrados en comboBox como la mesas de trabajo y funciones.
        /// Obtiene los campos habilitados para el registro al evento, estos datos serán para saber que 
        /// campos son los que se mostraran en la vista.
        /// </summary>
        /// <param name="nEvento"></param>
        /// <returns></returns>
        public ActionResult RegistroIndividual(string nEvento)
        {
            if (nEvento=="" || nEvento==null)
            {
                return RedirectToAction("Eventos");
            }
            modelEvento.estado = eventoBL.estadoEvento(nEvento);
            if (modelEvento.estado == "" || modelEvento.estado == "FINALIZADO")
            {
                return RedirectToAction("Eventos");
            }

            ViewBag.menu = "Evento";
            modelEvento.nombre = nEvento;
            nEvento = modelEvento.nombre;
            modelEvento.nombre = nEvento;
            modelEvento.idEvento = eventoBL.getIdEvento(nEvento);
            modelFuncionPart.listaFuncionPart = funcionParticipanteBL.getFuncionEventoXidEvento(modelEvento.idEvento);
            modelMesaEvento.listaMesasEvento = mesaEventoBL.getMesaTeventoXevento(modelEvento.idEvento);

            modelCampoEvento.listaCampoEvento = campoEvenBL.getCampoEventoXidEvento(modelEvento.idEvento);
            foreach (var item in modelCampoEvento.listaCampoEvento)
            {
                modelCampoEvento.CCT = item.CCT;
                modelCampoEvento.nombrePlantel = item.nombrePlantel;
                modelCampoEvento.municipio = item.municipio;
                modelCampoEvento.regionSEG = item.regionSEG;
                modelCampoEvento.nivel = item.nivel;
                modelCampoEvento.instiSubsis = item.instiSubsis;
                modelCampoEvento.CURP = item.CURP;
                modelCampoEvento.nombre = item.nombre;
                modelCampoEvento.apellidoPat = item.apellidoPat;
                modelCampoEvento.apellidoMat = item.apellidoMat;
                modelCampoEvento.correoElectronico = item.correoElectronico;
                modelCampoEvento.telefono = item.telefono;
                modelCampoEvento.sexo = item.sexo;
                modelCampoEvento.funcion = item.funcion;
                modelCampoEvento.mesaTrabajo = item.mesaTrabajo;
            }

            var tuple = new Tuple<EventoViewModel, FuncionParticipanteViewModel, MesaEventoViewModel, CampoEventoViewModel>(modelEvento, modelFuncionPart, modelMesaEvento, modelCampoEvento);
            return PartialView(tuple);
            //return View(modelEvento);
            //return View();
        }


        //public ActionResult _CamposRegistroIndividual()
        //{

        //    var tuple = new Tuple<EventoViewModel, FuncionParticipanteViewModel, MesaEventoViewModel, CampoEventoViewModel>(modelEvento, modelFuncionPart, modelMesaEvento, modelCampoEvento);
        //    return PartialView(tuple);
        //}

            /// <summary>
            /// Retorna verdadero o falso, realiza la validación de la CCT, que esta exista en la BD ademas 
            /// de que la CCT sea valida para realizar el registro al evento
            /// </summary>
            /// <param name="CCT"></param>
            /// <param name="ne"></param>
            /// <returns></returns>
        public bool ValidarCCT(string CCT, string ne)
        {
            bool existe = false;
            modelCEO.listCEO = ceoBL.getCEO(CCT);


            modelCEO.idCEO = ceoBL.getIdCEO(CCT);
            modelEvento.idEvento = eventoBL.getIdEvento(ne);

            modelEscuelaEvento.listaEscuelasEvento = escuelaEventoBL.getEscuelaEventos(modelEvento.idEvento, modelCEO.idCEO);
            if (modelEscuelaEvento.listaEscuelasEvento.Count > 0)
            {
                existe = true;
            }

            return existe;
        }

        /// <summary>
        /// Obtiene los datos de regionSEG, NombreCentroTrabajo, Nivel, municipio, instiSubsis
        /// utilizando el parámetro CCT
        /// </summary>
        /// <param name="CCT"></param>
        /// <returns></returns>
        public string ObtenerDatosCCT(string CCT)
        {
            string datos = "";
            modelCEO.idCEO = ceoBL.getIdCEO(CCT);
            modelCEO.listCEO = ceoBL.getCEO(modelCEO.idCEO);
            foreach (var item in modelCEO.listCEO)
            {
                datos = item.regionSEG + "|" + item.nombreCentroTrabajo + "|" + item.nivel + "|" + item.municipio + "|" + item.instiSubsis;
            }
            return datos;
        }

        /// <summary>
        /// Registro Individual al evento
        /// Valida que los campos necesarios para el registro al evento sean correctos
        /// Obtiene los datos necesarios utilizando la CCT
        /// Obtiene las mesas de trabajo a las cuales se inscribira 
        /// Realiza un nuevo registro en tblTipoRegistro, si el error falla se elimina el registro
        /// Utiliza el método agregarUserEnalceEve para realizar el registro masivo
        /// Si el registro es correcto se obtiene el folio (int) de lo contrario se obtiene el error (string)
        /// Si el evento es restringido o las cookies de sesión existen y el usuario es enlace se realiza el registro en tblUserEnlaceEvento en donde se relaciona el registro con el usuario que lo realizo
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sex"></param>
        /// <param name="fun"></param>
        /// <param name="me"></param>
        /// <param name="cct"></param>
        /// <param name="curp"></param>
        /// <param name="nom"></param>
        /// <param name="ap"></param>
        /// <param name="am"></param>
        /// <param name="email"></param>
        /// <param name="tel"></param>
        /// <returns></returns>
        public string RegistroIndividualParti(string e, string sex, string fun, string me, string cct, string curp, string nom, string ap, string am, string email, string tel)
        {
            //obtener datos faltastes para el registro utilizando la CCT
            string folio = "";
            modelEvento.idEvento = eventoBL.getIdEvento(e);
            modelEvento.listaEventos = eventoBL.getEvento(modelEvento.idEvento);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.tipoEvento = item.tipoEvento;
            }
            modelCEO.listCEO = ceoBL.getCEO(cct);
            foreach (var item in modelCEO.listCEO)
            {
                modelCEO.nombreCentroTrabajo = item.nombreCentroTrabajo;
                modelCEO.municipio = item.municipio;
                modelCEO.regionSEG = item.regionSEG;
                modelCEO.nivel = item.nivel;
                modelCEO.instiSubsis = item.instiSubsis;
            }

            string met = "";
            List<int> idMesaTrabajoParticipante = new List<int>();
            string[] splitCol = me.Split('|');
            foreach (var item in splitCol)
            {
                if (item.Length != 0)
                {

                    met = item.Trim();
                    int idMT = mesaEventoBL.getIDmesaXnombreYevento(modelEvento.idEvento, met);
                    idMesaTrabajoParticipante.Add(idMT);

                }

            }


            string idTipoReg = tipoRegBL.agregarTipoReg(modelEvento.tipoEvento);
            if (idTipoReg != "NO")
                folio = registroEvBL.agregarUserEnalceEve(modelEvento.idEvento, idTipoReg, cct, modelCEO.nombreCentroTrabajo, modelCEO.municipio, modelCEO.regionSEG, modelCEO.nivel, modelCEO.instiSubsis, curp, nom, ap, am, email, tel, sex, fun, idMesaTrabajoParticipante);

            int fol = 0;
            bool canConvert = int.TryParse(folio, out fol);

            if (canConvert == false)//if (folio != "Listo")
            {//Eliminar el idTipoRegistro si no se cumplio con el registro de manera correcta
                tipoRegBL.eliminarTipoRegistro(idTipoReg);
                //folio = "Error";
            }
            else
            {
                TempData["FolioUser"] = folio;
                //folio = idTipoReg;
                //Agregar registro a tblUserEnlaceEvento
                
                //Se agrego para registrar el usuario enlace en caso de ser un evento publico y el usuario enlace este logeado... 
                
                var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
                var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
               



                if (modelEvento.tipoEvento == "Restringido" || (c1 != null && perf != null))
                {
                    if (seguridad.DesEncriptar(perf.Value) == "Enlace")
                    {
                        bool agrego = userEnlaceEventoBL.agregarUserEnalceEve(idTipoReg, modelEvento.idEvento, seguridad.DesEncriptar(c1.Value));
                        while (agrego == false)
                        {
                            agrego = userEnlaceEventoBL.agregarUserEnalceEve(idTipoReg, modelEvento.idEvento, seguridad.DesEncriptar(c1.Value));
                            //Mientras no se haya agregado el registro se seguira intentando
                        }
                    }
                        
                }
               

            }


            


            return folio;
        }

        /// <summary>
        /// Pepara vista parcial para el PopUp, muestra el error ocurrido al momento del registro individual
        /// Muestra los datos del evento como Nombre y fecha de vigencia
        /// </summary>
        /// <param name="nomE"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public ActionResult _PopUpErrorRegistro(string nomE, string error)
        {
            modelEvento.nombre = nomE;
            modelEvento.objetivo = error;
            modelEvento.idEvento = eventoBL.getIdEvento(modelEvento.nombre);
            modelEvento.listaEventos = eventoBL.getEvento(modelEvento.idEvento);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.fVigenciaInicio = item.fVigenciaInicio;
                modelEvento.fVigenciaFin = item.fVigenciaFin;
            }
            return PartialView(modelEvento);
        }

        

       /// <summary>
       /// Prepara la vista de registro Exitoso
       /// Obtiene los datos del evento como nombre, fecha de vigencia, icono
       /// </summary>
       /// <param name="nomE"></param>
       /// <returns></returns>
        public ActionResult RegistroExitoso(string nomE)
        {
            //nomE = "Control de fecha";
            //TempData["FolioUser"] = "201";
            ViewBag.menu = "Evento";
            if (TempData["FolioUser"] == null)
            {
                return RedirectToAction("RegistroIndividual", new { nEvento = nomE });
            }
            modelRegistroEv.folio = int.Parse(TempData["FolioUser"].ToString());
            modelEvento.nombre = nomE;
            modelEvento.idEvento = eventoBL.getIdEvento(modelEvento.nombre);
            modelEvento.listaEventos = eventoBL.getEvento(modelEvento.idEvento);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.fVigenciaInicio = item.fVigenciaInicio;
                modelEvento.fVigenciaFin = item.fVigenciaFin;
                modelEvento.rutaIcono = item.rutaIcono;
            }

            var tuple = new Tuple<EventoViewModel, RegistroEventoViewModel>(modelEvento, modelRegistroEv);
            return View(tuple);
            
        }

        /// <summary>
        /// Prepara la vista que muestra el PDF de comprobante de registro para imprimir o guardar
        /// </summary>
        /// <param name="e"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        public ActionResult PDF_RegistroIndividual(string e, string folio)
        {
            modelEvento.nombre = e;
            modelEvento.idEvento = eventoBL.getIdEvento(modelEvento.nombre);
            modelRegistroEv.folio = int.Parse(folio);
            
           
            modelEvento.listaEventos = eventoBL.getEventoXnombre(modelEvento.nombre);
            foreach (var item in modelEvento.listaEventos)
            {
                modelEvento.fVigenciaInicio = item.fVigenciaInicio;
                modelEvento.fVigenciaFin = item.fVigenciaFin;
                
            }

            modelRegistroEv.listaRegistroEvento = registroEvBL.getRegistroEXfolio(modelRegistroEv.folio);
            foreach(var item in modelRegistroEv.listaRegistroEvento)
            {
                modelRegistroEv.nombre = item.nombre;
                modelRegistroEv.apellidoMat = item.apellidoMat;
                modelRegistroEv.apellidoPat = item.apellidoPat;
            }

            modelMesaParticipante.listaMesaPRI = mesaParticipanteBL.mesaParticipanteRegIn(modelRegistroEv.folio);


            var tuple = new Tuple<EventoViewModel, RegistroEventoViewModel,MesaParticipanteViewModel>(modelEvento, modelRegistroEv,modelMesaParticipante);
            return View(tuple);
        }

        /// <summary>
        /// Prepara la vista para la sesión de los usuarios Enlace
        /// Obtiene todos los eventos a los cuales el usuario se a registrado y utiliza paginación
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult SesionEnlaces(int? page)
        {
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];

            if (c1 == null || perf == null)
            {
                return RedirectToAction("Registro", "Usuario");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Enlace")
            {
                return RedirectToAction("Eventos", "Evento");
            }

            int pageSize = 15;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<SesionEnlace> stu = null;

            //BPRevisionTable modelInfoReviBP = new BPRevisionTable();
            //RevisionBPBL blRevi = new RevisionBPBL();
            List<SesionEnlace> lista = new List<SesionEnlace>();
            lista = eventoBL.getEventosSesionEnlace(seguridad.DesEncriptar(c1.Value));
            modelEvento.listSesionEnlace = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            var tuple = new Tuple<IPagedList<SesionEnlace>>(stu);
            return View(tuple);

            //return View();
        }


    }

}