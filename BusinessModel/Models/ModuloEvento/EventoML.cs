using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessModel.Persistence.BD;

namespace BusinessModel.Models.ModuloEvento
{
    /// <summary>
    /// Clase relacionada con la tabla tblEvento de la BD
    /// </summary>
    public class EventoML : tblEvento
    {
        public List<tblEvento> listaEventos = new List<tblEvento>();
        public List<EventoEnlace> tablaEventoEnlace = new List<EventoEnlace>();
        public List<MisRegistros> tablaMisRegistros = new List<MisRegistros>();
        public List<HomeEvento> listaEventosHome = new List<HomeEvento>();
        public List<SesionEnlace> listSesionEnlace = new List<SesionEnlace>();

        public List<tblImagenModuloBanner> listaImagenes = new List<tblImagenModuloBanner>();

        public new int idEvento { get; set; }
        public new string nombre { get; set; }
        public new string objetivo { get; set; }
        public new string rutaIcono { get; set; }
        public new System.DateTime fVigenciaInicio { get; set; }
        public new System.DateTime fVigenciaFin { get; set; }
        public new System.DateTime fRegistroInicio { get; set; }
        public new System.DateTime fRegistroFin { get; set; }
        public new string rutaPlantillaExcel { get; set; }
        public new string tipoEvento { get; set; }
        public new string estado { get; set; }
    }

    /// <summary>
    /// Clase específica para los eventos del usuario Enlace
    /// </summary>
    public class EventoEnlace
    {
        public List<EventoEnlace> tablaEventoEnlace = new List<EventoEnlace>();
        public int idEvento { get; set; }
        public string nombre { get; set; }
        public string estado { get; set; }
    }

    /// <summary>
    /// Clase específica para los registros a eventos
    /// </summary>
    public class MisRegistros
    {
        public List<MisRegistros> tablaMisRegistros = new List<MisRegistros>();
        public string idTipoRegistro { get; set; }
        public int idEvento { get; set; }
        public string nomEvento { get; set; }
    }

    /// <summary>
    /// Clase específica para el Home de eventos
    /// </summary>
    public class HomeEvento
    {
        public List<HomeEvento> listaEventosHome = new List<HomeEvento>();
        public int idEvento { get; set; }
        public string nombreEvento { get; set; }
        public string dia1R { get; set; }
        public string dia2R { get; set; }
        public string mes1R { get; set; }
        public string mes2R { get; set; }
        public string dia1E { get; set; }
        public string dia2E { get; set; }
        public string mes1E { get; set; }
        public string mes2E { get; set; }
        public int tipoFechaR { get; set; }
        public int tipoFechaE { get; set; }
        public string rutaImgIcono { get; set; }
        public string tipoEvento { get; set; }
    }

    /// <summary>
    /// Clase específica para los eventos SesionEnlace
    /// </summary>
    public class SesionEnlace
    {
        public List<SesionEnlace> listSesionEnlace = new List<SesionEnlace>();
        public int idEvento { get; set; }
        public string nombreEvento { get; set; }
        public string idTipoEventoReg { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fecahFin { get; set; }
        public string rutaImg { get; set; }
    }
}
