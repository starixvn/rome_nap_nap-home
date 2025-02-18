namespace Fanex.NAP.Home.Sites.Models;
public class CommandResult(int code, string message)
{
    public int Code { get; init; } = code;
    public string Message { get; init; } = message;

    public bool IsSuccessful => Code == 0;
}
