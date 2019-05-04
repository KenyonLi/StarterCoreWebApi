using Microsoft.AspNetCore.Authorization;
using Starter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.WebApi.Filters
{
    public class ValidJtiHandler : AuthorizationHandler<ValidJtiRequirement>
    {
        private readonly WriteDbContext _dbContext;

        public ValidJtiHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidJtiRequirement requirement)
        {
            // 检查 Jti 是否存在
            var jti = context.User.FindFirst("jti")?.Value;
            if (jti == null)
            {
                context.Fail(); // 显式的声明验证失败
                return Task.CompletedTask;
            }

            // 检查 jti 是否在黑名单
            var tokenExists = true;//_dbContext.BlackRecords.Any(r => r.Jti == jti);
            if (tokenExists)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement); // 显式的声明验证成功
            }
            return Task.CompletedTask;
        }
    }
}
