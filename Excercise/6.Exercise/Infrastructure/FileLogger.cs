namespace _6.Exercise.Infrastructure;

public sealed class FileLogger(string path)
{
    public void Log(string message)
    {
        File.AppendAllLines(path, [$"{DateTimeOffset.UtcNow:O} {message}"]);
    }
}
