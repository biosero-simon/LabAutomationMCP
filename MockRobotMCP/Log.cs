using System;
using System.Runtime.CompilerServices;

namespace MockRobotMCP;

public class Logger
{
    private static Action<string, string, string, int> _logAction = DefaultLog;

    private static void DefaultLog(string message, string methodName, string filePath, int lineNumber)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }

    public static void SetLogAction(Action<string, string, string, int> logAction)
    {
        _logAction = logAction ?? DefaultLog;
    }

    public static void Log(string message, 
        [CallerMemberName] string methodName = "", 
        [CallerFilePath] string filePath = "", 
        [CallerLineNumber] int lineNumber = 0)
    {
        _logAction(message, methodName, filePath, lineNumber);
    }
}