using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using Starter.WebApi.Message;
using Microsoft.AspNetCore.Authorization;

namespace Starter.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {

        private IConfiguration Configuration { get; }


        public OAuthController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> RequestToken([FromBody]TokenRequest request)
        {
            if (request != null)
            {
                //验证账号密码,这里只是为了demo，正式场景应该是与DB之类的数据源比对
                if ("kenyonli".Equals(request.UserName) && "123456".Equals(request.Password))
                {
                    var claims = new[] {
                        //加入用户的名称
                        new Claim(ClaimTypes.Name,request.UserName),
                        new Claim("Email","kenyonli@163.com")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var authTime = DateTime.UtcNow;
                    var expiresAt = authTime.AddDays(7);

                    var token = new JwtSecurityToken(
                        issuer: "kenyonli.com",
                        audience: "kenyonli.com",
                        claims: claims,
                        expires: expiresAt,//失效时间
                        signingCredentials: creds);

                    var result = await Task.Run(() =>
                    {
                        return new
                        {
                            access_token = new JwtSecurityTokenHandler().WriteToken(token),
                            token_type = "Bearer",
                            profile = new
                            {
                                name = request.UserName,
                                email="kenyonli@163.com",
                                auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                                expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                            }
                        };
                    });
                    return Ok(result);
                }
            }

            return BadRequest("Could not verify username and password.Pls check your information.");
        }
    }
}