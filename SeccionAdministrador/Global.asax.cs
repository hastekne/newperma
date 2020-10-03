using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SeccionAdministrador
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Session_Start(object sender,EventArgs e)
        {
            Session["itemSelect"] = "";
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
