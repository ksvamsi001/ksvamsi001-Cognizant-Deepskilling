public class ExcelDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument() => new ExcelDocument();
}
