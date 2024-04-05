namespace Cgql.Bot.Extension.Email;

public interface ICanSetSubject
{
    ICanSetBody WithSubject(string subject);
}