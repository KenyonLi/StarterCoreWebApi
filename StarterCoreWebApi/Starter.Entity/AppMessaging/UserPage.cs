using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Entity.Messaging
{
    public class UserActionPage : GetPagingRequest
    {
        public UserActionPage(int pageIndex, int pageSize) : base(pageIndex, pageSize)
        {

        }
        public string ActionName { get; set; }
    }
}
