using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.IO;

namespace LabAutomationMCP;

[McpServerResourceType]
public class SimpleResourceType
{
    [McpServerResource(UriTemplate = "docs://direct/text/resource", Name = "Path to Documentation", MimeType = "text/plain")]
    [Description("The full path to the documentation resources. This resource provides a direct link to the documentation file.")]
    public static string DirectTextResource() => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentation", "SampleDocumentation.pdf");

}