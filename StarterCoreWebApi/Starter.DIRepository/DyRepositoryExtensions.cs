using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Starter.DIExtension;
using Starter.Repository;
using Starter.Repository.Infrastructure;
using Starter.Service;
using Starter.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DyRepositoryExtensions
    {

        /// <summary>
        /// 注入仓储结构 By 程序集
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="setupOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositoryFromAssembly(this IServiceCollection builder, Action<AssemblyAutoRegisterOptions> setupOptions)
        {
            var regiOptions = new AssemblyAutoRegisterOptions();
            setupOptions(regiOptions);
            builder.AddDbContext<WriteDbContext>(options => options.UseMySql(regiOptions.WriteConnectionStrings, p => p.MigrationsAssembly(regiOptions.AssemblyServiceString)));
            builder.AddDbContext<ReadDbContext>(options => options.UseMySql(regiOptions.ReadConnectionStrings, p => p.MigrationsAssembly(regiOptions.AssemblyServiceString)));

            builder.AddSingleton<IUnitOfWork, UnitOfWork>();
            builder.AddSingleton<IUserRepository, UserService>();
            return builder;
        }

    }
}
