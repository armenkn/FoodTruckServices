using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FoodTruckServices.DataAccessLayer;
using FoodTruckServices.DataAccessLayer.Implementations;
using FoodTruckServices.Interfaces;
using FoodTruckServices.ExternalServices;
using FoodTruckServices.BusinessLayer;
using FoodTruckServices.Filters;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.Http;

namespace FoodTruckServices
{
    public class Startup
    {
        public static string ConnectionString { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            ConnectionString = Configuration.GetValue<string>("ConnectionString:DefaultConnectionString");
            env.ConfigureNLog("nlog.config");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //var builder = new ContainerBuilder();

            //builder.RegisterType<DataAccessImplementation>().As<IDataAccess>();
            //builder.RegisterType<FoodTruckCompanySqlAccessImplementation>().As<IFoodTruckCompanySqlAccess>();

            //builder.Register(x => new DataAccessImplementation(x.Resolve<IFoodTruckCompanySqlAccess>())).As<IDataAccess>();
            //builder.Register(x => new FoodTruckCompanySqlAccessImplementation()).As<IFoodTruckCompanySqlAccess>();

            //var container = builder.Build();
            //container.Resolve<IDataAccess>();
            //container.Resolve<IFoodTruckCompanySqlAccess>();

            services.AddTransient<IFoodTruckCompanySqlAccess, FoodTruckCompanySqlAccessImplementation>();
            services.AddTransient<IAddressSqlAccess, AddressSqlAccessImplementation>();
            services.AddTransient<IFoodTruckSqlAccess, FoodTruckSqlAccessImplementation>();
            services.AddTransient<ICoordinationServiceProvider, CoordinationServiceProviderImplementation>();
            services.AddTransient<IBusiness, BusinessLayerImplementation>();
            services.AddTransient<IContactSqlAccess, ContactSqlAccessImplementation>();
            services.AddTransient<IUserDataAccess, UserDataAccessImplementation>();
            services.AddTransient<ITokenProvider, JWTokenProvider>();            

            //services.AddMvc(x =>
            //{
            //    x.Filters.Add(new AuthFilter());
            //});

            services.AddScoped<AuthFilter>();

            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            app.UseMvc();          
        }
    }
}
