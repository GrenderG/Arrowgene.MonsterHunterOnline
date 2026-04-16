using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

internal static class MacNativePicker
{
    public static Task<string?> PickFileAsync(string prompt, IReadOnlyList<string>? allowedExtensions = null)
    {
        string typeClause = "";
        if (allowedExtensions is { Count: > 0 })
        {
            string types = string.Join(", ", allowedExtensions.Select(e => ToAppleScriptString(e)));
            typeClause = $" of type {{{types}}}";
        }

        return RunAppleScriptAsync(
            $"set selectedItem to choose file with prompt {ToAppleScriptString(prompt)}{typeClause}",
            "POSIX path of selectedItem");
    }

    public static Task<string?> PickFolderAsync(string prompt)
    {
        return RunAppleScriptAsync(
            $"set selectedItem to choose folder with prompt {ToAppleScriptString(prompt)}",
            "POSIX path of selectedItem");
    }

    public static Task<string?> PickFileOrFolderAsync(string prompt)
    {
        // AppleScript dialog with buttons for File or Folder
        return RunAppleScriptAsync(
            $"set choice to button returned of (display dialog {ToAppleScriptString(prompt)} buttons {{\"Cancel\", \"Folder\", \"File\"}} default button \"Folder\")",
            "if choice is \"File\" then",
            $"  set selectedItem to choose file with prompt {ToAppleScriptString(prompt)}",
            "else",
            $"  set selectedItem to choose folder with prompt {ToAppleScriptString(prompt)}",
            "end if",
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
