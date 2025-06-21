using System;

public class PdfDocument : IDocument
{
    public string FileName { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public PdfDocument()
    {
        CreatedAt = DateTime.Now;
        FileName = $"Document_{CreatedAt:yyyyMMdd_HHmmss}.pdf";
        Console.WriteLine("PdfDocumentFactory: Creating new PDF document...");
        Console.WriteLine("Document Type: Portable Document Format");
        Console.WriteLine($"Creation Time: {CreatedAt:o}");
    }

    public void Open()
    {
        Console.WriteLine($"Opening PDF document: {FileName}");
        Console.WriteLine("Loading PDF reader application...");
    }

    public void Save()
    {
        Console.WriteLine($"Saving PDF document: {FileName}");
        Console.WriteLine("Document saved in .pdf format");
    }

    public void Print()
    {
        Console.WriteLine($"Printing PDF document: {FileName}");
        Console.WriteLine("High-quality print mode enabled...");
    }

    public void Close()
    {
        Console.WriteLine($"Closing PDF document: {FileName}");
        Console.WriteLine("PDF reader application closed");
    }
}
