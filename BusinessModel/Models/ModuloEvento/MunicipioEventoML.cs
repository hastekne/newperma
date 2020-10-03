using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblMunicipioEvento de la BD
    /// </summary>
    public class MunicipioEventoML:tblMunicipioEvento
    {
        public List<tblMunicipioEvento> listaMuniEvento = new List<tblMunicipioEvento>();

        public new int idMunicipioEvento { get; set; }
        public new int idEvento { get; set; }
        public new int idMunicipio { get; set; }
    }
}
