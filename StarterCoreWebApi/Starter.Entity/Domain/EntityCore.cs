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

        /// <summary>
        /// 乐观锁
        /// </summary>
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// 数据等级
        /// </summary>
        public int DataLevel { get; set; } = 1;


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

        public override bool Equals(object entity)
        {
            return entity is EntityCore && this == (EntityCore)entity;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(EntityCore entity1,
           EntityCore entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            if (entity1.ID.ToString() == entity2.ID.ToString())
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(EntityCore entity1,
            EntityCore entity2)
        {
            return (!(entity1 == entity2));
        }
    }
}
