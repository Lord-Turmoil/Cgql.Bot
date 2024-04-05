namespace Cgql.Bot.Extension.Email;

public interface ICanSetBody
{
    ICanSend WithBody(string body);
}