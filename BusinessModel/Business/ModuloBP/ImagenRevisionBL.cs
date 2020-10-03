using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using BusinessModel.DataAccess.ModuloBP;
using BusinessModel.Models.ModuloBP;

namespace BusinessModel.Business.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblImagenRevision
    /// </summary>
    public class ImagenRevisionBL
    {
        ImagenRevisionDAL imgComentarioDAL = new ImagenRevisionDAL();

        /// <summary>
        /// Obtiene todos los registros de tblImagenRevision
        /// </summary>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenesRev()
        {
            try
            {
                return imgComentarioDAL.getImagenesRev();
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblImagenRevision donde idImagenComen == parámetro idImagen
        /// </summary>
        /// <param name="idImagen"></param>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenesXId(int idImagen)
        {
            try
            {
                return imgComentarioDAL.getImagenesXId(idImagen);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblImagenRevision donde idRevision == parámetro idRevision
        /// </summary>
        /// <param name="idRevision"></param>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenesRev(int idRevision)
        {
            try
            {
                return imgComentarioDAL.getImagenesRev(idRevision);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblImagenRevision donde campoPlantilla == parámetro campoBP y
        /// idRevision == parámetro idRevision
        /// </summary>
        /// <param name="idRevision"></param>
        /// <param name="campoBP"></param>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenRevision(int idRevision, int campoBP)
        {
            try
            {
                return imgComentarioDAL.getImagenesRev(idRevision, campoBP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Elimina en registro en tblImagenRevision donde idImagenComen == parámetro idImagen
        /// </summary>
        /// <param name="idImagen"></param>
        /// <returns>True=Delete exitoso || False=Error en Delete</returns>
        public bool eliminarImagen(int idImagen)
        {
            try
            {
                return imgComentarioDAL.eliminarImagen(idImagen);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
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
        public bool agregarImagen(int idRevision, int idCampo, string rutaImg,string titulo, string enumeracion)
        {
            try
            {
                return imgComentarioDAL.agregarImagen(idRevision,idCampo,rutaImg,titulo,enumeracion);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
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
            try
            {
                return imgComentarioDAL.getImagenesRevBP(idBP, campoBP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros de tblImagenRevision donde idRevision == parámetro idRevision y
        /// campoPlantilla == parámetro campoBP
        /// </summary>
        /// <param name="idRevision"></param>
        /// <param name="campoBP"></param>
        /// <returns>model.listImgenRevi</returns>
        public List<tblImagenRevision> getImagenRevision2(int idRevision, int campoBP)
        {
            try
            {
                return imgComentarioDAL.getImagenRevision2(idRevision, campoBP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }


        /// <summary>
        /// Utiliza la clase TabImagenRevisionBP obtiene los registros de tblImagenRevision join tblRevisionBP join tblCampoBP
        /// donde buenaPractica == parámetro BP
        /// </summary>
        /// <param name="BP"></param>
        /// <returns>model.tabImagenRevisionBP</returns>
        public List<TabImagenRevisionBP> getListaTabImagenBP(int BP)
        {
            try
            {
                return imgComentarioDAL.getListaTabImagenBP(BP);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
