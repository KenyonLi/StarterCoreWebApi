//*******************************
// Create By Ahoo Wang
// Date 2019-04-23 23:30
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Starter.WebApi.Messages
{
    public class QueryByPageRequest
    {
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
        public int Offset { get { return (PageIndex - 1) * PageSize; } }
    }
}
