using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using SeccionAdministrador.Helpers;
using System.IO;
using System.Net;

namespace SeccionAdministrador.Controllers
{
    public class VisualizacionPreviaController : Controller
    {
        //Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.66", "SEG\\mc_mendoza", "M2pm2019");
        // Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.69", "SEG\\mc_mendoza", "M2pm2019");
        //FTPClient con = new FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");//SEG
        Helpers.FTPClient con = new Helpers.FTPClient("ftp://192.168.1.66", "SEG\\mc_mendoza", "M2pm2019");//Mi casa

        public ActionResult Plantilla()
        {
          

            string extension = Path.GetExtension("plantillaBP.docx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; 
            FTPClient con = new FTPClient("ftp://192.168.23.170", "SEG\\mc_mendoza", "M2pm2019");
            Response.WriteFile(con.downloadVistaPrevia("MesBP/BPMes.webm"));
            return View();
        }



        // GET: VisualizacionPrevia
        /// <summary>
        /// Genera la vista utilizando la ruta del archivo generando la descarga con downloadVistaPrevia
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public ActionResult Index(string archivo)
        {
            string extension = Path.GetExtension(archivo);
            Response.ContentType = getResponseContentType(extension);
            Response.WriteFile(con.downloadVistaPrevia(archivo));
            return View();
        }

        /// <summary>
        /// Método utilizado para la descarga de archivos con extensión .pptx o .docx
        /// Realiza la descarga desde la ubicación en el servidor FTP
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public ActionResult Descarga(string archivo,string sec)
        {
            string extension = Path.GetExtension(archivo);
            if(extension==".pptx"|| extension == ".docx")
            {
                sec = "otro";
            }
            if (sec == "Intro")
            {
                extension = Path.GetExtension(archivo);
                Response.ContentType = getResponseContentType(extension);
                Response.WriteFile(con.downloadVistaPrevia(archivo));
                return View();
            }
            else
            {
                string nombre = Path.GetFileName(archivo);
                byte[] filedata = System.IO.File.ReadAllBytes(con.downloadVistaPrevia(archivo));
                return File(filedata, System.Net.Mime.MediaTypeNames.Application.Octet, nombre);
            }
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
        /// <returns></returns>
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
                case ".xls":
                    content = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    content = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
            }
            return content;
        }
    }
}