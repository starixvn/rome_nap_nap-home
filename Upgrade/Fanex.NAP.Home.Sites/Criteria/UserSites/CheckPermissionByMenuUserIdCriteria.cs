namespace Fanex.NAP.Home.Sites.Criteria.UserSites;

using Fanex.Data.Repository;

public class CheckPermissionByMenuUserIdCriteria(int userId, int siteId, int menuId, string environment) : NonQueryCommand
{
    public int UserId { get; init; } = userId;

    public int SiteId { get; init; } = siteId;

    public int MenuId { get; init; } = menuId;

    public string Environment { get; init; } = environment;

    public string ErrMsg { get; set; }

    public int ErrCode { get; set; }

    public override string GetSettingKey() => "NAP_Home_CheckUserPermissionByMenu";

    public override bool IsValid() => UserId > 0 && SiteId > 0 && MenuId > 0 && !string.IsNullOrEmpty(this.Environment);
}