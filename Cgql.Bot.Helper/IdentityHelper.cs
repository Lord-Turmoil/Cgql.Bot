using System.Security.Cryptography;
using Snowflake.Core;

namespace Cgql.Bot.Helper;

public static class IdentityHelper
{
    private static readonly IdWorker _impl = new(1, 1);

    public static long NextId()
    {
        return _impl.NextId();
    }


    public static string NextIdString(int length = 15)
    {
        byte[] randomNumber = new byte[length];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
        }

        return Convert.ToBase64String(randomNumber);
    }
}