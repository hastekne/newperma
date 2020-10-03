using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using BusinessModel.Models.ModuloBP;

namespace BusinessModel.DataAccess.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblImagenRevision
    /// </summary>
    public class ImagenRevisionDAL
    {
        ImagenRevisionML model = new ImagenRevisionML();

        /// <summary>
        /// Obtiene todos los registros de tblImagenRevision
        /// </summary>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenesRev()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listImgenRevi = (from img in db.tblImagenRevision select img).ToList();
            }
            return model.listImgenRevi;
        }

        /// <summary>
        /// Obtiene los registros de tblImagenRevision donde idImagenComen == parámetro idImagen
        /// </summary>
        /// <param name="idImagen"></param>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenesXId(int idImagen)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listImgenRevi = (from img in db.tblImagenRevision where img.idImagenComen==idImagen select img).ToList();
            }
            return model.listImgenRevi;
        }

        /// <summary>
        /// Obtiene los registros de tblImagenRevision donde idRevision == parámetro idRevision
        /// </summary>
        /// <param name="idRevision"></param>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenesRev(int idRevision)
        {
            using (var db=new SeguimientoPermanenciaEntities())
            {
                model.listImgenRevi = (from img in db.tblImagenRevision where img.idRevision==idRevision select img).ToList();
            }
            return model.listImgenRevi;
        }

        /// <summary>
        /// Obtiene los registros de tblImagenRevision donde campoPlantilla == parámetro campoBP y
        /// idRevision == parámetro idRevision
        /// </summary>
        /// <param name="idRevision"></param>
        /// <param name="campoBP"></param>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenesRev(int idRevision, int campoBP)
        {
            using(var db=new SeguimientoPermanenciaEntities())
            {
                model.listImgenRevi = (from img in db.tblImagenRevision where img.campoPlantilla==campoBP && img.idRevision==idRevision select img).ToList();
            }
            return model.listImgenRevi;
        }

        /// <summary>
        /// Elimina en registro en tblImagenRevision donde idImagenComen == parámetro idImagen
        /// </summary>
        /// <param name="idImagen"></param>
        /// <returns>True=Delete exitoso || False=Error en Delete</returns>
        public bool eliminarImagen(int idImagen)
        {
            bool eliminado = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                var imagenEli = (from i in db.tblImagenRevision where i.idImagenComen==idImagen select i).First<tblImagenRevision>();
                db.tblImagenRevision.Remove(imagenEli);
                eliminado = true;
                db.SaveChanges();
            }
            return eliminado;
        }

        /// <summary>
        /// Nuevo Registro en tblImagenRevision
        /// </summary>
        /// <param name="idRevision"></param>
        /// <param name="idCampo"></param>
        /// <param name="rutaImg"></param>
        /// <param name="titulo"></param>
        /// <param name="enumeracion"></param>
        /// <returns>True=Insert exitoso || False=Error en Insert</returns>
        public bool agregarImagen(int idRevision, int idCampo, string rutaImg,string titulo,string enumeracion)
        {
            bool agrego = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    tblImagenRevision newImagen = new tblImagenRevision()
                    {
                        idRevision = idRevision,
                        campoPlantilla = idCampo,
                        imagen = rutaImg,
                        titulo = titulo,
                        enumeracion = enumeracion,
                    };
                    db.tblImagenRevision.Add(newImagen);
                    db.SaveChanges();
                }
                agrego = true;
            }
            catch (Exception)
            {
                agrego = false;
                throw;
            }

            return agrego;
        }

        /// <summary>
        /// Obtiene los registros de tblImagenRevision join tblRevisionBP donde campoPlantilla == parámetro campoBP
        /// y buenaPractica == parámetro idBP
        /// </summary>
        /// <param name="idBP"></param>
        /// <param name="campoBP"></param>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenesRevBP(int idBP, int campoBP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listImgenRevi = (from img in db.tblImagenRevision join rev in  db.tblRevisionBP on img.idRevision equals rev.buenaPractica  where img.campoPlantilla == campoBP &&  rev.buenaPractica== idBP select img).ToList();
            }
            return model.listImgenRevi;
        }

        /// <summary>
        /// Obtiene los registros de tblImagenRevision donde idRevision == parámetro idRevision y
        /// campoPlantilla == parámetro campoBP
        /// </summary>
        /// <param name="idRevision"></param>
        /// <param name="campoBP"></param>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenRevision2(int idRevision,int campoBP){
            using (var db=new SeguimientoPermanenciaEntities()){
                model.listImgenRevi = (from img in db.tblImagenRevision where img.idRevision == idRevision && img.campoPlantilla == campoBP select img).ToList();
            }
            return model.listImgenRevi;
            }

        /// <summary>
        /// Utiliza la clase TabImagenRevisionBP obtiene los registros de tblImagenRevision join tblRevisionBP join tblCampoBP
        /// donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.tabImagenRevisionBP</returns>
        public List<TabImagenRevisionBP> getListaTabImagenBP(int BP)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.tabImagenRevisionBP = (from img in db.tblImagenRevision join rev in db.tblRevisionBP on img.idRevision equals rev.idRevision join c in db.tblCampoBP on img.campoPlantilla equals c.idCampo where rev.buenaPractica == BP select new TabImagenRevisionBP { idImagen = img.idImagenComen, figura = img.enumeracion, Titulo = img.titulo, campoBP = c.nombre }).ToList();

            }

            return model.tabImagenRevisionBP;
        }


    }
}
