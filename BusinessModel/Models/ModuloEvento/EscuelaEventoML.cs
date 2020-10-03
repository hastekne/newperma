using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblEscuelaEvento de la BD
    /// </summary>
    public class EscuelaEventoML:tblEscuelaEvento
    {
        public List<tblEscuelaEvento> listaEscuelasEvento = new List<tblEscuelaEvento>();
        public List<TablaEscuelaCantidad> tablaEscuelaCantida = new List<TablaEscuelaCantidad>();
        public new int idEscuela { get; set; }
        public new int idCCT { get; set; }
        public new int idEvento { get; set; }
        public new int cantidad { get; set; }
    }

    /// <summary>
    /// Clase específica para los datos de la Escuela
    /// </summary>
    public class TablaEscuelaCantidad
    {
        public List<TablaEscuelaCantidad> tablaEscuelaCantida = new List<TablaEscuelaCantidad>();
        public int idEscuela { get; set; }
        public string nombreEscuela { get; set; }
        public int cantidad { get; set; }
        public new int idCCT { get; set; }
        public new int idEvento { get; set; }
    }
}
