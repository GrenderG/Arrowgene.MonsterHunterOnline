using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

internal static class MacNativePicker
{
    public static Task<string?> PickFileAsync(string prompt)
    {
        return RunAppleScriptAsync(
            $"set selectedItem to choose file with prompt {ToAppleScriptString(prompt)}",
            "POSIX path of selectedItem");
    }

    public static Task<string?> PickFolderAsync(string prompt)
    {
        return RunAppleScriptAsync(
            $"set selectedItem to choose folder with prompt {ToAppleScriptString(prompt)}",
            "POSIX path of selectedItem");
    }

    private static async Task<string?> RunAppleScriptAsync(params string[] statements)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo("/usr/bin/osascript")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        foreach (string statement in statements)
        {
            startInfo.ArgumentList.Add("-e");
            startInfo.ArgumentList.Add(statement);
        }

        using Process process = new Process
        {
            StartInfo = startInfo
        };

        process.Start();

        Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
        Task<string> errorTask = process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        string output = (await outputTask).Trim();
        string error = (await errorTask).Trim();

        if (process.ExitCode == 0)
        {
            return string.IsNullOrWhiteSpace(output) ? null : output;
        }

        if (error.Contains("-128", StringComparison.Ordinal) ||
            error.Contains("User canceled", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        throw new InvalidOperationException($"macOS picker failed with exit code {process.ExitCode}: {error}");
    }

    private static string ToAppleScriptString(string value)
    {
        return "\"" + value.Replace("\\", "\\\\", StringComparison.Ordinal)
                          .Replace("\"", "\\\"", StringComparison.Ordinal) + "\"";
    }
}
