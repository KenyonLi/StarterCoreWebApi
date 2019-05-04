using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Authorization;
using Starter.Repository;
using Starter.Service;
using Starter.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static Starter.WebApi.DIAutofacSetting.CustomAutofacAop;

namespace Starter.WebApi.DIAutofacSetting
{
    public class CustomAutofacModule : Autofac.Module
    {
        /// <summary>
        /// 依赖注入 容器
        /// </summary>
        /// <param name="containerBuilder"></param>
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(c=>new CustomAutofacAop());//注册aop
            containerBuilder.RegisterType<UserService>().As<IUserRepository>();
            containerBuilder.RegisterType<StudentService>().As<IStudentRepository>();
            containerBuilder.Register(c => new WriteDbContext());
            containerBuilder.RegisterType<ReadDbContext>().As<ReadDbContext>();
            containerBuilder.RegisterType<Business>().As<Business>();
            containerBuilder.RegisterType<A>().As<IA>().EnableInterfaceInterceptors();//aop
            //注册验证要求的处理器，可通过这种方式对同一种要求添加多种验证
            containerBuilder.RegisterType<ValidJtiHandler>().As<IAuthorizationHandler>();
        }
    }
}
