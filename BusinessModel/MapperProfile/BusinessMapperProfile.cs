using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using AutoMapper;
using BusinessModel.Models.Usuario;
using BusinessModel.Models.Global;
using BusinessModel.Models.ModuloBP;
using BusinessModel.Models.ModuloEvento;

using BusinessModel.Persistence.BD;

namespace BusinessModel.MapperProfile
{
    /// <summary>
    /// Clase con el Mapeo de las clases en Models y tablas de la BD en Persistence.BD
    /// </summary>
    public class BusinessMapperProfile : Profile
    {
        public BusinessMapperProfile()
        {
            CreateMap<UsuarioML, tblUsuario>();
            CreateMap<UsuarioML, tblUsuario>().ReverseMap();

            CreateMap<PerfilML, tblPerfil>();
            CreateMap<PerfilML, tblPerfil>().ReverseMap();

            CreateMap<ModuloML, tblModulo>();
            CreateMap<ModuloML, tblModulo>().ReverseMap();

            CreateMap<ImagenModuloBannerML, tblImagenModuloBanner>();
            CreateMap<ImagenModuloBannerML, tblImagenModuloBanner>().ReverseMap();

            CreateMap<EjeML, tblEje>();
            CreateMap<EjeML, tblEje>().ReverseMap();

            CreateMap<BuenasPracticasML, tblBuenaPractica>();
            CreateMap<BuenasPracticasML, tblBuenaPractica>().ReverseMap();

            CreateMap<FuncionDesarrolloPML, tblFuncionDesarrolloP>();
            CreateMap<FuncionDesarrolloPML, tblFuncionDesarrolloP>().ReverseMap();

            CreateMap<PermisoModuloML, tblPermisoModulo>();
            CreateMap<PermisoModuloML, tblPermisoModulo>().ReverseMap();

            CreateMap<PerteneceInsSisML, tblPerteneceInsSis>();
            CreateMap<PerteneceInsSisML, tblPerteneceInsSis>().ReverseMap();

            CreateMap<MunicipioML, tblMunicipio>();
            CreateMap<MunicipioML, tblMunicipio>().ReverseMap();

            CreateMap<CampoBPML, tblCampoBP>();
            CreateMap<CampoBPML, tblCampoBP>().ReverseMap();

            CreateMap<CEOML, tblCEO>();
            CreateMap<CEOML, tblCEO>().ReverseMap();

            CreateMap<ImagenBPML, tblImagenBP>();
            CreateMap<ImagenBPML, tblImagenBP>().ReverseMap();

            CreateMap<AutoresML, tblAutores>();
            CreateMap<AutoresML, tblAutores>().ReverseMap();

            CreateMap<ImagenRevisionML, tblImagenRevision>();
            CreateMap<ImagenRevisionML, tblImagenRevision>().ReverseMap();

            CreateMap<RevisionBPML, tblRevisionBP>();
            CreateMap<RevisionBPML, tblRevisionBP>().ReverseMap();

            CreateMap<PlantillaML, tblPlantilla>();
            CreateMap<PlantillaML, tblPlantilla>().ReverseMap();

            CreateMap<EventoML, tblEvento>();
            CreateMap<EventoML, tblEvento>().ReverseMap();

            CreateMap<FuncionParticipanteML, tblFuncionParticipante>();
            CreateMap<FuncionParticipanteML, tblFuncionParticipante>().ReverseMap();

            CreateMap<SubsistemaEventoML, tblSubsistemaEvento>();
            CreateMap<SubsistemaEventoML, tblSubsistemaEvento>().ReverseMap();

            CreateMap<MesaEventoML, tblMesaTrabEvento>();
            CreateMap<MesaEventoML, tblMesaTrabEvento>().ReverseMap();

            CreateMap<EscuelaEventoML, tblEscuelaEvento>();
            CreateMap<EscuelaEventoML, tblEscuelaEvento>().ReverseMap();

            CreateMap<MunicipioEventoML, tblMunicipioEvento>();
            CreateMap<MunicipioEventoML, tblMunicipioEvento>().ReverseMap();

            CreateMap<MesaParticipanteML, tblMesaTrabParticipante>();
            CreateMap<MesaParticipanteML, tblMesaTrabParticipante>().ReverseMap();

            CreateMap<CampoEventoML, tblCampoEvento>();
            CreateMap<CampoEventoML, tblCampoEvento>().ReverseMap();

            CreateMap<RegistroEventoML, tblRegistroEvento>();
            CreateMap<RegistroEventoML, tblRegistroEvento>().ReverseMap();

            CreateMap<TipoRegistroML, tblTipoRegistro>();
            CreateMap<TipoRegistroML, tblTipoRegistro>().ReverseMap();

            CreateMap<UserEnlaceEventoML, tblUserEnlaceEvento>();
            CreateMap<UserEnlaceEventoML, tblUserEnlaceEvento>().ReverseMap();
        }
    }
}
