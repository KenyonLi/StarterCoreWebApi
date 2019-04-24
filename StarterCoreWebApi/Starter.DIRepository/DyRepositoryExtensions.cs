using Microsoft.Extensions.DependencyInjection;
using Starter.DIExtension;
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
            var options = new AssemblyAutoRegisterOptions
            {
                Filter = (type) => type.IsInterface
            };
            setupOptions(options);
            ScopeTemplateParser templateParser = new ScopeTemplateParser();
            var assembly = Assembly.Load(options.AssemblyRepositoryString);
            var allTypes = assembly.GetTypes().Where(options.Filter);
            foreach (var type in allTypes)
            {
                if (!type.Name.EndsWith("IRepository`2"))//过滤泛型接口
                    builder.AddSingleton(type, sp =>
                    {
                        var factory = sp.GetRequiredService<IRepositoryFactory>();
                        return factory.CreateInstance(type);
                    });
            }
            return builder;
        }

    }
}
