using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using BusinessModel.Business.Global;
using SeccionAdministrador.Models;
using SeccionAdministrador.Helpers;
using System.IO;

namespace SeccionAdministrador.Controllers
{
    /// <summary>
    /// Clases con ActionResult de ejemplos, no utilizados en el proyecto
    /// </summary>
    public class ModuloController : Controller
    {
        ModuloViewModel modelModulo = new ModuloViewModel();
        ModuloBL moduloBL = new ModuloBL();


        ImagenModuloBannerViewModel modelImagen = new ImagenModuloBannerViewModel();
        ImagenModuloBannerBL imagenBL = new ImagenModuloBannerBL();

       
        [HttpPost]
        public ActionResult SendImage(HttpPostedFileBase img)
        {
            var data = new byte[img.ContentLength];
            img.InputStream.Read(data, 0, img.ContentLength);
            var path = ControllerContext.HttpContext.Server.MapPath("C:\\Users\\CeciMP\\Desktop");
            var filename = Path.Combine(path, Path.GetFileName(img.FileName));
            System.IO.File.WriteAllBytes(Path.Combine(path, filename), data);
            ViewBag.ImageUploaded = filename;
            return View("Index");
        }
        public ActionResult Index()
        {
            
            //
            var tuple = new Tuple<ModuloViewModel, ImagenModuloBannerViewModel>(modelModulo, modelImagen);
            //
            string itemSe = (string)Session["itemSelect"].ToString();
            modelModulo.soloModulos = moduloBL.getSoloModulos();
            if (itemSe.Length==0)
            {
                modelModulo.listaModulos = moduloBL.getListModulo(modelModulo.soloModulos.First());
                //Obtener el ID del modulo 
                foreach(var item in modelModulo.listaModulos) { modelModulo.idModulo = item.idModulo; }
                modelImagen.listaImagenes = imagenBL.getModuloImagenes(modelModulo.idModulo);
            }
            else
            {
               modelModulo.listaModulos = moduloBL.getListModulo((string)Session["itemSelect"].ToString());
                //
                foreach (var item in modelModulo.listaModulos) { modelModulo.idModulo = item.idModulo; }
                modelImagen.listaImagenes = imagenBL.getModuloImagenes(modelModulo.idModulo);
            }
            
           foreach(var item in modelModulo.listaModulos)
            {
                modelModulo.nombreModulo = item.nombreModulo;
                modelModulo.titulo = item.titulo;
            }
            return View(tuple);
        }

        [HttpPost]
        public ActionResult Index(ModuloViewModel mod)
        {

            //
            var tuple = new Tuple<ModuloViewModel, ImagenModuloBannerViewModel>(modelModulo, modelImagen);
            //

            var selectMod = Request.Form["soloModulos"];
            Session["itemSelect"] = selectMod;
            modelModulo.soloModulos = moduloBL.getSoloModulos();
            modelModulo.listaModulos = moduloBL.getListModulo(selectMod);
            foreach (var item in modelModulo.listaModulos)
            {
                modelModulo.idModulo = item.idModulo;
                modelModulo.nombreModulo = item.nombreModulo;
                modelModulo.titulo = item.titulo;
            }
            modelImagen.listaImagenes = imagenBL.getModuloImagenes(modelModulo.idModulo);
            return View(tuple);

        }


