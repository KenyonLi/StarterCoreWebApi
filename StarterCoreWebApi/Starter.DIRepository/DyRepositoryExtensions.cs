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
            var assembly = Assembly.Load(options.AssemblyRepositoryString);
            var assembly2 = Assembly.Load("Starter.Service");
            var allTypes = assembly.GetTypes().Where(options.Filter);
            foreach (var t in allTypes)
            {
                if (!t.Name.EndsWith("IRepository`2"))//过滤泛型接口
                    //builder.AddSingleton(t, sp =>
                    //{
                    //    ScopeTemplateParser templateParser = new ScopeTemplateParser();
                    //    var scope = templateParser.Parse(t.Name);
                    //    var type = assembly2.GetType($"Starter.Service.{scope}Service");
                    //    return type;
                    //});

            }
            return builder;
        }

    }
}
