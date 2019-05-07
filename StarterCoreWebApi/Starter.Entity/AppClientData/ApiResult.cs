namespace Starter.Entity
{
    /// <summary>
    /// API 返回JSON字符串
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    public class ApiResult<TBody> where TBody : class
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// 数据集
        /// </summary>
        public TBody Body { get; set; }
    }
}
