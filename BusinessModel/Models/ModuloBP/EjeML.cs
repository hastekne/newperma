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
    /// Clase relacionada con la tabla tblEje de la BD
    /// </summary>
    public class EjeML:tblEje
    {
        public List<tblEje> listaEjes = new List<tblEje>();
        public tblEje tabEje = new tblEje();

        public new int idEje { get; set; }

        [MaxLength(70, ErrorMessage = "Máximo 70 caracteres.")]
        [Required(ErrorMessage = "Campo obligatorio.")]
        public new string nombre { get; set; }

        public new bool estado { get; set; }
    }
}
