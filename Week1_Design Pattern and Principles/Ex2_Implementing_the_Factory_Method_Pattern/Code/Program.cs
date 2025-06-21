using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Factory Method Pattern Demo ===\n");

        Console.WriteLine("1. Basic Factory Usage:");
        ProcessDocument(new WordDocumentFactory());
        ProcessDocument(new PdfDocumentFactory());
        ProcessDocument(new ExcelDocumentFactory());

        Console.WriteLine("\n2. Factory Registry Usage:");
        DocumentFactoryRegistry.PrintSupportedTypes();
        ProcessDocument(DocumentFactoryRegistry.GetFactory("WORD"));
        ProcessDocument(DocumentFactoryRegistry.GetFactory("PDF"));
        ProcessDocument(DocumentFactoryRegistry.GetFactory("EXCEL"));

        Console.WriteLine("\n3. Full Document Operations:");
        FullOperations(new WordDocumentFactory());
        FullOperations(new PdfDocumentFactory());
        FullOperations(new ExcelDocumentFactory());

        Console.WriteLine("====================================");
    }

    static void ProcessDocument(DocumentFactory? factory)
    {
        if (factory == null) return;

        Console.WriteLine("\n=== Document Processing Started ===");
        IDocument doc = factory.CreateDocument();
        doc.Open();
        doc.Save();
        Console.WriteLine("=== Document Processing Completed ===");
        doc.Close();
    }

    static void FullOperations(DocumentFactory factory)
    {
        Console.WriteLine("\n------------------------------");
        IDocument doc = factory.CreateDocument();
        doc.Open();
        doc.Save();
        doc.Print();
        doc.Close();
    }
}
