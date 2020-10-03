using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblSubsustemaEvento de la BD
    /// </summary>
    public class SubsistemaEventoML:tblSubsistemaEvento
    {
        public List<tblSubsistemaEvento> listaSubsisEvento = new List<tblSubsistemaEvento>();

        public new int idSubsistemaEv { get; set; }
        public new int idEvento { get; set; }
        public new string nombreSubsistema { get; set; }
        public new int cantidad { get; set; }

    }
}
