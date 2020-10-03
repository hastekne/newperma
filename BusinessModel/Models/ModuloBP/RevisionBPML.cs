using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using System.ComponentModel.DataAnnotations;

namespace BusinessModel.Models.ModuloBP
{
    /// <summary>
    /// Clase relacionada con la tabla tblRevisionBP de la BD
    /// </summary>
    public class RevisionBPML:tblRevisionBP
    {
        public List<tblRevisionBP> listRevision = new List<tblRevisionBP>();
        public tblRevisionBP tabRevision = new tblRevisionBP();
        public List<BPRevisionTable> listRevisionTable = new List<BPRevisionTable>();

        public new int idRevision { get; set; }
        public new string usuarioRevisor { get; set; }
        public new int buenaPractica { get; set; }
        public new System.DateTime fechaSoliRevi { get; set; }
        public new Nullable<System.DateTime> fechaListaPubli { get; set; }

        public new string comentarioTituloBP { get; set; }
        public new string comentarioSituacionMejora { get; set; }
        public new string comentarioDiagnosticoRealizo { get; set; }
        public new string comentarioCaracteristicasContexto { get; set; }
        public new string comentarioDescripcionActividadesRealizadas { get; set; }
        public new string comentarioElementoInnovador { get; set; }
        public new string comentarioResultadosObtenidos { get; set; }
        public new string comentarioObservacionesBP { get; set; }
        public new string comentarioFuenteInformacion { get; set; }
        public new string comentarioGeneral { get; set; }
    }

    /// <summary>
    /// Clase específica para los datos de las revisiones
    /// </summary>
    public class BPRevisionTable
    {
        public List<BPRevisionTable> listaTableRevision = new List<BPRevisionTable>();
        public string titulo { get; set; }
        public Nullable<System.DateTime> fechaSolicitud { get; set; }
        public Nullable<System.DateTime> fechaListaP { get; set; }
        public string estado { get; set; }
    }
}
