using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//
using AutoMapper;
using DesarrolloProyDSP.Helpers;
using BusinessModel.MapperProfile;

namespace DesarrolloProyDSP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Session_Start(object sender, EventArgs e)
        {
            Session["PassGenerada"] = "";
            Session["tituloBP"] = "";
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.Initialize(cfg =>
            {

                cfg.AddProfile<AutoMapperProfile>();
                cfg.AddProfile<BusinessMapperProfile>();

            });
        }
    }
}
