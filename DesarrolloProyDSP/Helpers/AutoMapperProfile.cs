using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using AutoMapper;
using BusinessModel.Models.Usuario;
using DesarrolloProyDSP.Models.Usuario;

namespace DesarrolloProyDSP.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UsuarioViewModel, UsuarioML>();
            CreateMap<UsuarioViewModel, UsuarioML>().ReverseMap();
        }
    }
}