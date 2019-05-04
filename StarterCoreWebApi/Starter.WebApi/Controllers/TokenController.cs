using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starter.Service;

namespace Starter.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly WriteDbContext _dbContext;

        public TokenController(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        //public IActionResult Get() => Json(_dbContext.BlackRecords);

        /// <summary>
        /// 使用户的 Token 失效
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete()
        {
            //// 从 payload 中提取 jti 字段
            //var jti = User.FindFirst("jti")?.Value;
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (jti == null)
            //{
            //    HttpContext.Response.StatusCode = 400;
            //    return (new { Result = false });
            //}
            //// 把这个 jti 加入数据库
            ////_dbContext.BlackRecords.Add(new BlackRecord { Jti = jti, UserId = userId });
            //_dbContext.SaveChanges();
            //return Json(new { Result = true });

            return null;
        }
    }
}
