using System;

namespace Starter.Common.Extension
{
    public class MemoryCacheHelper
    {
        //#region Keys
        ///// <summary>
        ///// 用户菜单
        ///// </summary>
        //private const string POWER_KEY = "POWERVALUES_{0}";
        //#endregion

        //#region Power
        ///// <summary>
        ///// 设置用户菜单
        ///// </summary>
        ///// <param name="guid"></param>
        ///// <param name="menus"></param>
        //public static void SetPower(string guid, IList<LoginMenu> menus)
        //{
        //    var cache = EngineContainerFactory.Context.GetInstance<ICacheManager>();
        //    cache.Set(string.Format(POWER_KEY, guid), menus);
        //}
        ///// <summary>
        ///// 获取用户菜单
        ///// </summary>
        ///// <param name="guid"></param>
        ///// <returns></returns>
        //public static IList<LoginMenu> GetPower(string guid)
        //{
        //    var cache = EngineContainerFactory.Context.GetInstance<ICacheManager>();

        //    var result = cache.GetOrCreate(string.Format(POWER_KEY, guid), entry =>
        //    {
        //        entry.SlidingExpiration = TimeSpan.FromDays(7);//有效期 7天
        //        var service = EngineContainerFactory.Context.GetInstance<IUserService>();
        //        Guid.TryParse(guid, out Guid g);
        //        // 获取并将结果存入缓存
        //        var list = service.GetMenusByGuidAsync(g).Result;
        //        return list;
        //    });
        //    return result;
        //}
        ///// <summary>
        ///// 清除
        ///// </summary>
        ///// <param name="guid"></param>
        //public static void ClearPower(string guid)
        //{
        //    Clear(string.Format(POWER_KEY, guid));
        //}
        //#endregion

        ///// <summary>
        ///// 清除缓存
        ///// </summary>
        ///// <param name="key"></param>
        //public static void Clear(string key)
        //{
        //    var cache = EngineContainerFactory.Context.GetInstance<ICacheManager>();
        //    cache.Remove(key);
        //}
    }
}
