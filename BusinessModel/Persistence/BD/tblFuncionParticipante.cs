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
    
    public partial class tblFuncionParticipante
    {
        public int idFuncionPart { get; set; }
        public int idEvento { get; set; }
        public string nomFuncion { get; set; }
    
        public virtual tblEvento tblEvento { get; set; }
    }
}
