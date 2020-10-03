using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using System.ComponentModel.DataAnnotations;

namespace BusinessModel.Models.Usuario
{
    /// <summary>
    /// Clase relacionada con la tabla tblPerteneceInsSis de la BD
    /// </summary>
    public class PerteneceInsSisML:tblPerteneceInsSis
    {
        public List<tblPerteneceInsSis> listaInsSis = new List<tblPerteneceInsSis>();
        public tblPerteneceInsSis tabInsSis = new tblPerteneceInsSis();

        public new int idPertenece { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string idUserRevisor { get; set; }

        [MaxLength(60, ErrorMessage = "Máximo 60 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string institucionSistema { get; set; }
    }
}
