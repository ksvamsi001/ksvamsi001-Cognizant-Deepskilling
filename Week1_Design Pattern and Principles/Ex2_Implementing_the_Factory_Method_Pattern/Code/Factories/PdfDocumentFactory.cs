public class PdfDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument() => new PdfDocument();
}
