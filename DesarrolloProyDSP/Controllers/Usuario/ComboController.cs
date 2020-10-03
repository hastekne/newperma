using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
//Se agrego la libreria newtonsoft
using Newtonsoft.Json;
using BusinessModel.Persistence.BD;
using DesarrolloProyDSP.Models.Usuario;

namespace DesarrolloProyDSP.Controllers.Usuario
{
    /// <summary>
    /// Clase de prueba
    /// </summary>
    public class ComboController : Controller
    {
        // GET: Combo
        PerfilViewModel model = new PerfilViewModel();
        public ActionResult Index()
        {
            llenar();
            return View(model);
        }
        [System.Web.Services.WebMethod]
        public static string llenar()
        {
            SeguimientoPermanenciaEntities db = new SeguimientoPermanenciaEntities();

            var query = from c in db.tblPerfil
                        select new
                        {
                            perfilID = c.idPerfil,
                            per = c.perfil
                        };

            return Newtonsoft.Json.JsonConvert.SerializeObject(query);

        }
    }
}