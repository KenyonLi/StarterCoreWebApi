using Starter.Entity;
using Starter.Entity.Domain;
using Starter.Entity.Messaging;
using Starter.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Repository
{
    public interface IUserActionService:IServiceCore<UserAction>
    {

        Task<ApiResult<string>> SaveAsync(UserActionModel request);

        Task<ApiResult<Page<UserActionModel>>> GetPagesAsync(UserActionPage request);

        ApiResult<UserActionModel> GetDetail(Guid guid);

        ApiResult<string> Delete(Guid guid);

        Task<ApiResult<List<CheckModel>>> GetCheckModelAsync();
    }
}
