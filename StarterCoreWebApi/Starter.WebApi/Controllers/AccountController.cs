using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Starter.Entity;
using Starter.Entity.RequestModel;
using Starter.Entity.ResponseModel;
using Starter.Repository;

namespace Starter.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _service;
        private IConfiguration _configuration { get; }

        public AccountController(IUserRepository service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
        // 登录
        [HttpPost("login")]
        public async Task<ApiResult<TokenResponse>> Login([FromBody]LoginRequest request)
        {
            var response = await _service.LoginAsync(request);
            if (response.IsSuccess)
            {
                var claims = new[] {
                        //加入用户的名称
                        new Claim(ClaimTypes.Name,response.Body.profile.LoginName),
                        new Claim(nameof(response.Body.profile.LoginName),response.Body.profile.LoginName)
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var authTime = DateTime.UtcNow;
                var expiresAt = authTime.AddDays(7);

                var token = new JwtSecurityToken(
                    issuer: "kenyonli.com",
                    audience: "kenyonli.com",
                    claims: claims,
                    expires: expiresAt,//失效时间
                    signingCredentials: creds);

                response.Body.access_token = new JwtSecurityTokenHandler().WriteToken(token);
                response.Body.token_type = "Bearer";
                response.Body.profile.auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds();
                response.Body.profile.expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds();
            }
            return response;
        }
        // 登出
        //[HttpPost("logout")]
        //public ApiResult<string> Logout([FromBody]LogoutReqeust reqeust)
        //{
        //    var result = new ApiResult<string>();
        //    try
        //    {
        //        ClearToken(reqeust.Guid);
        //        result.message = "注销成功.";
        //        result.statusCode = 200;
        //        result.success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.statusCode = (int)StatusCodeEnum.Error;
        //        result.message = StatusCodeEnum.Error.GetEnumText() + ":" + ex.Message;
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 根据用户获取token
        ///// </summary>
        ///// <param name="staffId"></param>
        ///// <returns></returns>
        //private Token GetToken(Guid staffId)
        //{
        //    var cacheManager = EngineContainerFactory.Context.GetInstance<ICacheManager>();
        //    ClearToken(staffId);
        //    //缓存有取缓存，缓存没有生成存入缓存
        //    var token = cacheManager.GetOrCreate(staffId.ToString(),
        //        entry => new Token
        //        {
        //            StaffId = staffId,
        //            SignToken = Guid.NewGuid(),
        //            ExpireTime = DateTime.Now.AddDays(1)//一天过期
        //        });
        //    //返回token信息
        //    return token;
        //}
        ///// <summary>
        ///// 清除Token
        ///// </summary>
        ///// <param name="staffId"></param>
        //private void ClearToken(Guid staffId)
        //{
        //    var cacheManager = EngineContainerFactory.Context.GetInstance<ICacheManager>();
        //    // 清除Token缓存
        //    cacheManager.Remove(staffId.ToString());
        //    // 清除权限缓存
        //    MemoryCacheHelper.ClearPower(staffId.ToString());
        //}
    }
}