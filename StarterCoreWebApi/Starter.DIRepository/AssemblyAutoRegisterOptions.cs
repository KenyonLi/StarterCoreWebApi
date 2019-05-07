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
        /// 写 数据库接连
        /// </summary>
        public string WriteConnectionStrings { get; set; }

        /// <summary>
        /// 读 数据库接连
        /// </summary>
        public string ReadConnectionStrings { get; set; }


    }
}