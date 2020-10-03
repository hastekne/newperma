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
    /// Clase relacionada con la tabla tblImagen determinada para los colaboradores de la BD
    /// </summary>
    public class ImagenBPML:tblImagenBP
    {
        public List<tblImagenBP> listImagenBP = new List<tblImagenBP>();
        public tblImagenBP tabImagen = new tblImagenBP();
        public List<TabImagenBP> listaTabImagenBP = new List<TabImagenBP>();
        // Listas para las imagenes
        public List<tblImagenBP> listArvhivoSMT = new List<tblImagenBP>();
        public List<tblImagenBP> listArvhivoDR = new List<tblImagenBP>();
        public List<tblImagenBP> listArvhivoCC = new List<tblImagenBP>();
        public List<tblImagenBP> listArvhivoDAR = new List<tblImagenBP>();
        public List<tblImagenBP> listArvhivoEI = new List<tblImagenBP>();
        public List<tblImagenBP> listArvhivoRO = new List<tblImagenBP>();
        public List<tblImagenBP> listArvhivoO = new List<tblImagenBP>();
        public List<tblImagenBP> listArvhivoFI = new List<tblImagenBP>();
        public List<tblImagenBP> listArvhivoA = new List<tblImagenBP>();

        public new int idImagen { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public new int buenaPractica { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public new int campoBP { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string imagen { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string titulo { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 15 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string enumeracion { get; set; }
    }

    /// <summary>
    /// Clase específica para los datos de la imagen
    /// </summary>
    public class TabImagenBP
    {
        public List<TabImagenBP> tabImagenBP = new List<TabImagenBP>();
        public int idImagen { get; set; }
        public string figura { get; set; }
        public string Titulo { get; set; }
        public string campoBP { get; set; }
    }
}
