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
    /// Clase para el acceso de datos a  tblMunicipio
    /// Clase temporal, se utilizara la API para el acceso de datos
    /// </summary>
    public class MunicipioBL
    {
        MunicipioDAL municipioDAL = new MunicipioDAL();

        /// <summary>
        /// Obtene todos los registros de tblMunicipio
        /// </summary>
        /// <returns>model.listMunicipios</returns>
        public List<tblMunicipio> getMunicipio()
        {
            try
            {
                return municipioDAL.getMunicipio();
            }catch(Exception ex) { Console.WriteLine(ex.Message);throw; }
        }

        /// <summary>
        /// Obtiene el registro de tblMunicipio donde nombreMunicipio es similar a palabraClave
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <returns>model.listMunicipios</returns>
        public List<tblMunicipio> getMunicipio(string palabraClave)
        {
            try
            {
                return municipioDAL.getMunicipio(palabraClave);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el id del municipio donde nombreMunicipio = parámetro municipio
        /// </summary>
        /// <param name="municipio"></param>
        /// <returns>model.idMunicipio</returns>
        public int idMunicipio(string municipio)
        {
            try
            {
                return municipioDAL.idMunicipio(municipio);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        /// <summary>
        /// Obtiene el nombreMunicipio donde idMunicipio = parámetro idMunicipio
        /// </summary>
        /// <param name="idMunicipio"></param>
        /// <returns>model.nombreMunicipio</returns>
        public string getNomMunicipio(int idMunicipio)
        {
            try
            {
                return municipioDAL.getNomMunicipio(idMunicipio);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}
