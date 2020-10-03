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
    /// Clase relacionada con la tabla tblCEO
    /// Clase termporal para pruebas, el verdadero dato se obtendra de la API de CEO
    /// </summary>
    public class CEOML:tblCEO
    {
        public List<tblCEO> listCEO = new List<tblCEO>();
        public tblCEO tabCEP = new tblCEO();
        public new int idCEO { get; set; }

        [MaxLength(15, ErrorMessage = "Máximo 15 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string claveCentroTrabajo { get; set; }

        [MaxLength(150, ErrorMessage = "Máximo 150 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string nombreCentroTrabajo { get; set; }
    }
}
