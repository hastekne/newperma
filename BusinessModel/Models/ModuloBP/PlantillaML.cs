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
    /// Clase relacionada con la tabla tblPlantilla de la BD
    /// </summary>
    public class PlantillaML:tblPlantilla
    {
        public List<tblPlantilla> listPlantilla = new List<tblPlantilla>();
        public tblPlantilla tabPlantilla = new tblPlantilla();

        public new int idPlantilla { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        [Required(ErrorMessage = "Campo obligatorio.")]
        public new string nombre { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Required(ErrorMessage = "Campo obligatorio.")]
        public new string descripcion { get; set; }
        public new bool estado { get; set; }
    }
}
