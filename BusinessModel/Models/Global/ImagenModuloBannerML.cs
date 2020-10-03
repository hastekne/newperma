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
    /// Clase relacionada con la tabla tblImagenModuloBanner de la BD
    /// </summary>
    public class ImagenModuloBannerML:tblImagenModuloBanner
    {
        public List<tblImagenModuloBanner> listaImagenes = new List<tblImagenModuloBanner>();
        public tblImagenModuloBanner tabImagen = new tblImagenModuloBanner();

        [Required(ErrorMessage = "Campo obligatorio.")]
        public new int idImgBanner { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public new string modulo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public new string imagen { get; set; }
    }
}
