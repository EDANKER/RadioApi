namespace Api.Data.Loggers.FileLoggerProvider;

public class FileLoggerProvider(string path) : ILoggerProvider
{
    private string _path = path;

    public void Dispose()
    {
        
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger.FileLogger(path);
    }
}