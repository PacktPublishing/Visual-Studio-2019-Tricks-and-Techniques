using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using CGHClientServer1.Repository;
using CGHClientServer1.Repository.Interface;
using CGHClientServer1.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.IO;
using AutoMapper;
using CGHClientServer1.DataAccess.Mapper;
using CodeGenHero.WebApi;

namespace CGHClientServer1.WebApi
{
    public static class WebApiConfig
    {
        public static UnityContainer _container;

        public static void Register(HttpConfiguration config)
        {
            _container = new UnityContainer();

            // DbContextOptions<CountryStateCityDbContext>
            // CountryStateCityDbContext

            LogConfig.RegisterLogger(); // ILogger
            _container.RegisterInstance<ILoggerFactory>(LogConfig.LoggerFactory, new ContainerControlledLifetimeManager());
            _container.RegisterFactory(typeof(ILogger<>), null, (container, type, name) =>
            {
                var factory = container.Resolve<ILoggerFactory>();
                var genericType = type.GetGenericArguments().First();
                var methodInfo = typeof(Microsoft.Extensions.Logging.LoggerFactoryExtensions).GetMethods().Single(m => m.Name == "CreateLogger" && m.IsGenericMethodDefinition);
                var genericMi = methodInfo.MakeGenericMethod(type.GetGenericArguments().First());
                var logger = genericMi.Invoke(null, new[] { factory });
                return logger;
            });

            string connectionString = ConfigurationManager.ConnectionStrings["CountryStateCityDbConn"].ConnectionString;
            var dbConnBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionString = dbConnBuilder.ToString();

            var optionsBuilder = new DbContextOptionsBuilder<CountryStateCityDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            _container.RegisterInstance(optionsBuilder.Options);

            _container.RegisterType<ICSCRepository, CSCRepository>();

            IMapper mapper = AutoMapperInitializer.Initialize();
            _container.RegisterInstance<IMapper>(mapper, InstanceLifetime.Singleton);

            config.DependencyResolver = new UnityResolver(_container);

            // Web API configuration and services

            #region Custom Controller Selector

            // Web API routes
            config.MapHttpAttributeRoutes(new InheritableDirectRouteProvider()); // Allow for inheritance of the RoutePrefix attribute (see: stackoverflow.com/questions/19989023/net-webapi-attribute-routing-and-inheritance)

            #endregion Custom Controller Selector

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}