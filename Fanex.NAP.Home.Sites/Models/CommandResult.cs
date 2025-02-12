namespace Fanex.NAP.Home.Sites.Models
{
    public class CommandResult
    {
        public CommandResult(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }
        public string Message { get; set; }

        public bool IsSuccessful => Code == 0;

    }
}
