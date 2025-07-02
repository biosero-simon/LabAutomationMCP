using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Text;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MockRobotMCP.Resources;

[McpServerResourceType]
public class TestingLogs
{
    private static readonly ConcurrentQueue<LogEntry> _logs = new();
    private static int _maxLogEntries = 500;

    public static void Log(string message, 
        [CallerMemberName] string methodName = "", 
        [CallerFilePath] string filePath = "", 
        [CallerLineNumber] int lineNumber = 0)
    {
        var fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
        var entry = new LogEntry
        {
            Timestamp = DateTime.Now,
            Message = message,
            Method = $"{fileName}.{methodName}",
            LineNumber = lineNumber
        };

        _logs.Enqueue(entry);

        // Keep only the most recent entries
        while (_logs.Count > _maxLogEntries)
        {
            _logs.TryDequeue(out _);
        }
    }

    [McpServerResource(UriTemplate = "logs://all", Name = "All Logs", MimeType = "text/plain")]
    [Description("Get all logged entries with timestamps and method context.")]
    public static string GetAllLogs()
    {
        var logs = _logs.ToArray();
        
        if (logs.Length == 0)
        {
            return "No logs available.";
        }

        var sb = new StringBuilder();
        sb.AppendLine($"System Logs ({logs.Length} entries)");
        sb.AppendLine(new string('=', 40));
        
        foreach (var log in logs)
        {
            sb.AppendLine($"[{log.Timestamp:HH:mm:ss.fff}] {log.Method}:{log.LineNumber} - {log.Message}");
        }

        return sb.ToString();
    }

    [McpServerResource(UriTemplate = "logs://recent", Name = "Recent Logs", MimeType = "text/plain")]
    [Description("Get the 10 most recent log entries.")]
    public static string GetRecentLogs()
    {
        var logs = _logs.ToArray().TakeLast(10).ToArray();
        
        if (logs.Length == 0)
        {
            return "No recent logs available.";
        }

        var sb = new StringBuilder();
        sb.AppendLine("Recent Logs (Last 10 entries)");
        sb.AppendLine(new string('-', 30));
        
        foreach (var log in logs)
        {
            sb.AppendLine($"[{log.Timestamp:HH:mm:ss.fff}] {log.Method} - {log.Message}");
        }

        return sb.ToString();
    }

    public static void Clear() => _logs.Clear();
}

public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public int LineNumber { get; set; }
}
