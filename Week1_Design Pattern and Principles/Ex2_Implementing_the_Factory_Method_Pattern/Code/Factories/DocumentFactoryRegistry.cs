using System.Collections.Generic;
public static class DocumentFactoryRegistry
{
    private static Dictionary<string, DocumentFactory> _registry = new()
    {
        { "WORD", new WordDocumentFactory() },
        { "PDF", new PdfDocumentFactory() },
        { "EXCEL", new ExcelDocumentFactory() }
    };
    public static DocumentFactory? GetFactory(string type)
    {
        return _registry.TryGetValue(type.ToUpper(), out var factory) ? factory : null;
    }
    public static void PrintSupportedTypes()
    {
        Console.WriteLine($"Supported document types: {string.Join(", ", _registry.Keys)}");
    }
}