using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BusinessLogic;
using BusinessLogic.MappingProfiles;
using DAO.Transactions;

namespace BikeStore.Autofac
{
    public static class AutofacWebApiConfig
    {
        private static IContainer _container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config,RegisterServices(new ContainerBuilder()));
        }

        private static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<BikeBL>().As<IBikeBL>().InstancePerRequest();
            builder.RegisterType<CategoriesBL>().As<ICategoriesBl>().InstancePerRequest();
            builder.RegisterType<BrandBL>().As<IBrandBl>().InstancePerRequest();
            builder.RegisterType<LogBL>().As<ILogBl>().InstancePerRequest();

            builder.RegisterType<BikeDal>().As<IBikeDal>().InstancePerLifetimeScope();
            builder.RegisterType<CategoriesDal>().As<ICategoriesDal>().InstancePerLifetimeScope();
            builder.RegisterType<BrandDal>().As<IBrandDal>().InstancePerLifetimeScope();
            builder.RegisterType<LogDal>().As<ILogDal>().InstancePerLifetimeScope();
            builder.RegisterType<MappingService>().As<IMappingService>().SingleInstance();
            _container = builder.Build();
            return _container;
        }
    }
}