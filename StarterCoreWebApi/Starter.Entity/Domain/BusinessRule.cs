namespace Starter.Entity.Domain
{
    /// <summary>
    /// 业务规则
    /// </summary>
    public class BusinessRule
    {
        public BusinessRule(string property, string rule)
        {
            Property = property;
            Rule = rule;
        }
        public string Property { get; set; }

        public string Rule { get; set; }
    }
}
