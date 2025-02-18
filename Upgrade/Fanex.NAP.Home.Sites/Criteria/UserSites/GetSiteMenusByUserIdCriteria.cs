namespace Fanex.NAP.Home.Sites.Criteria.UserSites;

using Fanex.Data.Repository;
public class GetSiteMenusByUserIdCriteria(int userId, string environment, string menuItemName) : CriteriaBase
{
    public int UserId { get; init; } = userId;

    public string Environment { get; init; } = environment;

    public string MenuItemName { get; init; } = menuItemName;

    public override string GetSettingKey() => "NAP_Home_MainPage";
    
    public override bool IsValid() => UserId > 0;
}
