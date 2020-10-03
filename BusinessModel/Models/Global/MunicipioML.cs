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
    /// Clase relacionada con la tabla tblMunicipio de la BD
    /// Clse de prueba los datos serán obtenidos de la API de municipios
    /// </summary>
    public class MunicipioML:tblMunicipio
    {
        public List<tblMunicipio> listMunicipios = new List<tblMunicipio>();
        public tblMunicipio tabMunicipio = new tblMunicipio();
        public new int idMunicipio { get; set; }

        [MaxLength(60, ErrorMessage = "Máximo 60 caracteres.")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public new string nombreMunicipio { get; set; }

    }
}
