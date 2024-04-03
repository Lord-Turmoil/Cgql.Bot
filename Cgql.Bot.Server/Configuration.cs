﻿namespace Cgql.Bot.Server;

/// <summary>
///     Store global configuration settings.
/// </summary>
public static class Configuration
{
    public static string Profile { get; set; } = string.Empty;
    public static string Version { get; set; } = string.Empty;
    public static string BotId { get; set; } = string.Empty;
    
    public static string ContentPath { get; set; } = string.Empty;
}