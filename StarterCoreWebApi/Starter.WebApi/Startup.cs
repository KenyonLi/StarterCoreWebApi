using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Starter.Repository;
using Starter.Service;
namespace Starter.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connection = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<MyDbContext>(options => options.UseMySql(connection));
            //services.Add(new ServiceDescriptor(typeof(UserContext), new UserContext(Configuration.GetConnectionString("DefaultConnection"))));

            //services.AddTransient<IUserReposity, Starter.Service.UserService>();

            services.AddSingleton<IUserReposity, Starter.Service.UserService>();
            services.AddSingleton<IStudentRepository, Starter.Service.StudentService>();
            services.AddSingleton<Business>();
         
        }

        public IServiceProvider ConfigureServices(
  IServiceCollection services)
        {
            services.AddMvc();
            // Add other framework services

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }


        private void RegisterRepository(IServiceCollection services)
        {
            services.AddRepositoryFromAssembly(options =>
           {
               options.AssemblyRepositoryString = "Starter.Repository";
           });
        }
        private void RegisterService(IServiceCollection services)
        {
            var assembly = Assembly.Load("Starter.Service");
            var allTypes = assembly.GetTypes();
            foreach (var type in allTypes)
            {
                services.AddSingleton(type);
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public void Reig() {
            var assemblyWeb = Assembly.GetExecutingAssembly();   
            // 自动注入    AutoInjection(services, assemblyApplication);

            
        }
    }
}
