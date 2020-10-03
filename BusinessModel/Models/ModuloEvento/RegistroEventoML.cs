using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblRegistroEvento de la BD
    /// </summary>
    public class RegistroEventoML:tblRegistroEvento
    {
        public List<tblRegistroEvento> listaRegistroEvento = new List<tblRegistroEvento>();

        public new int folio { get; set; }
        public new int idEvento { get; set; }
        public new string idTipoRegistro { get; set; }
        public new string CCT { get; set; }
        public new string nombrePlantel { get; set; }
        public new string municipio { get; set; }
        public new string regionSEG { get; set; }
        public new string nivel { get; set; }
        public new string instiSubsis { get; set; }
        public new string CURP { get; set; }
        public new string nombre { get; set; }
        public new string apellidoPat { get; set; }
        public new string apellidoMat { get; set; }
        public new string correoElectronico { get; set; }
        public new string telefono { get; set; }
        public new string sexo { get; set; }
        public new string funcion { get; set; }
    }
}
