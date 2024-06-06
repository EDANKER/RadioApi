namespace Api.Data.Loggers.FileLogger;

public class FileLogger(string path) : ILogger, IDisposable
{
    private string _filePath = path;
    private static object _lock = new();

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        lock (_lock)
        {
            File.Create(_filePath);
            File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
        }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }

    public void Dispose()
    {
        
    }
}