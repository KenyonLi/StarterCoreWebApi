using Starter.Entity;
using Starter.Entity.RequestModel;
using Starter.Entity.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        User GetEntityById(int id);
        Task<ApiResult<TokenResponse>> LoginAsync(LoginRequest loginRequest);
    }
}
