using Starter.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Entity.Models
{
    public class UserAction : EntityCore
    {
        /// <summary>
        /// 动作名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 动作触发参数
        /// </summary>
        public string Parameter { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                AddBrokenRule(new BusinessRule(nameof(Name), "动作名称不能为空."));
        }
    }
}
