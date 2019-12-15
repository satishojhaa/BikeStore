using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BikeStore.Autofac;

namespace BikeStore.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            AutofacWebApiConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}