using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Service
{
    public class Business
    {
        private IUserRepository userRepository;
        public Business(IUserRepository userReposity)
        {
            this.userRepository = userReposity;
        }

        public void GetValue()
        {
            userRepository.Delete("dfdfd");
        }
    }
}
