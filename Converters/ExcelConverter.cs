using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CaptainCP.MarkItDown.Converters
{
    public class ExcelToMarkdownConverter
    {
        public async Task<string> ConvertAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                var markdown = new StringBuilder();
                using (var doc = SpreadsheetDocument.Open(filePath, false))
                {
                    var sheets = doc.WorkbookPart?.Workbook?.Sheets;
                    if (sheets == null) return "# Empty Workbook";
                    
                    foreach (Sheet sheet in sheets)
                    {
                        markdown.AppendLine($"## {sheet.Name}");
                    }
                }
                return markdown.ToString();
            });
        }
    }
}
