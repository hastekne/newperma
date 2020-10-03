using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.DataAccess.Global;
using BusinessModel.Persistence.BD;

namespace BusinessModel.Business.Global
{
    /// <summary>
    /// Clase para el acceso a datos en tblImagenModuloBanner
    /// </summary>
    public class ImagenModuloBannerBL
    {
        ImagenModuloBannerDAL imagenDAL = new ImagenModuloBannerDAL();

        /// <summary>
        /// Obtine todos los registros de tblImagenModuloBanner
        /// </summary>
        /// <returns>model.listaImagenes</returns>
        public List<tblImagenModuloBanner> getListImagen()
        {
            try
            {
                return imagenDAL.getListImagen();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblImagenModuloBanner donde idImgBanner = parámetro idImg
        /// </summary>
        /// <param name="idImg"></param>
        /// <returns>model.listaImagenes</returns>
        public List<tblImagenModuloBanner> getListImagen(int idImg)
        {
            try
            {
                return imagenDAL.getListImagen(idImg);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registro en tblImagenModuloBanner donde imagen = parámetro rutaImagen
        /// </summary>
        /// <param name="rutaImagen"></param>
        /// <returns>model.listaImagenes</returns>
        public List<tblImagenModuloBanner> getListImagen(string rutaImagen)
        {
            try
            {
                return imagenDAL.getListImagen(rutaImagen);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblImagenModuloBanner donde modulo = parámetro modulo
        /// </summary>
        /// <param name="modulo"></param>
        /// <returns>model.listaImagenes</returns>
        public List<tblImagenModuloBanner> getModuloImagenes(string modulo)
        {
            try
            {
                return imagenDAL.getModuloImagenes(modulo);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
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
            try
            {
                return imagenDAL.EliminarImagen(modulo,rutaImagen);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Elimina un registro en tblImagenModuloBanner donde idImgBanner = parámetro idImg
        /// </summary>
        /// <param name="idImg"></param>
        /// <returns></returns>
        public bool EliminarImagen(int idImg)
        {
            try
            {
                return imagenDAL.EliminarImagen(idImg);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Nuevo registro a la tabla tblImagenModuloBanner
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="rutaImagen"></param>
        /// <returns>FALSE=No se agrego || TRUE=Se agrego registro</returns>
        public bool agregarImagen(string modulo, string rutaImagen)
        {
            try
            {
                return imagenDAL.agregarImagen(modulo, rutaImagen);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
