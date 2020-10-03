using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using BusinessModel.Persistence.BD;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;

namespace BusinessModel.Models.Usuario
{
    /// <summary>
    /// Clase relacionada con la tabla tblUsuario de la BD
    /// </summary>
    public class UsuarioML:tblUsuario
    {
        public List<tblUsuario> listaUsuario = new List<tblUsuario>();
        public tblUsuario tabUsuario = new tblUsuario();
        public List<UsuariosRBP> tabRevisorUser = new List<UsuariosRBP>();
        public List<string> listaInsSis = new List<string>();


        [Required(ErrorMessage = "El Nombre de usuario es obligatorio.")]
        [MaxLength(15, ErrorMessage = "Máximo 15 caracteres.")]
        public new string nombreUsuario { get; set; }

        [Required(ErrorMessage ="El campo de Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public new string nombre { get; set; }

        [Required(ErrorMessage = "El campo de Apellido paterno es obligatorio.")]
        [MaxLength(30, ErrorMessage = "Máximo 30 caracteres.")]
        public new string pApellido { get; set; }
        [MaxLength(30, ErrorMessage = "Máximo 30 caracteres.")]
        public new string sApellido { get; set; }

        [Required(ErrorMessage = "El campo de Contraseña es obligatorio.")]
        //[MinLength(8,ErrorMessage ="El minimo de caracteres para la contrasenia es 8")]
        //[MaxLength(10,ErrorMessage ="El maximo de caracteres para la contasenia es de 10")]
        //El la expresion se valida que la contrasenia este conformada de minusculas, almenos una mayuscula y un  numero, sin caracteres especiales.
        //Minumo de 8 caracteres y maximo de 10
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{6,10}",ErrorMessage ="Contrasenia Invalida")]
        public new string contrasenia { get; set; }

        [Required(ErrorMessage = "El campo de Correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage ="Introducir correo valido.")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public new string correoElectronico { get; set; }

        [Required(ErrorMessage = "El campo de telefono es obligatorio")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Número telefónico no valido.")]
        //Maximo 10 caracteres para el telefono, se controla con la expresion 
        public new string telefono { get; set; }
        public new bool estado { get; set; }

        [Required(ErrorMessage ="El campo de perfil es obligatorio.")]
        public new int perfil { get; set; }
      }

    public class UsuariosRBP
    {
        public List<UsuariosRBP> listaInfo = new List<UsuariosRBP>();
        public string user { get; set; }
        public string nombre { get; set; }
        public string apP { get; set; }
        public string apM { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string instSub { get; set; }
        public bool estado { get; set; }

    }
}
