using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Entity.ResponseModel
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; } = "Bearer";

        public profile profile { get; set; }
    }
    public class profile : LoginResponse
    {
        public Int64 auth_time { get; set; }
        public Int64 expires_at { get; set; }
    }

    public class LoginResponse
    {
        public string LoginName { get; set; }
        public string RealName { get; set; }
        public string Guid { get; set; }
    }
}
