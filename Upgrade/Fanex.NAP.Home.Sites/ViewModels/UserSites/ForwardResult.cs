namespace Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.NAP.Home.Sites.Models;

public class ForwardResult
{
    public ForwardResult(CommandResult result)
    {
        if (result != null)
        {
            ErrorCode = result.Code;
            ErrorMessage = result.Message;
        }
    }

    public string Link { get; set; }

    public int ErrorCode { get; init; }

    public string ErrorMessage { get; init; }
}
