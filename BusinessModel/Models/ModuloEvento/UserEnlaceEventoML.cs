using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblUserEnlaceEvento de la BD
    /// </summary>
    public class UserEnlaceEventoML:tblUserEnlaceEvento
    {
        public List<tblUserEnlaceEvento> listUserEnlaceEvento = new List<tblUserEnlaceEvento>();

        public new int idUserEnlaceEve { get; set; }
        public new string idTipoRegistro { get; set; }
        public new int idEvento { get; set; }
        public new string idUserEnlace { get; set; }
    }
}
