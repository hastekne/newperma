using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblFuncionParticipante de la BD
    /// </summary>
    public class FuncionParticipanteML:tblFuncionParticipante
    {
        public List<tblFuncionParticipante> listaFuncionPart = new List<tblFuncionParticipante>();

        public new int idFuncionPart { get; set; }
        public new int idEvento { get; set; }
        public new string nomFuncion { get; set; }
    }
}
