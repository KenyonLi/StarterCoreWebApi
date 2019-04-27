using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Service
{
    public class Business
    {
        private IUserReposity userReposity;
        public Business(IUserReposity userReposity)
        {
            this.userReposity = userReposity;
        }

        public void GetValue() {
            userReposity.Delete("dfdfd");
        }
    }
}
