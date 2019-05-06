using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Starter.Entity.Domain
{
    public abstract class EntityCore
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        public Guid Guid { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 创建时间 - 默认当前时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后变化时间
        /// </summary>
        public DateTime LastChangeTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否删除 - 伪删除
        /// </summary>
        public bool IsDelete { get; set; }

     
        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();

        protected abstract void Validate();

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }
    }
}
