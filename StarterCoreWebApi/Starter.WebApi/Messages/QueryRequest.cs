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
    public class QueryRequest
    {
        [Range(1, 100)]
        public int Taken { get; set; }
    }
}



