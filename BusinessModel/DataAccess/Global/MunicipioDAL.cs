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
    /// Clase para el acceso de datos a  tblMunicipio
    /// Clase temporal, se utilizara la API para el acceso de datos
    /// </summary>
    public class MunicipioDAL
    {
        MunicipioML model = new MunicipioML();

        /// <summary>
        /// Obtene todos los registros de tblMunicipio
        /// </summary>
        /// <returns>model.listMunicipios</returns>
        public List<tblMunicipio> getMunicipio()
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listMunicipios = (from m in db.tblMunicipio select m).ToList();
            }
            return model.listMunicipios;
        }

        /// <summary>
        /// Obtiene el registro de tblMunicipio donde nombreMunicipio es similar a palabraClave
        /// </summary>
        /// <param name="palabraClave"></param>
        /// <returns>model.listMunicipios</returns>
        public List<tblMunicipio> getMunicipio(string palabraClave)
        {
            using (var db = new SeguimientoPermanenciaEntities())
            {

                model.listMunicipios = (from m in db.tblMunicipio where m.nombreMunicipio.Contains(palabraClave) select m).ToList();
            }
            return model.listMunicipios;
        }

        /// <summary>
        /// Obtiene el id del municipio donde nombreMunicipio = parámetro municipio
        /// </summary>
        /// <param name="municipio"></param>
        /// <returns>model.idMunicipio</returns>
        public int idMunicipio(string municipio)
        {
            using (var db=new SeguimientoPermanenciaEntities())
            {
                //model.idMunicipio = Int32.Parse((from m in db.tblMunicipio where m.nombreMunicipio == municipio select m.idMunicipio).ToString());
                model.listMunicipios = (from m in db.tblMunicipio where m.nombreMunicipio == municipio select m).ToList();
                foreach(var item in model.listMunicipios)
                {
                    model.idMunicipio = item.idMunicipio;
                }
            }
            return model.idMunicipio;
        }

        /// <summary>
        /// Obtiene el nombreMunicipio donde idMunicipio = parámetro idMunicipio
        /// </summary>
        /// <param name="idMunicipio"></param>
        /// <returns>model.nombreMunicipio</returns>
        public string getNomMunicipio(int idMunicipio)
        {
            string nomM = "";
            using (var db = new SeguimientoPermanenciaEntities())
            {
                model.listMunicipios = (from m in db.tblMunicipio where m.idMunicipio==idMunicipio select m).ToList();
            }
            foreach(var item in model.listMunicipios)
            {
                nomM = item.nombreMunicipio;
            }
            return nomM;
        }
    }
}
