using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;

namespace Arrowgene.MonsterHunterOnline.UI.Infrastructure;

internal static class GlobalExceptionHandler
{
    private static readonly object WriteSync = new();
    private static int _dialogVisible;
    private static bool _registered;

    public static void Register()
    {
        if (_registered)
        {
            return;
        }

        _registered = true;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        Dispatcher.UIThread.UnhandledExceptionFilter += DispatcherOnUnhandledExceptionFilter;
        Dispatcher.UIThread.UnhandledException += DispatcherOnUnhandledException;
    }

    public static void HandleStartupException(Exception exception)
    {
        Report("Startup", exception, isTerminating: true, canContinue: false);
    }

    private static void CurrentDomainOnUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        Exception exception = e.ExceptionObject as Exception
            ?? new Exception($"Unhandled non-exception object: {e.ExceptionObject}");
        Report("AppDomain", exception, e.IsTerminating, canContinue: false);
    }

    private static void TaskSchedulerOnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        if (IsCancellationException(e.Exception))
        {
            e.SetObserved();
            return;
        }

        Report("TaskScheduler", e.Exception, isTerminating: false, canContinue: true);
        e.SetObserved();
    }

    private static void DispatcherOnUnhandledExceptionFilter(object? sender, DispatcherUnhandledExceptionFilterEventArgs e)
    {
        if (IsCancellationException(e.Exception))
        {
            e.RequestCatch = false;
            return;
        }

        e.RequestCatch = true;
    }

    private static void DispatcherOnUnhandledException(object? sender, DispatcherUnhandledExceptionEventArgs e)
    {
        if (IsCancellationException(e.Exception))
        {
            e.Handled = true;
            return;
        }

        Report("UI Thread", e.Exception, isTerminating: false, canContinue: true);
        e.Handled = true;
    }

    private static void Report(string source, Exception exception, bool isTerminating, bool canContinue)
    {
        string logPath = WriteCrashReport(source, exception, isTerminating);
        TryShowExceptionWindow(source, exception, logPath, isTerminating, canContinue);
        Console.Error.WriteLine(BuildConsoleMessage(source, exception, logPath, isTerminating));
    }

    private static string WriteCrashReport(string source, Exception exception, bool isTerminating)
    {
        foreach (string directory in GetCrashDirectories())
        {
            try
            {
                Directory.CreateDirectory(directory);
                string timestamp = DateTimeOffset.Now.ToString("yyyyMMdd-HHmmssfff");
                string logPath = Path.Combine(directory, $"crash-{timestamp}.log");
                string latestPath = Path.Combine(directory, "last-crash.log");
                string report = BuildCrashReport(source, exception, logPath, isTerminating);

                lock (WriteSync)
                {
                    File.WriteAllText(logPath, report, Encoding.UTF8);
                    File.WriteAllText(latestPath, report, Encoding.UTF8);
                }

                return logPath;
            }
            catch
            {
                // Try the next location.
            }
        }

        return "Failed to write crash report";
    }

    private static string BuildCrashReport(string source, Exception exception, string logPath, bool isTerminating)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Arrowgene.MonsterHunterOnline.UI crash report");
        builder.AppendLine($"Timestamp: {DateTimeOffset.Now:O}");
        builder.AppendLine($"Source: {source}");
        builder.AppendLine($"Terminating: {isTerminating}");
        builder.AppendLine($"Process Path: {Environment.ProcessPath ?? "unknown"}");
        builder.AppendLine($"Base Directory: {AppContext.BaseDirectory}");
        builder.AppendLine($"Log Path: {logPath}");
        builder.AppendLine($"OS: {Environment.OSVersion}");
        builder.AppendLine($".NET: {Environment.Version}");
        builder.AppendLine();
        builder.AppendLine(exception.ToString());
        return builder.ToString();
    }

    private static string BuildConsoleMessage(string source, Exception exception, string logPath, bool isTerminating)
    {
        return $"[{source}] {(isTerminating ? "fatal" : "handled")} exception: {exception.Message}{Environment.NewLine}Crash log: {logPath}";
    }

    private static string[] GetCrashDirectories()
    {
        return
        [
            Path.Combine(AppContext.BaseDirectory, "Crashes"),
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Arrowgene.MonsterHunterOnline",
                "UI",
                "Crashes"),
            Path.Combine(Path.GetTempPath(), "Arrowgene.MonsterHunterOnline.UI", "Crashes")
        ];
    }

    private static void TryShowExceptionWindow(string source,
        Exception exception,
        string logPath,
        bool isTerminating,
        bool canContinue)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
        {
            return;
        }

        async void ShowWindow()
        {
            if (Interlocked.CompareExchange(ref _dialogVisible, 1, 0) != 0)
            {
                return;
            }

            try
            {
                GlobalExceptionWindow window = new GlobalExceptionWindow(source, exception, logPath, isTerminating, canContinue);
                window.Closed += (_, _) => Interlocked.Exchange(ref _dialogVisible, 0);

                Window? owner = desktop.MainWindow;
                if (owner is { IsVisible: true })
                {
                    await window.ShowDialog(owner);
                }
                else
                {
                    window.Show();
                }
            }
            catch
            {
                Interlocked.Exchange(ref _dialogVisible, 0);
            }
        }

        if (Dispatcher.UIThread.CheckAccess())
        {
            ShowWindow();
            return;
        }

        Dispatcher.UIThread.Post(ShowWindow);
    }

    private static bool IsCancellationException(Exception exception)
    {
        if (exception is OperationCanceledException)
        {
            return true;
        }

        if (exception is AggregateException aggregateException)
        {
            AggregateException flattened = aggregateException.Flatten();
            foreach (Exception innerException in flattened.InnerExceptions)
            {
                if (!IsCancellationException(innerException))
                {
                    return false;
                }
            }

            return flattened.InnerExceptions.Count > 0;
        }

        return false;
    }
}
