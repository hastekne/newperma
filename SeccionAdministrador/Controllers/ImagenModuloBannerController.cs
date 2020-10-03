using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using SeccionAdministrador.Models;
using BusinessModel.Business.Global;

namespace SeccionAdministrador.Controllers
{
    public class ImagenModuloBannerController : Controller
    {
        ImagenModuloBannerViewModel model = new ImagenModuloBannerViewModel();
        ImagenModuloBannerBL imagneBL = new ImagenModuloBannerBL(); 

        // GET: ImagenModuloBanner
        /// <summary>
        /// Método de prueba, no se utiliza en el proyecto
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            model.listaImagenes = imagneBL.getModuloImagenes("01000000");
            return View(model);
        }
    }
}