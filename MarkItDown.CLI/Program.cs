using System;
using System.IO;
using System.Threading.Tasks;
using CaptainCP.MarkItDown;

if (args.Length == 0)
{
    Console.WriteLine("MarkItDown.NET - Built by Captain CP");
    Console.WriteLine("Convert documents to Markdown for LLM consumption");
    Console.WriteLine();
    Console.WriteLine("Usage: markitdown <file>");
    Console.WriteLine();
    Console.WriteLine("Supported formats:");
    Console.WriteLine("  - Word (.docx)");
    Console.WriteLine("  - Excel (.xlsx)");
    Console.WriteLine("  - PowerPoint (.pptx)");
    Console.WriteLine("  - HTML (.html, .htm)");
    Console.WriteLine("  - JSON (.json)");
    Console.WriteLine("  - CSV (.csv)");
    Console.WriteLine("  - Text (.txt, .md)");
    return 0;
}

var filePath = args[0];

if (!File.Exists(filePath))
{
    Console.Error.WriteLine($"Error: File not found: {filePath}");
    return 1;
}

try
{
    var converter = new MarkItDownConverter();
    var markdown = await converter.ConvertAsync(filePath);
    Console.WriteLine(markdown);
    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 1;
}
