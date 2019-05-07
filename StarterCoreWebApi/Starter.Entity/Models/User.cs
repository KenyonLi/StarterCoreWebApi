using Starter.Entity.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Starter.Entity
{
    [Table("user")]
    public class User
    {
        ///<summary>        
        ///主键        
        ///</summary>        
        public string Id { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public string LoginName { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public string Password { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public string Email { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public string Nickname { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public DateTime? LoginTime { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public DateTime? LastLoginTime { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public int? LoginNumber { get; set; }
        ///<summary>        
        ///        
        ///</summary>        
        public DateTime? ModityTime { get; set; }
    }
}
