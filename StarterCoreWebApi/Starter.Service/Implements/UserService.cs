using Starter.Entity;
using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Starter.Entity.RequestModel;
using Starter.Entity.ResponseModel;
using Starter.Common;

namespace Starter.Service
{
    public class UserService : BaseServiceCore<User>, IUserRepository
    {
        public UserService(WriteDbContext writeContext, ReadDbContext readContext) : base(writeContext, readContext)
        {
        }

        public User GetEntityById(int id)
        {
            throw new NotImplementedException();
        }
        //public User GetEntityById(int id)
        //{
        //    Query().FirstOrDefault(p => p.Id == id);
        //}

        public void Inset()
        {
            Insert(new User { });
        }

        public async Task<ApiResult<TokenResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var response = new ApiResult<TokenResponse>
            {
                StatusCode = "200"
            };
            try
            {
                var entity = await Query(p => true).FirstOrDefaultAsync(p => p.LoginName == loginRequest.LoginName);
                if (entity == null || entity.Password != loginRequest.Password.DESEncrypt())
                {
                    response.Message = "帐户或密码错误.";
                    return response;
                }
                response.Message = "登录成功.";
                response.IsSuccess = true;
                response.Body.profile = new profile
                {
                    Guid = entity.Id,
                    LoginName = entity.LoginName,
                    RealName = entity.Nickname
                };
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.StatusCode = "500";
            }
            return response;
        }
    }
}
