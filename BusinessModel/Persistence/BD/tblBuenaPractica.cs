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
    
    public partial class tblBuenaPractica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblBuenaPractica()
        {
            this.tblAutores = new HashSet<tblAutores>();
            this.tblImagenBP = new HashSet<tblImagenBP>();
            this.tblRevisionBP = new HashSet<tblRevisionBP>();
        }
    
        public int idBuenaPractica { get; set; }
        public int eje { get; set; }
        public string usuarioColaborador { get; set; }
        public int municipio { get; set; }
        public int idCEO { get; set; }
        public int idFuncionD { get; set; }
        public string tituloBP { get; set; }
        public string situacionMejora { get; set; }
        public string diagnosticoRealizo { get; set; }
        public string caracteristicasContexto { get; set; }
        public string descripcionActividadesRealizadas { get; set; }
        public string elementoInnovador { get; set; }
        public string resultadosObtenidos { get; set; }
        public string observacionesBP { get; set; }
        public string fuentesInformacion { get; set; }
        public string estado { get; set; }
        public int idPlantilla { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAutores> tblAutores { get; set; }
        public virtual tblCEO tblCEO { get; set; }
        public virtual tblFuncionDesarrolloP tblFuncionDesarrolloP { get; set; }
        public virtual tblMunicipio tblMunicipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblImagenBP> tblImagenBP { get; set; }
        public virtual tblPlantilla tblPlantilla { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRevisionBP> tblRevisionBP { get; set; }
        public virtual tblEje tblEje { get; set; }
        public virtual tblUsuario tblUsuario { get; set; }
    }
}