        // public ActionResult EditarTitulo(string modulo,string titulo)//Para actualizar el titulo de un modulo
        public ActionResult EditarTitulo(string modulo, string titulo)
        {
            if(titulo.Equals("") || titulo.Length < 0)
            {
               return RedirectToAction("Index");
            }
            else
            {
                moduloBL.editarTitulo(modulo, titulo); //actualizar titulo
                return RedirectToAction("Index");
            }
         }

        
        public ActionResult SubirIntroduccion(string modulo, string rutaOrigen)
        {
            string extension = Path.GetExtension(rutaOrigen);
            FileInfo fileS = new FileInfo(rutaOrigen);
            var size = fileS.Length; //El tamanio del archivo en bytes

            var p = "";

            //Obtener la ruta de la introducccion alamacenada en la BD
            modelModulo.listaModulos = moduloBL.getListModulo(modulo);
            foreach(var item in modelModulo.listaModulos) 
            {
                modelModulo.idModulo = item.idModulo;
                modelModulo.archivoIntroduccion =item.archivoIntroduccion;
                p = @"C:\Users\CeciMP\Documents\FTPPrueba\" + item.archivoIntroduccion;
            }
            string rutaDestinoFTP = "Introduccion_" + modelModulo.idModulo + extension;

            try
            {
                
                Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.25.7", "userPrueba", "userPrueba123");

                //if(con.getFileSize(model.archivoIntroduccion).Equals(""))
                if (System.IO.File.Exists(p) == true)
                {
                    con.delete(modelModulo.archivoIntroduccion); 
                    con.upload(rutaDestinoFTP, rutaOrigen);
                }
                else
                {

                    con.upload(rutaDestinoFTP, rutaOrigen);
                }

                
            }
            catch (Exception ex) { Console.WriteLine(ex); throw; }
            moduloBL.editarArchivo(modulo, rutaDestinoFTP);
            return RedirectToAction("Index");
        }

        public ActionResult DescargarIntroduccion(string modulo)
        {
            modelModulo.listaModulos = moduloBL.getListModulo(modulo);
            string ruta = "";
            foreach(var item in modelModulo.listaModulos)
            {
                ruta = item.archivoIntroduccion;
            }
            FileInfo infoFile = new FileInfo(ruta);
            var nombre = "Introduccion_"+modulo;
            var ext = infoFile.Extension;
            try
            {

                Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.25.7", "userPrueba", "userPrueba123");
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                var curFile = mdoc + "\\Downloads\\" + nombre + ext;
                if (System.IO.File.Exists(curFile) == true)
                {
                    int i = 1;
                    while (i < i + 1)
                    {
                        curFile = mdoc + "\\Downloads\\" + nombre + "(" + i + ")" + ext;
                        if (System.IO.File.Exists(curFile) == true)
                        {
                            i++;
                        }
                        else { break; }

                    }
                    con.download(ruta, mdoc + "\\Downloads\\" + nombre +"("+i+")"+ ext);
                }
                else
                {
                    con.download(ruta, mdoc + "\\Downloads\\" + nombre + ext);
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult SubirIntroduccion2(string modulo, string rutaO,string archivoO)
        {
            //El base64 data se extrar de rutaO (URI del file seleccionado por el ussuario)
            String[] MiArray = rutaO.Split(',');
            string b64 = MiArray[1];
            Byte[] bytes = Convert.FromBase64String(b64);

            string extension = Path.GetExtension(archivoO);
            

            //Se almacea en un tempPath la conversion del base64 data
                string fileNameX2 = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + extension;
            System.IO.File.WriteAllBytes(fileNameX2, bytes);

            //Valida que el archivo cumpla con el tamaño máximo permitido 
            FileInfo fileIn = new FileInfo(fileNameX2);
            var tam = fileIn.Length;
            if(tam> 100000000 || tam<0) //100,000,000 byte=100MB
            {
                return RedirectToAction("Index");
            }
            
            //Obtener la ruta de la introducccion alamacenada en la BD
            modelModulo.listaModulos = moduloBL.getListModulo(modulo);
            foreach (var item in modelModulo.listaModulos)
            {
                modelModulo.idModulo = item.idModulo;
                modelModulo.archivoIntroduccion = item.archivoIntroduccion;
            }
            string rutaDestinoFTP = "Introduccion_" + modelModulo.idModulo + extension;

            Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");

            con.delete(modelModulo.archivoIntroduccion);
            con.upload(rutaDestinoFTP, fileNameX2);

            moduloBL.editarArchivo(modulo, rutaDestinoFTP);
            return RedirectToAction("Index");
        }
    }
}