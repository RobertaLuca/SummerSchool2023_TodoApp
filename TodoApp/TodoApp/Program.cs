using Avalonia;
using Avalonia.Logging;

namespace TodoApp.Desktop;

internal class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        TaskScheduler.UnobservedTaskException += UnobservedTaskException;

        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception)
        {
        }
        finally
        {
        }
    }

    private static void UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        throw e.Exception;
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace(LogEventLevel.Debug, LogArea.Property, LogArea.Layout);
}
