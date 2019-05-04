using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starter.Entity;

namespace Starter.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        //private readonly IUserService _service;

        //public AccountController(IUserService service)
        //{
        //    _service = service;
        //}
        //// 登录
        //[HttpPost("login")]
        //public async Task<ApiResult<LoginResponse>> Login([FromBody]LoginRequest request)
        //{
        //    var response = await _service.LoginAsync(request);
        //    if (response.success)
        //    {
        //        var token = GetToken(response.data.Guid);
        //        response.data.SignToken = token.SignToken;
        //    }
        //    return response;
        //}
        //// 登出
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