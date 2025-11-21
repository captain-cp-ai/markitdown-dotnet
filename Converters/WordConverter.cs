using System.IO;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CaptainCP.MarkItDown.Converters
{
    public class WordToMarkdownConverter
    {
        public async Task<string> ConvertAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                var markdown = new StringBuilder();
                using (var doc = WordprocessingDocument.Open(filePath, false))
                {
                    var body = doc.MainDocumentPart?.Document?.Body;
                    if (body == null) return "# Empty Document";
                    
                    foreach (var para in body.Descendants<Paragraph>())
                    {
                        var text = para.InnerText;
                        if (!string.IsNullOrWhiteSpace(text))
                            markdown.AppendLine(text);
                    }
                }
                return markdown.ToString();
            });
        }
    }
}
