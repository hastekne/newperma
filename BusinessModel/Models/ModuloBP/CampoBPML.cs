using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloBP
{
    /// <summary>
    /// Clase relacionada con la tabla tblCamposBP de la BD
    /// </summary>
    public class CampoBPML:tblCampoBP
    {
        public List<tblCampoBP> listCamposBP = new List<tblCampoBP>();
        public tblCampoBP tabCampoBP = new tblCampoBP();

        public new int idCampo { get; set; }
        public new string nombre { get; set; }
        public new bool subeArchivo { get; set; }

    }
}
