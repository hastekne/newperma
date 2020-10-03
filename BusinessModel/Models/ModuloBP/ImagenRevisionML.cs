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
    /// Clase relacionada con la tabla tblImagenRevision determinada para los usuario Revisor de la BD
    /// </summary>
    public class ImagenRevisionML:tblImagenRevision
    {
        public List<tblImagenRevision> listImgenRevi= new List<tblImagenRevision>();
        public tblImagenRevision tabImagenRevi = new tblImagenRevision();
        public List<TabImagenRevisionBP> tabImagenRevisionBP = new List<TabImagenRevisionBP>();
        //
        public List<tblImagenRevision> listArchivoTitulo = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCSM = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCDR = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCCC = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCDAR = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCEI = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCRO = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCO = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCFI = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCG = new List<tblImagenRevision>();
        public List<tblImagenRevision> listArvhivoCA = new List<tblImagenRevision>();

        public new int idImagenComen { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public new int campoPlantilla { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public new int idRevision { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string imagen { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 15 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string titulo { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string enumeracion { get; set; }

    }

    /// <summary>
    /// Clase específica para los datos de las imagenes del usuario Revisor
    /// </summary>
    public class TabImagenRevisionBP
    {
        public List<TabImagenRevisionBP> tabImagenRevisionBP = new List<TabImagenRevisionBP>();
        public int idImagen { get; set; }
        public string figura { get; set; }
        public string Titulo { get; set; }
        public string campoBP { get; set; }
    }
}
