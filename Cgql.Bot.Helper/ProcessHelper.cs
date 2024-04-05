using System.Diagnostics;

namespace Cgql.Bot.Helper;

public static class ProcessHelper
{
    public static int Exec(string file, params string[] args)
    {
        var processInfo = new ProcessStartInfo(file, args) {
            CreateNoWindow = true
        };

        Process process = Process.Start(processInfo) ?? throw new Exception("Failed to start process.");

        process.WaitForExit();
        return process.ExitCode;
    }

    public static int ExecAt(string cwd, string file, params string[] args)
    {
        var processInfo = new ProcessStartInfo(file, args) {
            CreateNoWindow = true,
            WorkingDirectory = cwd
        };

        Process process = Process.Start(processInfo) ?? throw new Exception("Failed to start process.");

        process.WaitForExit();
        return process.ExitCode;
    }
}