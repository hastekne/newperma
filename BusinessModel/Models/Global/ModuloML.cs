using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using System.ComponentModel.DataAnnotations;

namespace BusinessModel.Models.Global
{
    /// <summary>
    /// Clase relacionada con la tabla tblModulo de la BD
    /// </summary>
    public class ModuloML:tblModulo
    {
        public List<tblModulo> listaModulos = new List<tblModulo>();
        public tblModulo tabModulo = new tblModulo();
        public List<string> soloModulos = new List<string>();


        public new string idModulo { get; set; }

        [Required(ErrorMessage ="Campo obligatorio.")]
        public new string nombreModulo { get; set; }

        [Required(ErrorMessage ="Campo obligatorio.")]
        [MaxLength(250,ErrorMessage ="Máximo 250 caracteres.")]
        public new string titulo { get; set; }

        [Required(ErrorMessage ="Campo obligatorio.")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres para la ruta del archivo.")]
        public new string archivoIntroduccion { get; set; }
    }
}
