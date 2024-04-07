namespace Cgql.Bot.Server.Exceptions;

public class ResultException : Exception
{
    public enum Types
    {
        Error,
        NotFound,
        Other
    }

    public ResultException(Types type)
    {
        Type = type;
    }

    public ResultException(Types type, string? message) : base(message)
    {
        Type = type;
    }

    public ResultException(Types type, string? message, Exception? innerException) : base(message, innerException)
    {
        Type = type;
    }

    public Types Type { get; set; }
}