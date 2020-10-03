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
    /// Clase relacionada con la tabla tblAutores de la BD
    /// </summary>
    public class AutoresML:tblAutores
    {
        public List<tblAutores> listAutores = new List<tblAutores>();
        public tblAutores tabAutores = new tblAutores();

        public new int idAutor { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public new int buenaPractica { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string nombreA { get; set; }

        [MaxLength(30, ErrorMessage = "Máximo 30 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string pApellidoA { get; set; }

        [MaxLength(30, ErrorMessage = "Máximo 30 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string mApellidoA { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [EmailAddress(ErrorMessage = "Introducir correo valido")]
        public new string correoElectronicoA { get; set; }

        [Required(ErrorMessage = "El campo de telefono es obligatorio")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Numero telefonico no valido.")]
        public new string telefonoA { get; set; }
    }
}
