using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CaptainCP.MarkItDown.Converters;

namespace CaptainCP.MarkItDown
{
    public class MarkItDownConverter
    {
        public async Task<string> ConvertAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            
            return extension switch
            {
                ".txt" or ".md" => await File.ReadAllTextAsync(filePath),
                ".html" or ".htm" => await ConvertHtmlAsync(filePath),
                ".docx" => await new WordToMarkdownConverter().ConvertAsync(filePath),
                ".xlsx" => await new ExcelToMarkdownConverter().ConvertAsync(filePath),
                ".pptx" => await new PowerPointToMarkdownConverter().ConvertAsync(filePath),
                ".json" => await ConvertJsonAsync(filePath),
                ".csv" => await ConvertCsvAsync(filePath),
                _ => $"# {Path.GetFileName(filePath)}\n\nUnsupported file type: {extension}"
            };
        }

        private async Task<string> ConvertHtmlAsync(string filePath)
        {
            var html = await File.ReadAllTextAsync(filePath);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            
            var markdown = new StringBuilder();
            markdown.AppendLine($"# {doc.DocumentNode.SelectSingleNode("//title")?.InnerText ?? "Document"}");
            markdown.AppendLine();
            
            var body = doc.DocumentNode.SelectSingleNode("//body");
            if (body != null)
            {
                markdown.AppendLine(body.InnerText);
            }
            
            return markdown.ToString();
        }

        private async Task<string> ConvertJsonAsync(string filePath)
        {
            var json = await File.ReadAllTextAsync(filePath);
            return $"# JSON Document\n\n```json\n{json}\n```";
        }

        private async Task<string> ConvertCsvAsync(string filePath)
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            var markdown = new StringBuilder();
            markdown.AppendLine("# CSV Data\n");
            
            foreach (var line in lines)
            {
                markdown.AppendLine("| " + line.Replace(",", " | ") + " |");
            }
            
            return markdown.ToString();
        }
    }
}
