using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Entity
{
    ///<summary>
    ///Books
    ///</summary>
    public class Books
    {
        ///<summary>        
        ///        
        ///</summary>        
        public string Id { get; set; }
        ///<summary>        
        ///类别外键id        
        ///</summary>        
        public string FKCategoryId { get; set; }
        ///<summary>        
        ///书名        
        ///</summary>        
        public string Title { get; set; }
        ///<summary>        
        ///作者        
        ///</summary>        
        public string Author { get; set; }
        ///<summary>        
        ///简介        
        ///</summary>        
        public string Summary { get; set; }
        ///<summary>        
        ///访问次数        
        ///</summary>        
        public int? Visits { get; set; }
        ///<summary>        
        ///来源        
        ///</summary>        
        public string Source { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public DateTime? CreateTime { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public DateTime? ModityTime { get; set; }
        ///<summary>        
        ///是否发布：true 表示发布 false表示不发布        
        ///</summary>        
        public bool? IsPublish { get; set; }
        ///<summary>        
        ///默认不启用        
        ///</summary>        
        public bool? IsEnable { get; set; }
    }
}
