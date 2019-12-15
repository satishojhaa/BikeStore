using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using BikeStore.App_Start;

namespace BikeStore
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            Bootstrapper.Run();

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
