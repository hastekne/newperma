using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using BusinessModel.DataAccess.ModuloBP;

namespace BusinessModel.Business.ModuloBP
{
    /// <summary>
    /// Clase para el acceso de datos a tblCampoBP
    /// </summary>
    public class CampoBPBL
    {
        CampoBPDAL campoDAL = new CampoBPDAL();

        /// <summary>
        /// Obtiene todos los registros en tblCampoBP
        /// </summary>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCamposBP()
        {
            try
            {
                return campoDAL.getCamposBP();
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblCampo donde nombre == parámetro nombreCampo
        /// </summary>
        /// <param name="nombreCampo"></param>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCamposBP(string nombreCampo)
        {
            try
            {
                return campoDAL.getCamposBP(nombreCampo);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblCampo donde idCampo == parámetro idCampo
        /// </summary>
        /// <param name="idCampo"></param>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCamposBP(int idCampo)
        {
            try
            {
                return campoDAL.getCamposBP(idCampo);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblCampo donde subeArchivo == parámetro subeArchivo
        /// </summary>
        /// <param name="subeArchivo"></param>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCampoBPArchivo(bool subeArchivo)
        {
            try
            {
                return campoDAL.getCampoBPArchivo(subeArchivo);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene los registros en tblCampo donde idCampo == parámetro idcampo y 
        /// subeArchivo == parámetro subeArchivo
        /// </summary>
        /// <param name="idcampo"></param>
        /// <param name="subeArchivo"></param>
        /// <returns>model.listCamposBP</returns>
        public List<tblCampoBP> getCampoBPArchivo(int idcampo, bool subeArchivo)
        {
            try
            {
                return campoDAL.getCampoBPArchivo(idcampo, subeArchivo);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        public List<tblCampoBP> getCamposBPSinAnexos()
        {
            try
            {
                return campoDAL.getCamposBPSinAnexos();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
