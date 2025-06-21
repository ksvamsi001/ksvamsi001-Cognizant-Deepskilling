public class WordDocument : IDocument
{
    public string FileName { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public WordDocument()
    {
        CreatedAt = DateTime.Now;
        FileName = $"Document_{CreatedAt:yyyyMMdd_HHmmss}.docx";
        Console.WriteLine("WordDocumentFactory: Creating new Word document...");
        Console.WriteLine("Document Type: Microsoft Word Document");
        Console.WriteLine($"Creation Time: {CreatedAt:o}");
    }

    public void Open()
    {
        Console.WriteLine($"Opening Word document: {FileName}");
        Console.WriteLine("Loading Microsoft Word application...");
    }

    public void Save()
    {
        Console.WriteLine($"Saving Word document: {FileName}");
        Console.WriteLine("Document saved in .docx format");
    }

    public void Print()
    {
        Console.WriteLine($"Printing Word document: {FileName}");
        Console.WriteLine("Sending to default printer...");
    }

    public void Close()
    {
        Console.WriteLine($"Closing Word document: {FileName}");
        Console.WriteLine("Microsoft Word application closed");
    }
}
