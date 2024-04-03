using Snowflake.Core;

namespace Cgql.Bot.Helper;

public static class SnowflakeHelper
{
    private static readonly IdWorker _impl = new(1, 1);

    public static long NextId()
    {
        return _impl.NextId();
    }
}