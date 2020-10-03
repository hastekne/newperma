using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using DesarrolloProyDSP.Models.ModuloBP;
using DesarrolloProyDSP.Models.Usuario;
using DesarrolloProyDSP.Models.Global;
using DesarrolloProyDSP.Helpers;

using System.Net;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

//
using BusinessModel.Business.ModuloBP;
using BusinessModel.Business.Global;
using BusinessModel.Business.Usuario;

//Crear PDF
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

//
//using System.Drawing;

// para la paginación en la tabla
using PagedList;
using BusinessModel.Models.ModuloBP;
//Regular Expresion Validar
using System.Text.RegularExpressions;

namespace DesarrolloProyDSP.Controllers.ModuloBP
{

    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class BuenasPracticasController : Controller
    {
        Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.66", "SEG\\mc_mendoza", "M2pm2019");
        //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");

        BuenaPracticaViewModel modelBP = new BuenaPracticaViewModel();
        UsuarioViewModel modelUser = new UsuarioViewModel();
        EjeViewModel modelEje = new EjeViewModel();
        MunicipioViewModel modelMunicipio = new MunicipioViewModel();
        FuncionDesarrolloViewModel modelFuncion = new FuncionDesarrolloViewModel();
        CEOViewModel modelCEO = new CEOViewModel();
        PlantillaViewModel modelPlantilla = new PlantillaViewModel();
        RevisionBPViewModel modelRevision = new RevisionBPViewModel();
        AutoresViewModel modelAutor = new AutoresViewModel();
        CampoBPViewModel modelCampoBP = new CampoBPViewModel();
        ImagenBPViewModel modelImagenBP = new ImagenBPViewModel();
        ModuloViewModel modelModulo = new ModuloViewModel();
        ImagenRevisionViewModel modelImgRevision = new ImagenRevisionViewModel();

        ModuloBL moduloBL = new ModuloBL();
        AutoresBL autorBL = new AutoresBL();
        BuenasPracticasBL bpBL = new BuenasPracticasBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        EjeBL ejeBL = new EjeBL();
        MunicipioBL municipioBL = new MunicipioBL();
        FuncionDesarrolloPBL funcionBL = new FuncionDesarrolloPBL();
        CEOBL ceoBL = new CEOBL();
        PlantillaBL plantillaBL = new PlantillaBL();
        RevisionBPBL revisionBL = new RevisionBPBL();
        CampoBPBL campoBPBL = new CampoBPBL();
        ImagenBPBL imagenBPBL = new ImagenBPBL();
        ImagenRevisionBL imagenRevisionBL = new ImagenRevisionBL();
        ImagenModuloBannerBL banerBL = new ImagenModuloBannerBL();

        Seguridad seguridad = new Seguridad();
        EnviarCorreo enviarCorreo = new EnviarCorreo();


        public ActionResult Index()
        {
            modelEje.listaEjes = ejeBL.getListaEje(true);
            modelMunicipio.listMunicipios = municipioBL.getMunicipio();
            modelFuncion.listFunciones = funcionBL.getFuncionesDesarrollo(true);
            modelCEO.listCEO = ceoBL.getCEO();
            var tuple = new Tuple<CEOViewModel, EjeViewModel, MunicipioViewModel, FuncionDesarrolloViewModel>(modelCEO, modelEje, modelMunicipio, modelFuncion);
            return View(tuple);
        }

        [HttpPost]
        public ActionResult Index(string titulo)
        {
            modelEje.idEje = ejeBL.getIdEje(Request.Form["selectEje"]);
            modelUser.nombreUsuario = "CeciMP"; //Va el usuario colaborador
            modelMunicipio.idMunicipio = municipioBL.idMunicipio(Request.Form["selectMunicipio"]);
            modelCEO.idCEO = ceoBL.getIdCEO(Request.Form["selectCEO"]);
            modelFuncion.idFuncionD = funcionBL.getIdFuncion(Request.Form["selectFuncion"]);

            //if (bpBL.existeTitulo(titulo) == false)
            //{
            bpBL.agregarBP(modelEje.idEje, modelUser.nombreUsuario, modelMunicipio.idMunicipio, modelCEO.idCEO, titulo, modelFuncion.idFuncionD, "GUARDADO");
            //Session["tituloBP"] = titulo;
            ValorSession(titulo);
            return RedirectToAction("SegundaParteBP");
            //}

            //return RedirectToAction("Index");

        }

        /// <summary>
        /// Vista para la segunda parte del registro de BP, utiliza la cookie del Titulo de la BP
        /// para obtener los puntos pbservacionesBP,situacionMejora, diagnosticoRealizo, caracteristicasContexto,
        /// descripcionActividadesRealizadas, elementoInnovador, resultadosObtenidos, fuentesInformacion
        /// los datos del modelo son desplegados en esta vista.
        /// </summary>
        /// <returns>View modelBP</returns>
        public ActionResult SegundaParteBP()
        {
            //modelBP.idBuenaPractica = bpBL.getIdBP((string)Session["tituloBP"].ToString());
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            int t = int.Parse(seguridad.DesEncriptar(tit.Value));
            modelBP.idBuenaPractica = t;
            modelBP.listaBP = bpBL.getBPXID(modelBP.idBuenaPractica);
            foreach (var item in modelBP.listaBP)
            {
                modelBP.tituloBP = item.tituloBP;
                modelBP.observacionesBP = item.observacionesBP;
                modelBP.tituloBP = item.tituloBP;
                modelBP.situacionMejora = item.situacionMejora;
                modelBP.diagnosticoRealizo = item.diagnosticoRealizo;
                modelBP.caracteristicasContexto = item.caracteristicasContexto;
                modelBP.descripcionActividadesRealizadas = item.descripcionActividadesRealizadas;
                modelBP.elementoInnovador = item.elementoInnovador;
                modelBP.resultadosObtenidos = item.resultadosObtenidos;
                modelBP.fuentesInformacion = item.fuentesInformacion;
            }
            // Session["tituloBP"] = modelBP.tituloBP;
            //Realizar la consulta para saber que hay en los campos con base en la buena practica 
            return View(modelBP);
        }

        //[HttpPost] RedirectResul return redirect("Segunda parte")
        //[HttpPost]
        /// <summary>
        /// Utiliza las cookies de titulo y estado de la buena practica, realiza la validación de 
        /// que el titulo no exista, utiliza el método editarBP para actualizar todos los datos de la BP y el estado
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="situacion"></param>
        /// <param name="diagnostico"></param>
        /// <param name="caracteristicas"></param>
        /// <param name="actividadesR"></param>
        /// <param name="innovador"></param>
        /// <param name="resultadosObt"></param>
        /// <param name="observaciones"></param>
        /// <param name="fuentesInf"></param>
        /// <returns>True=Exito en guardado de BP || False=Falla en guardado de BP</returns>
        public bool GuardarBP(string titulo, string situacion, string diagnostico, string caracteristicas, string actividadesR, string innovador, string resultadosObt, string observaciones, string fuentesInf)
        {
            bool guardo = false;
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            var estad = ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];
            if (bpBL.existeTitulo(titulo, int.Parse(seguridad.DesEncriptar(tit.Value))) == false)
            {
                var eBP = "GUARDADO";
                //modelBP.idBuenaPractica = bpBL.getIdBP((string)Session["tituloBP"].ToString());

                int t = int.Parse(seguridad.DesEncriptar(tit.Value));
                modelBP.idBuenaPractica = t;

                if (seguridad.DesEncriptar(estad.Value) != "GUARDADO")
                {
                    eBP = seguridad.DesEncriptar(estad.Value);
                }

                if (bpBL.editarBP(modelBP.idBuenaPractica, titulo, situacion, diagnostico, caracteristicas, actividadesR, innovador, resultadosObt, observaciones, fuentesInf, eBP) == true)
                {
                    //Enviar mensaje de BP guardada
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al guardar la Buena práctica, vuelva a intentar.");
                }
                guardo = true;
            }
            return guardo;

        }

        /// <summary>
        /// Obtiene el Valor True o False con base al estado de envio a revision 
        /// Primero se debe de actualizar todos los datos de la buena práctica en la segunda sección
        /// Se actualiza el estado a Revisando 
        /// Si la BP no ha sido asignada a un usuario revisor se debe asignar y se le notifica al usuario. 
        /// En caso de contar con un usuario revisor asignado se debe de notificar al usuario.
        /// Se valida que la cantidad de anexos sea minimo 2 y máximo 5 para enviar la BP a revision
        /// No se aceptan camposvacios
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="situacion"></param>
        /// <param name="diagnostico"></param>
        /// <param name="caracteristicas"></param>
        /// <param name="actividadesR"></param>
        /// <param name="innovador"></param>
        /// <param name="resultadosObt"></param>
        /// <param name="observaciones"></param>
        /// <param name="fuentesInf"></param>
        /// <returns></returns>
        public string EnviarRevision(string titulo, string situacion, string diagnostico, string caracteristicas, string actividadesR, string innovador, string resultadosObt, string observaciones, string fuentesInf)
        {
            string envio = "true";
            //Primero enviar correo, luego acturalizar, estado == EN revision
            //modelBP.idBuenaPractica = bpBL.getIdBP((string)Session["tituloBP"].ToString());

            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];

            int t = int.Parse(seguridad.DesEncriptar(tit.Value));

