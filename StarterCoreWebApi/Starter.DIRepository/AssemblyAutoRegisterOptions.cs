using System;

namespace Starter.DIExtension
{
    public class AssemblyAutoRegisterOptions
    {
        /// <summary>
        /// 服务程序集
        /// </summary>
        public string AssemblyServiceString { get; set; }
        /// <summary>
        /// 仓储接口程序集
        /// </summary>
        public string AssemblyRepositoryString { get; set; }

        /// <summary>
        /// 仓储接口筛选器
        /// </summary>
        public Func<Type, bool> Filter { get; set; }

        public void UseTypeFilter<T>()
        {
            Filter = (type) => typeof(T).IsAssignableFrom(type);
        }
    }
}