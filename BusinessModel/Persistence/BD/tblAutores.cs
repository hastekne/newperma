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
    
    public partial class tblAutores
    {
        public int idAutor { get; set; }
        public int buenaPractica { get; set; }
        public string nombreA { get; set; }
        public string pApellidoA { get; set; }
        public string mApellidoA { get; set; }
        public string correoElectronicoA { get; set; }
        public string telefonoA { get; set; }
    
        public virtual tblBuenaPractica tblBuenaPractica { get; set; }
    }
}
