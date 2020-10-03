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
    /// Clase relacionada con la tabla tblPerfil de la BD
    /// </summary>
    public class PerfilML:tblPerfil
    {
        public List<tblPerfil> listaPerfil = new List<tblPerfil>();
        public List<tblPerfil> datosPerfil = new List<tblPerfil>();
        public tblPerfil tabperfil = new tblPerfil();

        public new int idPerfil { get; set; }
        
        [Required(ErrorMessage ="El campo es obligatorio")]
        [MaxLength(20,ErrorMessage ="Tamaño máximo de caracteres 20")]
        public new string perfil { get; set; }

        [Required(ErrorMessage ="El campo es obligatorio")]
        [MaxLength(250, ErrorMessage = "Tamaño máximo de caracteres 250")]
        public new string descripcion { get; set; }

        public new bool estado { get; set; }
    }
}
