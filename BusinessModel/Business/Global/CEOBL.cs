using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using BusinessModel.DataAccess.Global;

namespace BusinessModel.Business.Global
{
    /// <summary>
    /// Clase con métodos para el acceso a tblCEO
    /// Clase temporal, el acceso a datos sera por merdio de la API
    /// </summary>
    public class CEOBL
    {

        CEODAL ceoDAL = new CEODAL();

        /// <summary>
        /// Select * tblCEO 
        /// </summary>
        /// <returns>model.listCEO</returns>
        public List<tblCEO> getCEO()
        {
            try
            {
                return ceoDAL.getCEO();
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw;}   
        }

        /// <summary>
        /// Select * from tblCEO where idCEO= parámetro idCEO específicado
        /// </summary>
        /// <param name="idCEO"></param>
        /// <returns>model.listCEO</returns>
        public List<tblCEO> getCEO(int idCEO)
        {
            try
            {
                return ceoDAL.getCEO(idCEO);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw;}
        }

        /// <summary>
        /// Select * from tblCEO where claveCentroTrabajo= parámetro claveCT específicado
        /// </summary>
        /// <param name="claveCT"></param>
        /// <returns>model.listCEO</returns>
        public List<tblCEO> getCEO(string claveCT)
        {
            try
            {
                return ceoDAL.getCEO(claveCT);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Select * from tblCEO where nombreCentroTrabajo= parámetro nombre específicado
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>model.listCEO</returns>
        public List<tblCEO> BusXnombreCT(string nombre)
        {
            try
            {
                return ceoDAL.BusXnombreCT(nombre);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el idCEO con el parámetro claveCEO
        /// </summary>
        /// <param name="claveCEO"></param>
        /// <returns>model.idCEO</returns>
        public int getIdCEO(string claveCEO)
        {
            try
            {
                return ceoDAL.getIdCEO(claveCEO);
            }catch(Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene la Clave del centro de trabajo con el idCEO
        /// </summary>
        /// <param name="idCEO"></param>
        /// <returns>model.claveCentroTrabajo</returns>
        public string getCVCEO(int idCEO)
        {
            try
            {
                return ceoDAL.getCVCEO(idCEO);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
