using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using DesarrolloProyDSP.Helpers;
using System.IO;


namespace DesarrolloProyDSP.Controllers.Global
{
    public class VisualizacionPreviaController : Controller
    {

        Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.66", "SEG\\mc_mendoza", "M2pm2019");
        //FTPClient con = new FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
        public ActionResult Plantilla()
        {
            //string extension = Path.GetExtension("plantillaBP.docx");
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; 
            //FTPClient con = new FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
            //Response.WriteFile(con.downloadVistaPrevia("Introduccion/plantillaBP.docx"));
            //return View();
            string archivo = "Introduccion/plantillaBP.docx";
            string nombre = Path.GetFileName(archivo);
            byte[] filedata = System.IO.File.ReadAllBytes(con.downloadVistaPrevia(archivo));
            return File(filedata, System.Net.Mime.MediaTypeNames.Application.Octet, nombre);
        }

        // GET: VisualizacionPrevia
        /// <summary>
        /// Genera la vista utilizando la ruta del archivo generando la descarga con downloadVistaPrevia
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns>Retorna vista</returns>
        public ActionResult Index(string archivo)
        {
            //Obtener el nombre del archivo con la ruta almacenada en la BD correspondiente
            //a la seleccion del archivo a visualizar.
            //string archivo = "3Desc. Actividades realizadasDesc. Actividades.png";
            string extension = Path.GetExtension(archivo);
            //Obtener la extension del archivo para establecer el ContentType
            
            Response.ContentType = getResponseContentType(extension);
            //FTPClient con = new FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
            Response.WriteFile(con.downloadVistaPrevia(archivo));
            return View();
        }

        public ActionResult eventos()
        {
            Session["var"] = 0;
            return View();
        }

        /// <summary>
        /// Obtiene el string MIME apartir de la extension
        /// </summary>
        /// <param name="extension"></param>
        /// <returns>String MIME</returns>
        public string getResponseContentType(string extension)
        {
            string content = "";
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    content = "image/jpg";
                    break;
                case ".pdf":
                    content = "application/pdf";
                    break;
                case ".gif":
                    content = "image/gif";
                    break;
                case ".ppt":
                    content = "application/vnd.ms-powerpoint";
                    break;
                case ".pptx":
                    content = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                case ".doc":
                    content = "application/msword";
                    break;
                case ".docx":
                    content = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".webm":
                    content = "video/webm";
                    break;
                case ".ogv":
                    content = "video/ogg";
                    break;
                case ".m4v":
                    content = "video/mp4";
                    break;
                case ".png":
                    content = "image/png";
                    break;
            }
            return content;
        }
    }
}