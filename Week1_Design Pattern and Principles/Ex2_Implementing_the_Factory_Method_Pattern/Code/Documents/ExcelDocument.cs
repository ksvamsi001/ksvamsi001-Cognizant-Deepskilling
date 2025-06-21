using System;

public class ExcelDocument : IDocument
{
    public string FileName { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ExcelDocument()
    {
        CreatedAt = DateTime.Now;
        FileName = $"Spreadsheet_{CreatedAt:yyyyMMdd_HHmmss}.xlsx";
        Console.WriteLine("ExcelDocumentFactory: Creating new Excel document...");
        Console.WriteLine("Document Type: Microsoft Excel Spreadsheet");
        Console.WriteLine($"Creation Time: {CreatedAt:o}");
    }

    public void Open()
    {
        Console.WriteLine($"Opening Excel document: {FileName}");
        Console.WriteLine("Loading Microsoft Excel application...");
    }

    public void Save()
    {
        Console.WriteLine($"Saving Excel document: {FileName}");
        Console.WriteLine("Workbook saved in .xlsx format");
    }

    public void Print()
    {
        Console.WriteLine($"Printing Excel document: {FileName}");
        Console.WriteLine("Printing all worksheets...");
    }

    public void Close()
    {
        Console.WriteLine($"Closing Excel document: {FileName}");
        Console.WriteLine("Microsoft Excel application closed");
    }
}
