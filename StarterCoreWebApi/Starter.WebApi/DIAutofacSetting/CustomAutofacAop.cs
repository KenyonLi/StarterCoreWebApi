using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.WebApi.DIAutofacSetting
{
    public class CustomAutofacAop : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Debug.WriteLine($"{invocation.Method}");
            Debug.WriteLine($"{string.Join(",", invocation.Arguments)}");
            invocation.Proceed();//继续执行

            Debug.WriteLine($"方法{invocation.Method}执行完成了");
        }

        public interface IA
        {
            void Show(int id, string name);
        }

        [Intercept(typeof(CustomAutofacAop))]
        public class A : IA
        {
            public void Show(int id, string name)
            {
                Debug.WriteLine($"This is {id} _ {name}");
            }
        }
    }
}
