namespace Cgql.Bot.Extension.Email;

public interface ICanSetToEmail
{
    ICanSetToName To(string email);
}