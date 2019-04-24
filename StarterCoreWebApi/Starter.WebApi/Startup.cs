using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Starter.Repository;
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

            //services.Add(new ServiceDescriptor(typeof(UserContext), new UserContext(Configuration.GetConnectionString("DefaultConnection"))));

            //services.AddTransient<IUserReposity, Starter.Service.UsertService>();
            RegisterRepository(services);
            RegisterService(services);
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
    }
}
