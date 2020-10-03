using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using BusinessModel.Models.Global;

namespace BusinessModel.DataAccess.Global
{
    /// <summary>
    /// Clase para el acceso a datos en tblImagenModuloBanner
    /// </summary>
    public class ImagenModuloBannerDAL
    {
        ImagenModuloBannerML model = new ImagenModuloBannerML();

        /// <summary>
        /// Obtine todos los registros de tblImagenModuloBanner
        /// </summary>
        /// <returns>model.listaImagenes</returns>
        public List<tblImagenModuloBanner> getListImagen()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaImagenes = (from i in db.tblImagenModuloBanner select i).ToList();
            }
            return model.listaImagenes;
        }

        /// <summary>
        /// Obtiene los registros en tblImagenModuloBanner donde idImgBanner = parámetro idImg
        /// </summary>
        /// <param name="idImg"></param>
        /// <returns>model.listaImagenes</returns>
        public List<tblImagenModuloBanner> getListImagen(int idImg)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaImagenes = (from i in db.tblImagenModuloBanner where i.idImgBanner==idImg select i).ToList();
            }
            return model.listaImagenes;
        }

        /// <summary>
        /// Obtiene los registro en tblImagenModuloBanner donde imagen = parámetro rutaImagen
        /// </summary>
        /// <param name="rutaImagen"></param>
        /// <returns>model.listaImagenes</returns>
        public List<tblImagenModuloBanner> getListImagen(string rutaImagen)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaImagenes = (from i in db.tblImagenModuloBanner where i.imagen==rutaImagen select i).ToList();
            }
            return model.listaImagenes;
        }

        /// <summary>
        /// Obtiene los registros en tblImagenModuloBanner donde modulo = parámetro modulo
        /// </summary>
        /// <param name="modulo"></param>
        /// <returns>model.listaImagenes</returns>
        public List<tblImagenModuloBanner> getModuloImagenes(string modulo)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listaImagenes = (from i in db.tblImagenModuloBanner where i.modulo==modulo select i).ToList();
            }
            return model.listaImagenes;
        }

        /// <summary>
        /// Elimina un registro en la tabal tblImagenModuloBanner donde modulo= parámetro modulo
        /// yimagen= parámetro rutaImagen
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="rutaImagen"></param>
        /// <returns></returns>
        public bool EliminarImagen(string modulo, string rutaImagen)
        {
            bool eliminado = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                var imagenEli = (from i in db.tblImagenModuloBanner where i.modulo == modulo && i.imagen==rutaImagen select i).First<tblImagenModuloBanner>();
                db.tblImagenModuloBanner.Remove(imagenEli);
                eliminado = true;
                db.SaveChanges();
            }
            return eliminado;
        }

        /// <summary>
        /// Elimina un registro en tblImagenModuloBanner donde idImgBanner = parámetro idImg
        /// </summary>
        /// <param name="idImg"></param>
        /// <returns></returns>
        public bool EliminarImagen(int idImg)
        {
            bool eliminado = false;
            using (var db = new SeguimientoPermanenciaEntities())
            {
                var imagenEli = (from i in db.tblImagenModuloBanner where i.idImgBanner==idImg select i).First<tblImagenModuloBanner>();
                db.tblImagenModuloBanner.Remove(imagenEli);
                eliminado = true;
                db.SaveChanges();
            }
            return eliminado;
        }

        /// <summary>
        /// Nuevo registro a la tabla tblImagenModuloBanner
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="rutaImagen"></param>
        /// <returns>FALSE=No se agrego || TRUE=Se agrego registro</returns>
        public bool agregarImagen(string modulo, string rutaImagen)
        {
            bool agrego = false;
            try
            {
                using (var db = new SeguimientoPermanenciaEntities())
                {
                    tblImagenModuloBanner newImagen = new tblImagenModuloBanner()
                    {
                        modulo = modulo,
                        imagen = rutaImagen,
                    };
                    db.tblImagenModuloBanner.Add(newImagen);
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


    }
}
