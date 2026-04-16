using Avalonia;
using System;
using System.Text;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.UI.Infrastructure;

namespace Arrowgene.MonsterHunterOnline.UI;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        GlobalExceptionHandler.Register();

        LogProvider.OnLogWrite += (_, e) =>
        {
            Log log = e.Log;
            ConsoleColor color = log.LogLevel switch
            {
                LogLevel.Error => ConsoleColor.Red,
                LogLevel.Info => ConsoleColor.Cyan,
                _ => ConsoleColor.Gray,
            };
            Console.ForegroundColor = color;
            Console.WriteLine($"[{log.LogLevel}] {log.LoggerIdentity}: {log.Text}");
            Console.ResetColor();
        };
        LogProvider.Start();

        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            GlobalExceptionHandler.HandleStartupException(ex);
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
