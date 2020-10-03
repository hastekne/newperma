using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblMesaTrabParticipante de la BD
    /// </summary>
    public class MesaParticipanteML:tblMesaTrabParticipante
    {
        public List<tblMesaTrabParticipante> listaMesaParticipante = new List<tblMesaTrabParticipante>();
        public List<MesaParticipanteRI> listaMesaPRI = new List<MesaParticipanteRI>();

        public new int idMesaTrabParti { get; set; }
        public new int folioParticipante { get; set; }
        public new int idMesaTrab { get; set; }
    }

    /// <summary>
    /// Clase específica para las mesas de trabajo del participante en el registro individual
    /// </summary>
    public class MesaParticipanteRI
    {
        public List<MesaParticipanteRI> listaMesaPRI = new List<MesaParticipanteRI>();
        public int folio { get; set; }
        public int idMesa { get; set; }
        public string nombreMT { get; set; }
    }
}