            modelBP.idBuenaPractica = t;
            if (imagenBPBL.contarArchivos(t, "Anexos") < 2)
            {

                envio = "false";
            }
            else
            {

                if (revisionBL.agregarRevision(modelBP.idBuenaPractica, DateTime.Now) == true)
                {
                    //enviar correo y update de la bp

                }
                

                if (bpBL.editarBP(modelBP.idBuenaPractica, titulo, situacion, diagnostico, caracteristicas, actividadesR, innovador, resultadosObt, observaciones, fuentesInf, "REVISANDO") == true)
                {
                    //Enviar mensaje de BP enviada a revision
                    //Obtener correo de usuario logeado
                    //var user =  Request.Cookies["CookieUsuario"].Value;//ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
                    //modelUser.listaUsuario = usuarioBL.listUsuario(user);
                    //foreach (var item in modelUser.listaUsuario)
                    //{
                    //    modelUser.nombre = item.nombre;
                    //    modelUser.pApellido = item.pApellido;
                    //    modelUser.sApellido = item.sApellido;
                    //}
                    //string nomAutorCompl = modelUser.nombre + " " + modelUser.pApellido + " " + modelUser.sApellido;
                    try
                    {
                        modelRevision.listRevision = revisionBL.getRevisiones(modelBP.idBuenaPractica);
                        foreach (var item in modelRevision.listRevision)
                        {
                            modelUser.listaUsuario = usuarioBL.listUsuario(item.usuarioRevisor);
                            foreach (var item2 in modelUser.listaUsuario)
                            {
                                modelUser.correoElectronico = item2.correoElectronico;
                            }
                        }
                        enviarCorreo.EnviarEmail("RevisarBP", titulo, modelUser.correoElectronico);

                        //Seccion para limpiar la cookie de nombre titulo
                        if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieIdTituloBP"))
                        {
                            HttpCookie CookieIdTituloBP = this.ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
                            CookieIdTituloBP.Expires = DateTime.Now.AddDays(-1);
                            this.ControllerContext.HttpContext.Response.Cookies.Add(CookieIdTituloBP);
                        }

                        if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieEstadoBP"))
                        {
                            HttpCookie CookieEstadoBP = this.ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];
                            CookieEstadoBP.Expires = DateTime.Now.AddDays(-1);
                            this.ControllerContext.HttpContext.Response.Cookies.Add(CookieEstadoBP);
                        }

                    }
                    catch {
                        envio = "crash";
                        bpBL.editarEstadoBP(modelBP.idBuenaPractica, "GUARDADO");
                        ModelState.AddModelError(string.Empty, "Error al enviar a revisión la Buena práctica, vuelva a intentar.");
                    }

                   




                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al enviar a revisión la Buena práctica, vuelva a intentar.");
                }

            }


            return envio;
        }

        /// <summary>
        /// Ejemplo para crear un archivo de Word en el servido
        /// Funcional, pero sin implementar aun, no se ha requerido 
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="situacion"></param>
        /// <param name="diagnostico"></param>
        /// <param name="caracteristicas"></param>
        /// <param name="actividadesR"></param>
        /// <param name="innovador"></param>
        /// <param name="resultadosObt"></param>
        /// <param name="observaciones"></param>
        /// <param name="fuentesInf"></param>
        public void crearWordServer(string titulo, string situacion, string diagnostico, string caracteristicas, string actividadesR, string innovador, string resultadosObt, string observaciones, string fuentesInf)
        {
            modelBP.idBuenaPractica = bpBL.getIdBP((string)Session["tituloBP"].ToString());
            //Helpers.FTPClient img = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
            Helpers.FTPClient img = new Helpers.FTPClient("ftp://192.168.1.69", "SEG\\mc_mendoza", "M2pm2019");
            //-----------------Para Crear el word--------------------------
            object ObjMiss = System.Reflection.Missing.Value;
            Word.Application ObjWord = new Word.Application();
            Word.Document ObjDoc = ObjWord.Documents.Add(ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss);
            ObjDoc.Activate();
            object oEndOfDoc = "\\endofdoc";
            object oRng;

            Word.Paragraph p1;
            p1 = ObjDoc.Content.Paragraphs.Add(ref ObjMiss);
            p1.Range.Text = "Titulo";
            p1.Range.Font.Bold = 1;
            p1.Format.SpaceAfter = 6;
            p1.Range.InsertParagraphAfter();

            Word.Paragraph p2;
            oRng = ObjDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            p2 = ObjDoc.Content.Paragraphs.Add(ref oRng);
            p2.Range.Text = titulo;
            p1.Range.Font.Bold = 0;
            p2.Format.SpaceAfter = 10;
            p2.Range.InsertParagraphAfter();

            Word.Paragraph p3;
            oRng = ObjDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            p3 = ObjDoc.Content.Paragraphs.Add(ref oRng);
            p3.Range.Text = "Situación que quiso mejorar";
            p3.Range.Font.Bold = 1;
            p3.Format.SpaceAfter = 6;
            p3.Range.InsertParagraphAfter();

            Word.Paragraph p4;
            oRng = ObjDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            p4 = ObjDoc.Content.Paragraphs.Add(ref oRng);
            p4.Range.Text = situacion;
            p4.Range.Font.Bold = 0;
            p4.Format.SpaceAfter = 10;
            p4.Range.InsertParagraphAfter();



            Word.InlineShape p5;
            oRng = ObjDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            p5 = ObjDoc.InlineShapes.AddPicture(img.downloadVistaPrevia("flor.jpg"), ref ObjMiss, ref ObjMiss, ref oRng);
            p5.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            // p3.Format.SpaceAfter = 24;
            p5.Range.InsertAfter("\r\n");

            object ruta = System.IO.Path.GetTempPath() + Guid.NewGuid() + ".doxc";
            ObjDoc.SaveAs2(ruta);

            ObjDoc.Close(ref ObjMiss, ref ObjMiss, ref ObjMiss);
            //FTPClient con = new FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
            con.upload("a.docx", ruta.ToString());
        }

        /// <summary>
        /// Método para crear un PDF y guardarlo en la dirección 
        /// Funcional pero no se ha implementado, no se ha requerido en el proyecto
        /// </summary>
        public void crearPDF2()
        {
            Document doc = new Document(PageSize.LETTER);
            doc.SetMargins(30f, 30f, 30f, 30f);

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\mc_mendoza\Documents\CarpetaServFTP\prueba18.pdf", FileMode.Create));

            doc.AddTitle("Buena Practica");
            doc.AddCreator("SEG");

            doc.Open();
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            //  BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, BaseFont.TIMES_ITALIC, false);
            Font times = new Font(Font.FontFamily.COURIER, 14, Font.BOLD, BaseColor.BLUE);

            Image logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/fondo.png"));
            logo.ScalePercent(40f);
            logo.SetAbsolutePosition(0f, 0f);
            doc.Add(logo);


            Image contenidoFondo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/f.png"));
            contenidoFondo.ScalePercent(40f);
            contenidoFondo.SetAbsolutePosition(0f, 0f);


            PdfPTable table = new PdfPTable(1);
            Paragraph titulo = new Paragraph();
            Paragraph contenido = new Paragraph();
            // Escribimos el encabezamiento en el documento

            doc.Add(new Paragraph("Buena Práctica"));
            doc.Add(Chunk.NEWLINE);
            doc.NewPage();


            foreach (var item in bpBL.getBPXID(4))
            {

                Paragraph paragraph = new Paragraph();
                paragraph.Clear();//ahora utilizo la clase Paragraph 
                paragraph.Font = new Font(FontFactory.GetFont("Arial", 10, Font.BOLD));
                paragraph.Alignment = Element.ALIGN_JUSTIFIED_ALL;
                paragraph.Add(item.situacionMejora);
                PdfPCell cell2 = new PdfPCell();
                cell2.Border = Rectangle.NO_BORDER;

                cell2.AddElement(paragraph);
                cell2.Colspan = 3;
                table.AddCell(cell2);
                doc.Add(table);
                paragraph.Clear();
            }




            doc.Close();
            writer.Close();
        }

        /// <summary>
        /// Método Ejemplo para crear un pdf, funcional, no se ha requerido implementar aun
        /// </summary>
        public void crearPDF()
        {
            Document doc = new Document(PageSize.LETTER);
            doc.SetMargins(30f, 30f, 30f, 30f);


            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\mc_mendoza\Documents\CarpetaServFTP\prueba18.pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Buena Practica");
            doc.AddCreator("SEG");

            // Abrimos el archivo
            doc.Open();


            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            //  BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, BaseFont.TIMES_ITALIC, false);
            Font fTitulo = new Font(Font.FontFamily.TIMES_ROMAN, 14, Font.BOLD, BaseColor.BLUE);
            Font fContenido = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK);

            Image logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/fondo.png"));
            logo.ScalePercent(40f);
            logo.SetAbsolutePosition(0f, 0f);
            doc.Add(logo);


            Image contenidoFondo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/f.png"));
            contenidoFondo.ScalePercent(40f);
            contenidoFondo.SetAbsolutePosition(0f, 0f);




            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("Buena Práctica"));
            doc.Add(Chunk.NEWLINE);

            doc.NewPage();




            Paragraph titulo = new Paragraph();
            Paragraph contenido = new Paragraph();


            foreach (var item in bpBL.getBPXID(4))
            {

                // p.Alignment = Element.ALIGN_CENTER;
                //doc.Add(p);
                doc.Add(contenidoFondo);
                titulo = new Paragraph("TITULO", fTitulo);
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                contenido = new Paragraph(item.tituloBP, fContenido);
                contenido.Alignment = Element.ALIGN_CENTER;
                doc.Add(contenido);
                doc.Add(contenidoFondo);

                titulo = new Paragraph("SITUACIÓN DE MEJORA", fTitulo);
                doc.Add(titulo);
                contenido = new Paragraph(item.situacionMejora, fContenido);
                contenido.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(contenido);
                doc.Add(contenidoFondo);

                titulo = new Paragraph("DIAGNOSTRICO REALIZADO", fTitulo);
                doc.Add(titulo);
                contenido = new Paragraph(item.diagnosticoRealizo, fContenido);
                contenido.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(contenido);
                doc.Add(contenidoFondo);

                titulo = new Paragraph("CARACTERISTICAS DEL CONTEXTO", fTitulo);
                doc.Add(titulo);
                contenido = new Paragraph(item.caracteristicasContexto, fContenido);
                contenido.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(contenido);
                doc.Add(contenidoFondo);

                titulo = new Paragraph("DESCRIPCIÓN DE LAS ACTIVIDADES REALIZADAS", fTitulo);
                doc.Add(titulo);
                contenido = new Paragraph(item.descripcionActividadesRealizadas, fContenido);
                contenido.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(contenido);
                doc.Add(contenidoFondo);

                titulo = new Paragraph("ELEMENTO INNOVADOR", fTitulo);
                doc.Add(titulo);
                contenido = new Paragraph(item.elementoInnovador, fContenido);
                contenido.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(contenido);
                doc.Add(contenidoFondo);

                titulo = new Paragraph("RESULTADOS OBTENIDOS", fTitulo);
                doc.Add(titulo);
                contenido = new Paragraph(item.resultadosObtenidos, fContenido);
                contenido.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(contenido);
                doc.Add(contenidoFondo);

                titulo = new Paragraph("OBSERVACIONES DE LA BUENA PRÁCTICA", fTitulo);
                doc.Add(titulo);
                contenido = new Paragraph(item.observacionesBP, fContenido);
                contenido.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(contenido);
                doc.Add(contenidoFondo);
            }
            doc.Add(contenidoFondo);
            doc.Close();
            writer.Close();
        }

        ///////////

            /// <summary>
            /// Valida las cookies de usuario y estado, si son correctas muesta el inicio de sesión del usuario Colaborador
            /// Hace paginación a la tabla en donde muestra las BP del usuario colaborador. 
            /// Sección donde selecciona la BP para seguir editando la BP o publicar la BP si el estado de la BP es Listo
            /// 
            /// </summary>
            /// <param name="page"></param>
            /// <returns>View Sesion</returns>
        public ActionResult Sesion(int? page)
        {
            ViewBag.menu = "Sesion";
            //Borrar cookie tituloBP
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieIdTituloBP"))
            {
                HttpCookie CookieIdTituloBP = this.ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
                CookieIdTituloBP.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(CookieIdTituloBP);
            }
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieEstadoBP"))
            {
                HttpCookie CookieEstadoBP = this.ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];
                CookieEstadoBP.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(CookieEstadoBP);
            }

            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];

            if (c1 == null || perf == null)
            {
                return RedirectToAction("Registro", "Usuario");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Colaborador")
            {
                return RedirectToAction("Registro", "Usuario");
            }
            //modelUsu.nombreUsuario = seguridad.DesEncriptar(c1.Value);
            Session["cbxEje"] = "Todos los ejes";
            modelBP.listaBP = bpBL.getBPMEstadoColaborador(seguridad.DesEncriptar(c1.Value));
            // modelBP.listaBPNoFK = bpBL.getBPNoFK("CeciMP");
            modelEje.listaEjes = ejeBL.getListaEje(true);


            int pageSize = 15;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<BPInformacion> stu = null;

            BPInformacion modelInfoBP = new BPInformacion();
            BuenasPracticasBL blinfo = new BuenasPracticasBL();
            List<BPInformacion> lista = new List<BPInformacion>();
            lista = blinfo.getBPNoFKInfo(seguridad.DesEncriptar(c1.Value));
            modelInfoBP.listaInfo = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            var tuple = new Tuple<BuenaPracticaViewModel, EjeViewModel, IPagedList<BPInformacion>>(modelBP, modelEje, stu);
            return View(tuple);




        }

        /// <summary>
        /// Sesión filtro para la palabra clave y páginación de la página
        /// </summary>
        /// <param name="page"></param>
        /// <param name="palabraClave"></param>
        /// <param name="selectEje"></param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult SesionP(int? page, string palabraClave, string selectEje)
        {
            ViewBag.menu = "Sesion";
            CargarComboNuevaBP();
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];

            if (c1 == null || perf == null)
            {
                return RedirectToAction("Registro", "Usuario");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Colaborador")
            {
                return RedirectToAction("Registro", "Usuario");
            }

            //var selectEje = Request.Form["cbxEje"];
            Session["cbxEje"] = selectEje;

            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<BPInformacion> stu = null;

            BPInformacion modelInfoBP = new BPInformacion();
            BuenasPracticasBL blinfo = new BuenasPracticasBL();
            List<BPInformacion> lista = new List<BPInformacion>();
            if (selectEje.Equals("Todos los ejes") && palabraClave.Length > 0)
                lista = blinfo.getBPInfoPC(palabraClave, seguridad.DesEncriptar(c1.Value));
            else if (selectEje.Equals("Todos los ejes") && palabraClave.Length == 0)
            {
                lista = blinfo.getBPNoFKInfo(seguridad.DesEncriptar(c1.Value));
            }
            else
            {
                lista = blinfo.getBPInfoEjePC(selectEje, palabraClave, seguridad.DesEncriptar(c1.Value));
            }
            modelInfoBP.listaInfo = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            // var tuple = new Tuple<BuenaPracticaViewModel, EjeViewModel, IPagedList<BPInformacion>>(modelBP, modelEje, stu);
            //return View(tuple);
            return PartialView("_FiltroInfoTabla", stu);

        }
        //

            /// <summary>
            /// Prueba para la paginación de tablas
            /// </summary>
            /// <param name="page"></param>
            /// <param name="palabraClave"></param>
            /// <returns></returns>
        [HttpPost]
        public JsonResult testList(int? page, string palabraClave = "")
        {
            modelBP.listaBP = bpBL.getBPMEstadoColaborador("CeciMP");
            modelEje.listaEjes = ejeBL.getListaEje(true);

            var selectEje = Request.Form["cbxEje"];
            Session["cbxEje"] = selectEje;
            Session["palabraClave"] = palabraClave;

            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<BPInformacion> stu = null;

            BPInformacion modelInfoBP = new BPInformacion();
            BuenasPracticasBL blinfo = new BuenasPracticasBL();
            List<BPInformacion> lista = new List<BPInformacion>();
            if (selectEje.Equals("Todos los ejes") && palabraClave.Length > 0)
                lista = blinfo.getBPInfoPC(palabraClave, "CeciMP");
            else if (selectEje.Equals("Todos los ejes") && palabraClave.Length == 0)
            {
                lista = blinfo.getBPNoFKInfo("CeciMP");
            }
            else
            {
                lista = blinfo.getBPInfoEjePC(selectEje, palabraClave, "CeciMP");
            }
            modelInfoBP.listaInfo = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            List<string> test = new List<string>();
            test.Add("Titulo BP CON");
            test.Add("EJE CON");
            test.Add("Archivo CON");
            test.Add("Estado CON");
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Paginación de la sesión del usuario 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult SesionPag(int? page)
        {
            CargarComboNuevaBP();
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];

            if (c1 == null || perf == null)
            {
                return RedirectToAction("Registro", "Usuario");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Colaborador")
            {
                return RedirectToAction("Registro", "Usuario");
            }
            int pageSize = 15;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<BPInformacion> stu = null;

            BPInformacion modelInfoBP = new BPInformacion();
            BuenasPracticasBL blinfo = new BuenasPracticasBL();
            List<BPInformacion> lista = new List<BPInformacion>();
            lista = blinfo.getBPNoFKInfo("CeciMP");
            modelInfoBP.listaInfo = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            //
            var tuple = new Tuple<IPagedList<BPInformacion>, EjeViewModel>(stu, modelEje);
            //  return View(stu);
            return View(tuple);
        }


        /// <summary>
        /// Sección para crear la buena práctica con sus datos generales
        /// Se validan 3 tipos de cookies
        /// 2 para permitir el acceso a la view
        /// La cookie de titulo es para saber si la vista sera utilizada para crear una nueva BP o si sera utilizada para actualizar los datos de una BP ya existente
        /// Si la vista es utilizada para la modificación de datos generales se obtiene el modelo de los datos actuales de la buena practica, estos podran ser modificados y se realizara el update a la base de datos
        /// Si es para una nueva BP se llenan los select list y se prepara la vista para ser utilizada
        /// </summary>
        /// <returns></returns>
        public ActionResult DatosGenerales()
        {
            CargarComboNuevaBP();
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];

            if (c1 == null || perf == null)
            {
                return RedirectToAction("Registro", "Usuario");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Colaborador")
            {
                return RedirectToAction("Registro", "Usuario");
            }
            if (tit != null)
            {
                int t = int.Parse(seguridad.DesEncriptar(tit.Value));
                modelBP.listaBP = bpBL.getBPXID(t);
                foreach (var item in modelBP.listaBP)
                {
                    modelBP.tituloBP = item.tituloBP;
                }
                Session["Titulo"] = modelBP.tituloBP;//Pasar a Session el nombre no el id

                modelBP.idBuenaPractica = t;
                modelBP.listaBP = bpBL.getBPXID(modelBP.idBuenaPractica);
                foreach (var item in modelBP.listaBP)
                {

                    modelEje.nombre = ejeBL.getNombreEje(item.eje);
                    modelMunicipio.nombreMunicipio = municipioBL.getNomMunicipio(item.municipio);
                    modelFuncion.nombreFunc = funcionBL.getNomFunc(item.idFuncionD);
                    modelCEO.claveCentroTrabajo = ceoBL.getCVCEO(item.idCEO);
                }

                var tuple = new Tuple<CEOViewModel, EjeViewModel, MunicipioViewModel, FuncionDesarrolloViewModel>(modelCEO, modelEje, modelMunicipio, modelFuncion);
                return View(tuple);
            }
            else {

                Session["Titulo"] = "";
                var tuple = new Tuple<CEOViewModel, EjeViewModel, MunicipioViewModel, FuncionDesarrolloViewModel>(modelCEO, modelEje, modelMunicipio, modelFuncion);
                return View(tuple);
            }

        }

        /// <summary>
        /// Método utilizado para acceder a los datos que se mostrarar en los select list
        /// </summary>
        public void CargarComboNuevaBP()
        {
            modelEje.listaEjes = ejeBL.getListaEje(true);
            modelMunicipio.listMunicipios = municipioBL.getMunicipio();
            modelFuncion.listFunciones = funcionBL.getFuncionesDesarrollo(true);
            modelCEO.listCEO = ceoBL.getCEO();
        }
        

        /// <summary>
        /// Para la actualización de los datos generales se recibe titulo, eje, municipio, CCT y función desde la vista
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="eje"></param>
        /// <param name="municipio"></param>
        /// <param name="ceo"></param>
        /// <param name="funcion"></param>
        /// <returns></returns>
        [HttpPost]
        //public ActionResult DatosGenerales(string txtTitulo, string escCCT)
        public ActionResult DatosGenerales(string titulo, string eje, string municipio, string ceo, string funcion)
        {
            CargarComboNuevaBP();
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];

            if (c1 == null || perf == null)
            {
                return RedirectToAction("Registro", "Usuario");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Colaborador")
            {
                return RedirectToAction("Registro", "Usuario");
            }

            //modelEje.idEje = ejeBL.getIdEje(Request.Form["selectEje"]);
            //modelUser.nombreUsuario = seguridad.DesEncriptar(c1.Value);  //Va el usuario colaborador
            //modelMunicipio.idMunicipio = municipioBL.idMunicipio(Request.Form["selectMunicipio"]);
            //modelCEO.idCEO = ceoBL.getIdCEO(escCCT);//Request.Form["ClaveCEO"]
            //modelFuncion.idFuncionD = funcionBL.getIdFuncion(Request.Form["funcion"]);
            modelBP.idBuenaPractica = bpBL.getIdBP(titulo);
            modelEje.idEje = ejeBL.getIdEje(eje);
            modelUser.nombreUsuario = seguridad.DesEncriptar(c1.Value);  //Va el usuario colaborador
            modelMunicipio.idMunicipio = municipioBL.idMunicipio(municipio);
            modelCEO.idCEO = ceoBL.getIdCEO(ceo);
            modelFuncion.idFuncionD = funcionBL.getIdFuncion(funcion);

            modelEje.nombre = Request.Form["selectEje"];
            modelMunicipio.nombreMunicipio = Request.Form["selectMunicipio"];
            modelFuncion.nombreFunc = Request.Form["funcion"];
            modelCEO.claveCentroTrabajo = ceo;//escCCT

            if (bpBL.agregarBP(modelEje.idEje, modelUser.nombreUsuario, modelMunicipio.idMunicipio, modelCEO.idCEO, titulo, modelFuncion.idFuncionD, "GUARDADO") == true && titulo != "")// 
            {
                //Session["tituloBP"] = titulo;
                ValorSession(titulo);//txtTitulo
                return RedirectToAction("RegistroBP");
            }
            else
            {
                var tuple = new Tuple<CEOViewModel, EjeViewModel, MunicipioViewModel, FuncionDesarrolloViewModel>(modelCEO, modelEje, modelMunicipio, modelFuncion);
                ModelState.AddModelError(string.Empty, "IDE"+modelEje.idEje + "usu"+modelUser.nombreUsuario+ "Mun"+modelMunicipio.idMunicipio+ "Ceo"+modelCEO.idCEO+ "tit"+titulo+ "fun"+modelFuncion.idFuncionD + "Error al crear la Buena práctica, pruebe con otro título.");
                return View(tuple);
            }
        }

        /// <summary>
        /// Actualiza los datos generales de la BP
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="eje"></param>
        /// <param name="municipio"></param>
        /// <param name="ceo"></param>
        /// <param name="funcion"></param>
        public void ActualizarDG(string titulo, string eje, string municipio, string ceo, string funcion) {
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            modelBP.idBuenaPractica = bpBL.getIdBP(titulo);
            modelEje.idEje = ejeBL.getIdEje(eje);
            modelUser.nombreUsuario = seguridad.DesEncriptar(c1.Value);  //Va el usuario colaborador
            modelMunicipio.idMunicipio = municipioBL.idMunicipio(municipio);
            modelCEO.idCEO = ceoBL.getIdCEO(ceo);
            modelFuncion.idFuncionD = funcionBL.getIdFuncion(funcion);

            if (bpBL.editarBP(modelBP.idBuenaPractica, modelEje.idEje, modelMunicipio.idMunicipio, modelFuncion.idFuncionD, modelCEO.idCEO) == true)
            {
                //Mensje de datos actualizados y enviar a otra vista
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al guardar los Datos Generales, vuelva a intentar.");

            }

        }

        /// <summary>
        /// Para obtener el nombre del centro de trabajo con uso de la clave de centro de trabajo
        /// Sección utilizada en la vista de datos generales de BP
        /// Al momento de escribir en el input la CCT se debe cargar automaticamente el nombre del centro de trabajo en otro input desactivado
        /// </summary>
        /// <param name="clavCEO"></param>
        /// <returns></returns>
        public string NameCEO(string clavCEO)
        {
            modelCEO.listCEO = ceoBL.getCEO(clavCEO);
            foreach (var item in modelCEO.listCEO)
            {
                modelCEO.nombreCentroTrabajo = item.nombreCentroTrabajo;
            }
            return modelCEO.nombreCentroTrabajo;
        }

        /// <summary>
        /// Prepara la vista de los datos de la buena práctica 
        /// Realiza validaciones de cookie para validar el acceso a la vista
        /// </summary>
        /// <returns></returns>
        public ActionResult RegistroBP()
        {
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];

            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            var estad = ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];

            if (c1 == null || perf == null)
            {
                //return RedirectToAction("Registro", "Usuario");
                return RedirectToAction("Practicas", "BuenasPracticas");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Colaborador")
            {
                //return RedirectToAction("Registro", "Usuario");
                return RedirectToAction("Practicas", "BuenasPracticas");

            }
            if (estad!=null && seguridad.DesEncriptar(estad.Value).Equals("LISTO"))
            {
                // Redireccionar a la ventana en la cual se elegiran las plantillas de la BP
                return RedirectToAction("Sesion", "BuenasPracticas");
            }
            if (tit == null || estad == null)
            {
                return RedirectToAction("Sesion", "BuenasPracticas");
            }

            //modelBP.idBuenaPractica = bpBL.getIdBP((string)Session["tituloBP"].ToString());

            int t = int.Parse(seguridad.DesEncriptar(tit.Value));
            modelBP.idBuenaPractica = t;
            modelBP.listaBP = bpBL.getBPXID(modelBP.idBuenaPractica);
            foreach (var item in modelBP.listaBP)
            {
                modelBP.tituloBP = item.tituloBP;
                modelBP.observacionesBP = item.observacionesBP;
                modelBP.tituloBP = item.tituloBP;
                modelBP.situacionMejora = item.situacionMejora;
                modelBP.diagnosticoRealizo = item.diagnosticoRealizo;
                modelBP.caracteristicasContexto = item.caracteristicasContexto;
                modelBP.descripcionActividadesRealizadas = item.descripcionActividadesRealizadas;
                modelBP.elementoInnovador = item.elementoInnovador;
                modelBP.resultadosObtenidos = item.resultadosObtenidos;
                modelBP.fuentesInformacion = item.fuentesInformacion;
            }
            return View(modelBP);
        }

        /// <summary>
        /// Almacena en cookies el estado de la buena práctica y su titulo
        /// </summary>
        /// <param name="tituloBP"></param>
        public void ValorSession(string tituloBP)
        {

            HttpCookie CookieIdTituloBP = new HttpCookie("CookieIdTituloBP");
            modelBP.idBuenaPractica = bpBL.getIdBP(tituloBP);
            CookieIdTituloBP.Value = seguridad.Encriptar(modelBP.idBuenaPractica.ToString());
            this.ControllerContext.HttpContext.Response.Cookies.Add(CookieIdTituloBP);
            //---
            modelBP.idBuenaPractica = bpBL.getIdBP(tituloBP);
            modelBP.listaBP = bpBL.getBPXID(modelBP.idBuenaPractica);

            foreach (var item in modelBP.listaBP)
            {
                modelBP.estado = item.estado;
            }

            HttpCookie CookieEstadoBP = new HttpCookie("CookieEstadoBP");
            CookieEstadoBP.Value = seguridad.Encriptar(modelBP.estado);
            this.ControllerContext.HttpContext.Response.Cookies.Add(CookieEstadoBP);

        }

        /// <summary>
        /// Destruye las cookies existentes hasta el momento, así que primero valida si existen
        /// </summary>
        /// <returns></returns>
        public ActionResult CerrarSesion()
        {
            //limpiar todas las cookies
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
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieIdTituloBP"))
            {
                HttpCookie CookieIdTituloBP = this.ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
                CookieIdTituloBP.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(CookieIdTituloBP);
            }
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieEstadoBP"))
            {
                HttpCookie CookieEstadoBP = this.ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];
                CookieEstadoBP.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(CookieEstadoBP);
            }

            return RedirectToAction("Practicas", "BuenasPracticas");
        }

        /// <summary>
        /// Prepara la vista para la sección de registro de autores 
        /// Obtiene al usuario responsable y todos los autores de la BP
        /// </summary>
        /// <returns></returns>
        public ActionResult Autores()
        {
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];

            if (tit == null)
            {
                return RedirectToAction("Sesion");
            }
            modelAutor.nombreA = "";
            modelAutor.pApellidoA = "";
            modelAutor.mApellidoA = "";
            modelAutor.correoElectronicoA = "";
            modelAutor.telefonoA = "";
            modelUser.listaUsuario = usuarioBL.listUsuario(seguridad.DesEncriptar(c1.Value));
            modelAutor.listAutores = autorBL.getAutores(int.Parse(seguridad.DesEncriptar(tit.Value)));
            var tuple = new Tuple<UsuarioViewModel, AutoresViewModel>(modelUser, modelAutor);
            return View(tuple);

        }

        /// <summary>
        /// Sección para agregar un nuevo autor a una BP
        /// Realiza la validación de los campos necesarios para un nuevo autor, en caso 
        /// de que todo este correcto se realiza el registro a la BD sobre el nuevo autor
        /// </summary>
        /// <param name="txtNombre"></param>
        /// <param name="txtPApellido"></param>
        /// <param name="txtSApellido"></param>
        /// <param name="txtCE"></param>
        /// <param name="txtTel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AgregarAutor(string txtNombre, string txtPApellido, string txtSApellido, string txtCE, string txtTel)
        {
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];

            Regex erTel = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            Regex erEmail = new Regex(@".+\.[a-z]{2,}");
            if (erTel.IsMatch(txtTel) == true && erEmail.IsMatch(txtCE) == true)
            {
                if (txtNombre != "" && txtPApellido != "" && txtCE != "" && txtTel != "")
                {
                    if (autorBL.agregarAutor(int.Parse(seguridad.DesEncriptar(tit.Value)), txtNombre, txtPApellido, txtSApellido, txtCE, txtTel) != true)
                    {
                        ModelState.AddModelError(string.Empty, "Error al guardar el Autor de la Buena práctica.");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Debe llenar los campos, Segundo apellido es opcional.");
                    //}
                }
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Correo electrónico o teléfono incorrecto.");
            }


            modelUser.listaUsuario = usuarioBL.listUsuario(seguridad.DesEncriptar(c1.Value));
            modelAutor.listAutores = autorBL.getAutores(int.Parse(seguridad.DesEncriptar(tit.Value)));

            var tuple = new Tuple<UsuarioViewModel, AutoresViewModel>(modelUser, modelAutor);
            //return View(tuple);
            return PartialView("_TablaAutores", tuple);
        }

        /// <summary>
        /// Sección para eliminar un autor de una BP
        /// </summary>
        /// <param name="idA"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EliminarAutor(int idA)
        {
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];

            if (autorBL.eliminarAutor(idA) == true)
            {
                //El autor fue eliminado
            }
            else
            {
                ModelState.AddModelError(string.Empty, "No se pudo eliminar el autor.");
            }

            modelUser.listaUsuario = usuarioBL.listUsuario(seguridad.DesEncriptar(c1.Value));
            modelAutor.listAutores = autorBL.getAutores(int.Parse(seguridad.DesEncriptar(tit.Value)));

            var tuple = new Tuple<UsuarioViewModel, AutoresViewModel>(modelUser, modelAutor);
            //return View(tuple);
            return PartialView("_TablaAutores", tuple);
        }

        /// <summary>
        /// Retorna un valor booleano de la existencia o no de una CCT en la BD
        /// </summary>
        /// <param name="CCT"></param>
        /// <returns></returns>
        public bool ValidarCCT(string CCT)
        {
            bool existe = false;
            modelCEO.listCEO = ceoBL.getCEO(CCT);

            if (modelCEO.listCEO.Count > 0)
            {
                existe = true;
            }

            return existe;
        }

        /// <summary>
        /// Realiza validaciones de cookie para permitir el acceso a esta página
        /// Prepara la vista con los archivos agregados referente a la BP
        /// Sección donde carga las sesiones de la BP a la cual se les puede añadir un archivo
        /// Paginación de la tabla de archivos
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Archivos(int? page)
        {
           ViewBag.EliminarArchivo = "true";
           var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
           var perfil = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
           ViewBag.perfil = seguridad.DesEncriptar(perfil.Value);

            if (tit == null && seguridad.DesEncriptar(perfil.Value)=="Colaborador")
            {
                return RedirectToAction("Sesion");
            }else if(tit == null && seguridad.DesEncriptar(perfil.Value) == "Revisor") {
                return RedirectToAction("SesionRevisor");
            }
            //
            // modelImagenBP.listImagenBP = imagenBPBL.getImagenes(int.Parse(seguridad.DesEncriptar(tit.Value)));

            IPagedList<TabImagenRevisionBP> stuRev = null;
            IPagedList<TabImagenBP> stu = null;
            if (seguridad.DesEncriptar(perfil.Value) == "Colaborador")
            {
                
                int pageSize = 10;//Cantidad de renglones que se mostraran de la tabla por pag.
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                

                TabImagenBP modelInfoBP = new TabImagenBP();
                ImagenBPBL blinfo = new ImagenBPBL();
                List<TabImagenBP> lista = new List<TabImagenBP>();
                lista = blinfo.getListaTabImagenBP(int.Parse(seguridad.DesEncriptar(tit.Value)));
                modelInfoBP.tabImagenBP = lista;


                stu = lista.ToPagedList(pageIndex, pageSize);
                modelCampoBP.listCamposBP = campoBPBL.getCampoBPArchivo(true);
                
            }
            else if (seguridad.DesEncriptar(perfil.Value) == "Revisor")
            {
                
                int pageSize = 10;//Cantidad de renglones que se mostraran de la tabla por pag.
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;


                TabImagenRevisionBP modelInfoBP = new TabImagenRevisionBP();
                ImagenRevisionBL blinfo = new ImagenRevisionBL();
                List<TabImagenRevisionBP> lista = new List<TabImagenRevisionBP>();
                lista = blinfo.getListaTabImagenBP(int.Parse(seguridad.DesEncriptar(tit.Value)));
                modelInfoBP.tabImagenRevisionBP = lista;


                stuRev = lista.ToPagedList(pageIndex, pageSize);
                modelCampoBP.listCamposBP = campoBPBL.getCamposBPSinAnexos();
                
            }


            var tuple = new Tuple<CampoBPViewModel, ImagenBPViewModel, IPagedList<TabImagenBP>, IPagedList<TabImagenRevisionBP>>(modelCampoBP, modelImagenBP, stu,stuRev);
            return View(tuple);




        }

        /// <summary>
        /// Sección para subir a un arcivo en una sección especifica de la BP
        /// En caso de ser un anexo máximo acepta 5 archivos.
        /// El archivo es subido al servior FTP y la dirección del archivo en el servidor es almacenada en un nuevo registro con todos los datos adicionales del arhcivo
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="campoBP"></param>
        /// <param name="numBP"></param>
        /// <param name="ruta"></param>
        /// <param name="titulo"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubirArchivo(int? page, string campoBP, string numBP, string ruta, string titulo, string archivo)
        {
            ViewBag.EliminarArchivo = "true";
           var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            // ModelState.AddModelError(string.Empty, "Error al guardar el Autor de la Buena práctica.");
            ///////////////////////////////////////////////////////
            //El base64 data se extrar de rutaO (URI del file seleccionado por el ussuario)
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
            else {

                //Obtener la ruta de la introducccion alamacenada en la BD
                //modelModulo.listaModulos = moduloBL.getListModulo(modulo);
                //foreach (var item in modelModulo.listaModulos)
                //{
                //    modelModulo.idModulo = item.idModulo;
                //    modelModulo.archivoIntroduccion = item.archivoIntroduccion;
                //}
                string rutaDestinoFTP = "ArchivosBP\\" + seguridad.DesEncriptar(tit.Value) + campoBP + titulo + extension;

                //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
                //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.69", "SEG\\mc_mendoza", "M2pm2019");

                //con.delete(modelModulo.archivoIntroduccion);

                //   con.upload(rutaDestinoFTP, fileNameX2);

                modelCampoBP.listCamposBP = campoBPBL.getCamposBP(campoBP);
                foreach (var item in modelCampoBP.listCamposBP)
                {
                    if (item.nombre == campoBP)
                    {

                        modelCampoBP.idCampo = item.idCampo;
                    }
                }

                // poner aqui una instruccion para el contador solo sobre anexos.

                if (campoBP == "Anexos" && imagenBPBL.contarArchivos(int.Parse(seguridad.DesEncriptar(tit.Value)), "Anexos") >= 5)
                {
                    ModelState.AddModelError(string.Empty, "Máximo 5 Anexos.");
                }
                else
                {
                    //Validar la conexion al server FTP
                    if (con.conexionFTP() == true)
                    {
                        if (imagenBPBL.agregarImagen(int.Parse(seguridad.DesEncriptar(tit.Value)), modelCampoBP.idCampo, rutaDestinoFTP, titulo, numBP) == false)
                        {
                            ModelState.AddModelError(string.Empty, "El número de la imagen no puede ser repetido.");
                        }
                        else
                        {
                            con.upload(rutaDestinoFTP, fileNameX2);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Por el momento no se pueden subir archivos.");
                    }



                }

                //moduloBL.editarArchivo(modulo, rutaDestinoFTP);

                ///////////////////////////////////////////////////////
            }

            //Actualizar contenido de la tabla
            int pageSize = 10;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<TabImagenBP> stu = null;

            TabImagenBP modelInfoBP = new TabImagenBP();
            ImagenBPBL blinfo = new ImagenBPBL();
            List<TabImagenBP> lista = new List<TabImagenBP>();
            lista = blinfo.getListaTabImagenBP(int.Parse(seguridad.DesEncriptar(tit.Value)));
            modelInfoBP.tabImagenBP = lista;


            stu = lista.ToPagedList(pageIndex, pageSize);



            //
            //modelCampoBP.listCamposBP = campoBPBL.getCampoBPArchivo(true);
            //var tuple = new Tuple<CampoBPViewModel, ImagenBPViewModel>(modelCampoBP, modelImagenBP);
            return PartialView("_TablaArchivos", stu);

            //return exito;
        }

        /// <summary>
        /// Elimina un archivo de una sección de la BP y actualiza la tabla paginada con todos los archivos de la BP
        /// </summary>
        /// <param name="page"></param>
        /// <param name="idImg"></param>
        /// <returns></returns>
        public ActionResult EliminarArchivo(int? page, int idImg)
        {
            ViewBag.EliminarArchivo = "true";

            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            if (tit == null)
            {
                return RedirectToAction("Sesion");
            }

            modelImagenBP.listImagenBP = imagenBPBL.getImagenesXID(idImg);
            if (imagenBPBL.eliminarImagen(idImg) == true)
            {


                foreach (var item in modelImagenBP.listImagenBP)
                {
                    //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");

                    con.delete(item.imagen);
                }


            }
            else
            {
                //no se pudo eliminar la imagen 
            }
            int pageSize = 10;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<TabImagenBP> stu = null;

            TabImagenBP modelInfoBP = new TabImagenBP();
            ImagenBPBL blinfo = new ImagenBPBL();
            List<TabImagenBP> lista = new List<TabImagenBP>();
            lista = blinfo.getListaTabImagenBP(int.Parse(seguridad.DesEncriptar(tit.Value)));
            modelInfoBP.tabImagenBP = lista;


            stu = lista.ToPagedList(pageIndex, pageSize);
            return PartialView("_TablaArchivos", stu);

        }

        /// <summary>
        /// Sección que deja visualizar el archivo almacenado en el servidor FTP con el uso del ID del archivo
        /// </summary>
        /// <param name="idArchivo"></param>
        /// <returns></returns>
        public ActionResult VistaPreviaArchivo(int idArchivo)
        {
            //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
            string ruta = "";
            modelImagenBP.listImagenBP = imagenBPBL.getImagenesXID(idArchivo);
            foreach (var item in modelImagenBP.listImagenBP)
            {

                ruta = item.imagen;
            }
            //ruta=con.downloadVistaPrevia("flor.jpg");
            return RedirectToAction("Index", "VisualizacionPrevia", new { archivo = ruta });
        }

        /// <summary>
        /// Sección que permite visualizar la vista previa de los archivos de introducción con el uso del idModulo
        /// </summary>
        /// <param name="idModulo"></param>
        /// <returns></returns>
        public ActionResult VistaPreviaIntroduccion(string idModulo)
        {
            string rutaIn = "";
            modelModulo.listaModulos = moduloBL.getListModulo(idModulo);

            foreach (var item in modelModulo.listaModulos)
            {
                rutaIn = item.archivoIntroduccion;
            }

            //return RedirectToAction("Index", "VisualizacionPrevia", new { archivo = rutaIn });
            string nombre = Path.GetFileName(rutaIn);
            byte[] filedata = System.IO.File.ReadAllBytes(con.downloadVistaPrevia(rutaIn));
            return File(filedata, System.Net.Mime.MediaTypeNames.Application.Octet, "Introduccion_"+nombre);
        }

        /// <summary>
        /// Obtiene el perfil de usuario almacenado en la cookie
        /// </summary>
        /// <returns></returns>
        public string PerfilUsuario()
        {
            string perfilU = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookiePerfilU"))
            {
                var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];
                perfilU = seguridad.DesEncriptar(perf.Value);
            }
            return perfilU;
        }

        /// <summary>
        /// Prepara la vista del Home de practicas 
        /// Obtiene las imagenes del banner del modulo BP
        /// Obtiene todas las BP con estado publicadas, crea páginación
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Practicas(int? page)
        {
            ViewBag.menu = "BP";
            modelBP.listaImagenes = banerBL.getModuloImagenes("01000000");

            //var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            // var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];

            // if (c1 == null || perf == null)
            // {
            //     return RedirectToAction("Registro", "Usuario");
            // }
            // if (seguridad.DesEncriptar(perf.Value) != "Colaborador")
            // {
            //     return RedirectToAction("Registro", "Usuario");
            // }

            //modelUsu.nombreUsuario = seguridad.DesEncriptar(c1.Value);
            Session["cbxEje"] = "Todos los ejes";
            //modelBP.listaBP = bpBL.getBPMEstadoColaborador(seguridad.DesEncriptar(c1.Value));
            // modelBP.listaBPNoFK = bpBL.getBPNoFK("CeciMP");
            modelEje.listaEjes = ejeBL.getListaEje(true);


            int pageSize = 25;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<BPHomeTable> stu = null;

            BPHomeTable modelHomeBP = new BPHomeTable();
            BuenasPracticasBL blinfo = new BuenasPracticasBL();
            List<BPHomeTable> lista = new List<BPHomeTable>();
            lista = blinfo.getListHomeBP();
            modelHomeBP.listaHomeBP = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);


            var tuple = new Tuple<UsuarioViewModel, IPagedList<BPHomeTable>, EjeViewModel, BuenaPracticaViewModel>(modelUser, stu, modelEje, modelBP);
            return View(tuple);



        }

        /// <summary>
        /// Paginación de la tabla con las BP publicadas 
        /// Acepta filtros por palabra clave o eje
        /// Carga banner
        /// </summary>
        /// <param name="page"></param>
        /// <param name="palabraClave"></param>
        /// <param name="selectEje"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PracticasP(int? page, string palabraClave, string selectEje)
        {
            ViewBag.menu = "BP";
            modelEje.listaEjes = ejeBL.getListaEje(true);



            //var selectEje = Request.Form["cbxEje"];
            Session["cbxEje"] = selectEje;

            int pageSize = 25;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<BPHomeTable> stu = null;

            BPHomeTable modelInfoBP = new BPHomeTable();
            BuenasPracticasBL blinfo = new BuenasPracticasBL();
            List<BPHomeTable> lista = new List<BPHomeTable>();

            if (selectEje.Equals("Todos los ejes") && palabraClave.Length > 0)
                lista = blinfo.getListHomeBP(palabraClave);
            else if (selectEje.Equals("Todos los ejes") && palabraClave.Length == 0)
            {
                lista = blinfo.getListHomeBP();
            }
            else
            {
                lista = blinfo.getListHomeBP(palabraClave, selectEje);
            }
            modelInfoBP.listaHomeBP = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            // var tuple = new Tuple<BuenaPracticaViewModel, EjeViewModel, IPagedList<BPInformacion>>(modelBP, modelEje, stu);
            //return View(tuple);
            return PartialView("_TablaHomeBP", stu);

        }
        
        /// <summary>
        /// Prepara vista para la sesión de los usuarios Revisores, es similar a la de los usuarios colaborador
        /// Obtiene tabla paginada con todas las BP asignadas al revisor
        /// Sección donde muetra las BP que puede comentar
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult SesionRevisor(int? page)
        {
            ViewBag.menu = "SesionRevisor";
            //Borrar cookie tituloBP
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieIdTituloBP"))
            {
                HttpCookie CookieIdTituloBP = this.ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
                CookieIdTituloBP.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(CookieIdTituloBP);
            }
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieEstadoBP"))
            {
                HttpCookie CookieEstadoBP = this.ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];
                CookieEstadoBP.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(CookieEstadoBP);
            }

            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];

            if (c1 == null || perf == null)
            {
                return RedirectToAction("Registro", "Usuario");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Revisor")
            {
                return RedirectToAction("Practicas", "BuenasPracticas");
            }
            //modelUsu.nombreUsuario = seguridad.DesEncriptar(c1.Value);
            //Session["cbxEje"] = "Todos los ejes";
            //modelBP.listaBP = bpBL.getBPMEstadoColaborador(seguridad.DesEncriptar(c1.Value));
            // modelBP.listaBPNoFK = bpBL.getBPNoFK("CeciMP");
            //modelEje.listaEjes = ejeBL.getListaEje(true);
            modelBP.listaBP = bpBL.getRevisionBP(seguridad.DesEncriptar(c1.Value));

            int pageSize = 15;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<BPRevisionTable> stu = null;

            BPRevisionTable modelInfoReviBP = new BPRevisionTable();
            RevisionBPBL blRevi = new RevisionBPBL();
            List<BPRevisionTable> lista = new List<BPRevisionTable>();
            lista = blRevi.getTablaRevision(seguridad.DesEncriptar(c1.Value));
            modelInfoReviBP.listaTableRevision = lista;

            stu = lista.ToPagedList(pageIndex, pageSize);

            var tuple = new Tuple<BuenaPracticaViewModel, IPagedList<BPRevisionTable>>(modelBP, stu);
            return View(tuple);

        }

        /// <summary>
        /// Paginación de tabla con BP asignadas al usuario reviso
        /// Filtro por palabra clave
        /// Validaciones de cookies
        /// </summary>
        /// <param name="page"></param>
        /// <param name="palabraClave"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SesionRevisorP(int? page, string palabraClave)
        {
            ViewBag.menu = "SesionRevisor";
            //Borrar cookie tituloBP
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieIdTituloBP"))
            {
                HttpCookie CookieIdTituloBP = this.ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
                CookieIdTituloBP.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(CookieIdTituloBP);
            }
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieEstadoBP"))
            {
                HttpCookie CookieEstadoBP = this.ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];
                CookieEstadoBP.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(CookieEstadoBP);
            }

            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];

            if (c1 == null || perf == null)
            {
                return RedirectToAction("Registro", "Usuario");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Revisor")
            {
                return RedirectToAction("Practicas", "BuenasPracticas");
            }
            //modelUsu.nombreUsuario = seguridad.DesEncriptar(c1.Value);
            //Session["cbxEje"] = "Todos los ejes";
            //modelBP.listaBP = bpBL.getBPMEstadoColaborador(seguridad.DesEncriptar(c1.Value));
            // modelBP.listaBPNoFK = bpBL.getBPNoFK("CeciMP");
            //modelEje.listaEjes = ejeBL.getListaEje(true);


            int pageSize = 15;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<BPRevisionTable> stu = null;

            BPRevisionTable modelInfoReviBP = new BPRevisionTable();
            RevisionBPBL blRevi = new RevisionBPBL();
            List<BPRevisionTable> lista = new List<BPRevisionTable>();

           // lista = blRevi.getTablaRevision(seguridad.DesEncriptar(c1.Value));
            lista = blRevi.getPCReviBP(palabraClave, seguridad.DesEncriptar(c1.Value));
            modelInfoReviBP.listaTableRevision = lista;


            stu = lista.ToPagedList(pageIndex, pageSize);

            //var tuple = new Tuple<BuenaPracticaViewModel, IPagedList<BPRevisionTable>>(modelBP, stu);
            //return View(tuple);

            return PartialView("_TablaSesionRevisor", stu);
            
        }

        /// <summary>
        /// Prepara vista para el usuario Revisor en donde se obtiene todo el contenido de la BP
        /// los comentarios por parte del revisor, si es que existen y la paginación en tabla de todas 
        /// las imagenes subidas a la BP
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult RevisionBP(int?page)
        {
            ViewBag.EliminarArchivo = "false";
            var c1 = ControllerContext.HttpContext.Request.Cookies["CookieUsuario"];
            var perf = ControllerContext.HttpContext.Request.Cookies["CookiePerfilU"];

            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            var estad = ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];

            if (c1 == null || perf == null)
            {
                //return RedirectToAction("Registro", "Usuario");
                return RedirectToAction("Practicas", "BuenasPracticas");
            }
            if (seguridad.DesEncriptar(perf.Value) != "Revisor")
            {
                //return RedirectToAction("Registro", "Usuario");
                return RedirectToAction("Practicas", "BuenasPracticas");

            }
            if (tit == null || estad == null)
            {
                return RedirectToAction("SesionRevisor", "BuenasPracticas");
            }
            if (seguridad.DesEncriptar(estad.Value).Equals("LISTO"))
            {
                // Redireccionar a la ventana en la cual se elegiran las plantillas de la BP
                return RedirectToAction("SesionRevisor", "BuenasPracticas");
            }
            

            //modelBP.idBuenaPractica = bpBL.getIdBP((string)Session["tituloBP"].ToString());

            int t = int.Parse(seguridad.DesEncriptar(tit.Value));
            modelBP.idBuenaPractica = t;
            modelBP.listaBP = bpBL.getBPXID(modelBP.idBuenaPractica);
            foreach (var item in modelBP.listaBP)
            {
                modelBP.tituloBP = item.tituloBP;
                modelBP.observacionesBP = item.observacionesBP;
                modelBP.tituloBP = item.tituloBP;
                modelBP.situacionMejora = item.situacionMejora;
                modelBP.diagnosticoRealizo = item.diagnosticoRealizo;
                modelBP.caracteristicasContexto = item.caracteristicasContexto;
                modelBP.descripcionActividadesRealizadas = item.descripcionActividadesRealizadas;
                modelBP.elementoInnovador = item.elementoInnovador;
                modelBP.resultadosObtenidos = item.resultadosObtenidos;
                modelBP.fuentesInformacion = item.fuentesInformacion;
            }
            modelBP.datosGeneralesBP = bpBL.DatosGeneralesBP(modelBP.idBuenaPractica);
            modelBP.autorResponsableBP = bpBL.AutorResponsableBP(modelBP.idBuenaPractica);
            modelBP.autoresBP = bpBL.AutoresBP(modelBP.idBuenaPractica);

            modelRevision.listRevision=revisionBL.getRevisiones(modelBP.idBuenaPractica);
            foreach(var item in modelRevision.listRevision)
            {
                modelRevision.comentarioTituloBP = item.comentarioTituloBP;
                modelRevision.comentarioCaracteristicasContexto = item.comentarioCaracteristicasContexto;
                modelRevision.comentarioDescripcionActividadesRealizadas = item.comentarioDescripcionActividadesRealizadas;
                modelRevision.comentarioDiagnosticoRealizo = item.comentarioDiagnosticoRealizo;
                modelRevision.comentarioElementoInnovador = item.comentarioElementoInnovador;
                modelRevision.comentarioFuenteInformacion = item.comentarioFuenteInformacion;
                modelRevision.comentarioGeneral = item.comentarioGeneral;
                modelRevision.comentarioObservacionesBP = item.comentarioObservacionesBP;
                modelRevision.comentarioResultadosObtenidos = item.comentarioResultadosObtenidos;
                modelRevision.comentarioSituacionMejora = item.comentarioSituacionMejora;
            }

            int pageSize = 10;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<TabImagenBP> stu = null;

            TabImagenBP modelInfoBP = new TabImagenBP();
            ImagenBPBL blinfo = new ImagenBPBL();
            List<TabImagenBP> lista = new List<TabImagenBP>();
            lista = blinfo.getListaTabImagenBP(int.Parse(seguridad.DesEncriptar(tit.Value)));
            modelInfoBP.tabImagenBP = lista;


            stu = lista.ToPagedList(pageIndex, pageSize);

            var tuple = new Tuple<BuenaPracticaViewModel,RevisionBPViewModel, IPagedList<TabImagenBP>>(modelBP,modelRevision,stu);
            return View(tuple);
        }

        /// <summary>
        /// Actualización de los comentarios a la BP
        /// </summary>
        /// <param name="cTitulo"></param>
        /// <param name="cSituacion"></param>
        /// <param name="cDiagnostico"></param>
        /// <param name="cCaracteristicas"></param>
        /// <param name="cActividadesR"></param>
        /// <param name="cInnovador"></param>
        /// <param name="cResultadosObt"></param>
        /// <param name="cObservaciones"></param>
        /// <param name="cFuentesInf"></param>
        /// <param name="cGenerales"></param>
        /// <returns></returns>
        public bool GuardarRevisionBP(string cTitulo, string cSituacion, string cDiagnostico, string cCaracteristicas, string cActividadesR, string cInnovador, string cResultadosObt, string cObservaciones, string cFuentesInf, string cGenerales)
        {
            bool guardo = false;
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            var estad = ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];
           
                
                //modelBP.idBuenaPractica = bpBL.getIdBP((string)Session["tituloBP"].ToString());

                int t = int.Parse(seguridad.DesEncriptar(tit.Value));
                modelBP.idBuenaPractica = t;

                //if (seguridad.DesEncriptar(estad.Value) != "GUARDADO")
                //{
                //    eBP = seguridad.DesEncriptar(estad.Value);
                //}

                if (revisionBL.editarRev(modelBP.idBuenaPractica, cTitulo, cSituacion, cDiagnostico, cCaracteristicas, cActividadesR, cInnovador, cResultadosObt, cObservaciones, cFuentesInf, cGenerales) == true)
                {
                    //Enviar mensaje de comentarios de revision guardados
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al guardar los comentarios.");
                }
                guardo = true;
            
            return guardo;

        }

        /// <summary>
        /// Vista de impresion de un pdf din estilo 
        /// Carga todo los datos de la BP para la visualización o conjunta de todos los elementos
        /// con los comentarios por parte del revisor
        /// Si el usuario es revisor solo accedera al contenido de la BP y si el usuario es colaborador 
        /// podra acceder solo si tiene correcciones que hacer y se le mostraran los comentarios por parte del revisor
        /// </summary>
        /// <returns></returns>
        public ActionResult PDF_sinEstilo()
        {
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            var estad = ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];
            

            int t = int.Parse(seguridad.DesEncriptar(tit.Value));
            modelBP.idBuenaPractica = t;

            modelBP.datosGeneralesBP = bpBL.DatosGeneralesBP(modelBP.idBuenaPractica);
            modelBP.autorResponsableBP = bpBL.AutorResponsableBP(modelBP.idBuenaPractica);
            modelBP.autoresBP = bpBL.AutoresBP(modelBP.idBuenaPractica);

            if (PerfilUsuario() == "Revisor") {
                ViewBag.perfil = "Revisor";
                modelBP.listaBP = bpBL.getBPXID(modelBP.idBuenaPractica);
                foreach (var item in modelBP.listaBP)
                {
                    modelBP.tituloBP = item.tituloBP;
                    modelBP.observacionesBP = item.observacionesBP;
                    modelBP.tituloBP = item.tituloBP;
                    modelBP.situacionMejora = item.situacionMejora;
                    modelBP.diagnosticoRealizo = item.diagnosticoRealizo;
                    modelBP.caracteristicasContexto = item.caracteristicasContexto;
                    modelBP.descripcionActividadesRealizadas = item.descripcionActividadesRealizadas;
                    modelBP.elementoInnovador = item.elementoInnovador;
                    modelBP.resultadosObtenidos = item.resultadosObtenidos;
                    modelBP.fuentesInformacion = item.fuentesInformacion;
                }

                

                modelImagenBP.listArvhivoSMT = imagenBPBL.getImagenes(modelBP.idBuenaPractica, 2);
                modelImagenBP.listArvhivoDR = imagenBPBL.getImagenes(modelBP.idBuenaPractica, 3);
                modelImagenBP.listArvhivoCC = imagenBPBL.getImagenes(modelBP.idBuenaPractica, 4);
                modelImagenBP.listArvhivoDAR = imagenBPBL.getImagenes(modelBP.idBuenaPractica, 5);
                modelImagenBP.listArvhivoEI = imagenBPBL.getImagenes(modelBP.idBuenaPractica, 6);
                modelImagenBP.listArvhivoRO = imagenBPBL.getImagenes(modelBP.idBuenaPractica, 7);
                modelImagenBP.listArvhivoA = imagenBPBL.getImagenes(modelBP.idBuenaPractica, 10);

            }
            else
            {
                ViewBag.perfil = "Colaborador";
                modelRevision.listRevision = revisionBL.getRevisionXid(modelBP.idBuenaPractica);
                foreach(var item in modelRevision.listRevision)
                {
                    modelRevision.idRevision = item.idRevision;
                    modelRevision.comentarioTituloBP = item.comentarioTituloBP;
                    modelRevision.comentarioSituacionMejora = item.comentarioSituacionMejora;
                    modelRevision.comentarioDiagnosticoRealizo = item.comentarioDiagnosticoRealizo;
                    modelRevision.comentarioCaracteristicasContexto = item.comentarioCaracteristicasContexto;
                    modelRevision.comentarioDescripcionActividadesRealizadas = item.comentarioDescripcionActividadesRealizadas;
                    modelRevision.comentarioElementoInnovador = item.comentarioElementoInnovador;
                    modelRevision.comentarioResultadosObtenidos = item.comentarioResultadosObtenidos;
                    modelRevision.comentarioObservacionesBP = item.comentarioObservacionesBP;
                    modelRevision.comentarioFuenteInformacion = item.comentarioFuenteInformacion;
                    modelRevision.comentarioGeneral = item.comentarioGeneral;
                }       

                modelImgRevision.listArchivoTitulo = imagenRevisionBL.getImagenRevision2(modelRevision.idRevision, 1);
                modelImgRevision.listArvhivoCSM = imagenRevisionBL.getImagenRevision2(modelRevision.idRevision, 2);
                modelImgRevision.listArvhivoCDR = imagenRevisionBL.getImagenRevision2(modelRevision.idRevision, 3);
                modelImgRevision.listArvhivoCCC = imagenRevisionBL.getImagenRevision2(modelRevision.idRevision, 4);
                modelImgRevision.listArvhivoCDAR = imagenRevisionBL.getImagenRevision2(modelRevision.idRevision, 5);
                modelImgRevision.listArvhivoCEI = imagenRevisionBL.getImagenRevision2(modelRevision.idRevision, 6);
                modelImgRevision.listArvhivoCRO = imagenRevisionBL.getImagenRevision2(modelRevision.idRevision, 7);
                modelImgRevision.listArvhivoCO = imagenRevisionBL.getImagenRevision2(modelRevision.idRevision, 8);
                modelImgRevision.listArvhivoCFI = imagenRevisionBL.getImagenRevision2(modelRevision.idRevision, 9);
                

            }


            var tuple = new Tuple<BuenaPracticaViewModel, ImagenBPViewModel,RevisionBPViewModel,ImagenRevisionViewModel>(modelBP, modelImagenBP,modelRevision,modelImgRevision);
            return View(tuple);
        }

        /// <summary>
        /// Retorna un string si el envio de correcciones fue satisfactorio o ocurrio un error
        /// Actualiza el estado de la BP y los comentarios por parte del revisor
        /// Envia correo al usuario colaborador para notificar de BP pendientes
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="situacion"></param>
        /// <param name="diagnostico"></param>
        /// <param name="caracteristicas"></param>
        /// <param name="actividadesR"></param>
        /// <param name="innovador"></param>
        /// <param name="resultadosObt"></param>
        /// <param name="observaciones"></param>
        /// <param name="fuentesInf"></param>
        /// <param name="cGenerales"></param>
        /// <returns></returns>
        public string EnviarCorreccion(string titulo, string situacion, string diagnostico, string caracteristicas, string actividadesR, string innovador, string resultadosObt, string observaciones, string fuentesInf, string cGenerales)
        {
            string envio = "true";
            //Primero enviar correo, luego acturalizar, estado == EN revision
            //modelBP.idBuenaPractica = bpBL.getIdBP((string)Session["tituloBP"].ToString());

            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];

            int t = int.Parse(seguridad.DesEncriptar(tit.Value));

            modelBP.idBuenaPractica = t;





            if (revisionBL.editarRev(modelBP.idBuenaPractica, titulo, situacion, diagnostico, caracteristicas, actividadesR, innovador, resultadosObt, observaciones, fuentesInf, cGenerales) == true)
            {
                if (bpBL.editarEstadoBP(modelBP.idBuenaPractica, "CORRIGIENDO") == true)
                {
                    modelBP.listaBP = bpBL.getBPXID(modelBP.idBuenaPractica);
                    foreach (var item in modelBP.listaBP)
                    {
                        modelBP.tituloBP = item.tituloBP;
                        modelUser.listaUsuario = usuarioBL.listUsuario(item.usuarioColaborador);
                        foreach (var item2 in modelUser.listaUsuario)
                        {

                            modelUser.correoElectronico = item2.correoElectronico;
                        }
                    }

                    enviarCorreo.EnviarEmail("CorregirBP", modelBP.tituloBP, modelUser.correoElectronico);

                    //Seccion para limpiar la cookie de nombre titulo
                    if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieIdTituloBP"))
                    {
                        HttpCookie CookieIdTituloBP = this.ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
                        CookieIdTituloBP.Expires = DateTime.Now.AddDays(-1);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(CookieIdTituloBP);
                    }

                    if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("CookieEstadoBP"))
                    {
                        HttpCookie CookieEstadoBP = this.ControllerContext.HttpContext.Request.Cookies["CookieEstadoBP"];
                        CookieEstadoBP.Expires = DateTime.Now.AddDays(-1);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(CookieEstadoBP);
                    }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar el estado de la BP.");
            }



        }
            return envio;
        }

        /// <summary>
        /// Sube archivos para los comentarios referentes a las secciones de la BP 
        /// Subida al servidor FTP y se almacenan los datos en ls BD
        /// </summary>
        /// <param name="page"></param>
        /// <param name="campoBP"></param>
        /// <param name="numBP"></param>
        /// <param name="ruta"></param>
        /// <param name="titulo"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubirArchivoRevision(int? page, string campoBP, string numBP, string ruta, string titulo, string archivo)
        {
            ViewBag.EliminarArchivo = "true";
            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            int idRev = revisionBL.getIdRevisionXIdBP(int.Parse(seguridad.DesEncriptar(tit.Value)));

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

                //Obtener la ruta de la introducccion alamacenada en la BD
                //modelModulo.listaModulos = moduloBL.getListModulo(modulo);
                //foreach (var item in modelModulo.listaModulos)
                //{
                //    modelModulo.idModulo = item.idModulo;
                //    modelModulo.archivoIntroduccion = item.archivoIntroduccion;
                //}
                string rutaDestinoFTP = "ArchivosBPRevision\\" + seguridad.DesEncriptar(tit.Value) + campoBP + titulo + extension;

                //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
                //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.69", "SEG\\mc_mendoza", "M2pm2019");

                //con.delete(modelModulo.archivoIntroduccion);

                //   con.upload(rutaDestinoFTP, fileNameX2);

                modelCampoBP.listCamposBP = campoBPBL.getCamposBP(campoBP);
                foreach (var item in modelCampoBP.listCamposBP)
                {
                    if (item.nombre == campoBP)
                    {

                        modelCampoBP.idCampo = item.idCampo;
                    }
                }

                // poner aqui una instruccion para el contador solo sobre anexos.

                
                    //Validar la conexion al server FTP
                    if (con.conexionFTP() == true)
                    {
                        if (imagenRevisionBL.agregarImagen(idRev, modelCampoBP.idCampo, rutaDestinoFTP, titulo, numBP) == false)
                        {
                            ModelState.AddModelError(string.Empty, "El número de la imagen no puede ser repetido.");
                        }
                        else
                        {
                            con.upload(rutaDestinoFTP, fileNameX2);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Por el momento no se pueden subir archivos.");
                    }



                

                //moduloBL.editarArchivo(modulo, rutaDestinoFTP);

                ///////////////////////////////////////////////////////
            }

            //Actualizar contenido de la tabla
            IPagedList<TabImagenRevisionBP> stuRev = null;
            int pageSize = 10;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;


            TabImagenRevisionBP modelInfoBP = new TabImagenRevisionBP();
            ImagenRevisionBL blinfo = new ImagenRevisionBL();
            List<TabImagenRevisionBP> lista = new List<TabImagenRevisionBP>();
            lista = blinfo.getListaTabImagenBP(int.Parse(seguridad.DesEncriptar(tit.Value)));
            modelInfoBP.tabImagenRevisionBP = lista;


            stuRev = lista.ToPagedList(pageIndex, pageSize);



            //
            //modelCampoBP.listCamposBP = campoBPBL.getCampoBPArchivo(true);
            //var tuple = new Tuple<CampoBPViewModel, ImagenBPViewModel>(modelCampoBP, modelImagenBP);
            return PartialView("_TablaArchivosRevisor", stuRev);

            //return exito;
        }

        /// <summary>
        /// Vista previa de los archivos del revisor 
        /// Accede a la dirección en la base de datos por el idArchivo 
        /// </summary>
        /// <param name="idArchivo"></param>
        /// <returns></returns>
        public ActionResult VistaPreviaArchivoRevisor(int idArchivo)
        {
            //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
            string ruta = "";
            modelImgRevision.listImgenRevi = imagenRevisionBL.getImagenesXId(idArchivo);
            foreach (var item in modelImgRevision.listImgenRevi)
            {

                ruta = item.imagen;
            }
            //ruta=con.downloadVistaPrevia("flor.jpg");
            return RedirectToAction("Index", "VisualizacionPrevia", new { archivo = ruta });
        }

        /// <summary>
        /// Elimina archivos del servidor FTP y su registro en la BD de un archivo subido por el usuario revisor
        /// </summary>
        /// <param name="page"></param>
        /// <param name="idImg"></param>
        /// <returns></returns>
        public ActionResult EliminarArchivoRevision(int? page, int idImg)
        {
            ViewBag.EliminarArchivo = "true";

            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            if (tit == null)
            {
                return RedirectToAction("SesionRegistro");
            }

            modelImgRevision.listImgenRevi = imagenRevisionBL.getImagenesXId(idImg);
            if (imagenRevisionBL.eliminarImagen(idImg) == true)
            {


                foreach (var item in modelImgRevision.listImgenRevi)
                {
                    //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");

                    con.delete(item.imagen);
                }


            }
            else
            {
                //no se pudo eliminar la imagen 
            }

            IPagedList<TabImagenRevisionBP> stuRev = null;
            int pageSize = 10;//Cantidad de renglones que se mostraran de la tabla por pag.
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;


            TabImagenRevisionBP modelInfoBP = new TabImagenRevisionBP();
            ImagenRevisionBL blinfo = new ImagenRevisionBL();
            List<TabImagenRevisionBP> lista = new List<TabImagenRevisionBP>();
            lista = blinfo.getListaTabImagenBP(int.Parse(seguridad.DesEncriptar(tit.Value)));
            modelInfoBP.tabImagenRevisionBP = lista;


            stuRev = lista.ToPagedList(pageIndex, pageSize);

            return PartialView("_TablaArchivosRevisor", stuRev);

        }

        /// <summary>
        /// Cuando la BP esta validada para publicar se le notifica al usuario colaborador sobre proceder con la publicación de su 
        /// BP por medio de un email y se actualiza el estado de la BP
        /// </summary>
        /// <returns></returns>
        public string EnviarListaParaPublicar()
        {
            string enviado = "false";

            var tit = ControllerContext.HttpContext.Request.Cookies["CookieIdTituloBP"];
            int t = int.Parse(seguridad.DesEncriptar(tit.Value));
            modelBP.idBuenaPractica = t;
            //modelImgRevision.listImgenRevi = imagenRevisionBL.getImagenesXId(idImg);
            //bpBL.editarEstadoBP(modelBP.idBuenaPractica, "LISTO") == true && 
            if (bpBL.editarEstadoBP(modelBP.idBuenaPractica, "LISTO") == true && revisionBL.editarFechaListPublicacion(modelBP.idBuenaPractica,DateTime.Now)== true)
            {
                bpBL.editarEstadoBP(modelBP.idBuenaPractica, "LISTO");
                enviado = "true";
                //enviar mensaje 
                modelBP.listaBP = bpBL.getBPXID(modelBP.idBuenaPractica);
                foreach (var item in modelBP.listaBP)
                {
                    modelBP.tituloBP = item.tituloBP;
                    modelUser.listaUsuario = usuarioBL.listUsuario(item.usuarioColaborador);
                    foreach (var item2 in modelUser.listaUsuario)
                    {
                        
                        modelUser.correoElectronico = item2.correoElectronico;
                    }
                }

                enviarCorreo.EnviarEmail("ListaBP", modelBP.tituloBP, modelUser.correoElectronico);

                //Eliminar archivos del server
                
                modelImgRevision.tabImagenRevisionBP = imagenRevisionBL.getListaTabImagenBP(modelBP.idBuenaPractica);
                

                    foreach (var item in modelImgRevision.tabImagenRevisionBP)
                    {
                    
                    modelImgRevision.listImgenRevi = imagenRevisionBL.getImagenesXId(item.idImagen);
                        foreach(var item2 in modelImgRevision.listImgenRevi)
                        {
                            con.delete(item2.imagen);
                        }
                    imagenRevisionBL.eliminarImagen(item.idImagen);

                }
             }

                return enviado;
        }
    }

}