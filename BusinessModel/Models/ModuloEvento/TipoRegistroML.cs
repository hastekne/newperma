using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblTipoReistro de la BD
    /// </summary>
    public class TipoRegistroML:tblTipoRegistro
    {
        public List<tblTipoRegistro> listaTipoRegistro=new List<tblTipoRegistro>();

        public new string idTipoRegistro { get; set; }
        public new string descripcion { get; set; }
    }
}
