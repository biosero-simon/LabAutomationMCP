using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace LabAutomationMCP;

[McpServerResourceType]
public class DocsPathResource
{
    [McpServerResource(UriTemplate = "docs://direct/text/resource", Name = "Path to Documentation", MimeType = "text/plain")]
    [Description("The full path to the documentation file. This resource provides a full path to the documentation file on the local machine.")]
    public static string DirectTextResource() => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentation", "SampleDocumentation.pdf");

    [McpServerResource(UriTemplate = "docs://pdf/page/{pageNumber}", Name = "PDF Page Content")]
    [Description("Extract and return the text content of a specific page from the PDF documentation. Page numbers start from 1.")]
    public static ResourceContents GetPdfPageContent(RequestContext<ReadResourceRequestParams> requestContext, int pageNumber)
    {
        try
        {
            var pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentation", "SampleDocumentation.pdf");
            
            if (!File.Exists(pdfPath))
            {
                throw new FileNotFoundException($"PDF file not found at: {pdfPath}");
            }

            using var reader = new PdfReader(pdfPath);
            using var document = new PdfDocument(reader);
            
            var totalPages = document.GetNumberOfPages();
            
            if (pageNumber < 1 || pageNumber > totalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), 
                    $"Page number must be between 1 and {totalPages}. Requested: {pageNumber}");
            }

            var page = document.GetPage(pageNumber);
            var strategy = new SimpleTextExtractionStrategy();
            var pageText = PdfTextExtractor.GetTextFromPage(page, strategy);
            
            return new TextResourceContents
            {
                Uri = $"docs://pdf/page/{pageNumber}",
                MimeType = "text/plain",
                Text = $"Page {pageNumber} of {totalPages}:\n\n{pageText}"
            };
        }
        catch (Exception ex)
        {
            return new TextResourceContents
            {
                Uri = $"docs://pdf/page/{pageNumber}",
                MimeType = "text/plain",
                Text = $"Error reading PDF page {pageNumber}: {ex.Message}"
            };
        }
    }

    [McpServerResource(UriTemplate = "docs://pdf/info", Name = "PDF Information", MimeType = "text/plain")]
    [Description("Get information about the PDF document including total page count and basic metadata.")]
    public static string GetPdfInfo()
    {
        try
        {
            var pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentation", "SampleDocumentation.pdf");
            
            if (!File.Exists(pdfPath))
            {
                return $"PDF file not found at: {pdfPath}";
            }

            using var reader = new PdfReader(pdfPath);
            using var document = new PdfDocument(reader);
            
            var totalPages = document.GetNumberOfPages();
            var info = document.GetDocumentInfo();
            
            return $"PDF Document Information:\n" +
                   $"- Total Pages: {totalPages}\n" +
                   $"- Title: {info.GetTitle() ?? "Not specified"}\n" +
                   $"- Author: {info.GetAuthor() ?? "Not specified"}\n" +
                   $"- Subject: {info.GetSubject() ?? "Not specified"}\n" +
                   $"- File Path: {pdfPath}\n" +
                   $"\nTo read individual pages, use the resource: docs://pdf/page/{{pageNumber}}";
        }
        catch (Exception ex)
        {
            return $"Error reading PDF information: {ex.Message}";
        }
    }

}