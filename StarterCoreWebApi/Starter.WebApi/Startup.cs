using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Starter.Common;
using Starter.Repository;
using Starter.Service;
using Starter.WebApi.DIAutofacSetting;
using Starter.WebApi.Filters;
using Swashbuckle.AspNetCore.Swagger;
using static Starter.WebApi.DIAutofacSetting.CustomAutofacAop;

namespace Starter.WebApi
{
    public class Startup
    {

        const string SERVICE_NAME = "Starter.WebApi";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = "kenyonli.com",
                       ValidAudience = "kenyonli.com",
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"]))
                   };
               });
            //
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .AddRequirements(new ValidJtiRequirement()) // 添加上面的验证要求
                    .Build());
            });
            // 注册验证要求的处理器，可通过这种方式对同一种要求添加多种验证
            //services.AddSingleton<IAuthorizationHandler, ValidJtiHandler>();

            services.AddMvc(options =>
            {
                //o.Filters.Add(typeof(CustomExceptionFilterAttribute));
                //o.Filters.Add(typeof(CustomGlobalActionFilterAttribute));
                // o.Filters.Add(typeof(MyAuthorizeFilter));//
            })//全局注册filter
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //启动 Swagger
            RegisterSwagger(services);
            //services.AddSingleton<BooksService>();
            services.AddRepositoryFromAssembly(options =>
            {
                options.AssemblyServiceString = "Starter.Service";
                options.WriteConnectionStrings = Configuration["ConnectionStrings:WriteDB"];
                options.ReadConnectionStrings = Configuration["ConnectionStrings:ReadDb-1"];
            });

            //DI
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<CustomAutofacModule>();
            //替换完容器，构造控制器需要的参数，是autofac做的，但是控制器本身是ServiceCollection做的，包括内置的那几个
            containerBuilder.Populate(services);

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);

        }
        #region -原有容器-

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        //    AppSetting.WriteDB = Configuration.GetConnectionString("WriteDB");
        //    AppSetting.ReadDb = Configuration.GetConnectionString("ReadDb-1");
        //    //services.AddDbContext<MyDbContext>(options => options.UseMySql(connection));
        //    //services.Add(new ServiceDescriptor(typeof(UserContext), new UserContext(Configuration.GetConnectionString("DefaultConnection"))));

        //    //services.AddTransient<IUserReposity, Starter.Service.UserService>();
        //    //services.AddSingleton<IUserReposity, Starter.Service.UserService>();
        //    //services.AddSingleton<IStudentRepository, Starter.Service.StudentService>();
        //    services.AddSingleton<WriteDbContext>();
        //    services.AddSingleton<ReadDbContext>();
        //    services.AddSingleton<Business>();
        //    services.AddSingleton<CustomAutofacAop>();
        //    services.AddSingleton<IA, A>();
        //    RegisterSwagger(services);
        //    //RegisterRepository(services);
        //}
        #endregion
        private void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = SERVICE_NAME,
                    Version = "v1",
                    Description = "https://github.com/Smart-Kit/SmartCode"
                });
                c.CustomSchemaIds((type) => type.FullName);
                var filePath = Path.Combine(AppContext.BaseDirectory, $"{SERVICE_NAME}.xml");
                if (File.Exists(filePath))
                {
                    c.IncludeXmlComments(filePath);
                }
                //c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
                //c.OperationFilter<SwaggerDefaultValues>();
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
            });
        }
        #region -注册-

      
        private void RegisterService(IServiceCollection services)
        {
            var assembly = Assembly.Load("Starter.Service");
            var allTypes = assembly.GetTypes();
            foreach (var type in allTypes)
            {
                services.AddSingleton(type);
            }
        }
        #endregion
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
            app.UseAuthentication();//身份验证
            app.UseHttpsRedirection();
            app.UseMvc();
            ConfigureSwagger(app);
        }


        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {

            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", SERVICE_NAME);
            });
        }
    }
}
