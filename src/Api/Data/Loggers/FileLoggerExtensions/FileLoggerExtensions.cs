namespace Api.Data.Loggers.FileLoggerExtensions;

public class FileLoggerExtensions
{
    public static ILoggingBuilder AddFile(ILoggingBuilder loggingBuilder, string filePath)
    {
        return loggingBuilder.AddProvider(new FileLoggerProvider.FileLoggerProvider(filePath));
    }
}