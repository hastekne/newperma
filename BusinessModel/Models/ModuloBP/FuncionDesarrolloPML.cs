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
    /// Clase relacionada con la tabla tblFuncionDesarrolloP de la BD
    /// </summary>
    public class FuncionDesarrolloPML:tblFuncionDesarrolloP
    {
        public List<tblFuncionDesarrolloP> listFunciones = new List<tblFuncionDesarrolloP>();
        public tblFuncionDesarrolloP tabFunciones = new tblFuncionDesarrolloP();

        public new int idFuncionD { get; set; }
        public new string nombreFunc { get; set; }
        public new bool estado { get; set; }
    }
}
