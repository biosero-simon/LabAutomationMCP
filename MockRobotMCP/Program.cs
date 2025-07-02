using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using LabAutomationMCP;
using MockRobotMCP;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    consoleLogOptions.LogToStandardErrorThreshold = Microsoft.Extensions.Logging.LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly()
    .WithResourcesFromAssembly();

builder.Services.AddSingleton<MockRobot>();

// Set up logging to capture method context
Logger.SetLogAction((message, methodName, filePath, lineNumber) =>
{
    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    
    // Also log to TestingLogs resource with method context
    MockRobotMCP.Resources.TestingLogs.Log(message, methodName, filePath, lineNumber);
});

// Add some sample logs to demonstrate the system
Logger.Log("System starting up");
Logger.Log("MockRobot MCP server initializing");

await builder.Build().RunAsync();