using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.DataAccess.ModuloBP;
using BusinessModel.Persistence.BD;
using BusinessModel.Models.ModuloBP;

namespace BusinessModel.Business.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblImagenBP
    /// </summary>
    public class ImagenBPBL
    {
        ImagenBPDAL imagenDAL = new ImagenBPDAL();

        /// <summary>
        /// Obtener todos los registros en tblImagenBP
        /// </summary>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenes()
        {
            try
            {
                return imagenDAL.getImagenes();
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtener los registros en tblImagenBP donde imagen == parámetro rutaImagen
        /// </summary>
        /// <param name="rutaImagen"></param>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenes(string rutaImagen)
        {
            try
            {
                return imagenDAL.getImagenes(rutaImagen);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtener los registros en tblImagenBP donde buenaPractica == parámetro BP y 
        /// campoBP == parámetro campoBP
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="campoBP"></param>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenes(int BP, int campoBP)
        {
            try
            {
                return imagenDAL.getImagenes(BP, campoBP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtener los registros de tblImagenBP donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenes(int BP)
        {
            try
            {
                return imagenDAL.getImagenes(BP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtener los registros de tblImagenBP donde idImagen == parámetro idImg
        /// </summary>
        /// <param name="idImg"></param>
        /// <returns>model.listImagenBP</returns>
        public List<tblImagenBP> getImagenesXID(int idImg)
        {
            try
            {
                return imagenDAL.getImagenesXID(idImg);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Eliminar un registro de tblImagenBP donde imagen == parámetro rutaImg
        /// </summary>
        /// <param name="rutaImg"></param>
        /// <returns>True=Delete exitoso || False=Error delete</returns>
        public bool eliminarImagen(string rutaImg)
        {
            try
            {
                return imagenDAL.eliminarImagen(rutaImg);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Eliminar un registro de tblImagenBP donde idImagen == parámetro idImagen
        /// </summary>
        /// <param name="idImagen"></param>
        /// <returns>True=Delete exitoso || False=Error delete</returns>
        public bool eliminarImagen(int idImagen)
        {
            try
            {
                return imagenDAL.eliminarImagen(idImagen);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene un valor bool si la enumeración existe dentro de una buena práctica en tblImagenBP
        /// </summary>
        /// <param name="enumeracion"></param>
        /// <param name="BP"></param>
        /// <returns>True=Exite enumeración || False=No existe la enumeración</returns>
        public bool existeEnumeracion(string enumeracion, int BP)
        {
            try
            {
                return imagenDAL.existeEnumeracion(enumeracion, BP);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Nuevo registr en tblImagen, valida que la enumeración no exista.
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="campo"></param>
        /// <param name="rutaImg"></param>
        /// <param name="titulo"></param>
        /// <param name="enumeracion"></param>
        /// <returns>True=Insert exitoso || False=Falla en Insert</returns>
        public bool agregarImagen(int BP, int campo, string rutaImg,string titulo,string enumeracion)
        {
            try
            {
                return imagenDAL.agregarImagen(BP,campo,rutaImg,titulo,enumeracion);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene valor bool, validando que minimo existan dos archivos tipo Anexos para la BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>True=mayor o igual a 2 anexos || False=Menos a dos anexos</returns>
        public bool existenciaMinimaAnexos(int BP)
        {
            try
            {
                return imagenDAL.existenciaMinimaAnexos(BP);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene valor bool, con base a la cantidad máxima de anexos permitidos por BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>True=Mayor a 5 anexos || False = menos a 5 anexos</returns>
        public bool existenciaMaximaAnexos(int BP)
        {
            try
            {
                return imagenDAL.existenciaMaximaAnexos(BP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Utiliza la clase TabImagenBP, obtiene los registros de tblImagenBP join tblCampoBP
        /// donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.listaTabImagenBP</returns>
        public List<TabImagenBP> getListaTabImagenBP(int BP)
        {
            try
            {
                return imagenDAL.getListaTabImagenBP(BP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene la cantidad de archivos de una sección de la buena práctica
        /// </summary>
        /// <param name="BP"></param>
        /// <param name="seccion"></param>
        /// <returns>int cantidad de archivos</returns>
        public int contarArchivos(int BP, string seccion)
        {
            try
            {
                return imagenDAL.contarArchivos(BP,seccion);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        }
}
