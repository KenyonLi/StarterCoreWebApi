using Microsoft.EntityFrameworkCore;
using Starter.Common.Extension;
using Starter.Entity;
using Starter.Entity.Domain;
using Starter.Entity.Messaging;
using Starter.Entity.Models;
using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Service.Implements
{
    public partial class UserActionService : ServiceCore<UserAction>, IUserActionService
    {

        public UserActionService(
            MyDbContext context
            ) : base(context )
        {
        }

        public ApiResult<string> Delete(Guid guid)
        {
            ApiResult<string> response = new ApiResult<string>();
            try
            {

                var school = Query().Where(n => n.Guid == guid).FirstOrDefault();
                if (school == null)
                {
                    response.Message = "未找到改数据！";
                    return response;
                }
                DeleteForge(school);
                var b = _unitofwork.Commit();
                response.IsSuccess = b;
                response.Message = b ? "删除成功！" : "删除失败！";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "删除失败！意外错误:" + ex.Message;
                return response;
            }
        }

        public async Task<ApiResult<List<CheckModel>>> GetCheckModelAsync()
        {
            ApiResult<List<CheckModel>> response = new ApiResult<List<CheckModel>>();
            try
            {

                var check = await Query().Select(n => new CheckModel
                {
                    label = n.Name,
                    value = n.ID
                }).ToListAsync();
                response.IsSuccess = true;
                response.Body = check;
                response.Message = "获取成功！";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "获取失败！意外错误:" + ex.Message;
                return response;
            }
        }
        public ApiResult<UserActionModel> GetDetail(Guid guid)
        {
            ApiResult<UserActionModel> response = new ApiResult<UserActionModel>();
            try
            {

                var entity = Query().Where(n => n.Guid == guid)
                    .Select(n => new UserActionModel
                    {
                        CreateTime = n.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        guid = n.Guid.ToString(),
                        Name = n.Name,
                        Parameter = n.Parameter,
                        Remark = n.Remark
                    }).FirstOrDefault();
                if (entity == null)
                {
                    response.Message = "未找到改数据！";
                    return response;
                }
                response.IsSuccess = true;
                response.Body = entity;
                response.Message = "获取成功！";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "获取失败！意外错误:" + ex.Message;
                return response;
            }
        }

        public async Task<ApiResult<Page<UserActionModel>>> GetPagesAsync(UserActionPage request)
        {
            ApiResult<Page<UserActionModel>> response = new ApiResult<Page<UserActionModel>>();
            try
            {
                var pages = await Query()
                            .HasWhere(request.ActionName, n => n.Name.Contains(request.ActionName))
                            .Select(n => new UserActionModel
                            {
                                CreateTime = n.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                guid = n.Guid.ToString(),
                                Name = n.Name,
                                Parameter = n.Parameter,
                                Remark = n.Remark
                            }).OrderByDescending(n => n.CreateTime).ToPageAsync(request.PageIndex, request.PageSize);

                response.IsSuccess = true;
                response.Message = "获取成功！";
                response.Body = pages;
                return response;

            }
            catch (Exception ex)
            {
                response.Message = "获取失败！意外错误：" + ex.Message;
                return response;
            }
        }



        public async Task<ApiResult<string>> SaveAsync(UserActionModel request)
        {
            ApiResult<string> response = new ApiResult<string>();
            try
            {
                if (string.IsNullOrEmpty(request.guid))
                {
                    UserAction model = new UserAction();

                    model.Parameter = request.Parameter;
                    model.Remark = request.Remark;
                    model.Name = request.Name;
                    await _unitofwork.RegisterNewAsync(model);
                }
                else
                {
                    var sguid = Guid.Parse(request.guid);
                    var model = Query().Where(n => n.Guid == sguid).FirstOrDefault();
                    if (model == null)
                    {
                        throw new Exception("未找到修改数据！！");
                    }

                    model.Parameter = request.Parameter;
                    model.Remark = request.Remark;
                    model.Name = request.Name;
                    _unitofwork.RegisterDirty(model);

                }

                var b = await _unitofwork.CommitAsync();

                response.IsSuccess = b;
                response.Message = b ? "保存成功！" : "保存失败";
                return response;
            }
            catch (Exception ex)
            {

                response.Message = "发生错误：" + ex.Message;
                return response;
            }
        }
    }
}
