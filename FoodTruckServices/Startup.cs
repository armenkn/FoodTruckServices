using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using FoodTruckServices.DataAccessLayer;
using FoodTruckServices.DataAccessLayer.Implementations;

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

            services.AddTransient<IDataAccess, DataAccessImplementation>();
            services.AddTransient<IFoodTruckCompanySqlAccess, FoodTruckCompanySqlAccessImplementation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
