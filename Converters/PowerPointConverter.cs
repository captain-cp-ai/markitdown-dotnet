using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace CaptainCP.MarkItDown.Converters
{
    public class PowerPointToMarkdownConverter
    {
        public async Task<string> ConvertAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                var markdown = new StringBuilder();
                using (var doc = PresentationDocument.Open(filePath, false))
                {
                    var slides = doc.PresentationPart?.Presentation?.SlideIdList;
                    if (slides == null) return "# Empty Presentation";
                    
                    int num = 1;
                    foreach (SlideId slide in slides)
                    {
                        markdown.AppendLine($"## Slide {num++}");
                    }
                }
                return markdown.ToString();
            });
        }
    }
}
