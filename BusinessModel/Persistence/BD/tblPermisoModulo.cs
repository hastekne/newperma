//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessModel.Persistence.BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPermisoModulo
    {
        public int idPermisoMod { get; set; }
        public string usuario { get; set; }
        public string modulo { get; set; }
        public bool permiso { get; set; }
    
        public virtual tblModulo tblModulo { get; set; }
        public virtual tblUsuario tblUsuario { get; set; }
    }
}
