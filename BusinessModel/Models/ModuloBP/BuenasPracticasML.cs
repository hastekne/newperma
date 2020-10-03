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
    /// Clase relacionada con la tabla tblBuenaPractica de la BD
    /// </summary>
    public class BuenasPracticasML:tblBuenaPractica
    {
        public List<tblBuenaPractica> listaBP = new List<tblBuenaPractica>();
        public List<BPInformacion> listaBPNoFK = new List<BPInformacion>();
        public List<BPHomeTable> listaHomeBP = new List<BPHomeTable>();
        public List<DatosGeneralesBP> datosGeneralesBP = new List<DatosGeneralesBP>();
        public List<AutorBP> autorResponsableBP = new List<AutorBP>();
        public List<AutorBP> autoresBP = new List<AutorBP>();
        //Para cargar en el home de la BP las imagenes del Banner 
        public List<tblImagenModuloBanner> listaImagenes = new List<tblImagenModuloBanner>();

        public tblBuenaPractica tabBP = new tblBuenaPractica();

        public new int idBuenaPractica { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public new int eje { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public new string usuarioColaborador { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public new int municipio { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio.")]
        public new int idCEO { get; set; }

        
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public new string tituloBP { get; set; }

        [MaxLength(1500, ErrorMessage = "Máximo 1500 caracteres.")]
        public new string situacionMejora { get; set; }

        [MaxLength(3000, ErrorMessage = "Máximo 3000 caracteres.")]
        public new string diagnosticoRealizo { get; set; }

        [MaxLength(3000, ErrorMessage = "Máximo 3000 caracteres.")]
        public new string caracteristicasContexto { get; set; }

        [MaxLength(9000, ErrorMessage = "Máximo 9000 caracteres.")]
        public new string descripcionActividadesRealizadas { get; set; }

        [MaxLength(1500, ErrorMessage = "Máximo 1500 caracteres.")]
        public new string elementoInnovador { get; set; }

        [MaxLength(3000, ErrorMessage = "Máximo 3000 caracteres.")]
        public new string resultadosObtenidos { get; set; }

        [MaxLength(1500, ErrorMessage = "Máximo 1500 caracteres.")]
        public new string observacionesBP { get; set; }

        public new string fuentesInformacion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public new string idFuncionD { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public new string estado { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public new int idPlantilla { get; set; }

    }

    /// <summary>
    /// Clase específica para la información de una Buena práctica
    /// </summary>
    public class BPInformacion
    {
        public List<BPInformacion> listaInfo = new List<BPInformacion>();
        public string titulo { get; set; }
        public string eje { get; set; }
        public string estado { get; set; }
    }

    /// <summary>
    /// Clase específica para la obtencion de los datos desplegados en el Home de la plataforma
    /// sobre una Buena práctica
    /// </summary>
    public class BPHomeTable
    {
        public List<BPHomeTable> listaHomeBP = new List<BPHomeTable>();
        public int idBP { get; set; }
        public string tituloBP { get; set; }
        public string eje { get; set; }
        public int idPantilla { get; set; }
        public Nullable<System.DateTime> fechaPublicacion { get; set; }
    }

    /// <summary>
    /// Clase específica para los datos generales de una buena práctica
    /// </summary>
    public class DatosGeneralesBP
    {
        public List<DatosGeneralesBP> datosGeneralesBP = new List<DatosGeneralesBP>();
        public string eje { get; set; }
        public string claveCT { get; set; }
        public string nombreCT { get; set; }
        public string municipio { get; set; }
        public string funcion { get; set; }
    }

    /// <summary>
    /// Clase específica para los responsables de una buena práctica
    /// </summary>
    public class AutorBP
    {
        public List<AutorBP> autorResponsableBP = new List<AutorBP>();
        public string nombreCompleto { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
    }
    
}
