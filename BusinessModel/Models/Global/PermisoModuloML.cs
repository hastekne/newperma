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
    /// Clase relacionada con la tabla tblPermisoModulo de la BD
    /// </summary>
    public class PermisoModuloML:tblPermisoModulo
    {
        public List<tblPermisoModulo> listPermisosMod = new List<tblPermisoModulo>();
        public tblPermisoModulo tabPermisosMod =new tblPermisoModulo();

        public new int idPermisoMod { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string usuario { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string modulo { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public new bool permiso { get; set; }
    }
}
