using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblCampoEvento de la BD
    /// </summary>
    public class CampoEventoML:tblCampoEvento
    {
        public List<tblCampoEvento> listaCampoEvento = new List<tblCampoEvento>();

        public new int idCampoEvento { get; set; }
        public new int idEvento { get; set; }
        public new bool CCT { get; set; }
        public new bool nombrePlantel { get; set; }
        public new bool municipio { get; set; }
        public new bool regionSEG { get; set; }
        public new bool nivel { get; set; }
        public new bool instiSubsis { get; set; }
        public new bool CURP { get; set; }
        public new bool nombre { get; set; }
        public new bool apellidoPat { get; set; }
        public new bool apellidoMat { get; set; }
        public new bool correoElectronico { get; set; }
        public new bool telefono { get; set; }
        public new bool sexo { get; set; }
        public new bool funcion { get; set; }
        public new bool mesaTrabajo { get; set; }
    }
}
