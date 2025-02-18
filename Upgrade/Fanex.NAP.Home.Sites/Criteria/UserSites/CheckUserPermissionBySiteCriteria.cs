
namespace Fanex.NAP.Home.Sites.Criteria.UserSites;

using Fanex.Data.Repository;

public class CheckUserPermissionBySiteCriteria(int userId, int siteId, string environment) : NonQueryCommand
{
    public int UserId { get; init; } = userId;

    public int SiteId { get; init; } = siteId;

    public string Environment { get; init; } = environment;

    public string ErrMsg { get; set; }

    public int ErrCode { get; set; }
    public override string GetSettingKey() => "NAP_Home_CheckUserPermissionBySite";

    public override bool IsValid() => UserId > 0 && SiteId > 0 && !string.IsNullOrEmpty(Environment);
}
