using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblMesaTrabEvento de la BD
    /// </summary>
    public class MesaEventoML:tblMesaTrabEvento
    {
        public List<tblMesaTrabEvento> listaMesasEvento=new List<tblMesaTrabEvento>();

        public new int idMesaTrab { get; set; }
        public new int idEvento { get; set; }
        public new string nombreMT { get; set; }
        public new int cantidad { get; set; }
        public new bool estado { get; set; }
    }
}
